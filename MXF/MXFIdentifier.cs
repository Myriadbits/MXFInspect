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
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
    public class MXFIdentifier : IEquatable<MXFIdentifier>
    {
        protected byte[] byteArray = null;

        [Browsable(false)]
        public int Length => byteArray.Length;

        public byte this[int key] => byteArray[key];


        /// <summary>
        /// Create a new identifier
        /// </summary>
        /// <param name="list"></param>
        public MXFIdentifier(params int[] list)
        {
            this.byteArray = new byte[list.Length];
            for (int n = 0; n < list.Length; n++)
            {
                byteArray[n] = (byte)list[n];
            }
        }

        /// <summary>
        /// Create a new identifier by reading from the current file location a given number of byter
        /// </summary>
        /// <param name="firstPart"></param>
        /// <param name="reader"></param>
        public MXFIdentifier(MXFReader reader, UInt32 length)
        {
            this.byteArray = reader.ReadArray(reader.ReadByte, (int)length); 
        }

        private MXFIdentifier() { }

        // TODO find a better name for this method
        public bool HasSameBeginning(MXFIdentifier id)
        {
            int len = Math.Min(this.byteArray.Length, id.byteArray.Length);
            if (len == 0)
            {
                return false;
            }
            else return this.byteArray.Take(len).SequenceEqual(id.byteArray.Take(len));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(string.Join("-", this.byteArray.Select(b => string.Format("{0:X2}", b))));
            sb.Append("}");
            return sb.ToString();
        }


        #region Equals
        public bool Equals(MXFIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.byteArray.SequenceEqual(other.byteArray);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MXFIdentifier)) return false;
            return Equals((MXFIdentifier)obj);
        }

        public override int GetHashCode() { return this.byteArray.GetHashCode(); }
        public static bool operator ==(MXFIdentifier x, MXFIdentifier y) { return Equals(x, y); }
        public static bool operator !=(MXFIdentifier x, MXFIdentifier y) { return !Equals(x, y); }
        #endregion
    }
}
