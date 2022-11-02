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

namespace Myriadbits.MXF.Identifiers
{
    public class AUID : KLVKey
    {
        /// <summary>
        /// AAF uses a 16-byte unique identifier known as an Authoring Unique Identifier
        /// (AUID) for this purpose. The value of an AUID is either an SMPTE 298M 
        /// Universal Label(UL) or a UUID.5
        /// </summary>
        public AUID(byte[] bytes) : base(KeyLengths.SixteenBytes, bytes)
        {

        }
    }
}
