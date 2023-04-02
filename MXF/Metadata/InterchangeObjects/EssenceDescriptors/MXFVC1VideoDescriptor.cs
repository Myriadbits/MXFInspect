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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01015f00")]
    public class MXFVC1VideoDescriptor : MXFCDCIDescriptor
    {
        private const string CATEGORYNAME = "VC1 Video Descriptor";

        private readonly UL initMetadata_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x01, 0x00, 0x00, 0x00);
        private readonly UL singleSequence_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x02, 0x00, 0x00, 0x00);
        private readonly UL codedContent_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x03, 0x00, 0x00, 0x00);
        private readonly UL identicalGOP_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x04, 0x00, 0x00, 0x00);
        private readonly UL maxGOP_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x05, 0x00, 0x00, 0x00);
        private readonly UL pictureCount_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x06, 0x00, 0x00, 0x00);
        private readonly UL avgBitRate_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x07, 0x00, 0x00, 0x00);
        private readonly UL maxBitRate_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x08, 0x00, 0x00, 0x00);
        private readonly UL profile_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x09, 0x00, 0x00, 0x00);
        private readonly UL level_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x01, 0x06, 0x04, 0x0a, 0x00, 0x00, 0x00);


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
        /// <param name="pack"></param>
        public MXFVC1VideoDescriptor(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "VC1 Video Descriptor";
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
                    case var _ when localTag.AliasUID == initMetadata_Key: 
                        this.VC1InitializationMetadata = reader.ReadBytes((int)localTag.Length.Value);
                        localTag.Value = this.VC1InitializationMetadata; 
                        return true;
                    case var _ when localTag.AliasUID == singleSequence_Key: 
                        this.VC1SingleSequence = reader.ReadBoolean(); 
                        localTag.Value = this.VC1SingleSequence;
                        return true;
                    case var _ when localTag.AliasUID == codedContent_Key: 
                        this.VC1CodedContentType = (MXFCodedContentScanning)reader.ReadByte(); 
                        localTag.Value = this.VC1CodedContentType;
                        return true;
                    case var _ when localTag.AliasUID == identicalGOP_Key: 
                        this.VC1IdenticalGOP = reader.ReadBoolean(); 
                        localTag.Value = this.VC1IdenticalGOP;
                        return true;
                    case var _ when localTag.AliasUID == maxGOP_Key:
                        this.VC1MaxGOP = reader.ReadUInt16(); 
                        localTag.Value = this.VC1MaxGOP;
                        return true;
                    case var _ when localTag.AliasUID == pictureCount_Key: 
                        this.VC1BPictureCount = reader.ReadUInt16(); 
                        localTag.Value = this.VC1BPictureCount;
                        return true;
                    case var _ when localTag.AliasUID == avgBitRate_Key: 
                        this.VC1AverageBitRate = reader.ReadUInt32(); 
                        localTag.Value = this.VC1AverageBitRate;
                        return true;
                    case var _ when localTag.AliasUID == maxBitRate_Key: 
                        this.VC1MaximumBitRate = reader.ReadUInt32(); 
                        localTag.Value = this.VC1MaximumBitRate;
                        return true;
                    case var _ when localTag.AliasUID == profile_Key: 
                        this.VC1Profile = reader.ReadByte(); 
                        localTag.Value = this.VC1Profile;
                        return true;
                    case var _ when localTag.AliasUID == level_Key: 
                        this.VC1Level = reader.ReadByte(); 
                        localTag.Value = this.VC1Level;
                        return true;
                }
            }

            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
