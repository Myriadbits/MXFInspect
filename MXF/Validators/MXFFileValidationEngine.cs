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
    public class MXFFileValidationEngine
    {
        public static async Task<List<MXFValidationResult>> Validate(MXFFile file, IEnumerable<MXFValidator> validatorList, IProgress<TaskReport> progress = null, CancellationToken ct = default)
        {
            List<MXFValidationResult> resultsList = new List<MXFValidationResult>();
            ct.ThrowIfCancellationRequested();


            if (!validatorList.OfType<MXFValidatorIndex>().Any())
            {
                resultsList.Add(new MXFValidationResult
                {
                    Category = "Index tables",
                    Severity = MXFValidationSeverity.Question,
                    Message = "Index table test not executed"
                });
            }

            // add exceptions
            foreach (var ex in file.Exceptions)
            {
                MXFValidationResult result;

                switch (ex)
                {
                    case EndOfKLVStreamException eofEx:
                        result = new MXFValidationResult
                        {
                            Category = "KLVStream",
                            Severity = MXFValidationSeverity.Error,
                            Object = eofEx.TruncatedObject,
                            Offset = eofEx.Offset,
                            Message = eofEx.Message
                        };
                        break;

                    case UnparseablePackException upEx:
                        result = new MXFValidationResult
                        {
                            Category = "Parser",
                            Severity = MXFValidationSeverity.Error,
                            Object = upEx.UnparseablePack,
                            Offset = upEx.Offset,
                            Message = upEx.Message
                        };
                        break;

                    case KLVParsingException pEx:
                        result = new MXFValidationResult
                        {
                            Category = "Parser",
                            Severity = MXFValidationSeverity.Error,
                            Object = file.Descendants().Where(o => o.Offset == pEx.Offset).FirstOrDefault(),
                            Offset = pEx.Offset,
                            Message = pEx.InnerException?.Message ?? ex.Message
                        };
                        break;

                    default:
                        result = new MXFValidationResult
                        {
                            Severity = MXFValidationSeverity.Error,
                            Category = ex.GetType().Name,
                            Message = ex.InnerException?.Message ?? ex.Message
                        };
                        break;
                }
                resultsList.Add(result);
            }

            // execute validators

            foreach (MXFValidator validator in validatorList)
            {
                resultsList.AddRange(await validator.Validate(progress, ct));
            }

            return resultsList;
        }
    }
}
