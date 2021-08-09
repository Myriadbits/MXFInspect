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

namespace Myriadbits.MXF.Identifiers
{
    // TODO why as struct? Probably performance
    // short keys implemented as struct, as there are loaded
    // over 1000 in the static constructor => so better performance?
    public struct MXFShortKey
    {
        public readonly UInt64 Key1;
        public readonly UInt64 Key2;
        public readonly byte[] array;

        public MXFShortKey(byte[] data)
        {
            // TODO why changing this?
            // Change endianess
            this.Key1 = 0;
            this.Key2 = 0;
            this.array = data;

            if (data.Length == 16)
            {
                byte[] datar = new byte[16];
                Array.Copy(data, datar, 16);
                Array.Reverse(datar);
                this.Key2 = BitConverter.ToUInt64(datar, 0);
                this.Key1 = BitConverter.ToUInt64(datar, 8);
            }
        }

        public override string ToString()
        {
            return string.Format(string.Format("{0:X16}.{1:X16}", this.Key1, this.Key2));
        }


        public override bool Equals(object obj)
        {
            return obj is MXFShortKey sk && sk == this;
        }

        public override int GetHashCode()
        {
            return 5;
        }

        public static bool operator ==(MXFShortKey first, MXFShortKey second)
        {
            for (int i = 0; i < Math.Min(first.array.Length, second.array.Length); i++)
            {
                // TODO: not really good way:
                // bypass klv syntaxes (i.e. 7F == 06 or 53) and the UL version number 
                if (i == 5 || i == 7)
                {
                    continue;
                }
                else if (first.array[i] != second.array[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(MXFShortKey first, MXFShortKey second)
        {
            // or !Equals(first, second), but we want to reuse the existing comparison 
            return !(first == second);
        }

    }
}
