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
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Serilog;
using System.Diagnostics;

namespace Myriadbits.MXF
{
    public class MXFValidatorInfo : MXFValidator
    {
        public MXFValidatorInfo(MXFFile file) : base(file)
        {

        }

        protected override async Task<List<MXFValidationResult>> OnValidate(IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            List<MXFValidationResult> result = await Task.Run(() =>
            {
                var retval = new List<MXFValidationResult>();
                Stopwatch sw = Stopwatch.StartNew();

                this.Description = "Track Info";
                MXFMaterialPackage mp = this.File.GetContentStorage()?.GetFirstMaterialPackage();
                List<MXFTrack> tracks = mp.GetGenericTracks().ToList();

                foreach (var t in tracks)
                {
                    int n = tracks.IndexOf(t);
                    progress?.Report(new TaskReport(n * 100 / tracks.Count, ""));
                    retval.Add(new MXFValidationResult
                    {
                        Category = "Track Info",
                        Severity = MXFValidationSeverity.Info,
                        Object = t,
                        Message = this.File.GetTrackInfo(t)
                    });
                }
                Log.ForContext<MXFValidatorInfo>().Information($"Validation completed in {sw.ElapsedMilliseconds} ms");
                return retval;
            }, ct);

            return result;
        }
    }
}
