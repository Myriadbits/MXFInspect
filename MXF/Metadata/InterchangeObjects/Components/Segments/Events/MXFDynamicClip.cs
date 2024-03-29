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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01015800")]
    public class MXFDynamicClip : MXFDynamicMarker
    {
        private const string CATEGORYNAME = "DynamicClip";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.06010103.09000000")]
        public UMID DynamicSourcePackageID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.06010103.0a000000")]
        [TypeConverter(typeof(UInt32ArrayConverter))]
        public UInt32[] DynamicSourceTrackIDs { get; set; }

        //TODO this is of type "indirect"
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.06010103.0b000000")]
        public object SourceIndex { get; set; }

        //TODO this is of type "indirect"
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.06010103.0c000000")]
        public object SourceSpecies { get; set; }

        public MXFDynamicClip(MXFPack pack)
        : base(pack)
        {
            this.MetaDataName = "DynamicClip";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x5801: 
                    this.DynamicSourcePackageID = reader.ReadUMIDKey(); 
                    localTag.Value = this.DynamicSourcePackageID;
                    return true;
                case 0x5802: 
                    this.DynamicSourceTrackIDs = reader.ReadArray(reader.ReadUInt32, localTag.Length.Value);
                    localTag.Value = this.DynamicSourceTrackIDs;
                    return true;
                case 0x5803: 
                    this.SourceIndex = reader.ReadBytes((int)localTag.Length.Value); 
                    localTag.Value = this.SourceIndex;
                    return true;
                case 0x5804: this.SourceSpecies = reader.ReadBytes((int)localTag.Length.Value);
                    localTag.Value = this.SourceSpecies;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }
    }
}
