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

namespace Myriadbits.MXF
{
    public enum FileParseMode
    {
        Full,
        Partial,
    }


    public enum PartialSeekMode
    {
        Unknown,
        UsingRIP,
        Backwards,
        //Full,
    }


    /// <summary>
    /// Name object, represents a complete MXF file
    /// </summary>
    public class MXFFile : MXFObject
    {
        private MXFReader m_reader;
        private List<MXFValidationResult> m_results;

        public string Filename { get; set; }
        public long Filesize { get; set; }

        public List<MXFPartition> Partitions { get; set; }
        public MXFRIP RIP { get; set; }

        public List<MXFValidationResult> Results { get { return m_results; } }
        public MXFSystemItem FirstSystemItem { get; set; }
        public MXFSystemItem LastSystemItem { get; set; }

        public MXFLogicalObject LogicalBase { get; set; }

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


        /// <summary>
        /// Create/open an MXF file
        /// </summary>
        /// <param name="fileName"></param>
        public MXFFile(string fileName, BackgroundWorker worker, FileParseMode options = FileParseMode.Full)
        {
            this.Filename = fileName;
            this.Partitions = new List<MXFPartition>();
            this.m_results = new List<MXFValidationResult>();


            switch (options)
            {
                case FileParseMode.Full:
                    ParseFull(worker);
                    break;

                case FileParseMode.Partial:
                    ParsePartial(worker);
                    break;
            }

        }


        /// <summary>
        /// Fully Parse an MXF file 
        /// </summary>
        protected void ParseFull(BackgroundWorker worker)
        {
            Stopwatch sw = Stopwatch.StartNew();

            MXFKLVFactory klvFactory = new MXFKLVFactory();

            MXFPartition currentPartition = null;
            int previousPercentage = 0;
            Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys = null;
            //int[] counters = new int[Enum.GetNames(typeof(KeyType)).Length];
            using (m_reader = new MXFReader(this.Filename))
            {
                this.Filesize = m_reader.Size;
                MXFObject partitions = new MXFNamedObject("Partitions", 0);
                this.AddChild(partitions);

                int partitionNumber = 0; // For easy partition identification
                while (!m_reader.EOF)
                {
                    try
                    {
                        MXFKLV klv = klvFactory.CreateObject(m_reader, currentPartition);
                        
                        //// Update overall counters
                        //if (klv.Key.Type == KeyType.None)
                        //    counters[(int)klv.Key.Type]++;

                        // Process the new KLV
                        ProcessKLVObject(klv, partitions, ref currentPartition, ref partitionNumber, ref allPrimerKeys);

                        // Next KLV please
                        m_reader.Seek(klv.DataOffset + klv.Length);
                    }
                    catch (Exception e)
                    {
                        m_reader.SeekForNextPotentialKey();
                    }



                    // Only report progress when the percentage has changed
                    int currentPercentage = (int)((m_reader.Position * 90) / m_reader.Size);
                    if (currentPercentage != previousPercentage)
                    {
                        worker.ReportProgress(currentPercentage, "Parsing MXF file");
                        previousPercentage = currentPercentage;
                    }
                }
            }

            Debug.WriteLine("Finished parsing file '{0}' in {1} ms", this.Filename, sw.ElapsedMilliseconds);

            // Progress should now be 90%

            DoPostWork(worker, sw, allPrimerKeys);

            // And Execute ALL test
            sw.Restart();
            this.ExecuteValidationTest(worker, true);
            Debug.WriteLine("Tests executed in {0} ms", sw.ElapsedMilliseconds);

            // Finished
            worker.ReportProgress(100, "Finished");
        }

