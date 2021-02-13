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

using Myriadbits.MXF.Utils;
using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012800")]
    public class MXFCDCIPictureEssenceDescriptor : MXFGenericPictureEssenceDescriptor
    {
        private const string CATEGORYNAME = "CDCIPictureEssenceDescriptor";
        private const int CATEGORYPOS = 4;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010503.0a000000")]
        public UInt32? ComponentDepth { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.05000000")]
        public UInt32? HorizontalSubsampling { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010501.10000000")]
        public UInt32? VerticalSubsampling { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.06000000")]
        public MXFColorSiting? ColorSiting { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.03010201.0a000000")]
        public bool? ReversedByteOrder { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04180104.00000000")]
        public Int16? PaddingBits { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010503.07000000")]
        public UInt32? AlphaSampleDepth { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010503.03000000")]
        public UInt32? BlackRefLevel { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010503.04000000")]
        public UInt32? WhiteRefLevel { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010503.05000000")]
        public UInt32? ColorRange { get; set; }

        public MXFCDCIPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "CDCI Picture Essence Descriptor")
        {
        }

        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x3301: this.ComponentDepth = reader.ReadUInt32(); return true;
                case 0x3302: this.HorizontalSubsampling = reader.ReadUInt32(); return true;
                case 0x3308: this.VerticalSubsampling = reader.ReadUInt32(); return true;
                case 0x3303: this.ColorSiting = (MXFColorSiting)reader.ReadByte(); return true;
                case 0x330B: this.ReversedByteOrder = reader.ReadBool(); return true;
                case 0x3307: this.PaddingBits = (Int16)reader.ReadUInt16(); return true;
                case 0x3309: this.AlphaSampleDepth = reader.ReadUInt32(); return true;
                case 0x3304: this.BlackRefLevel = reader.ReadUInt32(); return true;
                case 0x3305: this.WhiteRefLevel = reader.ReadUInt32(); return true;
                case 0x3306: this.ColorRange = reader.ReadUInt32(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
