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
    public class MXFValidatorKLVStream : MXFValidator
    {

        public MXFValidatorKLVStream(MXFFile file) : base(file)
        {
        }

        /// <summary>
        /// Check if klv stream contains errors
        /// </summary>
        /// <param name="this.File"></param>
        /// <param name="results"></param>
        public override async Task<List<MXFValidationResult>> OnValidate(IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            const string CATEGORY_NAME = "KLV Stream";

            List<MXFValidationResult> result = await Task.Run(() =>
            {
                var retval = new List<MXFValidationResult>();
                Stopwatch sw = Stopwatch.StartNew();

                this.Description = "Validating KLV Stream";


                // get Non-KLV areas
                var nonKLVs = File.Descendants().OfType<MXFNonKLV>();
                foreach (var nonKLV in nonKLVs)
                {
                    retval.Add(new MXFValidationResult
                    {
                        Category = CATEGORY_NAME,
                        Object = nonKLV,
                        Severity = MXFValidationSeverity.Error,
                        Result = $"Non-KLV chunk of data from {nonKLV.Offset} to {nonKLV.Offset + nonKLV.TotalLength}"
                    });
                }

                // get Run-In
                var runIn = File.Descendants().OfType<MXFRunIn>().SingleOrDefault();
                if (runIn != null)
                {
                    retval.Add(new MXFValidationResult
                    {
                        Category = CATEGORY_NAME,
                        Object = runIn,
                        Severity = MXFValidationSeverity.Error,
                        Result = $"Run-In present which is not allowed for Generic Operational Pattern and Operational Pattern Atom files."
                    });
                }


                // get KLV triplets with BER length: indefinite 
                var indefiniteKLVs = File.Descendants().OfType<MXFPack>().Where(p => p.Length.BERForm == KLVBERLength.BERForms.Indefinite);
                foreach (var iKLV in indefiniteKLVs)
                {
                    retval.Add(new MXFValidationResult
                    {
                        Category = CATEGORY_NAME,
                        Object = iKLV,
                        Severity = MXFValidationSeverity.Error,
                        Result = $"KLV triplet with indefinite BER length"
                    });
                }

                Log.ForContext<MXFValidatorKLVStream>().Information($"Validation completed in {sw.ElapsedMilliseconds} ms");
                return retval;

            }, ct);
            return result;


        }
    }
}
