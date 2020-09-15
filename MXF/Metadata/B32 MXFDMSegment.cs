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
	public class MXFDMSegment : MXFEvent
	{
		[CategoryAttribute("DMSegment"), Description("6102")]
		public UInt64? Track { get; set; }
		[CategoryAttribute("DMSegment"), Description("6101")]
		public MXFRefKey DMFramework { get; set; }

		public MXFDMSegment(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "DM Segment")
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
				case 0x6102: ReadKeyList(reader, "TrackIDs", "TrackID"); return true;
				case 0x6101: this.DMFramework = reader.ReadRefKey(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
