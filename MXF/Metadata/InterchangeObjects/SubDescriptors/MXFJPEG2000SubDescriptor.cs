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
    public class MXFJPEG2000SubDescriptor : MXFSubDescriptor
    {
        private const string CATEGORYNAME = "JPEG2000SubDescriptor";

        private readonly MXFKey rsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x01, 0x00, 0x00, 0x00);
        private readonly MXFKey xsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x02, 0x00, 0x00, 0x00);
        private readonly MXFKey ysiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x03, 0x00, 0x00, 0x00);
        private readonly MXFKey xOsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x04, 0x00, 0x00, 0x00);
        private readonly MXFKey yOsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x05, 0x00, 0x00, 0x00);
        private readonly MXFKey xTsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x06, 0x00, 0x00, 0x00);
        private readonly MXFKey yTsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x07, 0x00, 0x00, 0x00);
        private readonly MXFKey xTOsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x08, 0x00, 0x00, 0x00);
        private readonly MXFKey yTOsiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x09, 0x00, 0x00, 0x00);
        private readonly MXFKey csiz = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0a, 0x00, 0x00, 0x00);
        private readonly MXFKey pictureComponentSizing = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0b, 0x00, 0x00, 0x00);
        private readonly MXFKey codingStyleDefault = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0c, 0x00, 0x00, 0x00);
        private readonly MXFKey quantizationDefault = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x01, 0x06, 0x03, 0x0d, 0x00, 0x00, 0x00);
        private readonly MXFKey j2CLayout = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x03, 0x0e, 0x00, 0x00, 0x00);


        [Category(CATEGORYNAME)]
        public UInt16? Rsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? Xsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? Ysiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? XOsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? YOsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? XTsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? YTsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? XTOsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? YTOsiz { get; set; }

        [Category(CATEGORYNAME)]
        public UInt16? Csiz { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] PictureComponentSizing { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] CodingStyleDefault { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] QuantizationDefault { get; set; }

        [Category(CATEGORYNAME)]
        public MXFRGBAComponent[] J2CLayout { get; set; }


        public MXFJPEG2000SubDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "JPEG2000SubDescriptor")
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
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
                        this.PictureComponentSizing = reader.ReadArray(reader.ReadByte, localTag.Size);
                        return true;
                    case var _ when localTag.Key == codingStyleDefault:
                        this.CodingStyleDefault = reader.ReadArray(reader.ReadByte, localTag.Size);
                        return true;
                    case var _ when localTag.Key == quantizationDefault:
                        this.QuantizationDefault = reader.ReadArray(reader.ReadByte, localTag.Size);
                        return true;
                    case var _ when localTag.Key == j2CLayout:
                        this.J2CLayout = reader.ReadRGBALayout();
                        return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