        /// <summary>
        /// Partially Parse an MXF file, skip all data
        /// </summary>
        protected void ParsePartial(BackgroundWorker worker)
        {
            Stopwatch sw = Stopwatch.StartNew();

            MXFKLVFactory klvFactory = new MXFKLVFactory();

            MXFPartition currentPartition = null;
            int previousPercentage = 0;
            Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys = null;
            int[] counters = new int[Enum.GetNames(typeof(KeyType)).Length];
            PartialSeekMode seekMode = PartialSeekMode.Unknown;
            using (m_reader = new MXFReader(this.Filename))
            {
                this.Filesize = m_reader.Size;
                MXFObject partitions = new MXFNamedObject("Partitions", 0);
                this.AddChild(partitions);

                // Start with trying to find the RIP
                bool ripFound = ReadRIP(klvFactory);
                if (ripFound)
                    seekMode = PartialSeekMode.UsingRIP;
                m_reader.Seek(0); // Start at the beginning

                // Start by reading the first partition
                int partitionNumber = 0; // For easy partition identification
                while (!m_reader.EOF && seekMode != PartialSeekMode.Backwards) // Eof and NOT searching backwards
                {
                    MXFKLV klv = klvFactory.CreateObject(m_reader, currentPartition);

                    // Update overall counters
                    if (klv.Key.Type == KeyType.None)
                        counters[(int)klv.Key.Type]++;

                    if (klv.Key.Type == KeyType.Partition && seekMode == PartialSeekMode.Backwards)
                    {
                        if (this.Partitions.Exists(a => a.Offset == klv.Offset))
                        {
                            // A new partition has been found that we already found, quit the main loop
                            break;
                        }
                    }


                    // Process the new KLV
                    ProcessKLVObject(klv, partitions, ref currentPartition, ref partitionNumber, ref allPrimerKeys);


                    // If we found the second partition 
                    long nextSeekPosition = klv.DataOffset + klv.Length;
                    if (partitionNumber >= 2) // Header fully read, now busy with the second partition
                    {
                        switch (seekMode)
                        {
                            case PartialSeekMode.UsingRIP: // And we already found the RIP
                                if (currentPartition.FirstSystemItem != null) // And we found the first system item
                                {
                                    MXFEntryRIP ripEntry = this.RIP.GetPartition(partitionNumber);
                                    if (ripEntry != null)
                                    {
                                        // Mark the current partition as not-completely read
                                        currentPartition.IsLoaded = false;

                                        // Start at the next partition
                                        nextSeekPosition = (long)ripEntry.PartitionOffset;
                                    }
                                }
                                break;

                            case PartialSeekMode.Backwards: // NO RIP, searching backwards
                                                            // Backwards, jump to the PREVIOUS partition
                                if (currentPartition.FirstSystemItem != null) // And we found the first system item
                                {
                                    // Jump to the previous partition
                                    if (currentPartition.PreviousPartition != 0)
                                    {
                                        // And we haven't found this partition yet
                                        if (!this.Partitions.Exists(a => a.ThisPartition == currentPartition.PreviousPartition))
                                            nextSeekPosition = (long)currentPartition.PreviousPartition; // Jump to previous
                                    }
                                }
                                break;

                            case PartialSeekMode.Unknown: // No RIP....
                                                          // Hmmm, RIP is not found, check if we have a footer partition somewhere
                                MXFPartition part = this.Partitions.Where(a => a.FooterPartition != 0).FirstOrDefault();
                                if (part != null)
                                {
                                    // If we are already at the footer, don't bother to seek
                                    if (currentPartition.Offset != (long)part.FooterPartition)
                                    {
                                        nextSeekPosition = (long)part.FooterPartition; // Start at the footer
                                        seekMode = PartialSeekMode.Backwards;
                                    }
                                }
                                break;
                        }
                    }

                    // Next KLV please
                    m_reader.Seek(nextSeekPosition);

                    // Only report progress when the percentage has changed
                    int currentPercentage = (int)((m_reader.Position * 90) / m_reader.Size);
                    if (currentPercentage != previousPercentage)
                    {
                        worker.ReportProgress(currentPercentage, "Partial Parsing MXF file");
                        previousPercentage = currentPercentage;
                    }
                }
            }

            Debug.WriteLine("Finished parsing file '{0}' in {1} ms", this.Filename, sw.ElapsedMilliseconds);

            // Progress should now be 90%

            DoPostWork(worker, sw, allPrimerKeys);

            // And Execute FAST tests
            this.ExecuteValidationTest(worker, false);

            // Finished
            worker.ReportProgress(100, "Finished");
        }

