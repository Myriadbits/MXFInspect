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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01010400")]
    public class MXFEdgeCode : MXFSegment
    {
        private const string CATEGORYNAME = "EdgeCode";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.01030201.02000000")]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] EdgeCodeHeader { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.01040901.00000000")]
        public MXFPosition? EdgeCodeStart { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04100103.01020000")]
        public MXFEdge? EdgeCodeFormat { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04100103.01090000")]
        public MXFFilm? EdgeCodeFilmFormat { get; set; }

        public MXFEdgeCode(MXFPack pack)
            : base(pack, "EdgeCode")
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
                case 0x0404: this.EdgeCodeHeader = reader.ReadBytes((int)localTag.Length.Value); return true;
                case 0x0401: this.EdgeCodeStart = reader.ReadUInt64(); return true;
                case 0x0403: this.EdgeCodeFormat = (MXFEdge)reader.ReadUInt16(); return true;
                case 0x0402: this.EdgeCodeFilmFormat = (MXFFilm)reader.ReadUInt16(); return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
