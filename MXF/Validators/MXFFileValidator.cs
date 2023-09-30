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

using Myriadbits.MXF.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Myriadbits.MXF
{
	public class MXFFileValidator
	{
        public static async Task<List<MXFValidationResult>> ValidateFile(MXFFile file, bool extendedTest, IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            List<MXFValidationResult> resultsList = new List<MXFValidationResult>();
            ct.ThrowIfCancellationRequested();


            // create list of tests
            List<MXFValidator> allTests = new List<MXFValidator>
            {
                new MXFValidatorInfo(file),
                new MXFValidatorPartitions(file),
                new MXFValidatorRIP(file),
                new MXFValidatorUL(file)
            };

            if (extendedTest)
            {
                allTests.Add(new MXFValidatorIndex(file));
            }
            else
            {
                MXFValidationResult valResult = new MXFValidationResult("Index Table");
                valResult.SetQuestion("Index table test not executed.");
                resultsList.Add(valResult);
            }

            // add exceptions
            foreach (var ex in file.Exceptions)
            {
                MXFValidationResult result;

                switch (ex)
                {
                    case EndOfKLVStreamException eofEx:
                        result = new MXFValidationResult("KLVStream");
                        result.Object = eofEx.TruncatedKLV;
                        result.SetError(eofEx.Message, eofEx.Offset);
                        break;

                    case UnparseablePackException upEx:
                        result = new MXFValidationResult("Parser");
                        result.Object = upEx.UnparseablePack;
                        result.SetError(upEx.Message, upEx.Offset);
                        break;

                    case KLVParsingException pEx:
                        result = new MXFValidationResult("Parser");
                        result.Object = file.Descendants().Where(o => o.Offset == pEx.Offset).FirstOrDefault();
                        result.SetError(pEx.InnerException?.Message ?? ex.Message, pEx.Offset);
                        break;

                    default:
                        result = new MXFValidationResult(ex.GetType().Name);
                        result.SetError(ex.InnerException?.Message ?? ex.Message);
                        break;
                }
                resultsList.Add(result);
            }

            // execute validators

            foreach (MXFValidator mxfTest in allTests)
            {
                resultsList.AddRange(await mxfTest.Validate(progress, ct));
            }

            return resultsList;
        }
    }
}
