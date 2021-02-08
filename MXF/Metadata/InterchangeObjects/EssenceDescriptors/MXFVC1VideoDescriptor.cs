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
    public class MXFVC1VideoDescriptor : MXFCDCIPictureEssenceDescriptor
    {
        private const string CATEGORYNAME = "VC1 Video Descriptor";

        private readonly MXFKey initMetadata_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x01, 0x00, 0x00, 0x00);
        private readonly MXFKey singleSequence_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x02, 0x00, 0x00, 0x00);
        private readonly MXFKey codedContent_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x03, 0x00, 0x00, 0x00);
        private readonly MXFKey identicalGOP_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x04, 0x00, 0x00, 0x00);
        private readonly MXFKey maxGOP_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x05, 0x00, 0x00, 0x00);
        private readonly MXFKey pictureCount_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x06, 0x00, 0x00, 0x00);
        private readonly MXFKey avgBitRate_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x07, 0x00, 0x00, 0x00);
        private readonly MXFKey maxBitRate_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x08, 0x00, 0x00, 0x00);
        private readonly MXFKey profile_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x09, 0x00, 0x00, 0x00);
        private readonly MXFKey level_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x0a, 0x00, 0x00, 0x00);


        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.01000000")]
        public object VC1InitializationMetadata { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.02000000")]
        public bool? VC1SingleSequence { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.03000000")]
        public MXFCodedContentScanning VC1CodedContentType { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.04000000")]
        public bool? VC1IdenticalGOP { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.05000000")]
        public UInt16? VC1MaxGOP { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.06000000")]
        public UInt16? VC1BPictureCount { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.07000000")]
        public UInt32? VC1AverageBitRate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.08000000")]
        public UInt32? VC1MaximumBitRate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.09000000")]
        public byte? VC1Profile { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04010604.0a000000")]
        public byte? VC1Level { get; set; }


        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFVC1VideoDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "VC1 Video Descriptor";
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
                    case var _ when localTag.Key == initMetadata_Key: this.VC1InitializationMetadata = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
                    case var _ when localTag.Key == singleSequence_Key: this.VC1SingleSequence = reader.ReadBool(); return true;
                    case var _ when localTag.Key == codedContent_Key: this.VC1CodedContentType = (MXFCodedContentScanning)reader.ReadByte(); return true;
                    case var _ when localTag.Key == identicalGOP_Key: this.VC1IdenticalGOP = reader.ReadBool(); return true;
                    case var _ when localTag.Key == maxGOP_Key: this.VC1MaxGOP = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == pictureCount_Key: this.VC1BPictureCount = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == avgBitRate_Key: this.VC1AverageBitRate = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == maxBitRate_Key: this.VC1MaximumBitRate = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == profile_Key: this.VC1Profile = reader.ReadByte(); return true;
                    case var _ when localTag.Key == level_Key: this.VC1Level = reader.ReadByte(); return true;
                }
            }

            return base.ParseLocalTag(reader, localTag);
        }

    }
}
