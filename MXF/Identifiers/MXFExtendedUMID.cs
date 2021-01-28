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
    public class MXFExtendedUMID : MXFIdentifier
    {
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] UL { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte UMIDLength { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] InstanceNumber { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] MaterialNumber { get; set; }
        
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] DateTime { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] Spatial { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] Country { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] Organisation { get; set; }

        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] User { get; set; }


        public MXFExtendedUMID(params byte[] list) : base(list)
        {
            if (this.Length == 64)
            {
                UL = new ReadOnlySpan<byte>(list, 0, 12).ToArray();
                UMIDLength = list[12];
                InstanceNumber = new ReadOnlySpan<byte>(list, 13, 3).ToArray();
                MaterialNumber = new ReadOnlySpan<byte>(list, 16, 16).ToArray();
                DateTime = new ReadOnlySpan<byte>(list, 32, 8).ToArray();
                Spatial = new ReadOnlySpan<byte>(list, 40, 12).ToArray();
                Country = new ReadOnlySpan<byte>(list, 52, 4).ToArray();
                Organisation = new ReadOnlySpan<byte>(list, 56, 4).ToArray();
                User = new ReadOnlySpan<byte>(list, 60, 4).ToArray();
            }
            else throw new ArgumentException("Number of bytes must be 64", "list");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var bytes = this.GetByteArray();
            sb.Append("Extended UMID - { ");
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
