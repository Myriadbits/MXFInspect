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
using System.Text;

namespace Myriadbits.MXF
{
    public class KLVKey : ByteArray
    {
        public enum KeyLengths
        {

            OneByte = 1,
            TwoBytes = 2,
            FourBytes = 4,
            SixteenBytes = 16,
        }

        public KeyLengths KeyLength { get; }

        public KLVKey(KeyLengths keyLength, params byte[] bytes) : base(bytes)
        {
            // check if passed array length is contained in defined key lengths
            if ((int)keyLength != bytes.Length)
            {
                throw new ArgumentException($"Key length of {bytes.Length} is not a valid key length");
            }
            KeyLength = keyLength;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            int separatorDistance = KeyLength == KeyLengths.SixteenBytes ? 4 : 2;
            for (int n = 0; n < this.ArrayLength; n++)
            {
                if (n % separatorDistance == 0 && n > 0)
                {
                    sb.Append('.');
                }

                sb.Append(string.Format("{0:x2}", this[n]));
            }
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
