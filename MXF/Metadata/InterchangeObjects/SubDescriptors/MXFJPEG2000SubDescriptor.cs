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
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01015a00")]
    public class MXFJPEG2000SubDescriptor : MXFSubDescriptor
    {
        private const string CATEGORYNAME = "JPEG2000SubDescriptor";

        private readonly UL rsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x01, 0x00, 0x00, 0x00);
        private readonly UL xsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x02, 0x00, 0x00, 0x00);
        private readonly UL ysiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x03, 0x00, 0x00, 0x00);
        private readonly UL xOsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x04, 0x00, 0x00, 0x00);
        private readonly UL yOsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x05, 0x00, 0x00, 0x00);
        private readonly UL xTsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x06, 0x00, 0x00, 0x00);
        private readonly UL yTsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x07, 0x00, 0x00, 0x00);
        private readonly UL xTOsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x08, 0x00, 0x00, 0x00);
        private readonly UL yTOsiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x09, 0x00, 0x00, 0x00);
        private readonly UL csiz = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0a, 0x00, 0x00, 0x00);
        private readonly UL pictureComponentSizing = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0b, 0x00, 0x00, 0x00);
        private readonly UL codingStyleDefault = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0c, 0x00, 0x00, 0x00);
        private readonly UL quantizationDefault = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0d, 0x00, 0x00, 0x00);
        private readonly UL j2CLayout = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x03, 0x0e, 0x00, 0x00, 0x00);
        private readonly UL j2KExtendedCapabilities = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x03, 0x0f, 0x00, 0x00, 0x00);
        private readonly UL j2KProfile = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x03, 0x10, 0x00, 0x00, 0x00);
        private readonly UL j2KCorrespondingProfile = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x03, 0x11, 0x00, 0x00, 0x00);


        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.01000000")]
        public UInt16? Rsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.02000000")]
        public UInt32? Xsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.03000000")]
        public UInt32? Ysiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.04000000")]
        public UInt32? XOsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.05000000")]
        public UInt32? YOsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.06000000")]
        public UInt32? XTsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.07000000")]
        public UInt32? YTsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.08000000")]
        public UInt32? XTOsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.09000000")]
        public UInt32? YTOsiz { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.0a000000")]
        public UInt16? Csiz { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.0b000000")]
        public byte[] PictureComponentSizing { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.0c000000")]
        public byte[] CodingStyleDefault { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04010603.0d000000")]
        public byte[] QuantizationDefault { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(EnumArrayConverter<MXFRGBAComponent>))]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010603.0e000000")]
        public MXFRGBAComponent[] J2CLayout { get; set; }

        // TODO this shall be of type J2KExtendedCapabilities
        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(EnumArrayConverter<MXFRGBAComponent>))]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010603.0f000000")]
        public byte[] J2KExtendedCapabilities { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(EnumArrayConverter<MXFRGBAComponent>))]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010603.10000000")]
        public UInt16[] J2KProfile { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(EnumArrayConverter<MXFRGBAComponent>))]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04010603.11000000")]
        public UInt16[] J2KCorrespondingProfile { get; set; }


        public MXFJPEG2000SubDescriptor(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "JPEG2000SubDescriptor")
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
                    case var _ when localTag.Key == rsiz: this.Rsiz = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == xsiz: this.Xsiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == ysiz: this.Ysiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == xOsiz: this.XOsiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == yOsiz: this.YOsiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == xTsiz: this.XTsiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == yTsiz: this.YTsiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == xTOsiz: this.XTOsiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == yTOsiz: this.YTOsiz = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == csiz: this.Csiz = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == pictureComponentSizing:
                        this.PictureComponentSizing = reader.ReadBytes(localTag.Size);
                        return true;
                    case var _ when localTag.Key == codingStyleDefault:
                        this.CodingStyleDefault = reader.ReadBytes(localTag.Size);
                        return true;
                    case var _ when localTag.Key == quantizationDefault:
                        this.QuantizationDefault = reader.ReadBytes(localTag.Size);
                        return true;
                    case var _ when localTag.Key == j2CLayout:
                        this.J2CLayout = reader.ReadRGBALayout();
                        return true;
                    case var _ when localTag.Key == j2KExtendedCapabilities:
                        this.J2KExtendedCapabilities = reader.ReadBytes(localTag.Size);
                        return true;
                    case var _ when localTag.Key == j2KProfile:
                        this.J2KProfile = reader.ReadArray(reader.ReadUInt16, localTag.Size);
                        return true;
                    case var _ when localTag.Key == j2KCorrespondingProfile:
                        this.J2KCorrespondingProfile = reader.ReadArray(reader.ReadUInt16, localTag.Size);
                        return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
