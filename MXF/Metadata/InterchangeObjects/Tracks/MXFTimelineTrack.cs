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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public class MXFTimelineTrack : MXFTrack
	{
		private const string CATEGORYNAME = "TimeLineTrack";

		[Category(CATEGORYNAME)]
		public MXFRational EditRate { get; set; }
		[Category(CATEGORYNAME)]
		public MXFPosition? Origin { get; set; }
		[Category(CATEGORYNAME)]
		public MXFPosition? MarkIn { get; set; }
		[Category(CATEGORYNAME)]
		public MXFPosition? UserPosition { get; set; }
		[Category(CATEGORYNAME)]
		public MXFPosition? PackageMarkInPosition { get; set; }
		[Category(CATEGORYNAME)]
		public MXFPosition? MarkOut { get; set; }
		[Category(CATEGORYNAME)]
		public MXFPosition? PackageMarkOutPosition { get; set; }

		public MXFTimelineTrack(MXFPack pack)
			: base(pack, "Timeline Track")
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
		{
			switch (localTag.TagValue)
			{
				case 0x4B01: this.EditRate = reader.ReadRational(); return true;
				case 0x4B02: this.Origin = reader.ReadUInt64(); return true;
				case 0x4B03: this.MarkIn = reader.ReadUInt64(); return true;
				case 0x4B05: this.UserPosition = reader.ReadUInt64(); return true;
				case 0x4B06: this.PackageMarkInPosition = reader.ReadUInt64(); return true;
				case 0x4B04: this.MarkOut = reader.ReadUInt64(); return true;
				case 0x4B07: this.PackageMarkOutPosition = reader.ReadUInt64(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
