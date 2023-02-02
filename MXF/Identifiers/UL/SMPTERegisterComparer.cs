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

using System.Collections.Generic;

namespace Myriadbits.MXF.Identifiers
{
    public class SMPTERegisterComparer : IEqualityComparer<ByteArray>
    {
        //TOO bypassing of version byte shall be a settable property
        public bool Equals(ByteArray x, ByteArray y)
        {
            for (int i = 0; i < x.ArrayLength; i++)
            {
                // bypass klv syntaxes (i.e. 0x7F == 0x06 or 0x53) which is the sixth byte of an UL
                // bypass also version byte 
                if (i == 5 || i == 7)
                {
                    continue;
                }
                else if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(ByteArray theBytes)
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < theBytes.ArrayLength; i++)
                {
                    // bypass klv syntaxes (i.e. 0x7F == 0x06 or 0x53) which is the sixth byte of an UL
                    // bypass also version byte 
                    if (i == 5 || i == 7)
                    {
                        continue;
                    }
                    else
                    {
                        hash = (hash ^ theBytes[i]) * p;
                    }
                }

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
        }
    }
}
