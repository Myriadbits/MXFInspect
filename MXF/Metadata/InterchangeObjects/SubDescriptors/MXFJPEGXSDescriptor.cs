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

using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.02530101.0d010101.01018102")]
    public class MXFJPEGXSSubDescriptor : MXFSubDescriptor
    {
        private const string CATEGORYNAME = "JPEGXSSubDescriptor";

        private readonly UL ppih = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x01, 0x00, 0x00, 0x00);
        private readonly UL plev = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x02, 0x00, 0x00, 0x00);
        private readonly UL wf = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x03, 0x00, 0x00, 0x00);
        private readonly UL hf = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x04, 0x00, 0x00, 0x00);
        private readonly UL nc = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x05, 0x00, 0x00, 0x00);
        private readonly UL cw = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x07, 0x00, 0x00, 0x00);
        private readonly UL hsl = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x08, 0x00, 0x00, 0x00);
        private readonly UL maximumrate = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0b, 0x09, 0x00, 0x00, 0x00);


        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.01000000")]
        public UInt16? Ppih { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.02000000")]
        public UInt16? Plev { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.03000000")]
        public UInt16? Wf { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.04000000")]
        public UInt16? Hf { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.05000000")]
        public Byte? Nc { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.07000000")]
        public UInt16? Cw { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.08000000")]
        public UInt16? Hsl { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.0401060b.09000000")]
        public UInt32? MaximumRate { get; set; }


        public MXFJPEGXSSubDescriptor(MXFPack pack)
            : base(pack, "JPEGXS SubDescriptor")
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
                    case var _ when localTag.AliasUID == ppih:
                        this.Ppih = reader.ReadUInt16();
                        localTag.Value = this.Ppih;
                        return true;
                    case var _ when localTag.AliasUID == plev:
                        this.Plev = reader.ReadUInt16();
                        localTag.Value = this.Plev;
                        return true;
                    case var _ when localTag.AliasUID == wf:
                        this.Wf = reader.ReadUInt16();
                        localTag.Value = this.Wf;
                        return true;
                    case var _ when localTag.AliasUID == hf:
                        this.Hf = reader.ReadUInt16();
                        localTag.Value = this.Hf;
                        return true;
                    case var _ when localTag.AliasUID == nc:
                        this.Nc = reader.ReadByte();
                        localTag.Value = this.Nc;
                        return true;
                    case var _ when localTag.AliasUID == cw:
                        this.Cw = reader.ReadUInt16();
                        localTag.Value = this.Cw;
                        return true;
                    case var _ when localTag.AliasUID == hsl:
                        this.Hsl = reader.ReadUInt16();
                        localTag.Value = this.Hsl;
                        return true;
                    case var _ when localTag.AliasUID == maximumrate:
                        this.MaximumRate = reader.ReadUInt32();
                        localTag.Value = this.MaximumRate;
                        return true;
                }
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
