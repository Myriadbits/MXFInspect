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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Myriadbits.MXF
{
    public class MXFValidatorRIP : MXFValidator
    {

        public MXFValidatorRIP(MXFFile file) : base(file)
        {

        }

        /// <summary>
        /// Check if the RIP is present and valid
        /// </summary>
        /// <param name="this.File"></param>
        /// <param name="results"></param>
        public override async Task<List<MXFValidationResult>> OnValidate(IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            const string CATEGORY_NAME = "Random Index Pack";

            List<MXFValidationResult> result = await Task.Run(() =>
            {
                var retval = new List<MXFValidationResult>();
                Stopwatch sw = Stopwatch.StartNew();

                this.Description = "Validating partitions";

                MXFRIP rip = this.File.GetRIP();
                int ripEntryCount = rip.Children.Count;
                int partitionCount = this.File.GetPartitions().Count();

                if (rip != null)
                {
                    retval.Add(new MXFValidationResult
                    {
                        Category = CATEGORY_NAME,
                        Object = rip,
                        Severity = MXFValidationSeverity.Success,
                        Result = "Random Index Pack present"
                    });

                    if (ripEntryCount != partitionCount)
                    {
                        retval.Add(new MXFValidationResult
                        {
                            Category = CATEGORY_NAME,
                            Object = rip,
                            Severity = MXFValidationSeverity.Error,
                            Result = $"Number of RIP entries is not equal to the number of partitions ({ripEntryCount} vs {partitionCount})"
                        });
                    }

                    for (int n = 0; n < ripEntryCount; n++)
                    {
                        if (rip.Children[n] is MXFEntryRIP ripEntry)
                        {

                            MXFPartition partition = File.GetPartitions().FirstOrDefault(p => (ulong)p.Offset == ripEntry.PartitionOffset);
                            if (partition == null)
                            {
                                retval.Add(new MXFValidationResult
                                {
                                    Category = CATEGORY_NAME,
                                    Object = rip.Children[n],
                                    Severity = MXFValidationSeverity.Error,
                                    Result = $"RIP entry {rip.Children[n]} not pointing to a partition location."
                                });
                            }
                        }
                    }

                    // TODO check DeclaredTotalLength against effective TotalLength
                }
                Log.ForContext<MXFValidatorRIP>().Information($"Validation completed in {sw.ElapsedMilliseconds} ms");
                return retval;

            }, ct);
            return result;


        }
    }
}
