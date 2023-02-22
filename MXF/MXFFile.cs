#region license
//
// MXF - Myriadbits .NET MXF library. 
// Read MXF Files.
// Copyright (C) 2015 Myriadbits, Jochem Bakker
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// For more information, contact me at: info@myriadbits.com
//
#endregion

using Myriadbits.MXF.Identifiers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Myriadbits.MXF.Exceptions;

namespace Myriadbits.MXF
{
    public enum FileParseMode
    {
        Full,
        Partial,
    }

    /// <summary>
    /// Name object, represents a complete MXF file
    /// </summary>
    public class MXFFile : MXFObject
    {
        private List<MXFValidationResult> m_results;

        public FileInfo File { get; }

        public List<MXFPartition> Partitions { get; set; }
        public MXFRIP RIP { get; set; }

        public List<MXFValidationResult> Results { get { return m_results; } }

        public List<Exception> ParsingExceptions { get; } = new List<Exception>();
        public MXFSystemItem FirstSystemItem { get; set; }
        public MXFSystemItem LastSystemItem { get; set; }

        public MXFLogicalObject LogicalTreeRoot { get; set; }

        public int PartitionCount
        {
            get
            {
                if (this.Partitions == null)
                    return 0;
                return this.Partitions.Count;
            }
        }

        public int RIPEntryCount
        {
            get
            {
                return this.RIP?.Children.Count ?? 0;
            }
        }


        private MXFFile(FileInfo fi)
        {
            this.File = fi;
            this.Partitions = new List<MXFPartition>();
            this.m_results = new List<MXFValidationResult>();
        }

        public static Task<MXFFile> CreateAsync(FileInfo fi, IProgress<TaskReport> overallProgress = null, IProgress<TaskReport> singleProgress = null, CancellationToken ct = default)
        {
            var ret = new MXFFile(fi);
            return ret.ParseAsync(overallProgress, singleProgress, ct);
        }

        protected async Task<MXFFile> ParseAsync(IProgress<TaskReport> overallProgress = null, IProgress<TaskReport> singleProgress = null, CancellationToken ct = default)
        {
            var result = await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                int currentPercentage = 0;
                int previousPercentage = 0;

                using (var fileStream = new FileStream(File.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 10240))
                {
                    // Parse Packs 
                    MXFPackParser parser = new MXFPackParser(fileStream);
                    List<MXFObject> packList = new List<MXFObject>();
                    overallProgress?.Report(new TaskReport(0, "Reading KLV stream"));
                    while (parser.HasNext())
                    {
                        try
                        {
                            var pack = parser.GetNext();
                            packList.Add(pack);
                            ct.ThrowIfCancellationRequested();
                        }
                        // TODO be more selective with the exception
                        catch (KLVKeyParsingException ex)
                        {
                            long lastgoodPos = 0;
                            // klv stream error
                            if (parser.Current != null)
                            {
                                lastgoodPos = parser.Current.Offset + parser.Current.TotalLength;
                            }

                            // reposition klvstream

                            if (!parser.SeekForNextPotentialKey(out long newOffset))
                            {
                                // we have reached end of file, exceptional case so handle it
                            }
                            else
                            {
                                if (packList.Any())
                                {
                                    packList.Add(new MXFNamedObject("Non-KLV Data", lastgoodPos, newOffset - lastgoodPos));
                                }
                                else
                                {
                                    this.AddChild(new MXFNamedObject("Run-In", lastgoodPos, newOffset - lastgoodPos));
                                }
                            }

                            continue;
                        }
                        catch (KLVStreamException ex)
                        {
                            //TODO must be handled
                            break;
                        }
                        catch (Exception ex) when (ex is not OperationCanceledException)
                        {
                            // TODO log error
                        }

                        // Only report progress when the percentage has changed
                        currentPercentage = (int)((parser.Current.Offset + parser.Current.TotalLength) * 100 / this.File.Length);
                        if (currentPercentage != previousPercentage)
                        {
                            // TODO really need to check this?
                            if (currentPercentage <= 100)
                            {
                                int overallPercentage = 12 + currentPercentage * (65 - 12) / 100;
                                overallProgress?.Report(new TaskReport(overallPercentage, "Reading KLV stream"));
                                singleProgress?.Report(new TaskReport(currentPercentage, "Parsing packs..."));
                                previousPercentage = currentPercentage;
                            }
                        }
                    }

                    this.ParsingExceptions.AddRange(parser.Exceptions);

                    Log.ForContext<MXFFile>().Information($"Finished parsing MXF packs [{packList.Count} items] in {sw.ElapsedMilliseconds} ms");

                    // Now process the pack list (partition packs, treat special cases)
                    overallProgress?.Report(new TaskReport(65, "Process packs"));
                    ProcessAndAttachPacks(packList);

                    // Reparse all local tags, as now we know the primerpackage aliases
                    ResolveAndParseLocalTags();

                    overallProgress?.Report(new TaskReport(73, "Updating tree"));

                    // Resolve the references
                    sw.Restart();
                    overallProgress?.Report(new TaskReport(81, "Resolving references"));
                    int numOfResolved = ResolveReferences();
                    Log.ForContext<MXFFile>().Information($"{numOfResolved} references resolved in {sw.ElapsedMilliseconds} ms");

                    // Create the logical tree
                    overallProgress?.Report(new TaskReport(95, "Creating Logical tree"));
                    sw.Restart();
                    CreateLogicalTree();
                    Log.ForContext<MXFFile>().Information($"Logical tree created in {sw.ElapsedMilliseconds} ms");

                    // Set property description by reading the description attribute (for all types)
                    MXFPackFactory.SetDescriptionFromAttributeForAllTypes();

                    // Finished, return this (MXFFile)
                    overallProgress?.Report(new TaskReport(100, "Done"));
                    return this;
                }
            }, ct);

