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
using System.Collections.Generic;
using System.ComponentModel;

namespace Myriadbits.MXF
{
	public class MXFItemValue_ISO7 : MXFKLV
	{
        private const string CATEGORYNAME = "Item Value ISO7";

        [Category(CATEGORYNAME)]
		public string ItemValue_ISO7 { get; set; }

        public MXFItemValue_ISO7(MXFReader reader, MXFKLV headerKLV)
            : base(headerKLV, "Item Value ISO7", KeyType.MetaData)
        {
            this.m_eType = MXFObjectType.Meta;
            Initialize(reader);
        }

        private void Initialize(MXFReader reader)
        {
            // Make sure we read at the data position
            reader.Seek(this.DataOffset);
            ItemValue_ISO7 = reader.ReadUTF8String((int)this.Length);
        }
    }
}
