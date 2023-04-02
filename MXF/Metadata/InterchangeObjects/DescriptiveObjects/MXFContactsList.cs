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
    public class MXFContactsList : MXFDescriptiveObject
    {
        public readonly UL personObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x14, 0x00);
        public readonly UL organizationObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x15, 0x00);
        public readonly UL locationObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x16, 0x00);

        public MXFContactsList(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "Contacts List";
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
                    case var _ when localTag.AliasUID == personObjects_Key:
                        localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("PersonObject", localTag.Offset, localTag.Length.Value));
                        return true;
                    case var _ when localTag.AliasUID == organizationObjects_Key: 
                        localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("OrganizationObject", localTag.Offset, localTag.Length.Value));
                        return true;
                    case var _ when localTag.AliasUID == locationObjects_Key: 
                        localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("LocationObject", localTag.Offset, localTag.Length.Value));
                        return true;
                }
            }

            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
