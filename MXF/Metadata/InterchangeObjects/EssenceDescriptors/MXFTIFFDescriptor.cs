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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012b00")]
    public class MXFTIFFDescriptor : MXFFileDescriptor
    {
        private const string CATEGORYNAME = "TIFFDescriptor";

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03030302.03000000")]
        public byte[] TIFFSummary { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010302.03000000")]
        public Int32? LeadingLines { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010302.04000000")]
        public Int32? TrailingLines { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05020103.01010000")]
        public bool IsUniform { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05020103.01020000")]
        public Int32? JPEGTableID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.06080201.00000000")]
        public bool IsContiguous { get; set; }

        public MXFTIFFDescriptor(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "TIFFDescriptor")
        {
        }

        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            if (localTag.AliasUID != null)
            {
                switch (localTag.TagValue)
                {
                    case 0x3706: this.TIFFSummary = reader.ReadBytes((int)localTag.Length.Value); return true;
                    case 0x3703: this.LeadingLines = reader.ReadInt32(); return true;
                    case 0x3704: this.TrailingLines = reader.ReadInt32(); return true;
                    case 0x3701: this.IsUniform = reader.ReadBoolean(); return true;
                    case 0x3705: this.JPEGTableID = reader.ReadInt32(); return true;
                    case 0x3702: this.IsContiguous = reader.ReadBoolean(); return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
