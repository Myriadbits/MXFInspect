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
    public class MXFDynamicMarker : MXFDescriptiveMarker
    {
        private const string CATEGORYNAME = "DynamicMarker";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.07020501.00000000")]
        public MXFToleranceModeType ToleranceMode { get; set; }

        //TODO this is of type "indirect"
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010109.07020502.00000000")]
        public object ToleranceWindow { get; set; }

        public MXFDynamicMarker(MXFReader reader, MXFKLV headerKLV)
        : base(reader, headerKLV)
        {
            this.MetaDataName = "DynamicMarker";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x5701: this.ToleranceMode = (MXFToleranceModeType)reader.ReadByte(); return true;
                case 0x5703: this.ToleranceWindow = reader.ReadArray<byte>(reader.ReadByte, localTag.Size); return true;
                // TODO replace generic MXFObject with class ApplicationPluginObject once implemented
                case 0x5702: this.AddChild(reader.ReadReference<MXFObject>("InterpolationDefinition")); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
