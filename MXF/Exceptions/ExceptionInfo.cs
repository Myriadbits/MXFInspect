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
using System.ComponentModel;

namespace Myriadbits.MXF.Exceptions
{
    // Deliberately taken from https://stackoverflow.com/a/72968664
    public static class ExceptionExtensions
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ExceptionInfo
        {
            public ExceptionInfo() { }
            public string Type { get; set; }

            [MultiLine()]
            public string Message { get; set; }
            public string Source { get; set; }

            [MultiLine()]
            public string StackTrace { get; set; }
            public ExceptionInfo InnerException { get; set; }

            internal ExceptionInfo(Exception exception, bool includeInnerException = true, bool includeStackTrace = false)
            {
                if (exception is null)
                {
                    throw new ArgumentNullException(nameof(exception));
                }

                Type = exception.GetType().FullName;
                Message = exception.Message;
                Source = exception.Source;
                StackTrace = includeStackTrace ? exception.StackTrace : null;
                if (includeInnerException && exception.InnerException is not null)
                {
                    InnerException = new ExceptionInfo(exception.InnerException, includeInnerException, includeStackTrace);
                }
            }

            public override string ToString()
            {
                return Message.ToString();
            }
        }
    }
}
