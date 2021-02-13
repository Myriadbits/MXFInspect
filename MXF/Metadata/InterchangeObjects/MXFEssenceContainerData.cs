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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012300")]
    public class MXFEssenceContainerData : MXFInterchangeObject
    {
        private const string CATEGORYNAME = "EssenceContainerData";

        private readonly MXFKey precedingIndexTable_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);
        private readonly MXFKey followingIndexTable_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);
        private readonly MXFKey isSparse_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);
        private readonly MXFKey singularPartitionUsage_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010106.01000000")]
        public MXFUMID LinkedPackageID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.01030405.00000000")]
        public UInt32? IndexSID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.01030404.00000000")]
        public UInt32? EssenceSID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04040504.00000000")]
        public bool? PrecedingIndexTable { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04040505.00000000")]
        public bool? FollowingIndexTable { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04040506.00000000")]
        public bool? IsSparse { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04060207.00000000")]
        public bool? SingularPartitionUsage { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04070200.00000000")]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] EssenceStream { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010102.01000000")]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] SampleIndex { get; set; }


        public MXFEssenceContainerData(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "EssenceContainerData")
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x2701: this.LinkedPackageID = reader.ReadUMIDKey(); return true;
                case 0x3F06: this.IndexSID = reader.ReadUInt32(); return true;
                case 0x3F07: this.EssenceSID = reader.ReadUInt32(); return true;
                case 0x2702: this.EssenceStream = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
                case 0x2B01: this.SampleIndex = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
                case var _ when localTag.Key == precedingIndexTable_Key: this.PrecedingIndexTable = reader.ReadBool(); return true;
                case var _ when localTag.Key == followingIndexTable_Key: this.FollowingIndexTable = reader.ReadBool(); return true;
                case var _ when localTag.Key == isSparse_Key: this.IsSparse = reader.ReadBool(); return true;
                case var _ when localTag.Key == singularPartitionUsage_Key: this.SingularPartitionUsage = reader.ReadBool(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
