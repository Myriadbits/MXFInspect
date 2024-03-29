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
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01014800")]
    public class MXFWAVEPCMDescriptor : MXFSoundEssenceDescriptor
    {
        private const string CATEGORYNAME = "WAVE PCM Descriptor";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04020302.01000000")]
        public UInt16? BlockAlign { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04020302.02000000")]
        public byte? SequenceOffset { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04020303.05000000")]
        public UInt32? AverageBytesPerSecond { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010107.04020101.05000000")]
        public UL ChannelAssignment { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.06000000")]
        public UInt32? PeakEnvelopeVersion { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.07000000")]
        public UInt32? PeakEnvelopeFormat { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.08000000")]
        public UInt32? PointsPerPeakValue { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.09000000")]
        public UInt32? PeakEnvelopeBlockSize { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.0a000000")]
        public UInt32? PeakChannels { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.0b000000")]
        public UInt32? PeakFrames { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.0c000000")]
        public MXFPosition? PeakOfPeaksPosition { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(DateTimeTypeConverter))]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.0d000000")]
        public DateTime? PeakEnvelopeTimestamp { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        [ULElement("urn:smpte:ul:060e2b34.01010108.04020301.0e000000")]
        public byte[] PeakEnvelopeData { get; set; }


        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFWAVEPCMDescriptor(MXFPack pack)
            : base(pack, "WAVE PCM Descriptor")
        {
        }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFWAVEPCMDescriptor(MXFPack pack, string metadataName)
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
                case 0x3D0A: 
                    this.BlockAlign = reader.ReadUInt16();
                    localTag.Value = this.BlockAlign; 
                    return true;
                case 0x3D0B: 
                    this.SequenceOffset = reader.ReadByte(); 
                    localTag.Value = this.SequenceOffset;
                    return true;
                case 0x3D09: 
                    this.AverageBytesPerSecond = reader.ReadUInt32(); 
                    localTag.Value = this.AverageBytesPerSecond;
                    return true;
                case 0x3D32: 
                    this.ChannelAssignment = reader.ReadUL(); 
                    localTag.Value = this.ChannelAssignment;
                    return true;
                case 0x3D29: 
                    this.PeakEnvelopeVersion = reader.ReadUInt32(); 
                    localTag.Value = this.PeakEnvelopeVersion;
                    return true;
                case 0x3D2A: 
                    this.PeakEnvelopeFormat = reader.ReadUInt32(); 
                    localTag.Value = this.PeakEnvelopeFormat;
                    return true;
                case 0x3D2B: 
                    this.PointsPerPeakValue = reader.ReadUInt32();
                    localTag.Value = this.PointsPerPeakValue;
                    return true;
                case 0x3D2C: 
                    this.PeakEnvelopeBlockSize = reader.ReadUInt32(); 
                    localTag.Value = this.PeakEnvelopeBlockSize;
                    return true;
                case 0x3D2D: 
                    this.PeakChannels = reader.ReadUInt32();
                    localTag.Value = this.PeakChannels;
                    return true;
                case 0x3D2E: 
                    this.PeakFrames = reader.ReadUInt32(); 
                    localTag.Value = this.PeakFrames;
                    return true;
                case 0x3D2F: 
                    this.PeakOfPeaksPosition = reader.ReadUInt64();
                    localTag.Value = this.PeakOfPeaksPosition;
                    return true;
                case 0x3D30: 
                    this.PeakEnvelopeTimestamp = reader.ReadTimeStamp(); 
                    localTag.Value = this.PeakEnvelopeTimestamp;
                    return true;
                case 0x3D31: 
                    this.PeakEnvelopeData = reader.ReadBytes((int)localTag.Length.Value); 
                    localTag.Value = this.PeakEnvelopeData;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
