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
using System.Linq;

namespace Myriadbits.MXF.KLV
{
    public class UL : KLVKey
    {
        // TODO static really needed?
        private static readonly byte[] validULPrefix = new byte[] { 0x06, 0x0e, 0x2b, 0x34 };

        public UL(params byte[] bytes) : base(bytes)
        {
            if (bytes.Length != (int)KeyLength.SixteenBytes)
            {
                throw new ArgumentException("Wrong number of bytes. A SMPTE Universal Label must consist of exactly 16 bytes");
            }
            else if (!bytes.SequenceEqual(validULPrefix))
            {
                throw new ArgumentException("Wrong byte value. A SMPTE Universal Label  start with the following first 4-byte values: 0x06, 0x0e, 0x2b, 0x34");
            }
        }
    }

}
