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
using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011c00")]
    public class MXFOperationDefinition : MXFDefinitionObject
    {
        private const string CATEGORYNAME = "OperationDefinition";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.05300503.00000000")]
        public bool IsTimeWarp { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.05300504.00000000")]
        public Int32 OperationInputCount { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.05300505.00000000")]
        public UInt32 Bypass { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.0530050a.00000000")]
        public AUID OperationCategory { get; set; }

        public MXFOperationDefinition(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "OperationDefinition";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x1e02: 
                    IsTimeWarp = reader.ReadBoolean();
                    localTag.Value = IsTimeWarp;
                    return true;
                case 0x1e07: 
                    OperationInputCount = reader.ReadInt32();
                    localTag.Value = OperationInputCount;
                    return true;
                case 0x1e08: 
                    Bypass = reader.ReadUInt32();
                    localTag.Value = Bypass;
                    return true;
                case 0x1e01:
                    localTag.AddChild(reader.ReadReference<MXFDataDefinition>("OperationDataDefinition", localTag.Offset));
                    return true;
                case 0x1e06: 
                    OperationCategory = reader.ReadAUID();
                    localTag.Value = OperationCategory;
                    return true;
                case 0x1e09:
                    localTag.AddChildren(reader.GetReferenceSet<MXFParameterDefinition>("OperationParametersDefined", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x1e03:
                    localTag.AddChildren(reader.GetReferenceSet<MXFOperationDefinition>("DegradeTo", localTag.Offset, localTag.Length.Value));
                    return true;

            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
