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
using System.Threading;
using System.Threading.Tasks;

namespace Myriadbits.MXF
{
    public class MXFValidator
    {
        public MXFFile File { get; set; }
        public string Description { get; set; }
        protected List<MXFValidationResult> Results { get; set; }

        public MXFValidator(MXFFile file)
        {
            File = file;
        }

        public async Task<List<MXFValidationResult>> Validate(IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Results = await OnValidate(progress, ct);
            Debug.WriteLine("Validation test run in {0} ms", sw.ElapsedMilliseconds);
            return Results;
        }

        /// <summary>
        /// Override in derived classes
        /// </summary>
        protected virtual async Task<List<MXFValidationResult>> OnValidate(IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            List<MXFValidationResult> result = await Task.Run(() =>
            {
                return new List<MXFValidationResult>();
            }, ct);
            return result;
        }
    }
}
