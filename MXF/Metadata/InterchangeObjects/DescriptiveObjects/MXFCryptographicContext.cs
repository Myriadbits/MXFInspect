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
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010401.02020000")]
    public class MXFCryptographicContext : MXFDescriptiveObject
    {
        private const string CATEGORYNAME = "CryptographicContext";

        public readonly UL cryptoContextId_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x01, 0x01, 0x15, 0x11, 0x00, 0x00, 0x00, 0x00);
        public readonly UL cipherAlgorithm_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x02, 0x09, 0x03, 0x01, 0x01, 0x00, 0x00, 0x00);
        public readonly UL cryptoKeyID_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x02, 0x09, 0x03, 0x01, 0x02, 0x00, 0x00, 0x00);
        public readonly UL mICAlgorithm_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x02, 0x09, 0x03, 0x02, 0x01, 0x00, 0x00, 0x00);
        public readonly UL sourceContainerFormat_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x09, 0x06, 0x01, 0x01, 0x02, 0x02, 0x00, 0x00, 0x00);
        public readonly UL mICCarriage_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x01, 0x0d, 0x0e, 0x01, 0x01, 0x07, 0x04, 0x03, 0x00);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.01011511.00000000")]
        public UUID CryptographicContextID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.02090301.01000000")]
        public AUID CipherAlgorithm { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.02090301.02000000")]
        public AUID CryptographicKeyID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.02090302.01000000")]
        public AUID MICAlgorithm { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.06010102.02000000")]
        public AUID SourceContainerFormat { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.0d0e0101.07040300")]
        public AUID MICCarriage { get; set; }



        public MXFCryptographicContext(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "CryptographicContext";
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
                    case var _ when localTag.AliasUID == cryptoContextId_Key: 
                        this.CryptographicContextID = reader.ReadUUID();
                        localTag.Value = this.CryptographicContextID;
                        return true;
                    case var _ when localTag.AliasUID == cipherAlgorithm_Key: 
                        this.CipherAlgorithm = reader.ReadAUID();
                        localTag.Value = this.CipherAlgorithm;
                        return true;
                    case var _ when localTag.AliasUID == cryptoKeyID_Key: 
                        this.CryptographicKeyID = reader.ReadAUID();
                        localTag.Value = this.CryptographicKeyID;
                        return true;
                    case var _ when localTag.AliasUID == mICAlgorithm_Key: 
                        this.MICAlgorithm = reader.ReadAUID();
                        localTag.Value = this.MICAlgorithm;
                        return true;
                    case var _ when localTag.AliasUID == sourceContainerFormat_Key: 
                        this.SourceContainerFormat = reader.ReadAUID();
                        localTag.Value = this.SourceContainerFormat;
                        return true;
                    case var _ when localTag.AliasUID == mICCarriage_Key: 
                        this.MICCarriage = reader.ReadAUID();
                        localTag.Value = this.MICCarriage;
                        return true;

                }
            }

            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
