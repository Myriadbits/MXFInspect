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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011500")]
    public class MXFTimecodeStream : MXFSegment
    {
        private const string CATEGORYNAME = "TimecodeStream";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04040101.02010000")]
        public MXFRational TimecodeStreamSampleRate { get; private set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04040201.00000000")]
        public MXFTCSource? TimecodeSource { get; private set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04070300.00000000")]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] TimecodeStreamData { get; private set; }

        public MXFTimecodeStream(MXFReader reader, MXFKLV headerKLV, string metadataName)
            : base(reader, headerKLV, "TimecodeStream")
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
                case 0x1601: this.TimecodeStreamSampleRate = reader.ReadRational(); return true;
                case 0x1603: this.TimecodeSource = (MXFTCSource?)reader.ReadByte(); return true;
                case 0x1602: this.TimecodeStreamData = reader.ReadArray(reader.ReadByte, localTag.Size); return true;
            }

            return base.ParseLocalTag(reader, localTag);
        }

    }
}
