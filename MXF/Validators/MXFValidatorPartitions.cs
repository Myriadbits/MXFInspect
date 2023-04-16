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

using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Myriadbits.MXF
{
    public class MXFValidatorPartitions : MXFValidator
    {

        public MXFValidatorPartitions(MXFFile file) : base(file)
        {

        }

        public bool AnyPartitionsPresent()
        {
            return this.File.GetPartitions().Any();
        }

        public bool IsFooterPartitionPresent()
        {
            var lastPartition = this.File.GetPartitions()?.Last();
            return lastPartition != null && lastPartition.IsFooterPartition();
        }

        public bool IsHeaderPartitionPresent()
        {
            var firstPartition = this.File.GetPartitions()?.First();
            return firstPartition != null && firstPartition.IsHeaderPartition();
        }

        public bool IsHeaderPartitionUnique()
        {
            int count = this.File.GetPartitions().Where(p => p.IsHeaderPartition()).Count();
            return count == 1 || count == 0;
        }

        public bool IsFooterPartitionUnique()
        {
            int count = this.File.GetPartitions().Where(p => p.IsFooterPartition()).Count();
            return count == 1 || count == 0;
        }

        public bool AreBodyPartitionsPresent()
        {
            return File.GetBodyPartitions().Any();
        }

        public bool AreIndexTableSegmentsInBodyPartitions()
        {
            return this.File.GetBodyPartitions()?.Where(p => p.ContainsIndexTableSegments()).Any() ?? false;
        }

        public bool HeaderPartitionContainsIndexTableSegments()
        {
            if (IsHeaderPartitionPresent() && IsHeaderPartitionUnique())
            {
                return this.File.GetPartitions()?.First().ContainsIndexTableSegments() ?? false;
            }
            else return false;
        }

        public bool HeaderPartitionContainsMetadata()
        {
            if (IsHeaderPartitionPresent() && IsHeaderPartitionUnique())
            {
                return this.File.GetPartitions()?.First().Children.Any(c => c.IsMetadataLike()) ?? false;
            }
            else return false;
        }

        public bool IsOperationalPatternConsistent()
        {
            if (AnyPartitionsPresent())
            {
                return this.File.GetPartitions().GroupBy(p => p.OperationalPattern).Count() == 1;
            }
            else return false;
        }

        public bool IsMajorMinorVersionConsistent()
        {
            if (AnyPartitionsPresent())
            {
                return this.File.GetPartitions().GroupBy(p => new { p.MajorVersion, p.MinorVersion }).Count() == 1;
            }
            else return false;
        }

        public bool FooterPartitionContainsIndexTableSegments()
        {
            if (IsFooterPartitionPresent() && IsFooterPartitionUnique())
            {
                return this.File.GetPartitions()?.Last().Children.Where(c => c is MXFIndexTableSegment).Any() ?? false;
            }
            else return false;
        }

        public bool IsThisPartitionCorrect(MXFPartition p)
        {
            return (ulong)p.Offset == p.ThisPartition;
        }

        public bool IsPreviousPartitionCorrect(MXFPartition p)
        {
            if (p.PreviousSibling() is MXFPartition prev)
            {
                return p.PreviousPartition == (ulong)prev.Offset;
            }
            else return p.PreviousPartition == 0;
        }

        public bool IsPartitionStatusValid(MXFPartition p)
        {
            return
                p.Status == PartitionStatus.OpenComplete ||
                p.Status == PartitionStatus.OpenIncomplete ||
                p.Status == PartitionStatus.ClosedIncomplete ||
                p.Status == PartitionStatus.ClosedComplete;
        }

        public bool IsMajorVersionCorrect(MXFPartition p)
        {
            return p.MajorVersion == 1;
        }

        public bool IsMinorVersionCorrect(MXFPartition p)
        {
            // SMPTE 377:2011 requires Minor Version 1.3
            return p.MinorVersion == 3;
        }

        public bool IsHeaderByteCountCorrect(MXFPartition p)
        {
            // according to SMPTE 377:2011 this is the Count of Bytes used for Header Metadata and
            // Primer Pack. This starts at the first byte of the key of the Primer Pack and ends after
            // any trailing KLV Fill item which is included within this HeaderByteCount.
            ulong headerByteCount = 0;

            var primerPack = p.Children.FirstOrDefault(c => c is MXFPrimerPack);
            var lastHeaderMetadata = p.Children.TakeWhile(c => c.IsHeaderMetadataLike())?.LastOrDefault();
            //var trailingKLV = lastHeaderMetadata?.NextSibling();

            //// add trailing KLV Fill item if any
            //if (trailingKLV != null && trailingKLV.IsFiller())
            //{
            //    lastHeaderMetadata = trailingKLV;
            //}

            if (primerPack != null && lastHeaderMetadata != null)
            {
                headerByteCount = (ulong)(lastHeaderMetadata.Offset + lastHeaderMetadata.TotalLength) - (ulong)primerPack.Offset;
            }

            return p.HeaderByteCount == headerByteCount;
        }

        public bool IsHeaderPartitionOpen()
        {
            if (IsHeaderPartitionPresent())
            {
                return !this.File.GetPartitions().First().IsClosed();
            }
            return false;
        }

        //public MXFValidationResult AreAllPartitionsPointingToFooter()
        //{
        //    var partitions = this.File.GetPartitions();
        //    foreach (var p in partitions)
        //    {
        //        if (p.Closed && p.FooterPartition != )
        //        {
        //            return new MXFValidationResult
        //            {
        //                Severity = MXFValidationSeverity.Info,
        //                Result = "Body partition(s) present"
        //            };
        //        }
        //    }
        //    if (this.File.GetPartitions().Any())
        //    {

        //    }
        //    else return null;
        //}

        /// <summary>
        /// Check if all partitions are valid 
        /// </summary>
        /// <param name="results"></param>
        public override async Task<List<MXFValidationResult>> OnValidate(IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            List<MXFValidationResult> result = await Task.Run(() =>
            {
                var retval = new List<MXFValidationResult>();
                Stopwatch sw = Stopwatch.StartNew();

                this.Description = "Validating partitions";

                MXFValidationResult valResult = new MXFValidationResult("Partition Test");
                retval.Add(valResult);
                valResult.Category = "Partitions";

                if (AnyPartitionsPresent())
                {
                    if (AreBodyPartitionsPresent())
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Object = File.GetPartitionRoot(),
                            Severity = MXFValidationSeverity.Success,
                            Result = $"{this.File.GetBodyPartitions().Count()} Body Partition(s) present"
                        });

                        if (AreIndexTableSegmentsInBodyPartitions())
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Severity = MXFValidationSeverity.Success,
                                Result = $"At least one Body Partition contains Index Table Segments"
                            });
                        }

                        if (!IsOperationalPatternConsistent())
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = File.GetPartitionRoot(),
                                Severity = MXFValidationSeverity.Error,
                                Result = $"The Operational Pattern property is not consistent across all partitions"
                            });
                        }

                        if (!IsMajorMinorVersionConsistent())
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = File.GetPartitionRoot(),
                                Severity = MXFValidationSeverity.Error,
                                Result = $"The Major/Minor Version property is not consistent across all partitions"
                            });
                        }
                    }

                    // Header partition checks

                    if (!IsHeaderPartitionUnique())
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Offset = 0,
                            Severity = MXFValidationSeverity.Error,
                            Result = "Invalid partition structure. Only the first partition shall be a Header Partition"
                        });
                    }

                    if (!IsHeaderPartitionPresent())
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Object = this.File.GetPartitions()?.First(),
                            Severity = MXFValidationSeverity.Error,
                            Result = "Invalid partition structure. The first partition must be a Header Partition"
                        });
                    }
                    else
                    {
                        if (IsHeaderPartitionOpen())
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = this.File.GetPartitions()?.First(),
                                Severity = MXFValidationSeverity.Success,
                                Result = "Header Partition status is \"open\" (= required values may be absent)"
                            });
                        }

                        if (!HeaderPartitionContainsMetadata())
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = this.File.GetPartitions()?.First(),
                                Severity = MXFValidationSeverity.Error,
                                Result = "Header Partition does not contain header metadata"
                            });
                        }

                        if (HeaderPartitionContainsIndexTableSegments())
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = this.File.GetPartitions()?.First(),
                                Severity = MXFValidationSeverity.Success,
                                Result = $"Header Partition contains Index Table Segments"
                            });
                        }
                    }

                    // Footer partition checks

                    //  TODO check if at least two partitions
                    if (!IsFooterPartitionUnique())
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Offset = 0,
                            Severity = MXFValidationSeverity.Error,
                            Result = "Invalid partition structure. Only the last partition can be a Footer Partition"
                        });
                    }

                    if (IsFooterPartitionPresent())
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Object = this.File.GetPartitions().Last(),
                            Severity = MXFValidationSeverity.Success,
                            Result = "Footer Partition present"
                        });

                        if (FooterPartitionContainsIndexTableSegments())
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = this.File.GetPartitions().Last(),
                                Severity = MXFValidationSeverity.Success,
                                Result = $"Footer Partition contains Index Table Segments"
                            });
                        }
                    }
                    //else
                    //{
                    //    retval.Add(new MXFValidationResult
                    //    {
                    //        Object = this.File.GetPartitions().Last(),
                    //        Severity = MXFValidationSeverity.Error,
                    //        Result = "Invalid partition structure. The last partition is not a Footer Partition"
                    //    });
                    //}

                    // checks for all partitions 

                    foreach (var p in this.File.GetPartitions())
                    {
                        if (!IsThisPartitionCorrect(p))
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = p,
                                Severity = MXFValidationSeverity.Error,
                                Result = $"Partition #{p.PartitionNumber} has incorrect value for ThisPartition"
                            });
                        }

                        if (!IsPreviousPartitionCorrect(p))
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = p,
                                Severity = MXFValidationSeverity.Error,
                                Result = $"Partition #{p.PartitionNumber} has incorrect value for PreviousPartition"
                            });
                        }

                        if (!IsPartitionStatusValid(p))
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = p,
                                Severity = MXFValidationSeverity.Error,
                                Result = $"Partition #{p.PartitionNumber} has an invalid PartitionStatus (0x{(byte)p.Status:X2})"
                            });
                        }

                        if (!IsMajorVersionCorrect(p))
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = p,
                                Severity = MXFValidationSeverity.Warning,
                                Result = $"Partition #{p.PartitionNumber} Major Version property has an invalid value (read: {p.MajorVersion}, expected: 1)"
                            });
                        }

                        if (!IsMinorVersionCorrect(p))
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = p,
                                Severity = MXFValidationSeverity.Warning,
                                Result = $"Partition #{p.PartitionNumber} Major Version property has an invalid value (read: {p.MinorVersion}, expected: 3)"
                            });
                        }

                        if (!IsHeaderByteCountCorrect(p))
                        {
                            retval.Add(new MXFValidationResult
                            {
                                Object = p,
                                Severity = MXFValidationSeverity.Error,
                                Result = $"Partition #{p.PartitionNumber} has incorrect value for HeaderByteCount"
                            });
                        }
                    }
                }
                else
                {
                    retval.Add(new MXFValidationResult
                    {
                        // TODO make Offset nullable?
                        Offset = 0,
                        Severity = MXFValidationSeverity.Error,
                        Result = "No partitions detected."
                    });
                }

                MXFPartition footer = this.File.GetPartitions().Where(a => a.PartitionType == PartitionType.Footer).FirstOrDefault();
                long footerExpected = 0;
                if (footer == null)
                {
                    valResult.SetWarning(string.Format("Error! No footer partition present!"));
                    footerExpected = 0;
                }
                else
                    footerExpected = footer.Offset;

                // Check if all partitions point to the previous partition and check the this pointer	
                // Note that this is more serious and less likely to go wrong then the footer check
                for (int n = 0; n < this.File.GetPartitions().Count(); n++)
                {
                    if (this.File.GetPartitions().ToList()[n].ThisPartition != (ulong)this.File.GetPartitions().ToList()[n].Offset)
                    {
                        valResult.SetError(string.Format("Error! Partition[{0}] has an invalid 'ThisPartition' pointer.", n));
                        return retval;
                    }

                    if (n > 0)
                    {
                        if (this.File.GetPartitions().ToList()[n].PreviousPartition != (ulong)this.File.GetPartitions().ToList()[n - 1].Offset)
                        {
                            valResult.SetError(string.Format("Error! Partition[{0}] has no valid link to the previous partition.", n));
                            return retval;
                        }
                    }
                }

                // Check if all partitions point to the footer
                int errorCount = 0;
                for (int n = 0; n < this.File.GetPartitions().Count(); n++)
                {
                    if (this.File.GetPartitions().ToList()[n].IsClosed() && this.File.GetPartitions().ToList()[n].FooterPartition != (ulong)footerExpected)
                        errorCount++;
                }
                if (errorCount > 0)
                {
                    valResult.SetWarning(string.Format("There are {0} partitions that do not point to the footer partition.", errorCount));
                    return retval.Where(r => r is not null).ToList();
                }

                Log.ForContext<MXFValidatorPartitions>().Information($"Validation completed in {sw.ElapsedMilliseconds} ms");

                return retval;
            }, ct);
            return result;
        }
    }
}
