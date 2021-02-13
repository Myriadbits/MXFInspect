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
	public class MXFEventTrack : MXFGenericTrack
	{
		private const string CATEGORYNAME = "EventTrack";

		[Category(CATEGORYNAME)]
		public MXFRational EventTrackEditRate { get; set; }
		[Category(CATEGORYNAME)]
		public MXFPositionType? EventTrackOrigin { get; set; }

		public MXFEventTrack(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Event Track")
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
				case 0x4901: this.EventTrackEditRate = reader.ReadRational(); return true;
				case 0x4902: this.EventTrackOrigin = reader.ReadUInt64(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
