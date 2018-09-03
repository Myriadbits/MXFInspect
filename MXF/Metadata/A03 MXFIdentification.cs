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
	class MXFIdentification : MXFInterchangeObject
	{
		[CategoryAttribute("Identification"), Description("3C01")]
		public string CompanyName { get; set; }
		[CategoryAttribute("Identification"), Description("3C02")]
		public string ProductName { get; set; }
		[CategoryAttribute("Identification"), Description("3C03")]
		public UInt16[] ProductVersion { get; set; }
		[CategoryAttribute("Identification"), Description("3C04")]
		public string ProductVersionString { get; set; }
		[CategoryAttribute("Identification"), Description("3C05")]
		public MXFKey ProductUID { get; set; }
		[CategoryAttribute("Identification"), Description("3C06")]
		public DateTime? ModificationDate { get; set; }
		[CategoryAttribute("Identification"), Description("3C07")]
		public UInt16[] ToolkitVersion { get; set; }
		[CategoryAttribute("Identification"), Description("3C08")]
		public string Platform { get; set; }
		[CategoryAttribute("Identification"), Description("3C09")]
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
				case 0x3C09: this.ThisGenerationUID = reader.ReadKey(); return true;
				case 0x3C01: this.CompanyName = reader.ReadS(localTag.Size); return true;
				case 0x3C02: this.ProductName = reader.ReadS(localTag.Size); return true;
				case 0x3C03: this.ProductVersion = reader.ReadVersion(); return true;
				case 0x3C04: this.ProductVersionString = reader.ReadS(localTag.Size); return true;
				case 0x3C05: this.ProductUID = reader.ReadKey(); return true;
				case 0x3C06: this.ModificationDate = reader.ReadTimestamp(); return true;
				case 0x3C07: this.ToolkitVersion = reader.ReadVersion(); return true;
				case 0x3C08: this.Platform = reader.ReadS(localTag.Size); return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}

	}
}
