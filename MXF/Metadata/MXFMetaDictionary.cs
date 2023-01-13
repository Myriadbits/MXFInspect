﻿#region license
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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.02250000")]
    public class MXFMetaDictionary : MXFMetadataBaseclass, IUUIDIdentifiable
    {
        private const string CATEGORYNAME = "MetaDictionary";

        // TODO is not really part of MetaDictionary
        [Category(CATEGORYNAME)]
        public UUID InstanceId { get; set; }

        public MXFMetaDictionary(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "MetaDictionary")
        {
        }


        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x3c0a: this.InstanceId = reader.ReadUUID(); return true;
                case 0x0003:
                    this.AddChild(reader.ReadReferenceSet<MXFClassDefinition>("ClassDefinitions", "ClassDefinition")); return true;
                case 0x0004:
                    this.AddChild(reader.ReadReferenceSet<MXFTypeDefinition>("TypeDefinitions", "TypeDefinition")); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

        public UUID GetUUID()
        {
            return this.InstanceId;
        }
    }
}
