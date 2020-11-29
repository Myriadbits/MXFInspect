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
    public class MXFSourceReference : MXFSegment
    {
        [CategoryAttribute("SourceReference"), Description("1101")]
        public MXFUMID SourcePackageID { get; set; }
        [CategoryAttribute("SourceReference"), Description("1102")]
        public UInt32? SourceTrackId { get; set; }
        [CategoryAttribute("SourceReference"), Description("1102")]
        public UInt32[] ChannelIDs { get; set; }
        [CategoryAttribute("SourceReference"), Description("1102")]
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
