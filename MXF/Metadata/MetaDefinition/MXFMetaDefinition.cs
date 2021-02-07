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
using System.Collections.Generic;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup(SMPTEULString = "urn:smpte:ul:060e2b34.027f0101.0d010101.02240000",
    Deprecated = false,
    IsConcrete = false,
    NumberOfElements = 3)]
    public class MXFMetaDefinition : MXFMetadataBaseclass
    {
        private const string CATEGORYNAME = "MetaDefinition";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03020401.02010000")]
        public string MetaDefinitionName { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010107.13000000")]
        public MXFKey MetaDefinitionIdentification { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010107.14010000")]
        public string MetaDefinitionDescription { get; set; }

        public MXFMetaDefinition(MXFReader reader, MXFKLV headerKLV, string metadataName)
            : base(reader, headerKLV, "MetaDefinition")
        {

        }

        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x0006: MetaDefinitionName = reader.ReadUTF16String(localTag.Size); return true;
                case 0x0005: MetaDefinitionIdentification = reader.ReadULKey(); return true;
                case 0x0007: MetaDefinitionDescription = reader.ReadUTF16String(localTag.Size); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
