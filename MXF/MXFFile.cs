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
        private const int MIN_PARSER_PERCENTAGE = 12;
        private const int MAX_PARSER_PERCENTAGE = 65;
        private const int MIN_LOCALTAG_PERCENTAGE = 70;
        private const int MAX_LOCALTAG_PERCENTAGE = 88;
        private const int REFERENCE_PERCENTAGE = 90;
        private const int LOGICALTREE_PERCENTAGE = 97;

        private List<MXFValidationResult> validationResults = new List<MXFValidationResult>();

        public FileInfo File { get; }

        public IReadOnlyList<MXFValidationResult> ValidationResults
        {
            get => validationResults;
        }

        public List<Exception> Exceptions { get; } = new List<Exception>();
        public MXFSystemMetaDataPack FirstSystemItem { get; set; }
        public MXFSystemMetaDataPack LastSystemItem { get; set; }

        public MXFLogicalObject LogicalTreeRoot { get; set; }

        private MXFFile(FileInfo fi)
        {
            this.File = fi;
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

                using (var fileStream = new FileStream(File.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 10240))
                {
                    // Parse file and obtain a list of mxf packs

                    List<MXFObject> mxfPacks = ParseMXFPacks(fileStream, overallProgress, singleProgress, ct);
                    if (mxfPacks.OfType<MXFUnparseablePack>().Any())
                    {
                        Log.ForContext<MXFFile>().Warning($"Unparseable packs [{mxfPacks.OfType<MXFUnparseablePack>().Count()} items] encountered during parsing");
                    }
                    Log.ForContext<MXFFile>().Information($"Finished parsing MXF packs [{mxfPacks.Count} items] in {sw.ElapsedMilliseconds} ms");


                    // Now process the pack list (partition packs, treat special cases)

                    overallProgress?.Report(new TaskReport(MAX_PARSER_PERCENTAGE, "Process packs"));
                    sw.Restart();
                    PartitionAndPostProcessMXFPacks(mxfPacks);
                    Log.ForContext<MXFFile>().Information($"Finished processing MXF packs [{mxfPacks.Count} items] in {sw.ElapsedMilliseconds} ms");

                    // Reparse all local tags, as now we know the primerpackage aliases

                    sw.Restart();
                    overallProgress?.Report(new TaskReport(MIN_LOCALTAG_PERCENTAGE, "Resolving tags"));
                    ResolveAndReadLocalTags(overallProgress, singleProgress, ct);
                    Log.ForContext<MXFFile>().Information($"Finished resolving local tags in {sw.ElapsedMilliseconds} ms");

                    // Resolve the references

                    sw.Restart();
                    overallProgress?.Report(new TaskReport(REFERENCE_PERCENTAGE, "Resolving references"));
                    int numOfResolved = ResolveReferences();
                    Log.ForContext<MXFFile>().Information($"{numOfResolved} references resolved in {sw.ElapsedMilliseconds} ms");

                    // Create the logical tree

                    overallProgress?.Report(new TaskReport(LOGICALTREE_PERCENTAGE, "Creating Logical tree"));
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
                        sb.Append((seq.DataDefinition as UL)?.Name);
                    }

                    if (genericTrack is MXFTimelineTrack timeLineTrack)
                    {
                        sb.Append($" @ {timeLineTrack.EditRate.ToString(true)}");
                    }
                    if (!string.IsNullOrEmpty(genericTrack.TrackName))
                    {
                        sb.Append($" {{{genericTrack.TrackName}}}");
                    }
                    return sb.ToString();

                }
                return "";
            }
            catch (Exception)
            {
                return "Unable to retrieve track info. See error log for more details";
            }
        }

        public async Task<List<MXFValidationResult>> ExecuteValidationTest(bool extendedTest, IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            List<MXFValidationResult> results = new List<MXFValidationResult>();

            // Reset results
            this.validationResults.Clear();

            // Execute validation tests
            List<MXFValidator> allTests = new List<MXFValidator>
            {
                new MXFValidatorInfo(this),
                new MXFValidatorPartitions(this),
                new MXFValidatorRIP(this),
                new MXFValidatorUL(this)
            };

            // add exceptions
            foreach (var ex in Exceptions)
            {
                MXFValidationResult result;

                switch (ex)
                {
                    case EndOfKLVStreamException eofEx:
                        result = new MXFValidationResult("KLVStream");
                        result.Object = eofEx.TruncatedKLV;
                        result.SetError(eofEx.Message, eofEx.Offset);
                        break;

                    case UnparseablePackException upEx:
                        result = new MXFValidationResult("Parser");
                        result.Object = upEx.UnparseablePack;
                        result.SetError(upEx.Message, upEx.Offset);
                        break;

                    case KLVParsingException pEx:
                        result = new MXFValidationResult("Parser");
                        result.Object = this.Descendants().Where(o => o.Offset == pEx.Offset).FirstOrDefault();
                        result.SetError(pEx.InnerException?.Message ?? ex.Message, pEx.Offset);
                        break;

                    default:
                        result = new MXFValidationResult(ex.GetType().Name);
                        result.SetError(ex.InnerException?.Message ?? ex.Message);
                        break;
                }
                results.Add(result);
            }


            if (extendedTest)
            {
                allTests.Add(new MXFValidatorIndex(this));
            }
            foreach (MXFValidator mxfTest in allTests)
            {
                results.AddRange(await mxfTest.Validate(progress, ct));
            }

            if (!extendedTest)
            {
                MXFValidationResult valResult = new MXFValidationResult("Index Table");
                this.validationResults.Add(valResult);
                valResult.SetQuestion("Index table test not executed.");
                results.Add(valResult);
            }
            return results;
        }

        private List<MXFObject> ParseMXFPacks(FileStream fileStream, IProgress<TaskReport> overallProgress, IProgress<TaskReport> singleProgress, CancellationToken ct = default)
        {
            int currentPercentage;
            int previousPercentage = 0;
            MXFPackParser parser = new MXFPackParser(fileStream);
            List<MXFObject> mxfPacks = new List<MXFObject>();
            overallProgress?.Report(new TaskReport(0, "Reading KLV stream"));
            while (parser.HasNext())
            {
                try
                {
                    var pack = parser.GetNext();
                    mxfPacks.Add(pack);
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

                    if (!parser.SeekForNextPotentialKey(out long newOffset, ct))
                    {
                        // we have reached end of file, exceptional case so handle it
                        // TODO handle if exceeding 65536 bytes
                        throw new NotAnMXFFileException("No partition key found within the first 65536 bytes.", parser.Offset, null);
                    }
                    continue;
                }
                catch (EndOfKLVStreamException ex)
                {
                    Exceptions.Add(ex);
                    mxfPacks.Add(ex.TruncatedKLV);
                    break;
                }
                catch (UnparseablePackException ex)
                {
                    Exceptions.Add(ex);
                    mxfPacks.Add(ex.UnparseablePack);
                    continue;
                }

                // Only report progress when the percentage has changed
                currentPercentage = (int)((parser.Current.Offset + parser.Current.TotalLength) * 100 / this.File.Length);
                if (currentPercentage > previousPercentage)
                {
                    // TODO really need to check this?
                    if (currentPercentage < 100)
                    {
                        int overallPercentage = MIN_PARSER_PERCENTAGE + currentPercentage * (MAX_PARSER_PERCENTAGE - MIN_PARSER_PERCENTAGE) / 100;
                        overallProgress?.Report(new TaskReport(overallPercentage, "Reading KLV stream"));
                        singleProgress?.Report(new TaskReport(currentPercentage, "Parsing packs..."));
                        previousPercentage = currentPercentage;
                    }
                }
            }

            // Now search for holes, i.e. non-KLV-data

            var nonConsecutiveObjects = GetNonConsecutiveMXFObjects(mxfPacks);
            foreach (var obj in nonConsecutiveObjects)
            {
                long nonKLVOffset = obj.Item1.Offset + obj.Item1.TotalLength;
                long nonKLVLength = obj.Item2.Offset - nonKLVOffset;
                if (nonKLVOffset == 0)
                {
                    this.AddChild(new MXFNamedObject("Run-In", nonKLVOffset, nonKLVLength));
                }
                else
                {
                    var nonKLV = new MXFNamedObject("Non-KLV Data", nonKLVOffset, nonKLVLength);
                    mxfPacks.Insert(mxfPacks.IndexOf(obj.Item2), nonKLV);
                }
            }

            return mxfPacks;

        }


        private List<(MXFObject, MXFObject)> GetNonConsecutiveMXFObjects(List<MXFObject> objects)
        {
            var list = new List<(MXFObject, MXFObject)>();
            for (int i = 0; i < objects.Count - 1; i++)
            {
                var actual = objects[i];
                var next = objects[i + 1];
                if (actual.Offset + actual.TotalLength != next.Offset)
                {
                    list.Add((actual, next));
                }
            }
            return list;
        }

        private void PartitionAndPostProcessMXFPacks(IEnumerable<MXFObject> packList, CancellationToken ct = default)
        {
            MXFPartition currentPartition = null;
            int partitionNumber = 0;
            MXFObject partitionRoot = null;

            foreach (var obj in packList)
            {
                ct.ThrowIfCancellationRequested();

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
                        break;

                    case MXFRIP rip:
                        this.AddChild(rip);
                        break;

                    case MXFPreface preface:
                        this.LogicalTreeRoot = preface.CreateLogicalObject();
                        if (currentPartition != null)
                        {
                            currentPartition.AddChild(obj);
                        }
                        else
                        {
                            this.AddChild(obj);
                        }
                        break;

                    case MXFSystemMetaDataPack si:
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

        private void ResolveAndReadLocalTags(IProgress<TaskReport> overallProgress = null, IProgress<TaskReport> singleProgress = null, CancellationToken ct = default)
        {
            int currentPercentage = 0;
            int previousPercentage = 0;

            var localSetList = this.Descendants().OfType<MXFLocalSet>().Where(ls => ls.Children.OfType<MXFLocalTag>().Any());
            int localSetListCount = localSetList.Count();

            var collection = localSetList.ToList();

            for (int index = 0; index < collection.Count; index++)
            {
                var ls = collection[index];

                // link local tag keys to primer entry keys
                // TODO do this just once!
                ls.LookUpLocalTagKeys();

                ct.ThrowIfCancellationRequested();

                // now parse tags
                ls.ReadLocalTagValues();

                // update progress
                currentPercentage = (int)(index * 100.0 / localSetListCount);
                if (currentPercentage > previousPercentage)
                {
                    // TODO really need to check this?
                    if (currentPercentage < 100)
                    {
                        int overallPercentage = MIN_LOCALTAG_PERCENTAGE + currentPercentage * (MAX_LOCALTAG_PERCENTAGE - MIN_LOCALTAG_PERCENTAGE) / 100;
                        overallProgress?.Report(new TaskReport(overallPercentage, "Resolving tags"));
                        singleProgress?.Report(new TaskReport(currentPercentage, $"Resolving tag {index}/{localSetListCount}"));
                        previousPercentage = currentPercentage;
                    }
                }
            }
        }

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
