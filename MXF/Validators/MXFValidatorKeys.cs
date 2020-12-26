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
using System.Diagnostics;
using System.Linq;

namespace Myriadbits.MXF
{
    public class MXFValidatorKeys : MXFValidator
	{
		public override void OnExecuteTest(ref List<MXFValidationResult> results)
		{
			this.Task = "Checking MXF Keys";		
			Stopwatch sw = Stopwatch.StartNew();

			var klvWithUnknownKeys = this.File.Descendants()
											.OfType<MXFKLV>()
											.Where(klv => string.IsNullOrEmpty(klv.Key.Name))
											.OrderBy(klv => klv.Offset);

			foreach (var unkownKLV in klvWithUnknownKeys)
            {
				MXFValidationResult valResult = new MXFValidationResult("Keys");
                valResult.SetWarning(string.Format("Unknown key {0} @ {1}.", unkownKLV.Key, unkownKLV.Offset));
				results.Add(valResult); // And directly add the results
			}

			LogInfo("MXF Key check completed in {0} msec", sw.ElapsedMilliseconds);
		}
	}
}
