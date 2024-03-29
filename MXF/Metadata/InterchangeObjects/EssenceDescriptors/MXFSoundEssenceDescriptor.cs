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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01014200")]
    public class MXFSoundEssenceDescriptor : MXFFileDescriptor
    {
        private const string CATEGORYNAME = "SoundEssenceDescriptor";

        private readonly UL refImageEditRate_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x02, 0x01, 0x01, 0x06, 0x00, 0x00, 0x00);
        private readonly UL refAudioAlignmentLevel = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x02, 0x01, 0x01, 0x07, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04020301.01010000")]
        public MXFRational AudioSamplingRate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.04020301.04000000")]
        public bool? Locked { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04020101.03000000")]
        public sbyte? AudioRefLevel { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04020101.01000000")]
        public MXFElectroSpatialFormulation? ElectroSpatialFormulation { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04020101.04000000")]
        public UInt32? ChannelCount { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.04020303.04000000")]
        public UInt32? QuantizationBits { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04020701.00000000")]
        public sbyte? DialNorm { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04020402.00000000")]
        public UL SoundEssenceCoding { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04020101.06000000")]
        public MXFRational ReferenceImageEditRate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04020101.07000000")]
        public byte? ReferenceAudioAlignmentLevel { get; set; }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFSoundEssenceDescriptor(MXFPack pack)
            : base(pack, "Sound Essence Descriptor")
        {
        }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFSoundEssenceDescriptor(MXFPack pack, string metadataName)
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
                case 0x3D03:
                    this.AudioSamplingRate = reader.ReadRational();
                    localTag.Value = this.AudioSamplingRate;
                    return true;
                case 0x3D02:
                    this.Locked = reader.ReadBoolean();
                    localTag.Value = this.Locked;
                    return true;
                case 0x3D04:
                    this.AudioRefLevel = reader.ReadSByte();
                    localTag.Value = this.AudioRefLevel;
                    return true;
                case 0x3D05:
                    this.ElectroSpatialFormulation = (MXFElectroSpatialFormulation)reader.ReadByte();
                    localTag.Value = this.ElectroSpatialFormulation;
                    return true;
                case 0x3D07:
                    this.ChannelCount = reader.ReadUInt32();
                    localTag.Value = this.ChannelCount;
                    return true;
                case 0x3D01:
                    this.QuantizationBits = reader.ReadUInt32();
                    localTag.Value = this.QuantizationBits;
                    return true;
                case 0x3D0C:
                    this.DialNorm = reader.ReadSByte();
                    localTag.Value = this.DialNorm;
                    return true;
                case 0x3D06:
                    this.SoundEssenceCoding = reader.ReadUL();
                    localTag.Value = this.SoundEssenceCoding;
                    return true;
                case var _ when localTag.AliasUID == refImageEditRate_Key:
                    this.ReferenceImageEditRate = reader.ReadRational();
                    localTag.Value = this.ReferenceImageEditRate;
                    return true;
                case var _ when localTag.AliasUID == refAudioAlignmentLevel:
                    this.ReferenceAudioAlignmentLevel = reader.ReadByte();
                    localTag.Value = this.ReferenceAudioAlignmentLevel;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
