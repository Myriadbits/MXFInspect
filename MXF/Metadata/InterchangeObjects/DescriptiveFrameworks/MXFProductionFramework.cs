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

namespace Myriadbits.MXF
{
    public class MXFProductionFramework : MXFProductionClipFramework
    {
        private const string CATEGORYNAME = "ProductionFramework";

        public readonly MXFKey integrationIndication_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x04, 0x05, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00);
        public readonly MXFKey groupRelationshipObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x05, 0x00);
        public readonly MXFKey identificationObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x06, 0x00);
        public readonly MXFKey brandingObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x08, 0x00);
        public readonly MXFKey eventObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x09, 0x00);
        public readonly MXFKey awardObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x0b, 0x00);
        public readonly MXFKey prodSettingPerObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x0e, 0x01);

        [Category(CATEGORYNAME)]
        public string IntegrationIndication { get; set; }

        public MXFProductionFramework(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "ProductionFramework";
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
                    case var _ when localTag.Key == integrationIndication_Key: this.IntegrationIndication = reader.ReadUTF16String(localTag.Size); return true;
                    case var _ when localTag.Key == groupRelationshipObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("GroupRelationshipObjects", "GroupRelationshipObject")); 
                        return true;
                    case var _ when localTag.Key == identificationObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("IdentificationObjects", "IdentificationObject"));
                        return true;
                    case var _ when localTag.Key == brandingObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("BrandingObjects", "BrandingObject"));
                        return true;
                    case var _ when localTag.Key == eventObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("EventObjects", "EventObject"));
                        return true;
                    case var _ when localTag.Key == awardObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("AwardObjects", "AwardObject"));
                        return true;
                    case var _ when localTag.Key == prodSettingPerObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("ProductionSettingPeriodObjects", "ProductionSettingPeriodObject"));
                        return true;
                }
            }

            return base.ParseLocalTag(reader, localTag);
        }
    }
}
