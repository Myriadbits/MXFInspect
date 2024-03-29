﻿#region license
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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011000")]
    public class MXFSourceReference : MXFSegment
    {
        private const string CATEGORYNAME = "SourceReference";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010103.01000000")]
        public UMID SourcePackageID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010103.02000000")]
        public UInt32? SourceTrackId { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010107.06010103.07000000")]
        [TypeConverter(typeof(UInt32ArrayConverter))]
        public UInt32[] ChannelIDs { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.06010103.08000000")]
        [TypeConverter(typeof(UInt32ArrayConverter))]
        public UInt32[] MonoSourceTrackIDs { get; set; }

        public MXFSourceReference(MXFPack pack)
            : base(pack, "SourceReference")
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x1101:
                    this.SourcePackageID = reader.ReadUMIDKey(); 
                    localTag.Value = this.SourcePackageID; 
                    return true;
                case 0x1102:
                    this.SourceTrackId = reader.ReadUInt32();
                    localTag.Value = this.SourceTrackId;
                    return true;
                case 0x1103:
                    this.ChannelIDs = reader.ReadArray(reader.ReadUInt32, localTag.Length.Value);
                    localTag.Value = this.ChannelIDs;
                    return true;
                case 0x1104:
                    this.MonoSourceTrackIDs = reader.ReadArray(reader.ReadUInt32, localTag.Length.Value);
                    localTag.Value = this.MonoSourceTrackIDs;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
