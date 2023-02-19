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
	public class MXFValidatorRIP : MXFValidator
	{
		/// <summary>
		/// Check if the RIP is present and valid
		/// </summary>
		/// <param name="this.File"></param>
		/// <param name="results"></param>
		public override void OnExecuteTest(ref List<MXFValidationResult> results)
		{
			MXFValidationResult valResult = new MXFValidationResult("Random Index Pack Test");
			results.Add(valResult);
			valResult.Category = "Random Index Pack";

			Stopwatch sw = Stopwatch.StartNew();

			if (this.File.RIP == null)
			{
				valResult.SetError(string.Format("Error! No RIP found."));
				return;
			}

			if (this.File.RIPEntryCount != this.File.PartitionCount)
			{
				valResult.SetError(string.Format("Error! Number of RIP entries is not equal to the number of partitions ({0} vs {1}).", this.File.RIPEntryCount, this.File.PartitionCount));
				return;
			}

			int ripErrorCount = 0;
			for(int n = 0; n < this.File.RIPEntryCount; n++)
			{
				MXFEntryRIP rip = this.File.RIP.Children[n] as MXFEntryRIP;
				if (rip != null)
				{
					MXFPartition part = this.File.Partitions.Where(a => (ulong) a.Offset == rip.PartitionOffset).FirstOrDefault();
					if (part == null)
					{
						ripErrorCount++;
						valResult.AddError(string.Format("Error! RIP entry {0} not pointing to a valid partion.", n));
						return;
					}
				}
			}
			if (ripErrorCount > 0)
			{
				valResult.SetError(string.Format("Error! {0} RIP entries are not pointing to a valid partion.", ripErrorCount));
				return;
			}

			valResult.SetSuccess("Random Index Pack (RIP) is valid.");
			
			LogInfo("Validation completed in {0} msec", sw.ElapsedMilliseconds);
		}


		
	}
}
