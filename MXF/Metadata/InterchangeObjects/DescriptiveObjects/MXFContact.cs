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

using System.ComponentModel;
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01013a00")]
    public class MXFContact : MXFThesaurus
    {
        private const string CATEGORYNAME = "Contact";

        public readonly UL contactID_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x08, 0x01, 0x01, 0x15, 0x40, 0x01, 0x02, 0x00, 0x00);
        public readonly UL addressObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x17, 0x00);
        public readonly UL nameValueObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x1f, 0x02);

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.01011540.01020000")]
        public UUID ContactID { get; set; }

        public MXFContact(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "Contact";
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
                    case var _ when localTag.AliasUID == contactID_Key:
                        this.ContactID = reader.ReadUUID();
                        localTag.Value = this.ContactID;
                        return true;
                    case var _ when localTag.AliasUID == addressObjects_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("Address Object", localTag.Offset, localTag.Length.Value));
                        return true;
                    // TODO replace generic MXFObject with class NameValue once implemented
                    case var _ when localTag.AliasUID == nameValueObjects_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFObject>("NameValue Object", localTag.Offset, localTag.Length.Value));
                        return true;
                }
            }

            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
