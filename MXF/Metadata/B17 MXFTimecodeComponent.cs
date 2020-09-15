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
	public class MXFTimecodeComponent : MXFStructuralComponent
	{
		[CategoryAttribute("TimecodeComponent"), Description("1501")]
		public UInt64? StartTimecode { get; set; }
		[CategoryAttribute("TimecodeComponent"), Description("1502")]
		public UInt16? RoundedTimecodeBase { get; set; }
		[CategoryAttribute("TimecodeComponent"), Description("1503")]
		public bool? DropFrame { get; set; }

		public MXFTimecodeComponent(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Timecode Component")
		{
		}

		public MXFTimecodeComponent(MXFReader reader, MXFKLV headerKLV, string metadataName)
			: base(reader, headerKLV, metadataName)
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
				case 0x1501: this.StartTimecode = reader.ReadL(); return true;
				case 0x1502: this.RoundedTimecodeBase = reader.ReadW(); return true;
				case 0x1503: this.DropFrame = (reader.ReadB() != 0); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
