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

using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01014100")]
    public class MXFDescriptiveMarker : MXFCommentMarker
    {
        private const string CATEGORYNAME = "DescriptiveMarker";

        public readonly UL metadataScheme_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x06, 0x08, 0x04, 0x00, 0x00, 0x00, 0x00);
        public readonly UL metadataPlugInID_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x05, 0x20, 0x07, 0x01, 0x0e, 0x00, 0x00, 0x00);
        public readonly UL metadataApplicationEnvironmentID_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x05, 0x20, 0x07, 0x01, 0x10, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.01070105.00000000")]
        [TypeConverter(typeof(UInt32ArrayConverter))]
        public UInt32[] DescribedTrackIDs { get; set; }

        // TODO should this be UUID or AUID or UL?
        [CategoryAttribute(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04060804.00000000")]
        public AUID DescriptiveMetadataScheme { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.05200701.0e000000")]
        public UUID DescriptiveMetadataPlugInID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.05200701.10000000")]
        public string DescriptiveMetadataApplicationEnvironmentID { get; set; }

        public MXFDescriptiveMarker(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "Descriptive Marker";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x6102:
                    this.DescribedTrackIDs = reader.ReadArray(reader.ReadUInt32, localTag.Length.Value / sizeof(UInt32));
                    localTag.Value = this.DescribedTrackIDs;
                    return true;
                case 0x6101: 
                    localTag.AddChild(reader.ReadReference<MXFDescriptiveFramework>("DescriptiveFrameworkObject", localTag.Offset)); 
                    return true;
                case var _ when localTag.AliasUID == metadataScheme_Key:
                    this.DescriptiveMetadataScheme = reader.ReadAUID();
                    localTag.Value = this.DescriptiveMetadataScheme;
                    return true;
                case var _ when localTag.AliasUID == metadataPlugInID_Key: 
                    this.DescriptiveMetadataPlugInID = reader.ReadUUID();
                    localTag.Value = this.DescriptiveMetadataPlugInID;
                    return true;
                case var _ when localTag.AliasUID == metadataApplicationEnvironmentID_Key: 
                    this.DescriptiveMetadataApplicationEnvironmentID = reader.ReadUTF16String(localTag.Length.Value);
                    localTag.Value = this.DescriptiveMetadataApplicationEnvironmentID;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }
    }
}
