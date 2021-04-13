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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.020d0000")]
    public class MXFTypeDefinitionRecord : MXFTypeDefinition
    {
        private const string CATEGORYNAME = "TypeDefinitionRecord";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03010203.06000000")]
        public string[] MemberNames { get; set; }

        public MXFTypeDefinitionRecord(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "TypeDefinitionRecord";
        }


        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x001d: MemberNames = reader.ReadUTF16String(localTag.Size).Split((char)0x00); return true;
                case 0x001c: this.AddChild(reader.ReadReferenceSet<MXFTypeDefinition>("MemberTypes", "MemberType")); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
