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
using Myriadbits.MXF.Properties;

namespace Myriadbits.MXF
{
    public class MXFValidatorRIP : MXFValidator
    {
        private ulong runInHeaderOffset = 0;

        public MXFValidatorRIP(MXFFile file) : base(file)
        {
            // if there is a RunIn consider it for the partition offsets
            var runIn = File.Descendants().OfType<MXFRunIn>().SingleOrDefault();
            if (runIn != null)
            {
                runInHeaderOffset = (ulong)runIn.TotalLength;
            }
        }

        /// <summary>
        /// Check if klv stream contains errors
        /// </summary>
        /// <param name="this.File"></param>
        /// <param name="results"></param>
        protected override async Task<List<MXFValidationResult>> OnValidate(IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            const string CATEGORY_NAME = "Random Index Pack";

            List<MXFValidationResult> result = await Task.Run(() =>
            {
                var retval = new List<MXFValidationResult>();
                Stopwatch sw = Stopwatch.StartNew();

                this.Description = "Validating RIP";

                MXFRIP rip = this.File.GetRIP();

                IEnumerable<MXFEntryRIP> ripEntries = rip?.Children.OfType<MXFEntryRIP>();

                // TODO check if RIP is unique

                if (rip != null)
                {
                    retval.Add(new MXFValidationResult
                    {
                        Category = CATEGORY_NAME,
                        Object = rip,
                        Severity = MXFValidationSeverity.Success,
                        Message = ValidationMessages.ID_0074
                    });


                    if (!AreAllRIPChildrenRIPEntries(rip))
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Category = CATEGORY_NAME,
                            Object = rip,
                            Severity = MXFValidationSeverity.Error,
                            Message = ValidationMessages.ID_0712
                        });
                    }


                    // for every partition there must be a RIP entry
                    int ripEntryCount = rip.Children.Count;
                    List<MXFPartition> partitions = this.File.GetPartitions().ToList();
                    int partitionCount = partitions.Count;

                    if (!RIPEntryCountEqualsPartitionCount(rip))
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Category = CATEGORY_NAME,
                            Object = rip,
                            Severity = MXFValidationSeverity.Error,
                            Message = string.Format(ValidationMessages.ID_0713, ripEntryCount, partitionCount)
                        });

                        foreach (var p in partitions)
                        {
                            if (!HasPartitionRIPEntry(p, rip))
                            {
                                retval.Add(new MXFValidationResult
                                {
                                    Category = CATEGORY_NAME,
                                    Object = rip,
                                    Severity = MXFValidationSeverity.Error,
                                    Message = string.Format(ValidationMessages.ID_0714, p.PartitionNumber)
                                });
                            }
                        }
                    }

                    // check validity of all RIP entries
                    for (int n = 0; n < ripEntryCount; n++)
                    {
                        if (rip.Children[n] is MXFEntryRIP ripEntry)
                        {
                            if (!IsRIPEntryPointingToPartition(ripEntry))
                            {
                                retval.Add(new MXFValidationResult
                                {
                                    Category = CATEGORY_NAME,
                                    Object = rip.Children[n],
                                    Severity = MXFValidationSeverity.Error,
                                    Message = string.Format(ValidationMessages.ID_0062, rip.Children[n])
                                });
                            }
                        }
                    }

                    // check if RIP entries are ordered ascending
                    if (!AreAllRIPEntriesAscending(rip))
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Category = CATEGORY_NAME,
                            Object = rip,
                            Severity = MXFValidationSeverity.Error,
                            Message = ValidationMessages.ID_0715
                        });
                    }

                    // TODO check DeclaredTotalLength against effective TotalLength
                    // really neccessary? would be caught earlier as pack not parseable, i.e. exception at ctor
                }
                else
                {
                    retval.Add(new MXFValidationResult
                    {
                        Category = CATEGORY_NAME,
                        Object = rip,
                        Severity = MXFValidationSeverity.Success,
                        Message = ValidationMessages.ID_0075
                    });
                }
                Log.ForContext<MXFValidatorRIP>().Information($"Validation completed in {sw.ElapsedMilliseconds} ms");
                return retval;

            }, ct);
            return result;


        }

        public bool IsRIPEntryPointingToPartition(MXFEntryRIP ripEntry)
        {
            // TODO beware of SingleOrDefault
            return File.GetPartitions().SingleOrDefault(p => (ulong)p.Offset == ripEntry.PartitionOffset + runInHeaderOffset) != null;
        }

        public bool AreAllRIPChildrenRIPEntries(MXFRIP rip)
        {
            return rip.Children.All(e => e is MXFEntryRIP);
        }

        // The pairs shall be stored in ascending Byte Offset order
        public bool AreAllRIPEntriesAscending(MXFRIP rip)
        {
            var ripEntries = GetRIPEntries(rip);
            var orderedRipEntries = ripEntries.OrderBy(e => e.PartitionOffset);
            return Enumerable.SequenceEqual(ripEntries, orderedRipEntries);
        }

        public bool HasPartitionRIPEntry(MXFPartition p, MXFRIP rip)
        {
            var ripEntries = GetRIPEntries(rip);
            return ripEntries.Any(e => e.PartitionOffset == (ulong)p.Offset);
        }

        // Every Partition shall be indexed if the Random Index Pack exists.
        public bool RIPEntryCountEqualsPartitionCount(MXFRIP rip)
        {
            int ripEntryCount = rip.Children.Count;
            int partitionCount = this.File.GetPartitions().Count();
            return ripEntryCount == partitionCount;
        }

        public IEnumerable<MXFEntryRIP> GetRIPEntries(MXFRIP rip)
        {
            return rip.Children.OfType<MXFEntryRIP>();
        }

        public bool IsRIPPresent()
        {
            // TODO beware of SingleOrDefault inside GetRip()
            return File.GetRIP() != null;
        }
    }
}