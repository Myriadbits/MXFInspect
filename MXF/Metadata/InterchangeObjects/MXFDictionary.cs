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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012200")]
    public class MXFDictionary : MXFInterchangeObject
    {
        public MXFDictionary(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "Dictionary")
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
                case 0x2603:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("OperationDefinitions","OperationDefinitions")); 
                    return true;
                case 0x2604:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("ParameterDefinitions","ParameterDefinitions")); 
                    return true;
                case 0x2605:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("DataDefinitions",	"DataDefinitions")); 
                    return true;
                case 0x2606:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("PluginDefinitions","PluginDefinitions")); 
                    return true;
                case 0x2607:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("CodecDefinitions","CodecDefinitions")); 
                    return true;
                case 0x2608:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("ContainerDefinitions","ContainerDefinitions")); 
                    return true;
                case 0x2609:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("InterpolationDefinitions","InterpolationDefinitions")); 
                    return true;
                case 0x260a:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("KLVDataDefinitions","KLVDataDefinitions")); 
                    return true;
                case 0x260b:
                    this.AddChild(reader.ReadReferenceSet<MXFObject>("TaggedValueDefinitions","TaggedValueDefinitions")); 
                    return true;

            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
