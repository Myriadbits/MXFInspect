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

using System.ComponentModel;
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010401.04030100")]
    public class MXFTextBasedObject : MXFDescriptiveObject
    {
        private const string CATEGORYNAME = "TextBasedObject";

        public readonly UL rfc5646TextLanguageCode_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0d, 0x03, 0x01, 0x01, 0x02, 0x02, 0x14, 0x00, 0x00);
        public readonly UL textDataDescription_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0d, 0x03, 0x02, 0x01, 0x06, 0x03, 0x02, 0x00, 0x00);
        public readonly UL textBasedMetadataPayloadSchemeID_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0d, 0x04, 0x06, 0x08, 0x06, 0x00, 0x00, 0x00, 0x00);
        public readonly UL textMIMEMediaType_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0d, 0x04, 0x09, 0x02, 0x02, 0x00, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        public string RFC5646TextLanguageCode { get; set; }

        [Category(CATEGORYNAME)]
        public string TextDataDescription { get; set; }


        // TODO this should be a AUID?
        [Category(CATEGORYNAME)]
        public UUID TextBasedMetadataPayloadSchemeID { get; set; }

        [Category(CATEGORYNAME)]
        public string TextMIMEMediaType { get; set; }


        public MXFTextBasedObject(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "Text Based Object";
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
                    case var _ when localTag.AliasUID == rfc5646TextLanguageCode_Key: 
                        this.RFC5646TextLanguageCode = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.RFC5646TextLanguageCode;
                        return true;
                    case var _ when localTag.AliasUID == textDataDescription_Key: 
                        this.TextDataDescription = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.TextDataDescription;
                        return true;
                    case var _ when localTag.AliasUID == textBasedMetadataPayloadSchemeID_Key: 
                        this.TextBasedMetadataPayloadSchemeID = reader.ReadUUID();
                        localTag.Value = this.TextBasedMetadataPayloadSchemeID;
                        return true;
                    case var _ when localTag.AliasUID == textMIMEMediaType_Key:
                        this.TextMIMEMediaType = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.TextMIMEMediaType;
                        return true;
                }
            }

            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
