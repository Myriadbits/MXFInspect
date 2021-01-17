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
    public class MXFOperationGroup : MXFSegment
    {
        [Category("OperationGroup")]
        [UL("urn:smpte:ul:060e2b34.01010102.0530050c.00000000")]
        public UInt32 BypassOverride { get; private set; }

        public MXFOperationGroup(MXFReader reader, MXFKLV headerKLV, string metadataName)
            : base(reader, headerKLV, "OperationGroup")
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                // TODO replace generic MXFObject with class OperationDefinition once implemented
                case 0x0B01: this.ReadReference<MXFObject>(reader, "Operation"); return true;
                case 0x0B04: this.BypassOverride = reader.ReadUInt32(); return true;
                case 0x0B05: this.ReadReference<MXFSourceReference>(reader, "Rendering"); return true;
                case 0x0B02: this.ReadReferenceSet<MXFSegment>(reader, "InputSegments", "InputSegment"); return true;
                case 0x0B03: this.ReadReferenceSet<MXFParameter>(reader, "Parameters", "Parameter"); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
