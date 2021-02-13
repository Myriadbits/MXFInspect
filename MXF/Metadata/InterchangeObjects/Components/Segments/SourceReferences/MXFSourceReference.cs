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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011000")]
    public class MXFSourceReference : MXFSegment
    {
        private const string CATEGORYNAME = "SourceReference";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010103.01000000")]
        public MXFUMID SourcePackageID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010103.02000000")]
        public UInt32? SourceTrackId { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010107.06010103.07000000")]
        [TypeConverter(typeof(IntegerArrayConverter))]
        public UInt32[] ChannelIDs { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010108.06010103.08000000")]
        [TypeConverter(typeof(IntegerArrayConverter))]
        public UInt32[] MonoSourceTrackIDs { get; set; }

        public MXFSourceReference(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "SourceReference")
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
                case 0x1101: this.SourcePackageID = reader.ReadUMIDKey(); return true;
                case 0x1102: this.SourceTrackId = reader.ReadUInt32(); return true;
                case 0x1103: this.ChannelIDs = reader.ReadArray(reader.ReadUInt32, localTag.Size); return true;
                case 0x1104: this.MonoSourceTrackIDs = reader.ReadArray(reader.ReadUInt32, localTag.Size); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
