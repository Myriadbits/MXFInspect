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

using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
	class MXFGenericTrack : MXFInterchangeObject
	{
		[CategoryAttribute("GenericTrack"), Description("4801")]
		public UInt32? TrackID { get; set; }
		[CategoryAttribute("GenericTrack"), Description("4802")]
		public UInt32? TrackNumber { get; set; }
		[CategoryAttribute("GenericTrack"), Description("4803")]
		public string TrackName { get; set; }
		[CategoryAttribute("GenericTrack"), Description("4804")]
		public MXFRefKey Sequence { get; set; }

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
				case 0x4801: this.TrackID = reader.ReadD(); return true;
				case 0x4802: this.TrackName = reader.ReadS(localTag.Size); return true;
				case 0x4803: this.Sequence = reader.ReadRefKey(); return true;
				case 0x4804: this.TrackNumber = reader.ReadD(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
