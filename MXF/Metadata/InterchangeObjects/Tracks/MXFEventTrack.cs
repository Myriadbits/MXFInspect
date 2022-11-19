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
	[ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01013900")]
	public class MXFEventTrack : MXFTrack
	{
		private const string CATEGORYNAME = "EventTrack";

		[Category(CATEGORYNAME)]
		[ULElement("urn:smpte:ul:060e2b34.01010102.05300402.00000000")]
		public MXFRational EventTrackEditRate { get; set; }
		
		[Category(CATEGORYNAME)]
		[ULElement("urn:smpte:ul:060e2b34.01010105.07020103.010b0000")]
		public MXFPosition? EventTrackOrigin { get; set; }

		public MXFEventTrack(IMXFReader reader, MXFPack pack)
			: base(reader, pack, "Event Track")
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(IMXFReader reader, MXFLocalTag localTag)
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
