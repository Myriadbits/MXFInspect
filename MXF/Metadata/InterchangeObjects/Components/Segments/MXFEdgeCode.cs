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

namespace Myriadbits.MXF
{
    public class MXFEdgeCode : MXFSegment
    {
        private const string CATEGORYNAME = "EdgeCode";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.01030201.02000000")]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] EdgeCodeHeader { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.01040901.00000000")]
        public MXFPositionType? EdgeCodeStart { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04100103.01020000")]
        public MXFEdgeType? EdgeCodeFormat { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04100103.01090000")]
        public MXFFilmType? EdgeCodeFilmFormat { get; set; }

        public MXFEdgeCode(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "EdgeCode")
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
                case 0x0404: this.EdgeCodeHeader = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
                case 0x0401: this.EdgeCodeStart = reader.ReadUInt64(); return true;
                case 0x0403: this.EdgeCodeFormat = (MXFEdgeType)reader.ReadUInt16(); return true;
                case 0x0402: this.EdgeCodeFilmFormat = (MXFFilmType)reader.ReadUInt16(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
