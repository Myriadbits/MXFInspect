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
using System.Text;


namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFUMID : MXFIdentifier
    {
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] UL { get; set; }
        public byte UMIDLength { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] InstanceNumber { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] MaterialNumber { get; set; }

        public MXFUMID(params byte[] list) : base(list)
        {
            if (this.Length == 32)
            {
                UL = new ReadOnlySpan<byte>(list, 0, 12).ToArray();
                UMIDLength = list[12];
                InstanceNumber = new ReadOnlySpan<byte>(list, 13, 3).ToArray();
                MaterialNumber = new ReadOnlySpan<byte>(list, 16, 16).ToArray();
            }
            else throw new ArgumentException("Number of bytes must be 32", "list");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var bytes = this.GetByteArray();
            sb.Append("UMID - { ");
            for (int n = 0; n < this.Length; n++)
            {
                if (n > 0)
                {
                    sb.Append(".");
                }

                sb.Append(string.Format("{0:X2}", bytes[n]));
            }
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
