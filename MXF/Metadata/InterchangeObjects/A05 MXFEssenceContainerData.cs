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
    public class MXFEssenceContainerData : MXFInterchangeObject
    {
        private readonly MXFKey precedingIndexTable_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);
        private readonly MXFKey followingIndexTable_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);
        private readonly MXFKey isSparse_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);
        private readonly MXFKey singularPartitionUsage_Key = new MXFKey(0x06, 0x0E, 0x2B, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x04, 0x06, 0x10, 0x00, 0x00);

        [CategoryAttribute("EssenceContainerData"), Description("2701")]
        public MXFUMID LinkedPackageID { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("3F06")]
        public UInt32? IndexSID { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("3F07")]
        public UInt32? BodySID { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("")]
        public bool? PrecedingIndexTable { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("")]
        public bool? FollowingIndexTable { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("")]
        public bool? IsSparse { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("")]
        public bool? SingularPartitionUsage { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("")]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] EssenceStream { get; set; }
        [CategoryAttribute("EssenceContainerData"), Description("")]
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
                case 0x3F07: this.BodySID = reader.ReadUInt32(); return true;
                case 0x2702: this.EssenceStream = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
                case 0x2B01: this.SampleIndex = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
                case var a when localTag.Key == precedingIndexTable_Key: this.PrecedingIndexTable = reader.ReadBool(); return true;
                case var a when localTag.Key == followingIndexTable_Key: this.FollowingIndexTable = reader.ReadBool(); return true;
                case var a when localTag.Key == isSparse_Key: this.IsSparse = reader.ReadBool(); return true;
                case var a when localTag.Key == singularPartitionUsage_Key: this.SingularPartitionUsage = reader.ReadBool(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
