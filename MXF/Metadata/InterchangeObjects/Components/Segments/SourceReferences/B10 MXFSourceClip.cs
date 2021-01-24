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
    public class MXFSourceClip : MXFSourceReference
    {
        private const string CATEGORYNAME = "SourceClip";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020103.01040000")]
        public MXFPositionType? StartPosition { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020201.01050200")]
        public MXFLengthType? FadeInLength { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.05300501.00000000")]
        public MXFFadeType? FadeInType { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020201.01050300")]
        public MXFLengthType? FadeOutLength { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.05300502.00000000")]
        public MXFFadeType? FadeOutType { get; set; }

        public MXFSourceClip(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "SourceClip";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x1201: this.StartPosition = reader.ReadUInt64(); return true;
                case 0x1202: this.FadeInLength = reader.ReadUInt64(); return true;
                case 0x1203: this.FadeInType = (MXFFadeType?) reader.ReadByte(); return true;
                case 0x1204: this.FadeOutLength = reader.ReadUInt64(); return true;
                case 0x1205: this.FadeOutType = (MXFFadeType?) reader.ReadByte(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
