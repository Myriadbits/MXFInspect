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


        public MXFAVCSubDescriptor(MXFPack pack)
            : base(pack, "AVCSubDescriptor")
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            if (localTag.AliasUID != null)
            {
                switch (localTag.AliasUID)
                {
                    case var _ when localTag.AliasUID == constantBPictureFlag_Key: 
                        this.AVCConstantBPictureFlag = reader.ReadBoolean();
                        localTag.Value = this.AVCConstantBPictureFlag; 
                        return true;
                    case var _ when localTag.AliasUID == codedContentKind_Key:
                        this.AVCCodedContentKind = (MXFAVCContentScanning)reader.ReadByte(); 
                        localTag.Value = this.AVCCodedContentKind;
                        return true;
                    case var _ when localTag.AliasUID == closedGOPIndicator_Key:
                        this.AVCClosedGOPIndicator = reader.ReadBoolean(); 
                        localTag.Value = this.AVCClosedGOPIndicator;
                        return true;
                    case var _ when localTag.AliasUID == identicalGOPIndicator_Key:
                        this.AVCIdenticalGOPIndicator = reader.ReadBoolean(); 
                        localTag.Value = this.AVCIdenticalGOPIndicator;
                        return true;
                    case var _ when localTag.AliasUID == maximumGOPSize_Key: 
                        this.AVCMaximumGOPSize = reader.ReadUInt16(); 
                        localTag.Value = this.AVCMaximumGOPSize;
                        return true;
                    case var _ when localTag.AliasUID == maximumBPictureCount_Key:
                        this.AVCMaximumBPictureCount = reader.ReadUInt16(); 
                        localTag.Value = this.AVCMaximumBPictureCount;
                        return true;
                    case var _ when localTag.AliasUID == profile_Key:
                        this.AVCProfile = reader.ReadByte(); 
                        localTag.Value = this.AVCProfile;
                        return true;
                    case var _ when localTag.AliasUID == maximumBitRate_Key: 
                        this.AVCMaximumBitRate = reader.ReadUInt32(); 
                        localTag.Value = this.AVCMaximumBitRate;
                        return true;
                    case var _ when localTag.AliasUID == profileConstraint_Key:
                        this.AVCProfileConstraint = reader.ReadByte();
                        localTag.Value = this.AVCProfileConstraint;
                        return true;
                    case var _ when localTag.AliasUID == level_Key: 
                        this.AVCLevel = reader.ReadByte(); 
                        localTag.Value = this.AVCLevel;
                        return true;
                    case var _ when localTag.AliasUID == decodingDelay_Key: 
                        this.AVCDecodingDelay = reader.ReadByte(); 
                        localTag.Value = this.AVCDecodingDelay;
                        return true;
                    case var _ when localTag.AliasUID == maximumRefFrames_Key: 
                        this.AVCMaximumRefFrames = reader.ReadByte(); 
                        localTag.Value = this.AVCMaximumRefFrames;
                        return true;
                    case var _ when localTag.AliasUID == sequenceParameterSetFlag_Key:
                        this.AVCSequenceParameterSetFlag = reader.ReadByte();
                        localTag.Value = this.AVCSequenceParameterSetFlag;
                        return true;
                    case var _ when localTag.AliasUID == pictureParameterSetFlag_Key:
                        this.AVCPictureParameterSetFlag = reader.ReadByte(); 
                        localTag.Value = this.AVCPictureParameterSetFlag;
                        return true;
                    case var _ when localTag.AliasUID == averageBitRate_Key:
                        this.AVCAverageBitRate = reader.ReadByte(); 
                        localTag.Value = this.AVCAverageBitRate;
                        return true;
                }
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
