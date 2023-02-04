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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011f00")]
    public class MXFCodecDefinition : MXFDefinitionObject
    {
        public MXFCodecDefinition(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "CodecDefinition";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x2301:
                    localTag.AddChild(reader.ReadReference<MXFClassDefinition>("FileDescriptorClass", localTag.Offset));
                    return true;
                case 0x2302:
                    localTag.AddChildren(reader.GetReferenceSet<MXFDataDefinition>("CodecDataDefinitions", localTag.Offset, localTag.Length.Value));
                    return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
