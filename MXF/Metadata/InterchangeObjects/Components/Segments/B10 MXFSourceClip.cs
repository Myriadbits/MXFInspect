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
	public class MXFSourceClip : MXFStructuralComponent
	{
		[CategoryAttribute("SourceClip"), Description("1201")]
		public UInt64? StartPosition { get; set; }
		[CategoryAttribute("SourceClip"), Description("1101")]
		public MXFUMID SourcePackageID { get; set; }
		[CategoryAttribute("SourceClip"), Description("1102")]
		public UInt32? SourceTrackId { get; set; }

		public MXFSourceClip(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "SourceClip")
		{
		}

		public MXFSourceClip(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
				case 0x1201: this.StartPosition = reader.ReadUInt64(); return true;
				case 0x1101: this.SourcePackageID = reader.ReadUMIDKey(); return true;
				case 0x1102: this.SourceTrackId = reader.ReadUInt32(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
