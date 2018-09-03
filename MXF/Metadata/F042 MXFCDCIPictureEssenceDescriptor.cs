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
	public class MXFCDCIPictureEssenceDescriptor : MXFGenericPictureEssenceDescriptor
	{
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3301")]
		public UInt32? ComponentDepth { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3302")]
		public UInt32? HorizontalSubsampling { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3308")]
		public UInt32? VerticalSubsampling { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3303")]
		public byte? ColorSiting { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("330B")]
		public bool? ReversedByteOrder { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3307")]
		public Int16? PaddingBits { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3309")]
		public UInt32? AlphaSampleDepth { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3304")]
		public UInt32? BlackRefLevel { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3305")]
		public UInt32? WhiteRefLevel { get; set; }
		[CategoryAttribute("CDCIPictureEssenceDescriptor"), Description("3306")]
		public UInt32? ColorRange { get; set; }

		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFCDCIPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "CDCI Picture Essence Descriptor")
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
				case 0x3301: this.ComponentDepth = reader.ReadD(); return true;
				case 0x3302: this.HorizontalSubsampling = reader.ReadD(); return true;
				case 0x3308: this.VerticalSubsampling = reader.ReadD(); return true;
				case 0x3303: this.ColorSiting = reader.ReadB(); return true;
				case 0x330B: this.ReversedByteOrder = reader.ReadBool(); return true;
				case 0x3307: this.PaddingBits = (Int16) reader.ReadW(); return true;
				case 0x3309: this.AlphaSampleDepth = reader.ReadD(); return true;
				case 0x3304: this.BlackRefLevel = reader.ReadD(); return true;
				case 0x3305: this.WhiteRefLevel = reader.ReadD(); return true;
				case 0x3306: this.ColorRange = reader.ReadD(); return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}

	}
}
