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


namespace Myriadbits.MXF
{
    public enum MXFValidationSeverity
    {
        Success = 1,
        Warning = 2,
        Error = 3,
        Info = 4,
        Question = 5
    };

    public class MXFValidationResult
    {
        public string Category { get; set; }
        public MXFObject Object { get; set; }
        public long Offset { get { return Object?.Offset ?? 0; } set { } }
        public MXFValidationSeverity Severity { get; set; }
        public string Message { get; set; }

        public MXFValidationResult()
        {

        }
    }
}
