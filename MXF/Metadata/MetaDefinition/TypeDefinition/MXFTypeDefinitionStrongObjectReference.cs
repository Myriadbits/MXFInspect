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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.02050000")]
    public class MXFTypeDefinitionStrongObjectReference : MXFTypeDefinition
    {
        private const string CATEGORYNAME = "TypeDefinitionStrongObjectReference";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010107.09000000")]
        public MXFKey ReferencedType { get; set; }

        public MXFTypeDefinitionStrongObjectReference(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "TypeDefinitionStrongObjectReference ";
        }


        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x0011: ReferencedType = reader.ReadULKey(); return true;
                // TODO maybe this is more appropriate?
                //case 0x0011: this.AddChild(reader.ReadReference<MXFTypeDefinition>("ReferencedType")); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
