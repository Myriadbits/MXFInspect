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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public enum FileParseMode
    {
        Full,
        Partial,
    }


    //public enum PartialSeekMode
    //{
    //    Unknown,
    //    UsingRIP,
    //    Backwards,
    //    //Full,
    //}


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


        /// <summary>
        /// Fully Parse an MXF file 
        /// </summary>
        //protected void ParseFull(BackgroundWorker worker)
        //{
        //    Stopwatch sw = Stopwatch.StartNew();

        //    MXFPackFactory mxfPackFactory = new MXFPackFactory();

        //    MXFPartition currentPartition = null;
        //    int previousPercentage = 0;
        //    Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys = null;
        //    int[] counters = new int[Enum.GetNames(typeof(KeyType)).Length];
        //    using (mxfReader = new MXFReader(this.Filename))
        //    {
        //        this.Filesize = mxfReader.Size;
        //        MXFObject partitions = new MXFNamedObject("Partitions", 0);
        //        this.AddChild(partitions);

        //        int partitionNumber = 0; // For easy partition identification
        //        while (!m_reader.EOF)
        //        {
        //            try
        //            {
        //                MXFPack pack = mxfPackFactory.CreatePack();

        //                //// Update overall counters
        //                //if (klv.Key.Type == KeyType.None)
        //                //    counters[(int)klv.Key.Type]++;

        //                // Process the new KLV
        //                ProcessKLVObject(pack, partitions, ref currentPartition, ref partitionNumber, ref allPrimerKeys);

        //                // Next KLV please
        //                m_reader.Seek(pack.ValueOffset + pack.Length.Value);
        //            }
        //            catch (Exception e)
        //            {
        //                m_reader.SeekForNextPotentialKey();
        //            }



        //            // Only report progress when the percentage has changed
        //            int currentPercentage = (int)((m_reader.Position * 90) / m_reader.Size);
        //            if (currentPercentage != previousPercentage)
        //            {
        //                worker.ReportProgress(currentPercentage, "Parsing MXF file");
        //                previousPercentage = currentPercentage;
        //            }
        //        }
        //    }

        //    Debug.WriteLine("Finished parsing file '{0}' in {1} ms", this.Filename, sw.ElapsedMilliseconds);

        //    Progress should now be 90 %

        //   DoPostWork(worker, sw, allPrimerKeys);

        //    And Execute ALL test
        //    sw.Restart();
        //    this.ExecuteValidationTest(worker, true);
        //    Debug.WriteLine("Tests executed in {0} ms", sw.ElapsedMilliseconds);

        //    Finished
        //    worker.ReportProgress(100, "Finished");
        //}

        /// <summary>
        /// Partially Parse an MXF file, skip all data
        /// </summary>
        //protected void ParsePartial(BackgroundWorker worker)
        //{

        //    Stopwatch sw = Stopwatch.StartNew();
        //    int previousPercentage = 0;
        //    Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys = null;

        //    //PartialSeekMode seekMode = PartialSeekMode.Unknown;
        //    using (mxfReader = new MXFReader(this.Filename))
        //    {
        //        this.Filesize = mxfReader.Size;
        //        MXFObject root = new MXFNamedObject("Partitions", 0);
        //        this.AddChild(root);

        //        // Parse Packs 
        //        KLVParser parser = new KLVParser(mxfReader);
        //        List<MXFObject> packList = new List<MXFObject>();
        //        while (parser.HasNext())
        //        {
        //            try
        //            {
        //                var pack = parser.GetNextMXFPack();
        //                packList.Add(pack);
        //            }
        //            // TODO be more selective with the exception
        //            catch (ArgumentException e)
        //            {
        //                // error in klv-stream

        //                long lastgoodPos = parser.CurrentPack.Offset + parser.CurrentPack.TotalLength;
        //                if (!parser.SeekForNextPotentialKey(out long newOffset))
        //                {
        //                    // we have reached end of file, exceptional case so handle it
        //                }
        //                else
        //                {
        //                    packList.Add(new MXFNamedObject("Non-KLV Data", lastgoodPos, newOffset - lastgoodPos));
        //                }

        //            }

        //            if (currentPercentage != previousPercentage)
        //            {
        //                // TODO really need to check this?
        //                if (currentPercentage <= 100)
        //                {
        //                    overallProgress?.Report(new TaskReport(7 + (int)(currentPercentage * 0.73), "Partial Parsing..."));
        //                    //previousPercentage = currentPercentage;
        //                }
        //            }
        //        }


        //        // Now process the pack list (partition, treat special cases)
        //        ProcessPacks(packList);


        //        Debug.WriteLine("Finished parsing file '{0}' in {1} ms", this.Filename, sw.ElapsedMilliseconds);

        //        // Progress should now be 90%

        //        // Resolve the references
        //        sw.Restart();
        //        worker.ReportProgress(93, "Resolving flatlist");
        //        int numOfResolved = ResolveReferences();
        //        Debug.WriteLine("{0} references resolved in {1} ms", numOfResolved, sw.ElapsedMilliseconds);


        //        // Create the logical tree
        //        worker.ReportProgress(94, "Creating Logical tree");
        //        sw.Restart();
        //        CreateLogicalTree();
        //        Debug.WriteLine("Logical tree created in {0} ms", sw.ElapsedMilliseconds);
        //        DoPostWork(worker, sw, allPrimerKeys);

        //        // And Execute FAST tests
        //        this.ExecuteValidationTest(worker, false);

        //        // Finished
        //        worker.ReportProgress(100, "Finished");

        //        #region old
        //        // Partition the packs
        //        // Partition(packList);

        //        //// Start with trying to find the RIP
        //        //bool ripFound = ReadRIP(mxfPackFactory);
        //        //if (ripFound)
        //        //    seekMode = PartialSeekMode.UsingRIP;
        //        //m_reader.Seek(0); // Start at the beginning

        //        //// Start by reading the first partition
        //        //int partitionNumber = 0; // For easy partition identification
        //        //while (!m_reader.EOF && seekMode != PartialSeekMode.Backwards) // Eof and NOT searching backwards
        //        //{
        //        //    MXFPack pack = mxfPackFactory.CreatePack(m_reader, currentPartition);

        //        //    pack.Pack = list.Single(p => p.Offset == pack.Offset);

        //        //    if (pack is MXFPartition && seekMode == PartialSeekMode.Backwards)
        //        //    {
        //        //        if (this.Partitions.Exists(a => a.Offset == pack.Offset))
        //        //        {
        //        //            // A new partition has been found that we already found, quit the main loop
        //        //            break;
        //        //        }
        //        //    }


        //        //    // Process the new KLV
        //        //    ProcessKLVObject(pack, partitions, ref currentPartition, ref partitionNumber, ref allPrimerKeys);


        //        //    // If we found the second partition 
        //        //    long nextSeekPosition = pack.ValueOffset + pack.Length.Value;
        //        //    if (partitionNumber >= 2) // Header fully read, now busy with the second partition
        //        //    {
        //        //        switch (seekMode)
        //        //        {
        //        //            case PartialSeekMode.UsingRIP: // And we already found the RIP
        //        //                if (currentPartition.FirstSystemItem != null) // And we found the first system item
        //        //                {
        //        //                    MXFEntryRIP ripEntry = this.RIP.GetPartition(partitionNumber);
        //        //                    if (ripEntry != null)
        //        //                    {
        //        //                        // Mark the current partition as not-completely read
        //        //                        currentPartition.IsLoaded = false;

        //        //                        // Start at the next partition
        //        //                        nextSeekPosition = (long)ripEntry.PartitionOffset;
        //        //                    }
        //        //                }
        //        //                break;

        //        //            case PartialSeekMode.Backwards: // NO RIP, searching backwards
        //        //                                            // Backwards, jump to the PREVIOUS partition
        //        //                if (currentPartition.FirstSystemItem != null) // And we found the first system item
        //        //                {
        //        //                    // Jump to the previous partition
        //        //                    if (currentPartition.PreviousPartition != 0)
        //        //                    {
        //        //                        // And we haven't found this partition yet
        //        //                        if (!this.Partitions.Exists(a => a.ThisPartition == currentPartition.PreviousPartition))
        //        //                            nextSeekPosition = (long)currentPartition.PreviousPartition; // Jump to previous
        //        //                    }
        //        //                }
        //        //                break;

        //        //            case PartialSeekMode.Unknown: // No RIP....
        //        //                                          // Hmmm, RIP is not found, check if we have a footer partition somewhere
        //        //                MXFPartition part = this.Partitions.Where(a => a.FooterPartition != 0).FirstOrDefault();
        //        //                if (part != null)
        //        //                {
        //        //                    // If we are already at the footer, don't bother to seek
        //        //                    if (currentPartition.Offset != (long)part.FooterPartition)
        //        //                    {
        //        //                        nextSeekPosition = (long)part.FooterPartition; // Start at the footer
        //        //                        seekMode = PartialSeekMode.Backwards;
        //        //                    }
        //        //                }
        //        //                break;
        //        //        }
        //        //    }

        //        //    // Next KLV please
        //        //    m_reader.Seek(nextSeekPosition);

        //        //    // Only report progress when the percentage has changed
        //        //    int currentPercentage = (int)((m_reader.Position * 90) / m_reader.Size);
        //        //    if (currentPercentage != previousPercentage)
        //        //    {
        //        //        worker.ReportProgress(currentPercentage, "Partial Parsing MXF file");
        //        //        previousPercentage = currentPercentage;
        //        //    }
        //        //}
        //        #endregion
        //    }
        //}


        protected async Task<MXFFile> ParseAsync(IProgress<TaskReport> overallProgress = null, IProgress<TaskReport> singleProgress = null, CancellationToken ct = default)
        {
            var result = await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                int currentPercentage = 0;
                int previousPercentage = 0;             

                using (var fileStream = new FileStream(File.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 10240))
                {
                    // Prepare
                    //this.Filesize = fileStream.Length;

                    // Create root node
                    MXFObject root = new MXFNamedObject("Partitions", 0);
                    this.AddChild(root);

                    // Parse Packs 
                    //var klvParser = new KLVTripletParser<UL, KLVBERLength, ByteArray>(fileStream,);

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
                        catch (ArgumentException e)
                        {
                            // error in klv-stream

                            long lastgoodPos = parser.Current.Offset + parser.Current.TotalLength;
                            if (!parser.SeekForNextPotentialKey(out long newOffset))
                            {
                                // we have reached end of file, exceptional case so handle it
                            }
                            else
                            {
                                packList.Add(new MXFNamedObject("Non-KLV Data", lastgoodPos, newOffset - lastgoodPos));
                            }

                        }
                        catch (Exception e) when (e is not OperationCanceledException)
                        {
                            // error in klv-stream

                            long lastgoodPos = parser.Current.Offset + parser.Current.TotalLength;
                            if (!parser.SeekForNextPotentialKey(out long newOffset))
                            {
                                // we have reached end of file, exceptional case so handle it
                            }
                            else
                            {
                                packList.Add(new MXFNamedObject("Non-Parseable Data", lastgoodPos, newOffset - lastgoodPos));
                            }

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

                    Debug.WriteLine("Finished parsing file '{0}' in {1} ms", this.File.FullName, sw.ElapsedMilliseconds);

                    // Now process the pack list (partition packs, treat special cases)
                    overallProgress?.Report(new TaskReport(65, "Process packs"));
                    ProcessPacks(packList);

                    // Set property description by reading the description attribute (for all types)
                    MXFPackFactory.SetDescriptionFromAttributeForAllTypes();

                    // parse all local tags, as now we know the primerpackage aliases
                    ReparseLocalTags(packList.OfType<MXFLocalSet>().Where(ls => ls.Children.OfType<MXFLocalTag>().Any()));

                    // Progress should now be 80%
                    overallProgress?.Report(new TaskReport(73, "Update tree"));

                    // Resolve the references
                    sw.Restart();
                    overallProgress?.Report(new TaskReport(81, "Resolving references"));
                    int numOfResolved = ResolveReferences();
                    Debug.WriteLine("{0} references resolved in {1} ms", numOfResolved, sw.ElapsedMilliseconds);

                    // Create the logical tree
                    overallProgress?.Report(new TaskReport(95, "Creating Logical tree"));
                    sw.Restart();
                    CreateLogicalTree();
                    Debug.WriteLine("Logical tree created in {0} ms", sw.ElapsedMilliseconds);

                    // Finished, return this (MXFFile)
                    overallProgress?.Report(new TaskReport(100, "Done"));
                    return this;

                }
            }, ct);

            return result;
        }

        private void ProcessPacks(IEnumerable<MXFObject> packList)
        {
            MXFPartition currentPartition = null;
            int partitionNumber = 0;

            foreach (var obj in packList)
            {
                switch (obj)
                {
                    case MXFPartition partition:
                        currentPartition = partition;
                        currentPartition.File = this;
                        currentPartition.PartitionNumber = partitionNumber++;
                        this.Children.First().AddChild(currentPartition);
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

                    case MXFPrimerPack primer:
                        if (currentPartition != null)
                        {
                            // Let the partition know all primer keys
                            //allPrimerKeys = primer.AllKeys;
                            currentPartition.PrimerKeys = primer.AllKeys;
                            currentPartition.AddChild(primer); // Add the primer 
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

        private void ReparseLocalTags(IEnumerable<MXFLocalSet> localSetList)
        {
            using (var byteReader = new KLVStreamReader(new FileStream(File.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 10240)))
            {
                foreach (var ls in localSetList)
                {
                    ls.ParseTagsAgain(byteReader);
                }
            }
        }

        /// <summary>
        /// Try to locate the RIP
        /// </summary>
        //private bool ReadRIP(MXFPackFactory mxfPackFactory)
        //{
        //    if (this.RIP == null)
        //    {
        //        // Read the last 4 bytes of the file
        //        mxfReader.Seek(this.Filesize - 4);
        //        uint ripSize = mxfReader.ReadUInt32();
        //        if (ripSize < this.Filesize && ripSize >= 4) // At least 4 bytes
        //        {
        //            mxfReader.Seek(this.Filesize - ripSize);
        //            MXFPack pack = MXFPackFactory.CreatePack(null, mxfReader);
        //            if (pack is MXFRIP rip)
        //            {
        //                // Yes, RIP found
        //                this.AddChild(rip);
        //                this.RIP = rip;
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

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

        /// <summary>
        /// Create the logical view (starting with the preface)
        /// </summary>
        protected void CreateLogicalTree()
        {
            if (this.LogicalTreeRoot == null)
                return;

            LogicalAddChilds(this.LogicalTreeRoot);

            this.LogicalTreeRoot.Children = this.LogicalTreeRoot.Children.OrderBy(c => c.Object.Offset).ToList();
        }

        /// <summary>
        /// Add all children (recursively)
        /// </summary>
        /// <param name="lObj"></param>
        protected MXFLogicalObject LogicalAddChilds(MXFLogicalObject lObj)
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
    }
}
