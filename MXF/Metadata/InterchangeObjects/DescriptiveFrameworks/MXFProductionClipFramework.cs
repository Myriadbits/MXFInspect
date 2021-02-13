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
    public class MXFProductionClipFramework : MXFDMS1Framework
    {
        public readonly MXFKey pictureFormatObject_Key = new MXFKey(0x06,0x0e,0x2,0xb,0x34,0x01,0x01,0x01,0x05,0x06,0x01,0x01,0x04,0x02,0x40,0x1d,0x00);
        public readonly MXFKey projectObject_Key = new MXFKey(0x06,0x0e,0x2,0xb,0x34,0x01,0x01,0x01,0x05,0x06,0x01,0x01,0x04,0x02,0x40,0x21,0x00);
        public readonly MXFKey captionsDescriptionObjects_Key = new MXFKey(0x06,0x0e,0x2,0xb,0x34,0x01,0x01,0x01,0x05,0x06,0x01,0x01,0x04,0x05,0x40,0x0c,0x00);
        public readonly MXFKey contractObjects_Key = new MXFKey(0x06,0x0e,0x2,0xb,0x34,0x01,0x01,0x01,0x05,0x06,0x01,0x01,0x04,0x05,0x40,0x19,0x00);

        public MXFProductionClipFramework(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "ProductionClipFramework";
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
                    case var _ when localTag.Key == pictureFormatObject_Key:
                        this.AddChild(reader.ReadReference<MXFDescriptiveObject>("PictureFormatObject"));
                        return true;
                    case var _ when localTag.Key == projectObject_Key:
                        this.AddChild(reader.ReadReference<MXFDescriptiveObject>("ProjectObject"));
                        return true;
                    case var _ when localTag.Key == captionsDescriptionObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("CaptionsDescriptionObjects", "CaptionsDescriptionObject"));
                        return true;
                    case var _ when localTag.Key == contractObjects_Key:
                        this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("ContractObjects", "ContractObject"));
                        return true;
                }
            }

            return base.ParseLocalTag(reader, localTag);
        }
    }
}
