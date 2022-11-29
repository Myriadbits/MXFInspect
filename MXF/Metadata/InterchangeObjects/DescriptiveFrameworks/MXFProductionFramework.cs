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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010401.01010100")]
    public class MXFProductionFramework : MXFProductionClipFramework
    {
        private const string CATEGORYNAME = "ProductionFramework";

        public readonly UL integrationIndication_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x04, 0x05, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00);
        public readonly UL groupRelationshipObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x05, 0x00);
        public readonly UL identificationObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x06, 0x00);
        public readonly UL brandingObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x08, 0x00);
        public readonly UL eventObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x09, 0x00);
        public readonly UL awardObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x0b, 0x00);
        public readonly UL prodSettingPerObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x0e, 0x01);

        [Category(CATEGORYNAME)]
        public string IntegrationIndication { get; set; }

        public MXFProductionFramework(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack)
        {
            this.MetaDataName = "ProductionFramework";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            if (localTag.AliasUID != null)
            {
                switch (localTag.AliasUID)
                {
                    case var _ when localTag.AliasUID == integrationIndication_Key: this.IntegrationIndication = reader.ReadUTF16String(localTag.Length.Value); return true;
                    case var _ when localTag.AliasUID == groupRelationshipObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("GroupRelationshipObjects", "GroupRelationshipObject")); 
                        return true;
                    case var _ when localTag.AliasUID == identificationObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("IdentificationObjects", "IdentificationObject"));
                        return true;
                    case var _ when localTag.AliasUID == brandingObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("BrandingObjects", "BrandingObject"));
                        return true;
                    case var _ when localTag.AliasUID == eventObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("EventObjects", "EventObject"));
                        return true;
                    case var _ when localTag.AliasUID == awardObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("AwardObjects", "AwardObject"));
                        return true;
                    case var _ when localTag.AliasUID == prodSettingPerObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("ProductionSettingPeriodObjects", "ProductionSettingPeriodObject"));
                        return true;
                }
            }

            return base.ParseLocalTag(reader, localTag);
        }
    }
}
