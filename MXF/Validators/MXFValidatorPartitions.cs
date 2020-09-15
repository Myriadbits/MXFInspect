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

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Myriadbits.MXF
{	
	public class MXFValidatorPartitions : MXFValidator
	{
		/// <summary>
		/// Check if all partitions are valid 
		/// </summary>
		/// <param name="results"></param>
		public override void OnExecuteTest(ref List<MXFValidationResult> results)
		{
			this.Task = "Validating partitions";

			MXFValidationResult valResult = new MXFValidationResult("Partition Test");
			results.Add(valResult);
			valResult.Category = "Partitions";

			Stopwatch sw = Stopwatch.StartNew();

			if (this.File.Partitions == null || this.File.Partitions.Count == 0)
			{
				valResult.SetError(string.Format("Error! No partitions present."));
				return;
			}


			// Check if only a single header is present
			if (this.File.Partitions.Where(a => a.PartitionType == PartitionType.Header).Count() > 1)
			{
				valResult.SetError(string.Format("Error! More then 1 header partion present!"));
				return;
			}
			MXFPartition header = this.File.Partitions.Where(a => a.PartitionType == PartitionType.Header).FirstOrDefault();
			if (header == null)
			{
				valResult.SetError(string.Format("Error! No header partition present!"));
				return;
			}

			// Check if only a single footer is present
			if (this.File.Partitions.Where(a => a.PartitionType == PartitionType.Footer).Count() > 1)
			{
				valResult.SetError(string.Format("Error! More then 1 footer partion present!"));
				return;
			}
			MXFPartition footer = this.File.Partitions.Where(a => a.PartitionType == PartitionType.Footer).FirstOrDefault();
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
			for (int n = 0; n < this.File.Partitions.Count(); n++)
			{
				if (this.File.Partitions[n].ThisPartition != (ulong)this.File.Partitions[n].Offset)
				{
					valResult.SetError(string.Format("Error! Partition[{0}] has an invalid 'ThisPartition' pointer.", n));
					return;
				}

				if (n > 0)
				{
					if (this.File.Partitions[n].PreviousPartition != (ulong)this.File.Partitions[n - 1].Offset)
					{
						valResult.SetError(string.Format("Error! Partition[{0}] has no valid link to the previous partition.", n));
						return;
					}
				}
			}

			// Check if all partitions point to the footer
			int errorCount = 0;
			for (int n = 0; n < this.File.Partitions.Count(); n++)
			{
				if (this.File.Partitions[n].Closed && this.File.Partitions[n].FooterPartition != (ulong)footerExpected)
					errorCount++;
			}
			if (errorCount > 0)
			{
				valResult.SetWarning(string.Format("There are {0} partitions that do not point to the footer partition.", errorCount));
				return;
			}
				
			valResult.SetSuccess("Partition structure is valid.");
			
			LogInfo("Validation completed in {0} msec", sw.ElapsedMilliseconds);
		}	
	}
}
