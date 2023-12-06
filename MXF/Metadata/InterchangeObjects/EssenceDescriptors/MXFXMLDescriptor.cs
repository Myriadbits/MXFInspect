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

using Myriadbits.MXF.KLV;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01015400")]
    public class MXFXMLDescriptor : MXFSGMLDescriptor
    {
        private const string CATEGORYNAME = "XML Descriptor";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.01020104.01000000")]
        public string DefaultNamespaceURI { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.01020106.01000000")]
        public string[] NamespaceURIs { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.01030606.01000000")]
        public string[] NamespacePrefixes { get; set; }

        public MXFXMLDescriptor(MXFPack pack)
            : base(pack, "XML Descriptor")
        {
            MetaDataName = "XML Descriptor";
        }

        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x5401:
                    this.DefaultNamespaceURI = reader.ReadUTF16String(localTag.Length.Value);
                    localTag.Value = this.DefaultNamespaceURI;
                    return true;

                case 0x5402:
                    this.NamespaceURIs = reader.ReadUTF16String(localTag.Length.Value).Split((char)0x00);
                    localTag.Value = this.NamespaceURIs;
                    return true;

                case 0x5403:
                    this.NamespacePrefixes = reader.ReadUTF16String(localTag.Length.Value).Split((char)0x00);
                    localTag.Value = this.NamespacePrefixes;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }
    }
}
