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
using System.ComponentModel;

namespace Myriadbits.MXF
{
    public class MXFEssenceElement : MXFKLV
    {
        private const string CATEGORYNAME = "EssenceElement";

        private static Dictionary<int, string> m_itemTypes = new Dictionary<int, string>()
        {
            {0x05, "CP Picture (SMPTE 326M)"},
            {0x06, "CP Sound (SMPTE 326M)"},
            {0x07, "CP Data (SMPTE 326M)"},
            {0x15, "GC Picture"},
            {0x16, "GC Sound"},
            {0x17, "GC Data"},
            {0x18, "GC Compound"}
        };

        [Category(CATEGORYNAME)]
        public string ItemType { get; set; }
        [Category(CATEGORYNAME)]
        public byte ElementCount { get; set; }
        [Category(CATEGORYNAME)]
        public byte ElementType { get; set; }
        [Category(CATEGORYNAME)]
        public byte ElementNumber { get; set; }
        [Browsable(false)]
        public bool IsPicture { get; set; }

        [Category(CATEGORYNAME)]
        public long EssenceOffset
        {
            get
            {
                if (this.Partition == null) return this.Offset; // Unknown
                if (this.Partition.FirstPictureEssenceElement == null) return this.Offset; // Unknown
                return (this.Offset - this.Partition.FirstPictureEssenceElement.Offset) + ((long)this.Partition.BodyOffset);
            }
        }

        [Browsable(false)]
        public bool Indexed { get; set; }

        public MXFEssenceElement(MXFReader reader, MXFKLV headerKLV)
            : base(headerKLV, "EssenceElement", KeyType.Essence)
        {
            this.m_eType = MXFObjectType.Essence;
            if (m_itemTypes.ContainsKey(this.Key[12]))
                this.ItemType = m_itemTypes[this.Key[12]];
            else
                this.ItemType = "<unknown>";
            this.IsPicture = (this.Key[12] == 0x05 || this.Key[12] == 0x15);
            this.ElementCount = this.Key[13];
            this.ElementType = this.Key[14];
            this.ElementNumber = this.Key[15];
        }

        public override string ToString()
        {
            return string.Format("{0} Essence [len {1}]", this.ItemType, this.Length);
        }
    }
}
