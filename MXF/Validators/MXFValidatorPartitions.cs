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
    public class MXFValidatorPartitions : MXFValidator
    {

        public MXFValidatorPartitions(MXFFile file) : base(file)
        {

        }

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


                if (!this.File.GetPartitions().Any())
                {
                    valResult.SetError(string.Format("Error! No partitions detected."));
                    return retval;
                }

                if (this.File.GetFooter() != null)
                {
                    valResult.SetInfo("Footer partition detected");
                }


                // Check if only a single header is present
                if (this.File.GetPartitions().Where(a => a.PartitionType == PartitionType.Header).Count() > 1)
                {
                    valResult.SetError(string.Format("Error! More then 1 header partion present!"));
                    return retval;
                }
                MXFPartition header = this.File.GetPartitions().Where(a => a.PartitionType == PartitionType.Header).FirstOrDefault();
                if (header == null)
                {
                    valResult.SetError(string.Format("Error! No header partition present!"));
                    return retval;
                }

                // Check if only a single footer is present
                if (this.File.GetPartitions().Where(a => a.PartitionType == PartitionType.Footer).Count() > 1)
                {
                    valResult.SetError(string.Format("Error! More then 1 footer partion present!"));
                    return retval;
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
                    if (this.File.GetPartitions().ToList()[n].Closed && this.File.GetPartitions().ToList()[n].FooterPartition != (ulong)footerExpected)
                        errorCount++;
                }
                if (errorCount > 0)
                {
                    valResult.SetWarning(string.Format("There are {0} partitions that do not point to the footer partition.", errorCount));
                    return retval;
                }

                valResult.SetSuccess("Partition structure is valid.");
                Log.ForContext<MXFValidatorPartitions>().Information($"Validation completed in {sw.ElapsedMilliseconds} ms");
                return retval;
            }, ct);
            return result;
        }
    }
}
