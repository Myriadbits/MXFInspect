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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01015100")]
    public class MXFMPEGPictureEssenceDescriptor : MXFCDCIDescriptor
    {
        private const string CATEGORYNAME = "MPEGPictureEssenceDescriptor";

        private readonly UL bitRate_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x0b, 0x00, 0x00);
        private readonly UL identicalGOPIndicator_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x07, 0x00, 0x00);
        private readonly UL maxGOPSize_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x08, 0x00, 0x00);
        private readonly UL maxBPictureCount_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x09, 0x00, 0x00);
        private readonly UL constantBPictureFlag_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x03, 0x00, 0x00);
        private readonly UL codedContentScanningKind_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x04, 0x00, 0x00);
        private readonly UL profileAndLevel_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x0a, 0x00, 0x00);
        private readonly UL singleSequenceFlag_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x02, 0x00, 0x00);
        private readonly UL closedGOP_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x06, 0x00, 0x00);
        private readonly UL lowDelay_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x05, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01020000")]
        public bool? SingleSequenceFlag { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01050000")]
        public bool? LowDelayIndicator { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.010b0000")]
        public UInt32? BitRate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01070000")]
        public bool? IdenticalGOPIndicator { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01030000")]
        public bool? ConstantBPictureFlag { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01060000")]
        public bool? ClosedGOPIndicator { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01080000")]
        public UInt16? MaximumGOPSize { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01090000")]
        public UInt16? MaximumBPictureCount { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.010a0000")]
        public byte? ProfileAndLevel { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010602.01040000")]
        public MXFCodedContentScanning? CodedContentScanningKind { get; set; }


        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFMPEGPictureEssenceDescriptor(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "MPEG Picture Essence Descriptor";
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
                    case var _ when localTag.AliasUID == bitRate_Key: 
                        this.BitRate = reader.ReadUInt32();
                        localTag.Value = this.BitRate;
                        return true;
                    case var _ when localTag.AliasUID == identicalGOPIndicator_Key:
                        this.IdenticalGOPIndicator = reader.ReadBoolean();
                        localTag.Value = this.IdenticalGOPIndicator;
                        return true;
                    case var _ when localTag.AliasUID == maxGOPSize_Key:
                        this.MaximumGOPSize = reader.ReadUInt16();
                        localTag.Value = this.MaximumGOPSize;
                        return true;
                    case var _ when localTag.AliasUID == maxBPictureCount_Key: 
                        this.MaximumBPictureCount = reader.ReadUInt16();
                        localTag.Value = this.MaximumBPictureCount;
                        return true;
                    case var _ when localTag.AliasUID == constantBPictureFlag_Key: 
                        this.ConstantBPictureFlag = reader.ReadBoolean();
                        localTag.Value = this.ConstantBPictureFlag;
                        return true;
                    case var _ when localTag.AliasUID == codedContentScanningKind_Key: 
                        this.CodedContentScanningKind = (MXFCodedContentScanning)reader.ReadByte();
                        localTag.Value = this.CodedContentScanningKind;
                        return true;
                    case var _ when localTag.AliasUID == profileAndLevel_Key:
                        this.ProfileAndLevel = reader.ReadByte();
                        localTag.Value = this.ProfileAndLevel;
                        return true;
                    case var _ when localTag.AliasUID == singleSequenceFlag_Key: 
                        this.SingleSequenceFlag = reader.ReadBoolean();
                        localTag.Value = this.SingleSequenceFlag;
                        return true;
                    case var _ when localTag.AliasUID == closedGOP_Key:
                        this.ClosedGOPIndicator = reader.ReadBoolean();
                        localTag.Value = this.ClosedGOPIndicator;
                        return true;
                    case var _ when localTag.AliasUID == lowDelay_Key: 
                        this.LowDelayIndicator = reader.ReadBoolean();
                        localTag.Value = this.LowDelayIndicator;
                        return true;
                }
            }

            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