            return result;
        }

        public override string ToString()
        {
            return $"{File.FullName} ({File.Length:N0})";
        }

        /// <summary>
        /// Return info for a generic track packet
        /// </summary>
        /// <returns>A string representing information about a generic track</returns>
        public string GetTrackInfo(MXFTrack genericTrack)
        {
            try
            {
                if (genericTrack != null)
                {
                    StringBuilder sb = new StringBuilder();
                    MXFSequence seq = genericTrack.GetFirstMXFSequence();
                    if (seq != null && seq.DataDefinition != null && seq.DataDefinition is UL ul)
                    {
                        sb.Append(seq.DataDefinition.ToString());
                    }

                    if (genericTrack is MXFTimelineTrack timeLineTrack)
                    {
                        sb.Append(string.Format(" @ {0}, ", timeLineTrack.EditRate));
                    }
                    sb.Append(string.Format(@"""{0}"", ", genericTrack.TrackName));

                    return sb.ToString();

                }
                return "";
            }
            catch (Exception)
            {
                return "Unable to retrieve track info. See error log for more details";
            }
        }

        public void ExecuteValidationTest(BackgroundWorker worker, bool extendedTest)
        {
            // Reset results
            this.m_results.Clear();

            // Execute validation tests
            List<MXFValidator> allTests = new List<MXFValidator>
            {
                new MXFValidatorInfo(),
                new MXFValidatorPartitions(),
                new MXFValidatorRIP(),
                new MXFValidatorKeys()
            };

            if (extendedTest)
            {
                allTests.Add(new MXFValidatorIndex());
            }
            foreach (MXFValidator mxfTest in allTests)
            {
                mxfTest.Initialize(this, worker);
                mxfTest.ExecuteTest(ref m_results);
            }

            if (!extendedTest)
            {
                MXFValidationResult valResult = new MXFValidationResult("Index Table");
                this.m_results.Add(valResult);
                valResult.SetQuestion("Index table test not executed in partial loading mode (to execute test press the execute all test button).");
            }
        }

        private void ProcessAndAttachPacks(IEnumerable<MXFObject> packList)
        {
            MXFPartition currentPartition = null;
            int partitionNumber = 0;
            MXFObject partitionRoot = null;

            foreach (var obj in packList)
            {
                switch (obj)
                {
                    case MXFPartition partition:
                        if (partitionRoot == null)
                        {
                            partitionRoot = new MXFNamedObject("Partitions", partition.Offset);
                            this.AddChild(partitionRoot);
                        }
                        currentPartition = partition;
                        currentPartition.File = this;
                        currentPartition.PartitionNumber = partitionNumber++;
                        partitionRoot.AddChild(currentPartition);
                        this.Partitions.Add(currentPartition);
                        break;

                    case MXFRIP rip:
                        this.AddChild(rip);
                        this.RIP = rip;
                        break;

                    case MXFPreface preface:
                        this.LogicalTreeRoot = preface.CreateLogicalObject();
                        if (currentPartition != null)
                        {
                            currentPartition.AddChild(obj);
                        }
                        break;

                    case MXFSystemItem si:
                        if (currentPartition != null)
                        {
                            // Store the first system item for every partition
                            // (required to calculate essence positions)
                            if (currentPartition.FirstSystemItem == null)
                                currentPartition.FirstSystemItem = si;
                            currentPartition.AddChild(si);
                        }
                        else
                            this.AddChild(si);

                        // Store the first and the last system item
                        if (this.FirstSystemItem == null)
                        {
                            this.FirstSystemItem = si;
                        }
                        this.LastSystemItem = si;
                        break;


                    case MXFEssenceElement el:
                        if (currentPartition != null)
                        {
                            // Store the first system item for every partition
                            // (required to calculate essence positions)
                            if (el.IsPicture && currentPartition.FirstPictureEssenceElement == null)
                            {
                                currentPartition.FirstPictureEssenceElement = el;
                            }

                            currentPartition.AddChild(el);
                        }
                        else
                            this.AddChild(el);
                        break;


                    default:
                        // Normal
                        if (currentPartition != null)
                            currentPartition.AddChild(obj);
                        else
                            this.AddChild(obj);
                        break;

                }
            }
        }

        private void ResolveAndParseLocalTags()
        {
            var localSetList = this.Descendants().OfType<MXFLocalSet>().Where(ls => ls.Children.OfType<MXFLocalTag>().Any());

            foreach (var ls in localSetList.ToList())
            {
                // link local tag keys to primer entry keys
                ls.LookUpLocalTagKeys();

                // now parse tags
                ls.ParseTags();
            }
        }

        /// <summary>
        /// Create the logical view (starting with the preface)
        /// </summary>
        private void CreateLogicalTree()
        {
            if (this.LogicalTreeRoot == null)
                return;

            LogicalAddChilds(this.LogicalTreeRoot);

            // order children by offset
            var orderedChildren = this.LogicalTreeRoot.Children.OrderBy(c => c.Object.Offset).ToList();
            this.LogicalTreeRoot.ClearChildren();
            this.LogicalTreeRoot.AddChildren(orderedChildren);
        }

        /// <summary>
        /// Add all children (recursively)
        /// </summary>
        /// <param name="lObj"></param>
        private MXFLogicalObject LogicalAddChilds(MXFLogicalObject lObj)
        {
            // Check properties for reference
            MXFObject obj = lObj.Object;
            if (obj != null)
            {
                var desc = obj.Descendants().OfType<IResolvable>();

                foreach (var r in desc)
                {
                    // create and add the logical child
                    var refObj = r.GetReference();
                    if (refObj != null)
                    {
                        MXFLogicalObject newLObj = refObj.CreateLogicalObject();
                        lObj.AddChild(newLObj);

                        if (refObj.Children.Any())
                        {
                            LogicalAddChilds(newLObj);
                        }
                    }


                }
            }
            return lObj;
        }

        /// <summary>
        /// Loop through all resolvable items and try to find the object with a matching UUID 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>the number of successfully resolved references</returns>  
        private int ResolveReferences()
        {
            // TODO optimize further, rethink solution
            var refs = this.Descendants().OfType<IResolvable>().ToList();
            var uuidObjs = this.Descendants().OfType<IUUIDIdentifiable>().ToList();
            int numOfResolved = 0;

            foreach (var r in refs)
            {
                foreach (var o in uuidObjs)
                {
                    bool IsResolved = r.ResolveReference(o);
                    if (IsResolved)
                    {
                        numOfResolved++;
                    }
                }
            }
            return numOfResolved;
        }
    }
}
