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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01016e00")]
    public class MXFAVCSubDescriptor : MXFSubDescriptor
    {
        private const string CATEGORYNAME = "AVCSubDescriptor";

        private readonly UL constantBPictureFlag_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x03, 0x00, 0x00);
        private readonly UL codedContentKind_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x04, 0x00, 0x00);
        private readonly UL closedGOPIndicator_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x06, 0x00, 0x00);
        private readonly UL identicalGOPIndicator_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x07, 0x00, 0x00);
        private readonly UL maximumGOPSize_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x08, 0x00, 0x00);
        private readonly UL maximumBPictureCount_Key = new UL(06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x09, 0x00, 0x00);
        private readonly UL profile_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x1, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x0a, 0x00, 0x00);
        private readonly UL maximumBitRate_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x0b, 0x00, 0x00);
        private readonly UL profileConstraint_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x0c, 0x00, 0x00);
        private readonly UL level_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x0d, 0x00, 0x00);
        private readonly UL decodingDelay_Key = new UL(0x06, 0x0E, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0E, 0x04, 0x01, 0x06, 0x06, 0x01, 0x0e, 0x00, 0x00);
        private readonly UL maximumRefFrames_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x0f, 0x00, 0x00);
        private readonly UL sequenceParameterSetFlag_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x10, 0x00, 0x00);
        private readonly UL pictureParameterSetFlag_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x11, 0x00, 0x00);
        private readonly UL averageBitRate_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x06, 0x01, 0x14, 0x00, 0x00);


        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01030000")]
        public bool? AVCConstantBPictureFlag { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01040000")]
        public MXFAVCContentScanning? AVCCodedContentKind { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01060000")]
        public bool? AVCClosedGOPIndicator { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01070000")]
        public bool? AVCIdenticalGOPIndicator { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01080000")]
        public UInt16? AVCMaximumGOPSize { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01090000")]
        public UInt16? AVCMaximumBPictureCount { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.010a0000")]
        public byte? AVCProfile { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.010b0000")]
        public UInt32? AVCMaximumBitRate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.010c0000")]
        public byte? AVCProfileConstraint { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.010d0000")]
        public byte? AVCLevel { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.010e0000")]
        public byte? AVCDecodingDelay { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.010f0000")]
        public byte? AVCMaximumRefFrames { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01100000")]
        public byte? AVCSequenceParameterSetFlag { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01110000")]
        public byte? AVCPictureParameterSetFlag { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010606.01140000")]
        public UInt32? AVCAverageBitRate { get; set; }


        public MXFAVCSubDescriptor(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "AVCSubDescriptor")
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            if (localTag.Key != null)
            {
                switch (localTag.Key)
                {
                    case var _ when localTag.Key == constantBPictureFlag_Key: this.AVCConstantBPictureFlag = reader.ReadBoolean(); return true;
                    case var _ when localTag.Key == codedContentKind_Key: this.AVCCodedContentKind = (MXFAVCContentScanning)reader.ReadByte(); return true;
                    case var _ when localTag.Key == closedGOPIndicator_Key: this.AVCClosedGOPIndicator = reader.ReadBoolean(); return true;
                    case var _ when localTag.Key == identicalGOPIndicator_Key: this.AVCIdenticalGOPIndicator = reader.ReadBoolean(); return true;
                    case var _ when localTag.Key == maximumGOPSize_Key: this.AVCMaximumGOPSize = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == maximumBPictureCount_Key: this.AVCMaximumBPictureCount = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == profile_Key: this.AVCProfile = reader.ReadByte(); return true;
                    case var _ when localTag.Key == maximumBitRate_Key: this.AVCMaximumBitRate = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == profileConstraint_Key: this.AVCProfileConstraint = reader.ReadByte(); return true;
                    case var _ when localTag.Key == level_Key: this.AVCLevel = reader.ReadByte(); return true;
                    case var _ when localTag.Key == decodingDelay_Key: this.AVCDecodingDelay = reader.ReadByte(); return true;
                    case var _ when localTag.Key == maximumRefFrames_Key: this.AVCMaximumRefFrames = reader.ReadByte(); return true;
                    case var _ when localTag.Key == sequenceParameterSetFlag_Key: this.AVCSequenceParameterSetFlag = reader.ReadByte(); return true;
                    case var _ when localTag.Key == pictureParameterSetFlag_Key: this.AVCPictureParameterSetFlag = reader.ReadByte(); return true;
                    case var _ when localTag.Key == averageBitRate_Key: this.AVCAverageBitRate = reader.ReadByte(); return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
