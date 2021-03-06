﻿//
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

using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
	public class MXFEntryPrimer : MXFObject
	{
		[CategoryAttribute("PrimerEntry"), ReadOnly(true)]
		public UInt16 LocalTag { get; set; }
		[CategoryAttribute("PrimerEntry"), ReadOnly(true)]
		public MXFRefKey AliasUID { get; set; }

		public MXFEntryPrimer(MXFReader reader)
			: base(reader)
		{
			this.Offset = reader.Position;
			this.LocalTag = reader.ReadW();
			this.AliasUID = new MXFRefKey(reader, 16, "AliasUID");
			this.Length = 20; // Fixed length
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("Localtag 0x{0:X4} -> {1}", this.LocalTag, this.AliasUID.ToString());
		}
	}
}
