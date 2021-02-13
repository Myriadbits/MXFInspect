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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.02020000")]
    public class MXFPropertyDefinition : MXFMetaDefinition
    {
        private const string CATEGORYNAME = "PropertyDefinition";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03010202.00000000")]
        public bool IsOptional { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03010202.03000000")]
        public MXFKey PropertyType { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010c.06010104.03060000")]
        public UInt16 LocalIdentification { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.06010107.20000000")]
        public bool IsUniqueIdentifier { get; set; }

        public MXFPropertyDefinition(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "PropertyDefinition")
        {
            this.MetaDataName = "PropertyDefinition";
        }


        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x000c: IsOptional = reader.ReadBool(); return true;
                case 0x000b: PropertyType = reader.ReadULKey(); return true;
                case 0x000d: LocalIdentification = reader.ReadUInt16(); return true;
                case 0x000e: IsUniqueIdentifier = reader.ReadBool(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
