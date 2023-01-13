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

using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using Myriadbits.MXF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.02240000")]
    public class MXFMetaDefinition : MXFMetadataBaseclass, IUUIDIdentifiable
    {
        private const string CATEGORYNAME = "MetaDefinition";
        private const int CATEGORYPOS = 2;

        // TODO is not really part of MetaDefinition
        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        public UUID InstanceId { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03020401.02010000")]
        public string MetaDefinitionName { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010107.13000000")]
        public AUID MetaDefinitionIdentification { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010107.14010000")]
        public string MetaDefinitionDescription { get; set; }

        public MXFMetaDefinition(IKLVStreamReader reader, MXFPack pack, string metaDataName)
            : base(reader, pack, metaDataName)
        {
        }

        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x3c0a: InstanceId = reader.ReadUUID(); return true;
                case 0x0006: MetaDefinitionName = reader.ReadUTF16String(localTag.Length.Value); return true;
                case 0x0005: MetaDefinitionIdentification = reader.ReadAUID(); return true;
                case 0x0007: MetaDefinitionDescription = reader.ReadUTF16String(localTag.Length.Value); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

        public UUID GetUUID()
        {
            return this.InstanceId;
        }
    }
}
