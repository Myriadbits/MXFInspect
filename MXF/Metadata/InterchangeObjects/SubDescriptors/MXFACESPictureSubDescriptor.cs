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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01017900")]
    public class MXFACESPictureSubDescriptor : MXFSubDescriptor
    {
        private const string CATEGORYNAME = "ACESPictureSubDescriptor";

        private readonly UL aCESAuthoringInformation_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x01, 0x00, 0x00, 0x00);
        private readonly UL aCESMasteringDisplayPrimaries_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x02, 0x00, 0x00, 0x00);
        private readonly UL aCESMasteringDisplayWhitePointChromaticity_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x03, 0x00, 0x00, 0x00);
        private readonly UL aCESMasteringDisplayMaximumLuminance_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x04, 0x00, 0x00, 0x00);
        private readonly UL aCESMasteringDisplayMinimumLuminance_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x05, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060a.01000000")]
        public string ACESAuthoringInformation { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(EnumArrayConverter<MXFColorPrimary>))]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060a.02000000")]
        public MXFColorPrimary[] ACESMasteringDisplayPrimaries { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060a.03000000")]
        public MXFColorPrimary ACESMasteringDisplayWhitePointChromaticity { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060a.04000000")]
        public UInt32? ACESMasteringDisplayMaximumLuminance { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060a.05000000")]
        public UInt32? ACESMasteringDisplayMinimumLuminance { get; set; }

        public MXFACESPictureSubDescriptor(MXFPack pack)
            : base(pack, "ACESPictureSubDescriptor")
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
                    case var _ when localTag.AliasUID == aCESAuthoringInformation_Key: 
                        this.ACESAuthoringInformation = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.ACESAuthoringInformation;
                        return true;
                    case var _ when localTag.AliasUID == aCESMasteringDisplayPrimaries_Key: 
                        this.ACESMasteringDisplayPrimaries = reader.ReadArray(reader.ReadColorPrimary, 3);
                        localTag.Value = this.ACESMasteringDisplayPrimaries;
                        return true;
                    case var _ when localTag.AliasUID == aCESMasteringDisplayWhitePointChromaticity_Key: this.ACESMasteringDisplayWhitePointChromaticity = reader.ReadColorPrimary();
                        localTag.Value = this.ACESMasteringDisplayWhitePointChromaticity;
                        return true;
                    case var _ when localTag.AliasUID == aCESMasteringDisplayMaximumLuminance_Key: 
                        this.ACESMasteringDisplayMaximumLuminance = reader.ReadUInt32();
                        localTag.Value = this.ACESMasteringDisplayMaximumLuminance;
                        return true;
                    case var _ when localTag.AliasUID == aCESMasteringDisplayMinimumLuminance_Key: 
                        this.ACESMasteringDisplayMinimumLuminance = reader.ReadUInt32();
                        localTag.Value = this.ACESMasteringDisplayMinimumLuminance;
                        return true;
                }
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
