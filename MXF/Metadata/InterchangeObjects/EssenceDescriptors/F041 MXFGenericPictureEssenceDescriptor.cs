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
    [ULGroup(Deprecated = false, IsConcrete = false, NumberOfElements = 35)]
    public class MXFGenericPictureEssenceDescriptor : MXFFileDescriptor
    {
        private readonly MXFKey altCenterCuts_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x03, 0x02, 0x0b, 0x00, 0x00, 0x00);
        private readonly MXFKey activeHeight_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x13, 0x00, 0x00, 0x00);
        private readonly MXFKey activeWidth_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x14, 0x00, 0x00, 0x00);
        private readonly MXFKey activeXOffset_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x15, 0x00, 0x00, 0x00);
        private readonly MXFKey activeYOffset_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x05, 0x01, 0x16, 0x00, 0x00, 0x00);
        private readonly MXFKey displayPrimaries_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x20, 0x04, 0x01, 0x01, 0x01, 0x00, 0x00);
        private readonly MXFKey displayWhitePointChromaticity_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x04, 0x20, 0x04, 0x01, 0x01, 0x02, 0x00, 0x00);
        private readonly MXFKey displayMaxLuminance_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x20, 0x04, 0x01, 0x01, 0x03, 0x00, 0x00);
        private readonly MXFKey displayMinLuminance_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x20, 0x04, 0x01, 0x01, 0x04, 0x00, 0x00);

        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3215")]
        public MXFSignalStandard? SignalStandard { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320C")]
        public MXFFrameLayout? FrameLayout { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3203")]
        public UInt32? StoredWidth { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3202")]
        public UInt32? StoredHeight { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3216")]
        public Int32? StoredF2Offset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3205")]
        public UInt32? SampledWidth { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3204")]
        public UInt32? SampledHeight { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3206")]
        public Int32? SampledXOffset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3207")]
        public Int32? SampledYOffset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3209")]
        public UInt32? DisplayHeight { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3208")]
        public UInt32? DisplayWidth { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320A")]
        public Int32? DisplayXOffset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320B")]
        public Int32? DisplayYOffset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3217")]
        public Int32? DisplayF2Offset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320E")]
        public MXFRational ImageAspectRatio { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3218")]
        public byte? ActiveFormatDescriptor { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320D")]
        [TypeConverter(typeof(IntegerArrayConverter))]
        public Int32[] VideoLineMap { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320F")]
        public MXFAlphaTransparencyType? AlphaTransparency { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3210")]
        public MXFKey TransferCharacteristic { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3211")]
        public UInt32? ImageAlignmentFactor { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3213")]
        public UInt32? ImageStartOffset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3214")]
        public UInt32? ImageEndOffset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3212")]
        public MXFFieldNumber? FieldDominance { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3201")]
        public MXFKey PictureCompression { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("321A")]
        public MXFKey CodingEquations { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3219")]
        public MXFKey ColorPrimaries { get; set; }

        // new ones
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public UInt32? ActiveHeight { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public UInt32? ActiveWidth { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public UInt32? ActiveXOffset { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public UInt32? ActiveYOffset { get; set; }
        //[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        //public UInt32? AlternativeCenterCuts { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public MXFColorPrimary[] MasteringDisplayPrimaries { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public MXFColorPrimary MasteringDisplayWhitePointChromaticity { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public UInt32? MasteringDisplayMaximumLuminance { get; set; }
        [CategoryAttribute("GenericPictureEssenceDescriptor"), Description("")]
        public UInt32? MasteringDisplayMinimumLuminance { get; set; }




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
                case 0x3210: this.TransferCharacteristic = reader.ReadKey(); return true;
                case 0x3211: this.ImageAlignmentFactor = reader.ReadUInt32(); return true;
                case 0x3213: this.ImageStartOffset = reader.ReadUInt32(); return true;
                case 0x3214: this.ImageEndOffset = reader.ReadUInt32(); return true;
                case 0x3212: this.FieldDominance = (MXFFieldNumber)reader.ReadByte(); return true;
                case 0x3201: this.PictureCompression = reader.ReadKey(); return true;
                case 0x321A: this.CodingEquations = reader.ReadKey(); return true;
                case 0x3219: this.ColorPrimaries = reader.ReadKey(); return true;
                case var a when localTag.Key == altCenterCuts_Key: this.AddChild(reader.ReadAUIDSet("AlternativeCenterCuts", "AlternativeCenterCut")); return true;
                case var a when localTag.Key == activeHeight_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var a when localTag.Key == activeWidth_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var a when localTag.Key == activeXOffset_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var a when localTag.Key == activeYOffset_Key: this.ActiveHeight = reader.ReadUInt32(); return true;
                case var a when localTag.Key == displayPrimaries_Key: this.MasteringDisplayPrimaries = reader.ReadArray(reader.ReadColorPrimary, 3);  return true;
                case var a when localTag.Key == displayWhitePointChromaticity_Key: this.MasteringDisplayWhitePointChromaticity = reader.ReadColorPrimary(); return true;
                case var a when localTag.Key == displayMaxLuminance_Key: this.MasteringDisplayMaximumLuminance = reader.ReadUInt32(); return true;
                case var a when localTag.Key == displayMinLuminance_Key: this.MasteringDisplayMinimumLuminance = reader.ReadUInt32(); return true;

            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
