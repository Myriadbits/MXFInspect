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
	public class MXFGenericPackage : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "GenericPackage";

		[Category(CATEGORYNAME)]
		public MXFUMID PackageID { get; set; }

		[Category(CATEGORYNAME)]
		public string PackageName { get; set; }

		[Category(CATEGORYNAME)]
		public DateTime? ModifiedDate { get; set; }

		[Category(CATEGORYNAME)]
		public DateTime? CreationDate { get; set; }

		public MXFGenericPackage(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Generic Package")
		{
		}

		public MXFGenericPackage(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
				case 0x4401: this.PackageID = reader.ReadUMIDKey(); return true;
				case 0x4402: this.PackageName = reader.ReadUTF16String(localTag.Size); return true;
				//case 0x4403: ReadReferenceSet<MXFGenericTrack>(reader, "Tracks", "Track"); return true;
				case 0x4403: this.AddChild(reader.ReadReferenceSet<MXFGenericTrack>("Tracks", "Track")); return true;
				case 0x4404: this.ModifiedDate = reader.ReadTimestamp(); return true;
				case 0x4405: this.CreationDate = reader.ReadTimestamp(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
