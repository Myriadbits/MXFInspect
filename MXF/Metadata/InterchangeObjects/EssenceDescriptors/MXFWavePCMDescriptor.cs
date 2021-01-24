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
    public class MXFWAVEPCMDescriptor : MXFGenericSoundEssenceDescriptor
    {
        private const string CATEGORYNAME = "WAVE PCM Descriptor";

        [Category(CATEGORYNAME)]
        public UInt16? BlockAlign { get; set; }

        [Category(CATEGORYNAME)]
        public byte? SequenceOffset { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? AverageBytesPerSecond { get; set; }

        [Category(CATEGORYNAME)]
        public MXFKey ChannelAssignment { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? PeakEnvelopeVersion { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? PeakEnvelopeFormat { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? PointsPerPeakValue { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? PeakEnvelopeBlockSize { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? PeakChannels { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? PeakFrames { get; set; }

        [Category(CATEGORYNAME)]
        public MXFPositionType? PeakOfPeaksPosition { get; set; }

        [Category(CATEGORYNAME)]
        public DateTime? PeakEnvelopeTimestamp { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] PeakEnvelopeData { get; set; }


        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFWAVEPCMDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "WAVE PCM Descriptor")
        {
        }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFWAVEPCMDescriptor(MXFReader reader, MXFKLV headerKLV, string metadataName)
            : base(reader, headerKLV, metadataName)
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
                case 0x3D0A: this.BlockAlign = reader.ReadUInt16(); return true;
                case 0x3D0B: this.SequenceOffset = reader.ReadByte(); return true;
                case 0x3D09: this.AverageBytesPerSecond = reader.ReadUInt32(); return true;
                case 0x3D32: this.ChannelAssignment = reader.ReadULKey(); return true;
                case 0x3D29: this.PeakEnvelopeVersion = reader.ReadUInt32(); return true;
                case 0x3D2A: this.PeakEnvelopeFormat = reader.ReadUInt32(); return true;
                case 0x3D2B: this.PointsPerPeakValue = reader.ReadUInt32(); return true;
                case 0x3D2C: this.PeakEnvelopeBlockSize = reader.ReadUInt32(); return true;
                case 0x3D2D: this.PeakChannels = reader.ReadUInt32(); return true;
                case 0x3D2E: this.PeakFrames = reader.ReadUInt32(); return true;
                case 0x3D2F: this.PeakOfPeaksPosition = reader.ReadUInt64(); return true;
                case 0x3D30: this.PeakEnvelopeTimestamp = reader.ReadTimestamp(); return true;
                case 0x3D31: this.PeakEnvelopeData = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
