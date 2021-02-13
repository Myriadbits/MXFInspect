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
	public class MXFTimecodeComponent : MXFSegment
	{
		private const string CATEGORYNAME = "TimecodeComponent";

		[Category(CATEGORYNAME)]
		[ULElement("urn:smpte:ul:060e2b34.01010102.07020103.01050000")]
		public MXFPositionType? StartTimecode { get; set; }
		
		[Category(CATEGORYNAME)]
		[ULElement("urn:smpte:ul:060e2b34.01010102.04040101.02060000")]
		public UInt16? FramesPerSecond { get; set; }
		
		[Category(CATEGORYNAME)]
		[ULElement("urn:smpte:ul:060e2b34.01010101.04040101.05000000")]
		public bool? DropFrame { get; set; }

		public MXFTimecodeComponent(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "TimeCodeComponent")
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
				case 0x1501: this.StartTimecode = reader.ReadUInt64(); return true;
				case 0x1502: this.FramesPerSecond = reader.ReadUInt16(); return true;
				case 0x1503: this.DropFrame = (reader.ReadByte() != 0); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
