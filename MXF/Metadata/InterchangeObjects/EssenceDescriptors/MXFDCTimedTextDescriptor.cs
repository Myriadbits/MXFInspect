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
using Myriadbits.MXF.Properties;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01016400")]
    public class MXFDCTimedTextDescriptor : MXFGenericDataEssenceDescriptor
    {
        private const string CATEGORYNAME = "DC Timed Text Descriptor";

        private UL resourceID_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x01, 0x01, 0x15, 0x12, 0x00, 0x00, 0x00, 0x00);
        private UL namespaceURI_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x08, 0x01, 0x02, 0x01, 0x05, 0x01, 0x00, 0x00, 0x00);
        private UL rFC5646LanguageTagList_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x03, 0x01, 0x01, 0x02, 0x02, 0x16, 0x00, 0x00);
        private UL uCSEncoding_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x04, 0x09, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00);
        private UL displayType_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x06, 0x01, 0x01, 0x02, 0x04, 0x00, 0x00, 0x00);
        private UL intrinsicPictureResolution_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x06, 0x01, 0x01, 0x02, 0x05, 0x00, 0x00, 0x00);
        private UL zpositionInUse_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x06, 0x01, 0x01, 0x02, 0x06, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.01011512.00000000")]
        public UUID ResourceID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.01020105.01000000")]
        public string NamespaceURI { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.03010102.02160000")]
        public string RFC5646LanguageTagList { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.04090500.00000000")]
        public string UCSEncoding { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.06010102.04000000")]
        public string DisplayType { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.06010102.05000000")]
        public string IntrinsicPictureResolution { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.06010102.06000000")]
        public byte? ZpositionInUse { get; set; }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFDCTimedTextDescriptor(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "DC Timed Text Descriptor")
        {
            this.MetaDataName = this.Key.Name;
        }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFDCTimedTextDescriptor(IKLVStreamReader reader, MXFPack pack, string metadataName)
            : base(reader, pack, metadataName)
        {




        }

        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            if (localTag.AliasUID != null)
            {
                switch (localTag.AliasUID)
                {
                    case var _ when localTag.AliasUID == resourceID_Key: this.ResourceID = reader.ReadUUID(); return true;
                    case var _ when localTag.AliasUID == namespaceURI_Key: this.NamespaceURI = reader.ReadUTF16String(localTag.Length.Value); return true;
                    case var _ when localTag.AliasUID == rFC5646LanguageTagList_Key: this.RFC5646LanguageTagList = reader.ReadUTF16String(localTag.Length.Value); return true;
                    case var _ when localTag.AliasUID == uCSEncoding_Key: this.UCSEncoding = reader.ReadUTF16String(localTag.Length.Value); return true;
                    case var _ when localTag.AliasUID == displayType_Key: this.DisplayType = reader.ReadUTF16String(localTag.Length.Value); return true;
                    case var _ when localTag.AliasUID == intrinsicPictureResolution_Key: this.IntrinsicPictureResolution = reader.ReadUTF16String(localTag.Length.Value); return true;
                    case var _ when localTag.AliasUID == zpositionInUse_Key: this.ZpositionInUse = reader.ReadByte(); return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
