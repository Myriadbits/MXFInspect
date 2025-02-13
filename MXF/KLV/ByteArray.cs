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
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
    public class ByteArray : IEquatable<ByteArray>, IArrayLength
    {
        private readonly ImmutableArray<byte> theBytes;

        [Browsable(false)]
        public int ArrayLength => theBytes.Length;

        public byte this[int index] => theBytes[index];

        public ByteArray(params byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes), "bytes cannot be null");
            }
            else if (bytes.Length == 0)
            {
                throw new ArgumentException("There must at least be one byte", nameof(bytes));
            }
            else
            {
                theBytes = ImmutableArray.Create(bytes);
            }
        }

        public bool HasSameBeginning(ByteArray other)
        {
            int smallerLength = Math.Min(this.ArrayLength, other.ArrayLength);

            for (int i = 0; i < smallerLength; i++)
            {
                if (this[i] != other[i])
                {
                    return false;
                }
            }
            return true;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int n = 0; n < this.ArrayLength; n++)
            {
                if (n > 0 && n < 16)
                {
                    sb.Append('.');
                }
                if (n >= 16)
                {
                    sb.Append("...");
                    break;
                }
                sb.Append(string.Format("{0:x2}", this[n]));
            }
            return sb.ToString();
        }


        #region Equals
        public bool Equals(ByteArray other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.theBytes.SequenceEqual(other.theBytes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not ByteArray) return false;
            return Equals((ByteArray)obj);
        }

        //see https://stackoverflow.com/a/468084 and https://stackoverflow.com/a/53316768
        public override int GetHashCode()
        {

            return GetHashCode(theBytes.Length);
        }

        public int GetHashCode(int arrayLength)
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < arrayLength; i++)
                    hash = (hash ^ theBytes[i]) * p;

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
        }

        public static bool operator ==(ByteArray x, ByteArray y) { return Equals(x, y); }
        public static bool operator !=(ByteArray x, ByteArray y) { return !Equals(x, y); }
        #endregion
    }

}
