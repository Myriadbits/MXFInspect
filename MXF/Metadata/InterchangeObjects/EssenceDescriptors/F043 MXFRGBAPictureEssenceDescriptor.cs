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
	public class MXFRGBAPictureEssenceDescriptor : MXFGenericPictureEssenceDescriptor
	{
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3406")]
		public UInt32? ComponentMaxRef { get; set; }
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3407")]
		public UInt32? ComponentMinRef { get; set; }
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3408")]
		public UInt32? AlphaMaxRef { get; set; }
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3409")]
		public UInt32? AlphaMinRef { get; set; }
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3405")]
		public MXFScanningDirectionType? ScanningDirection { get; set; }
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3401")]
		public MXFRGBAComponent[] PixelLayout { get; set; }
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3403")]
		public byte[] Palette { get; set; }
		[CategoryAttribute("RGBAPictureEssenceDescriptor"), Description("3404")]
		public MXFRGBAComponent[] PaletteLayout { get; set; }

		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFRGBAPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "RGBA Picture Essence Descriptor")
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
				case 0x3406: this.ComponentMaxRef = reader.ReadUInt32(); return true;
				case 0x3407: this.ComponentMinRef = reader.ReadUInt32(); return true;
				case 0x3408: this.AlphaMaxRef = reader.ReadUInt32(); return true;
				case 0x3409: this.AlphaMinRef = reader.ReadUInt32(); return true;
				case 0x3405: this.ScanningDirection = (MXFScanningDirectionType) reader.ReadByte(); return true;
				case 0x3401: this.PixelLayout = reader.ReadRGBALayout(); return true;
				case 0x3403: this.Palette = reader.ReadArray(reader.ReadByte, localTag.Size); return true; 
				case 0x3404: this.PaletteLayout = reader.ReadRGBALayout(); return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}

	}
}
