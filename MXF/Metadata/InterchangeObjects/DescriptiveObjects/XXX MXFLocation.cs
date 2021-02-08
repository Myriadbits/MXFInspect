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
    public class MXFLocation : MXFContact
    {
        private const string CATEGORYNAME = "Location";

        public readonly MXFKey locDescription_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x04, 0x07, 0x01, 0x20, 0x02, 0x02, 0x01, 0x00, 0x00);
        public readonly MXFKey locKind_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x04, 0x07, 0x01, 0x20, 0x02, 0x03, 0x01, 0x00, 0x00);


        [Category(CATEGORYNAME)]
        public string LocationDescription { get; set; }

        [Category(CATEGORYNAME)]
        public string LocationKind { get; set; }

        public MXFLocation(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "Location";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            if (localTag.Key != null)
            {
                switch (localTag.Key)
                {
                    case var _ when localTag.Key == locDescription_Key: this.LocationDescription = reader.ReadUTF16String(localTag.Size); return true;
                    case var _ when localTag.Key == locKind_Key: this.LocationKind = reader.ReadUTF16String(localTag.Size); return true;
                }
            }

            return base.ParseLocalTag(reader, localTag);
        }

    }
}
