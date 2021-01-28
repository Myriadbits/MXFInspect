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

namespace Myriadbits.MXF
{
    public class MXFTIFFDescriptor : MXFFileDescriptor
    {
        private const string CATEGORYNAME = "TIFFDescriptor";

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] TIFFSummary { get; set; }

        [Category(CATEGORYNAME)]
        public Int32? LeadingLines { get; set; }

        [Category(CATEGORYNAME)]
        public Int32? TrailingLines { get; set; }

        [Category(CATEGORYNAME)]
        public bool IsUniform { get; set; }

        [Category(CATEGORYNAME)]
        public Int32? JPEGTableID { get; set; }

        [Category(CATEGORYNAME)]
        public bool IsContiguous { get; set; }

        public MXFTIFFDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "TIFFDescriptor")
        {
        }

        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            if (localTag.Key != null)
            {
                switch (localTag.Tag)
                {
                    case 0x3706: this.TIFFSummary = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
                    case 0x3703: this.LeadingLines = reader.ReadInt32(); return true;
                    case 0x3704: this.TrailingLines = reader.ReadInt32(); return true;
                    case 0x3701: this.IsUniform = reader.ReadBool(); return true;
                    case 0x3705: this.JPEGTableID = reader.ReadInt32(); return true;
                    case 0x3702: this.IsContiguous = reader.ReadBool(); return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
