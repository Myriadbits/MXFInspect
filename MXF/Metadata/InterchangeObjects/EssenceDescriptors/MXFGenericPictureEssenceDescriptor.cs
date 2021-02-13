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

using Myriadbits.MXF.Utils;
using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012700")]
    public class MXFGenericPictureEssenceDescriptor : MXFFileDescriptor
    {
        private const string CATEGORYNAME = "GenericPictureEssenceDescriptor";
        private const int CATEGORYPOS = 4;

        private readonly MXFKey altCenterCuts_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x03, 0x02, 0x0b, 0x00, 0x00, 0x00);
        private readonly MXFKey activeHeight_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x13, 0x00, 0x00, 0x00);
        private readonly MXFKey activeWidth_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x14, 0x00, 0x00, 0x00);
        private readonly MXFKey activeXOffset_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x15, 0x00, 0x00, 0x00);
        private readonly MXFKey activeYOffset_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x16, 0x00, 0x00, 0x00);
        private readonly MXFKey displayPrimaries_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x20, 0x04, 0x01, 0x01, 0x01, 0x00, 0x00);
        private readonly MXFKey displayWhitePointChromaticity_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x04, 0x20, 0x04, 0x01, 0x01, 0x02, 0x00, 0x00);
        private readonly MXFKey displayMaxLuminance_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x20, 0x04, 0x01, 0x01, 0x03, 0x00, 0x00);
        private readonly MXFKey displayMinLuminance_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x20, 0x04, 0x01, 0x01, 0x04, 0x00, 0x00);

        #region properties
        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04050113.00000000")]
        public MXFSignalStandard? SignalStandard { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010301.04000000")]
        public MXFFrameLayout? FrameLayout { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010502.02000000")]
        public UInt32? StoredWidth { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010502.01000000")]
        public UInt32? StoredHeight { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010302.08000000")]
        public Int32? StoredF2Offset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.08000000")]
        public UInt32? SampledWidth { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.07000000")]
        public UInt32? SampledHeight { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.09000000")]
        public Int32? SampledXOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.0a000000")]
        public Int32? SampledYOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.0b000000")]
        public UInt32? DisplayHeight { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.0c000000")]
        public UInt32? DisplayWidth { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.0d000000")]
        public Int32? DisplayXOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010501.0e000000")]
        public Int32? DisplayYOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010302.07000000")]
        public Int32? DisplayF2Offset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04010101.01000000")]
        public MXFRational ImageAspectRatio { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.04010302.09000000")]
        public byte? ActiveFormatDescriptor { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010302.05000000")]
        [TypeConverter(typeof(IntegerArrayConverter))]
        public Int32[] VideoLineMap { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200102.00000000")]
        public MXFAlphaTransparencyType? AlphaTransparency { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010201.01010200")]
        public MXFKey TransferCharacteristic { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04180101.00000000")]
        public UInt32? ImageAlignmentFactor { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04180102.00000000")]
        public UInt32? ImageStartOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04180103.00000000")]
        public UInt32? ImageEndOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010301.06000000")]
        public MXFFieldNumber? FieldDominance { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010601.00000000")]
        public MXFKey PictureCompression { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010201.01030100")]
        public MXFKey CodingEquations { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.04010201.01060100")]
        public MXFKey ColorPrimaries { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010501.13000000")]
        public UInt32? ActiveHeight { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010501.14000000")]
        public UInt32? ActiveWidth { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010501.15000000")]
        public UInt32? ActiveXOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010501.16000000")]
        public UInt32? ActiveYOffset { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200401.01010000")]
        public MXFColorPrimary[] MasteringDisplayPrimaries { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200401.01020000")]
        public MXFColorPrimary MasteringDisplayWhitePointChromaticity { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200401.01030000")]
        public UInt32? MasteringDisplayMaximumLuminance { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200401.01040000")]
        public UInt32? MasteringDisplayMinimumLuminance { get; set; }

        #endregion



        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFGenericPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "Generic Picture Essence Descriptor")
        {
        }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFGenericPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
                case 0x3215: this.SignalStandard = (MXFSignalStandard)reader.ReadByte(); return true;
                case 0x320C: this.FrameLayout = (MXFFrameLayout)reader.ReadByte(); return true;
                case 0x3203: this.StoredWidth = reader.ReadUInt32(); return true;
                case 0x3202: this.StoredHeight = reader.ReadUInt32(); return true;
                case 0x3216: this.StoredF2Offset = reader.ReadInt32(); return true;
                case 0x3205: this.SampledWidth = reader.ReadUInt32(); return true;
                case 0x3204: this.SampledHeight = reader.ReadUInt32(); return true;
                case 0x3206: this.SampledXOffset = reader.ReadInt32(); return true;
                case 0x3207: this.SampledYOffset = reader.ReadInt32(); return true;
                case 0x3208: this.DisplayHeight = reader.ReadUInt32(); return true;
                case 0x3209: this.DisplayWidth = reader.ReadUInt32(); return true;
                case 0x320A: this.DisplayXOffset = reader.ReadInt32(); return true;
                case 0x320B: this.DisplayYOffset = reader.ReadInt32(); return true;
                case 0x3217: this.DisplayF2Offset = reader.ReadInt32(); return true;
                case 0x320E: this.ImageAspectRatio = reader.ReadRational(); return true;
                case 0x3218: this.ActiveFormatDescriptor = reader.ReadByte(); return true;
                case 0x320D: this.VideoLineMap = reader.ReadArray(reader.ReadInt32, 4); return true;
                case 0x320F: this.AlphaTransparency = (MXFAlphaTransparencyType)reader.ReadByte(); return true;
                case 0x3210: this.TransferCharacteristic = reader.ReadULKey(); return true;
                case 0x3211: this.ImageAlignmentFactor = reader.ReadUInt32(); return true;
                case 0x3213: this.ImageStartOffset = reader.ReadUInt32(); return true;
                case 0x3214: this.ImageEndOffset = reader.ReadUInt32(); return true;
                case 0x3212: this.FieldDominance = (MXFFieldNumber)reader.ReadByte(); return true;
                case 0x3201: this.PictureCompression = reader.ReadULKey(); return true;
                case 0x321A: this.CodingEquations = reader.ReadULKey(); return true;
                case 0x3219: this.ColorPrimaries = reader.ReadULKey(); return true;
                case var _ when localTag.Key == altCenterCuts_Key: this.AddChild(reader.ReadAUIDSet("AlternativeCenterCuts", "AlternativeCenterCut")); return true;
                case var _ when localTag.Key == activeHeight_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var _ when localTag.Key == activeWidth_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var _ when localTag.Key == activeXOffset_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var _ when localTag.Key == activeYOffset_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var _ when localTag.Key == displayPrimaries_Key: this.MasteringDisplayPrimaries = reader.ReadArray(reader.ReadColorPrimary, 3);  return true;
                case var _ when localTag.Key == displayWhitePointChromaticity_Key: this.MasteringDisplayWhitePointChromaticity = reader.ReadColorPrimary(); return true;
                case var _ when localTag.Key == displayMaxLuminance_Key: this.MasteringDisplayMaximumLuminance = reader.ReadUInt32(); return true;
                case var _ when localTag.Key == displayMinLuminance_Key: this.MasteringDisplayMinimumLuminance = reader.ReadUInt32(); return true;

            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
