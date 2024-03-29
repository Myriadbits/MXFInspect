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
using Myriadbits.MXF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012500")]
    public class MXFFileDescriptor : MXFEssenceDescriptor
    {
        private const string CATEGORYNAME = "FileDescriptor";
        private const int CATEGORYPOS = 3;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.06010103.05000000")]
        public UInt32? LinkedTrackId { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04060101.00000000")]
        public MXFRational? SampleRate { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04060102.00000000")]
        public MXFLength? ContainerDuration { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010104.01020000")]
        public UL EssenceContainer { get; set; }

        /// <summary>
        /// Constructor when used as base class
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFFileDescriptor(MXFPack pack, string metadataName)
            : base(pack, metadataName)
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
                case 0x3006: 
                    this.LinkedTrackId = reader.ReadUInt32();
                    localTag.Value = this.LinkedTrackId;
                    return true;
                case 0x3001: 
                    this.SampleRate = reader.ReadRational();
                    localTag.Value = this.SampleRate;
                    return true;
                case 0x3002: 
                    this.ContainerDuration = reader.ReadUInt64();
                    localTag.Value = this.ContainerDuration;
                    return true;
                case 0x3004: 
                    this.EssenceContainer = reader.ReadUL();
                    localTag.Value = this.EssenceContainer;
                    return true;
                case 0x3005:
                    localTag.AddChild(reader.ReadReference<MXFCodecDefinition>("Codec", localTag.Offset));
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
