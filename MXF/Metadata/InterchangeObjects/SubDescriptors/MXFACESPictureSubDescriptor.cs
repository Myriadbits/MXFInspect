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

namespace Myriadbits.MXF
{
    public class MXFACESPictureSubDescriptor : MXFSubDescriptor
    {
        private const string CATEGORYNAME = "ACESPictureSubDescriptor";

        private readonly MXFKey aCESAuthoringInformation_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x01, 0x00, 0x00, 0x00);
        private readonly MXFKey aCESMasteringDisplayPrimaries_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x02, 0x00, 0x00, 0x00);
        private readonly MXFKey aCESMasteringDisplayWhitePointChromaticity_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x03, 0x00, 0x00, 0x00);
        private readonly MXFKey aCESMasteringDisplayMaximumLuminance_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x04, 0x00, 0x00, 0x00);
        private readonly MXFKey aCESMasteringDisplayMinimumLuminance_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x01, 0x06, 0x0a, 0x05, 0x00, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        public string ACESAuthoringInformation { get; set; }

        [Category(CATEGORYNAME)]
        public MXFColorPrimary[] ACESMasteringDisplayPrimaries { get; set; }

        [Category(CATEGORYNAME)]
        public MXFColorPrimary ACESMasteringDisplayWhitePointChromaticity { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? ACESMasteringDisplayMaximumLuminance { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? ACESMasteringDisplayMinimumLuminance { get; set; }

        public MXFACESPictureSubDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "ACESPictureSubDescriptor")
        {
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
                    case var _ when localTag.Key == aCESAuthoringInformation_Key: this.ACESAuthoringInformation = reader.ReadUTF16String(localTag.Size); return true;
                    case var _ when localTag.Key == aCESMasteringDisplayPrimaries_Key: this.ACESMasteringDisplayPrimaries = reader.ReadArray(reader.ReadColorPrimary, 3); return true;
                    case var _ when localTag.Key == aCESMasteringDisplayWhitePointChromaticity_Key: this.ACESMasteringDisplayWhitePointChromaticity = reader.ReadColorPrimary(); return true;
                    case var _ when localTag.Key == aCESMasteringDisplayMaximumLuminance_Key: this.ACESMasteringDisplayMaximumLuminance = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == aCESMasteringDisplayMinimumLuminance_Key: this.ACESMasteringDisplayMinimumLuminance = reader.ReadUInt32(); return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
