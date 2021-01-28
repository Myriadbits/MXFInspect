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

namespace Myriadbits.MXF
{
    public class MXFDescriptiveMarker : MXFCommentMarker
    {
        public readonly MXFKey metadataScheme_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x06, 0x08, 0x04, 0x00, 0x00, 0x00, 0x00);
        public readonly MXFKey metadataPlugInID_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x05, 0x20, 0x07, 0x01, 0x0e, 0x00, 0x00, 0x00);
        public readonly MXFKey metadataApplicationEnvironmentID_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x05, 0x20, 0x07, 0x01, 0x10, 0x00, 0x00, 0x00);

        [CategoryAttribute("DescriptiveMarker"), Description("6102")]
        public UInt32[] DescribedTrackIDs { get; set; }
        [CategoryAttribute("DescriptiveMarker"), Description("")]

        // TODO should this be UUID or AUID or UL?
        public MXFKey DescriptiveMetadataScheme { get; set; }
        [CategoryAttribute("DescriptiveMarker"), Description("")]
        public MXFUUID DescriptiveMetadataPlugInID { get; set; }
        [CategoryAttribute("DescriptiveMarker"), Description("")]
        public string DescriptiveMetadataApplicationEnvironmentID { get; set; }

        public MXFDescriptiveMarker(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "Descriptive Marker";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x6102:
                    this.DescribedTrackIDs = reader.ReadArray(reader.ReadUInt32, localTag.Size / sizeof(UInt32));
                    return true;
                case 0x6101: ReadReference<MXFDescriptiveFramework>(reader, "DescriptiveFrameworkObject"); return true;
                case var a when localTag.Key == metadataScheme_Key: 
                    this.DescriptiveMetadataScheme = reader.ReadULKey(); 
                    return true;
                case var a when localTag.Key == metadataPlugInID_Key: this.DescriptiveMetadataPlugInID = reader.ReadUUIDKey(); return true;
                case var a when localTag.Key == metadataApplicationEnvironmentID_Key: 
                    this.DescriptiveMetadataApplicationEnvironmentID = reader.ReadUTF16String(localTag.Size); 
                    return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
