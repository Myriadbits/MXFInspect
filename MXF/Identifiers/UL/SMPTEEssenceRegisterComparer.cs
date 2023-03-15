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

namespace Myriadbits.MXF.Identifiers
{
    public class SMPTEEssenceRegisterComparer : IEqualityComparer<ByteArray>
    {
        private const byte WILDCARD_BYTE = 0x7f;

        public bool Equals(ByteArray x, ByteArray y)
        {
            return IsWildCardEqual(x, y);
        }

        public int GetHashCode(ByteArray obj)
        {
            // TODO think about hashing, but at the moment due to dictionary size let's keep it simple
            return 0;
        }

        private bool IsWildCardEqual(ByteArray b1, ByteArray b2)
        {
            int smallerLength = Math.Min(b1.ArrayLength, b2.ArrayLength);

            for (int i = 0; i < smallerLength; i++)
            {
                if (b1[i] == WILDCARD_BYTE || b2[i] == WILDCARD_BYTE)
                {
                    continue;
                }
                else if (b1[i] != b2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
