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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012f00")]
    public class MXFPreface : MXFInterchangeObject
    {
        private const string CATEGORYNAME = "Preface";

        private readonly UL isRIPPresent_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x04, 0x05, 0x03, 0x00, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.01020203.00000000")]
        public UL OperationalPattern { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.01020210.02040000")]
        public UL[] ConformsToSpecifications { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.03010201.02000000")]
        public UInt16? ByteOrder { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03010201.04000000")]
        public UInt32? ObjectModelVersion { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03010201.05000000")]
        public MXFVersion FormatVersion { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04040503.00000000")]
        public bool? IsRIPPresent { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020110.02040000")]
        public DateTime? FileLastModified { get; set; }


        public MXFPreface(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "Preface")
        {
            //this.Key.Type = KeyType.Preface;
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x3B01: this.ByteOrder = reader.ReadUInt16(); return true;
                case 0x3B02: this.FileLastModified = reader.ReadTimestamp(); return true;
                case 0x3B03: this.AddChild(reader.ReadReference<MXFContentStorage>("ContentStorage")); return true;
                case 0x3B04: this.AddChild(reader.ReadReference<MXFDictionary>("Dictionary")); return true;
                case 0x3B05: this.FormatVersion = reader.ReadVersion(); return true;
                case 0x3B06: this.AddChild(reader.ReadReferenceSet<MXFIdentification>("Identifications", "Identification")); return true;
                case 0x3B07: this.ObjectModelVersion = reader.ReadUInt32(); return true;
                case 0x3B08: this.AddChild(reader.ReadReference<MXFPackage>("PrimaryPackage")); return true;
                case 0x3B09: this.OperationalPattern = reader.ReadUL(); return true;
                case 0x3B0A: this.AddChild(reader.ReadAUIDSet("EssenceContainers", "EssenceContainer")); return true;
                    // TODO review how the metadataschemes are read (especially if there are no schemes present)
                case 0x3B0B: this.AddChild(reader.ReadAUIDSet("Descriptive Metadata Schemes", "DM scheme")); return true;
                case var _ when localTag.AliasUID == isRIPPresent_Key: this.IsRIPPresent = reader.ReadBoolean(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
