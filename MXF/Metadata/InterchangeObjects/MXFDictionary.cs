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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012200")]
    public class MXFDictionary : MXFInterchangeObject
    {
        public MXFDictionary(MXFPack pack)
            : base(pack, "Dictionary")
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x2603:
                    localTag.AddChildren(reader.GetReferenceSet<MXFOperationDefinition>("OperationDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x2604:
                    localTag.AddChildren(reader.GetReferenceSet<MXFParameterDefinition>("ParameterDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x2605:
                    localTag.AddChildren(reader.GetReferenceSet<MXFDataDefinition>("DataDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x2606:
                    localTag.AddChildren(reader.GetReferenceSet<MXFObject>("PluginDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x2607:
                    localTag.AddChildren(reader.GetReferenceSet<MXFCodecDefinition>("CodecDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x2608:
                    localTag.AddChildren(reader.GetReferenceSet<MXFContainerDefinition>("ContainerDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x2609:
                    localTag.AddChildren(reader.GetReferenceSet<MXFInterpolationDefinition>("InterpolationDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x260a:
                    localTag.AddChildren(reader.GetReferenceSet<MXFObject>("KLVDataDefinition", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x260b:
                    localTag.AddChildren(reader.GetReferenceSet<MXFObject>("TaggedValueDefinition", localTag.Offset, localTag.Length.Value));
                    return true;

            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
