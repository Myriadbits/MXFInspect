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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01015e00")]
    public class MXFMPEGAudioDescriptor : MXFSoundEssenceDescriptor
    {
        private const string CATEGORYNAME = "MPEG AudioDescriptor";

        private readonly UL audioBitRate_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0a, 0x04, 0x02, 0x04, 0x03, 0x01, 0x02, 0x00, 0x00);
        private readonly UL channelAssignment_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x02, 0x04, 0x03, 0x01, 0x05, 0x00, 0x00);


        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010a.04020403.01020000")]
        public UInt32 MPEGAudioBitRate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04020403.01050000")]
        public UL MPEGAudioChannelAssignment { get; set; }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFMPEGAudioDescriptor(MXFPack pack)
            : base(pack, "MPEG AudioDescriptor")
        {
        }

        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case var _ when localTag.AliasUID == audioBitRate_Key: this.MPEGAudioBitRate = reader.ReadUInt32(); return true;
                case var _ when localTag.AliasUID == channelAssignment_Key: this.MPEGAudioChannelAssignment = reader.ReadUL(); return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }
    }
}
