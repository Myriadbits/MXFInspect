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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011400")]
    public class MXFTimecodeComponent : MXFSegment
    {
        private const string CATEGORYNAME = "TimecodeComponent";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020103.01050000")]
        public MXFPosition? StartTimecode { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04040101.02060000")]
        public UInt16? FramesPerSecond { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04040101.05000000")]
        public bool? DropFrame { get; set; }

        public MXFTimecodeComponent(MXFPack pack)
            : base(pack, "TimeCodeComponent")
        {
        }

        public MXFTimecodeComponent(MXFPack pack, string metadataName)
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
                case 0x1501:
                    this.StartTimecode = reader.ReadUInt64();
                    localTag.Value = this.StartTimecode;
                    return true;
                case 0x1502:
                    this.FramesPerSecond = reader.ReadUInt16();
                    localTag.Value = this.FramesPerSecond;
                    return true;
                case 0x1503:
                    this.DropFrame = reader.ReadBoolean();
                    localTag.Value = this.DropFrame;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
