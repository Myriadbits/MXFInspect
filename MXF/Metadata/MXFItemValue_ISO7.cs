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
	public class MXFItemValue_ISO7 : MXFPack
	{
        private const string CATEGORYNAME = "Item Value ISO7";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.0301020a.02000000")]
        public string ItemValue_ISO7 { get; set; }

        public MXFItemValue_ISO7(IMXFReader reader, MXFPack pack)
            : base(pack)
        {
            this.Key.Name ??= "Item Value ISO7";
            Initialize(reader);
        }

        private void Initialize(IMXFReader reader)
        {
            // Make sure we read at the data position
            reader.Seek(this.RelativeValueOffset);
            ItemValue_ISO7 = reader.ReadUTF8String((int)this.Length.Value);
        }
    }
}
