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
    public class MXFDynamicMarker : MXFDescriptiveMarker
    {
        [CategoryAttribute("DynamicMarker"), Description("")]
        public MXFToleranceModeType ToleranceMode { get; set; }
        [CategoryAttribute("DynamicMarker"), Description("")]

        //TODO this is of type "indirect"
        public int ToleranceWindow { get; set; }

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
                case 0x5703: /* TODO implement property */ return true;

                // TODO replace generic MXFObject with class ApplicationPluginObject once implemented
                case 0x5702: ReadReference<MXFObject>(reader, "InterpolationDefinition"); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
