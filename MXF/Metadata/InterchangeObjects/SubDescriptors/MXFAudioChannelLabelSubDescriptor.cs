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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01016b00")]
	public class MXFAudioChannelLabelSubDescriptor : MXFMCALabelSubDescriptor
    {
        private const string CATEGORYNAME = "AudioChannelLabelSubDescriptor";
        private readonly UL SoundfieldGroupLinkId_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x01, 0x03, 0x07, 0x01, 0x06, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
		[ULElement("urn:smpte:ul:060e2b34.0101010e.01030701.06000000")]
		public UUID SoundfieldGroupLinkID { get; set; }
		
		public MXFAudioChannelLabelSubDescriptor(MXFPack pack)
			: base(pack)
		{
			this.MetaDataName = "AudioChannelLabelSubDescriptor";
		}

        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            if (localTag.AliasUID != null)
            {
                switch (localTag.AliasUID)
                {
                    case var _ when localTag.AliasUID == SoundfieldGroupLinkId_Key: 
                        this.SoundfieldGroupLinkID = reader.ReadUUID();
                        localTag.Value = this.SoundfieldGroupLinkID;
                        return true;
                }
            }
            return base.ReadLocalTagValue(reader, localTag);
        }
    }
}
