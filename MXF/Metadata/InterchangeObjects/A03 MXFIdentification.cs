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
	public class MXFIdentification : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "Identification";

		[Category(CATEGORYNAME)]
		public string CompanyName { get; set; }

		[Category(CATEGORYNAME)]
		public string ProductName { get; set; }

		[Category(CATEGORYNAME)]
		public MXFProductVersion ProductVersion { get; set; }

		[Category(CATEGORYNAME)]
		public string ProductVersionString { get; set; }

		[Category(CATEGORYNAME)]
		public MXFKey ProductUID { get; set; }

		[Category(CATEGORYNAME)]
		public DateTime? ModificationDate { get; set; }

		[Category(CATEGORYNAME)]
		public MXFProductVersion ToolkitVersion { get; set; }

		[Category(CATEGORYNAME)]
		public string Platform { get; set; }

		[Category(CATEGORYNAME)]
		public MXFKey ThisGenerationUID { get; set; }

		public MXFIdentification(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Identification")
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
				case 0x3C09: this.ThisGenerationUID = reader.ReadULKey(); return true;
				case 0x3C01: this.CompanyName = reader.ReadUTF16String(localTag.Size); return true;
				case 0x3C02: this.ProductName = reader.ReadUTF16String(localTag.Size); return true;
				case 0x3C03: this.ProductVersion = reader.ReadProductVersion(); return true;
				case 0x3C04: this.ProductVersionString = reader.ReadUTF16String(localTag.Size); return true;
				case 0x3C05: this.ProductUID = reader.ReadULKey(); return true;
				case 0x3C06: this.ModificationDate = reader.ReadTimestamp(); return true;
				case 0x3C07: this.ToolkitVersion = reader.ReadProductVersion(); return true;
				case 0x3C08: this.Platform = reader.ReadUTF16String(localTag.Size); return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}

	}
}