        private void ProcessKLVObject(MXFKLV klv, MXFObject partitions, ref MXFPartition currentPartition, ref int partitionNumber, ref Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys)
        {
            // Is this a header, add to the partitions
            switch (klv.Key.Type)
            {
                case KeyType.Partition:
                    currentPartition = klv as MXFPartition;
                    currentPartition.File = this;
                    currentPartition.PartitionNumber = partitionNumber;
                    this.Partitions.Add(currentPartition);
                    partitions.AddChild(currentPartition);
                    partitionNumber++;
                    break;

                case KeyType.PrimerPack:
                    if (currentPartition != null)
                    {
                        if (klv is MXFPrimerPack primer)
                        {
                            // Let the partition know all primer keys
                            allPrimerKeys = primer.AllKeys;
                            currentPartition.PrimerKeys = primer.AllKeys;
                        }
                        currentPartition.AddChild(klv); // Add the primer 
                    }
                    break;

                case KeyType.RIP:
                    // Only add the RIP when not yet present
                    if (this.RIP == null)
                    {
                        this.AddChild(klv);
                        this.RIP = klv as MXFRIP;
                    }
                    break;

                case KeyType.SystemItem:
                    if (currentPartition != null)
                    {
                        // Store the first system item for every partition
                        // (required to calculate essence positions)
                        if (currentPartition.FirstSystemItem == null)
                            currentPartition.FirstSystemItem = klv as MXFSystemItem;
                        currentPartition.AddChild(klv);
                    }
                    else
                        this.AddChild(klv);

                    // Store the first and the last system item
                    if (this.FirstSystemItem == null)
                        this.FirstSystemItem = klv as MXFSystemItem;
                    this.LastSystemItem = klv as MXFSystemItem;
                    break;


                case KeyType.Essence:
                    if (currentPartition != null)
                    {
                        // Store the first system item for every partition
                        // (required to calculate essence positions)
                        MXFEssenceElement ee = klv as MXFEssenceElement;
                        if (ee.IsPicture && currentPartition.FirstPictureEssenceElement == null)
                            currentPartition.FirstPictureEssenceElement = ee;
                        currentPartition.AddChild(klv);
                    }
                    else
                        this.AddChild(klv);
                    break;

                case KeyType.Preface:
                    this.LogicalBase = klv.CreateLogicalObject();
                    // Normal
                    if (currentPartition != null)
                        currentPartition.AddChild(klv);
                    else
                        this.AddChild(klv);
                    break;

                default:
                    // Normal
                    if (currentPartition != null)
                        currentPartition.AddChild(klv);
                    else
                        this.AddChild(klv);
                    break;
            }
        }

        private void DoPostWork(BackgroundWorker worker, Stopwatch sw, Dictionary<UInt16, MXFEntryPrimer> allPrimerKeys)
        {
            // Update all type descriptions
            MXFKLVFactory.UpdateAllTypeDescriptions(allPrimerKeys);

            // Resolve the references
            sw.Restart();
            worker.ReportProgress(93, "Resolving flatlist");
            int numOfResolved = ResolveReferences();
            Debug.WriteLine("{0} references resolved in {1} ms", numOfResolved, sw.ElapsedMilliseconds);


            // Create the logical tree
            worker.ReportProgress(94, "Creating Logical tree");
            sw.Restart();
            CreateLogicalTree();
            Debug.WriteLine("Logical tree created in {0} ms", sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// Try to locate the RIP
        /// </summary>
        private bool ReadRIP(MXFKLVFactory klvFactory)
        {
            if (this.RIP == null)
            {
                // Read the last 4 bytes of the file
                m_reader.Seek(this.Filesize - 4);
                uint ripSize = m_reader.ReadUInt32();
                if (ripSize < this.Filesize && ripSize >= 4) // At least 4 bytes
                {
                    m_reader.Seek(this.Filesize - ripSize);
                    MXFKLV klv = klvFactory.CreateObject(m_reader, null);
                    if (klv.Key.Type == KeyType.RIP)
                    {
                        // Yes, RIP found
                        this.AddChild(klv);
                        this.RIP = klv as MXFRIP;
                        return true;
                    }
                }
            }
            return false;
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

        /// <summary>
        /// Create the logical view (starting with the preface)
        /// </summary>
        protected void CreateLogicalTree()
        {
            if (this.LogicalBase == null)
                return;

            LogicalAddChilds(this.LogicalBase);

            this.LogicalBase.Children = this.LogicalBase.Children.OrderBy(c => c.Object.Offset).ToList();
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
                    if(refObj != null)
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
        protected int ResolveReferences()
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
        public string GetTrackInfo(MXFGenericTrack genericTrack)
        {
            try
            {
                if (genericTrack != null)
                {
                    StringBuilder sb = new StringBuilder();
                    MXFSequence seq = genericTrack.GetFirstMXFSequence();
                    if (seq != null && seq.DataDefinition != null)
                    {
                        sb.Append(string.Format("{0}", seq.DataDefinition.Name));
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
