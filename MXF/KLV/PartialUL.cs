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

using Myriadbits.MXF.Identifiers;
using System;

namespace Myriadbits.MXF
{
    /// <summary>
    /// Helper class for comparing ULs not by all entire 16 bytes (as per definition)
    /// but instead allow for wildcards. See <see cref="MXFPackFactory"/>.
    /// </summary>
    public class PartialUL : ByteArray
    {
        public PartialUL(params byte[] bytes) : base(bytes)
        {
            ValidateByteArray(bytes);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        private static void ValidateByteArray(byte[] bytes)
        {
            if (!UL.HasValidULPrefix(bytes))
            {
                throw new ArgumentException("Wrong byte value. A partial Universal Label must start with the following byte sequence: 0x06, 0x0e, 0x2b, 0x34");
            }
        }
    }

}
