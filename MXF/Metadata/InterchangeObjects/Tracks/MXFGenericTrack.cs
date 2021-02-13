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
	public class MXFGenericTrack : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "GenericTrack";

		[Category(CATEGORYNAME)]
		public UInt32? TrackID { get; set; }

		[Category(CATEGORYNAME)]
		public UInt32? TrackNumber { get; set; }

		[Category(CATEGORYNAME)]
		public string TrackName { get; set; }


		public MXFGenericTrack(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Generic Track")
		{
		}


		public MXFGenericTrack(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
				case 0x4801: this.TrackID = reader.ReadUInt32(); return true;
				case 0x4802: this.TrackName = reader.ReadUTF16String(localTag.Size); return true;
				case 0x4803: this.AddChild(reader.ReadReference<MXFSegment>("Segment")); return true;
				case 0x4804: this.TrackNumber = reader.ReadUInt32(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
