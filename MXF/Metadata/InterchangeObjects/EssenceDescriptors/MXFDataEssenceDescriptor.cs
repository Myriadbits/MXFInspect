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

using System.ComponentModel;
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01014300")]
    public class MXFDataEssenceDescriptor : MXFFileDescriptor
    {
        private const string CATEGORYNAME = "DataEssenceDescriptor";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010103.04030302.00000000")]
        public UL DataEssenceCoding { get; set; }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFDataEssenceDescriptor(MXFPack pack)
            : base(pack, "Data Essence Descriptor")
        {
            // TODO remove code, once implemented the subclasses
            if (pack.Key[14] == 0x5B)
                this.Key.Name = "VBI Data Descriptor";
            if (pack.Key[14] == 0x5C)
                this.Key.Name = "ANC Data Descriptor";
            this.MetaDataName = this.Key.Name;
        }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="pack"></param>
        public MXFDataEssenceDescriptor(MXFPack pack, string metadataName)
            : base(pack, metadataName)
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
                case 0x3E01: this.DataEssenceCoding = reader.ReadUL(); return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
