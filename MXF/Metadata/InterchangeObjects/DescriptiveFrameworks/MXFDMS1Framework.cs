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
    // TODO complete this class
    // TODO add ULElement attributes
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010401.017f0100")]
    public class MXFDMS1Framework : MXFDescriptiveFramework
    {
        private const string CATEGORYNAME = "DMS1Framework";

        public readonly UL frameworkTitle_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x01, 0x05, 0x0f, 0x01, 0x00, 0x00, 0x00, 0x00);
        public readonly UL extTextLanguageCode_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x03, 0x01, 0x01, 0x02, 0x02, 0x13, 0x00, 0x00);
        public readonly UL primExtSpokenLanguageCode_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x03, 0x01, 0x01, 0x02, 0x03, 0x11, 0x00, 0x00);
        public readonly UL secExtSpokenLanguageCode_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x03, 0x01, 0x01, 0x02, 0x03, 0x12, 0x00, 0x00);
        public readonly UL orgExtSpokenLanguageCode_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x03, 0x01, 0x01, 0x02, 0x03, 0x13, 0x00, 0x00);
        public readonly UL thesaurusName_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x03, 0x02, 0x01, 0x02, 0x15, 0x01, 0x00, 0x00);
        public readonly UL contactsListObject_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x02, 0x40, 0x22, 0x00);
        public readonly UL locations_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x03, 0x40, 0x16, 0x00);
        public readonly UL titlesObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x04, 0x00);
        public readonly UL annotationObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x0d, 0x00);
        public readonly UL participantObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x13, 0x00);
        public readonly UL metadataServerLocators_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x06, 0x0c, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        public string FrameworkTitle { get; set; }

        [Category(CATEGORYNAME)]
        public string FrameworkExtendedTextLanguageCode { get; set; }

        [Category(CATEGORYNAME)]
        public string PrimaryExtendedSpokenLanguageCode { get; set; }

        [Category(CATEGORYNAME)]
        public string SecondaryExtendedSpokenLanguageCode { get; set; }

        [Category(CATEGORYNAME)]
        public string OriginalExtendedSpokenLanguageCode { get; set; }

        [Category(CATEGORYNAME)]
        public string FrameworkThesaurusName { get; set; }

        public MXFDMS1Framework(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "DMS1Framework";
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
                    case var _ when localTag.AliasUID == frameworkTitle_Key: 
                        this.FrameworkTitle = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.FrameworkTitle;
                        return true;
                    case var _ when localTag.AliasUID == extTextLanguageCode_Key: 
                        this.FrameworkExtendedTextLanguageCode = reader.ReadUTF8String(localTag.Length.Value);
                        localTag.Value = this.FrameworkExtendedTextLanguageCode;
                        return true;
                    case var _ when localTag.AliasUID == primExtSpokenLanguageCode_Key: 
                        this.PrimaryExtendedSpokenLanguageCode = reader.ReadUTF8String(localTag.Length.Value);
                        localTag.Value = this.PrimaryExtendedSpokenLanguageCode;
                        return true;
                    case var _ when localTag.AliasUID == secExtSpokenLanguageCode_Key: 
                        this.SecondaryExtendedSpokenLanguageCode = reader.ReadUTF8String(localTag.Length.Value);
                        localTag.Value = this.SecondaryExtendedSpokenLanguageCode;
                        return true;
                    case var _ when localTag.AliasUID == orgExtSpokenLanguageCode_Key: 
                        this.OriginalExtendedSpokenLanguageCode = reader.ReadUTF8String(localTag.Length.Value);
                        localTag.Value = this.OriginalExtendedSpokenLanguageCode;
                        return true;
                    case var _ when localTag.AliasUID == thesaurusName_Key: 
                        this.FrameworkThesaurusName = reader.ReadUTF8String(localTag.Length.Value);
                        localTag.Value = this.FrameworkThesaurusName;
                        return true;
                    case var _ when localTag.AliasUID == contactsListObject_Key:
                        localTag.AddChild(reader.ReadReference<MXFContactsList>("ContactsListObject", localTag.Offset)); 
                        return true;
                    case var _ when localTag.AliasUID == locations_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFLocation>("LocationObject", localTag.Offset, localTag.Length.Value));
                        return true;
                    case var _ when localTag.AliasUID == titlesObjects_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("TitlesObject", localTag.Offset, localTag.Length.Value));
                        return true;
                    case var _ when localTag.AliasUID == annotationObjects_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("AnnotationObject", localTag.Offset, localTag.Length.Value));
                        return true;
                    case var _ when localTag.AliasUID == participantObjects_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("ParticipantObject", localTag.Offset, localTag.Length.Value));
                        return true;
                    case var _ when localTag.AliasUID == metadataServerLocators_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFLocator>("MetadataServerLocator", localTag.Offset, localTag.Length.Value));
                        return true;
                }
            }

            return base.ReadLocalTagValue(reader, localTag);
        }
    }
}
