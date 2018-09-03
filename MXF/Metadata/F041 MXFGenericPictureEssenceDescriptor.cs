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
	public class MXFGenericPictureEssenceDescriptor : MXFFileDescriptor
	{
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3215")]
		public Byte? SignalStandard { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320C")]
		public Byte? FrameLayout { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3203")]
		public UInt32? StoredWidth { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3202")]
		public UInt32? StoredHeight { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3216")]
		public Int32? StoredF2Offset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3205")]
		public UInt32? SampledWidth { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3204")]
		public UInt32? SampledHeight { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3206")]
		public Int32? SampledXOffset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3207")]
		public Int32? SampledYOffset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3209")]
		public UInt32? DisplayHeight { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3208")]
		public UInt32? DisplayWidth { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320A")]
		public Int32? DisplayXOffset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320B")]
		public Int32? DisplayYOffset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3217")]
		public Int32? DisplayF2Offset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320E")]
		public MXFRational AspectRatio { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3218")]
		public byte? ActiveFormatDescriptor { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320D")]
		public Int32[] VideoLineMap { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("320F")]
		public byte? AlphaTransparency { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3210")]
		public MXFKey TransferCharacteristics { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3211")]
		public UInt32? ImageAlignmentOffset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3213")]
		public UInt32? ImageStartOffset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3214")]
		public UInt32? ImageEndOffset { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3212")]
		public byte? FieldDominance { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3201")]
		public MXFKey PictureEssenceCoding { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("321A")]
		public MXFKey CodingEquations { get; set; }
		[CategoryAttribute("GenericPictureEssenceDescriptor"), Description("3219")]
		public MXFKey ColorPrimaries { get; set; }
		
		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFGenericPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Generic Picture Essence Descriptor")
		{
		}

		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFGenericPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
				case 0x3215: this.SignalStandard = reader.ReadB(); return true;
				case 0x320C: this.FrameLayout = reader.ReadB(); return true;
				case 0x3203: this.StoredWidth = reader.ReadD(); return true;
				case 0x3202: this.StoredHeight = reader.ReadD(); return true;
				case 0x3216: this.StoredF2Offset = (Int32) reader.ReadD(); return true;
				case 0x3205: this.SampledWidth = reader.ReadD(); return true;
				case 0x3204: this.SampledHeight = reader.ReadD(); return true;
				case 0x3206: this.SampledXOffset = (Int32) reader.ReadD(); return true;
				case 0x3207: this.SampledYOffset = (Int32) reader.ReadD(); return true;
				case 0x3208: this.DisplayWidth = reader.ReadD(); return true;
				case 0x3209: this.DisplayHeight = reader.ReadD(); return true;
				case 0x320A: this.DisplayXOffset = (Int32)reader.ReadD(); return true;
				case 0x320B: this.DisplayYOffset = (Int32)reader.ReadD(); return true;
				case 0x3217: this.DisplayF2Offset = (Int32)reader.ReadD(); return true;
				case 0x320E: this.AspectRatio = reader.ReadRational(); return true;
				case 0x3218: this.ActiveFormatDescriptor = reader.ReadB(); return true;
				case 0x320D: 
					this.VideoLineMap = new Int32[4];
					this.VideoLineMap[0] = (Int32) reader.ReadD();
					this.VideoLineMap[1] = (Int32) reader.ReadD();
					this.VideoLineMap[2] = (Int32) reader.ReadD();
					this.VideoLineMap[3] = (Int32) reader.ReadD();
					return true;
				case 0x320F: this.AlphaTransparency = reader.ReadB(); return true;
				case 0x3210: this.TransferCharacteristics = reader.ReadKey(); return true;
				case 0x3211: this.ImageAlignmentOffset = reader.ReadD(); return true;
				case 0x3213: this.ImageStartOffset= reader.ReadD(); return true;
				case 0x3214: this.ImageEndOffset = reader.ReadD(); return true;
				case 0x3212: this.FieldDominance = reader.ReadB(); return true;
				case 0x3201: this.PictureEssenceCoding = reader.ReadKey(); return true;
				case 0x321A: this.CodingEquations = reader.ReadKey(); return true;
				case 0x3219: this.ColorPrimaries = reader.ReadKey(); return true;
			}

			//PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(MXFGenericPictureEssenceDescriptor))["StoredWidth"];
			//DescriptionAttribute attr = prop.Attributes[typeof(DescriptionAttribute)] as DescriptionAttribute;
			//FieldInfo fi = attr.GetType().GetField("description", BindingFlags.NonPublic | BindingFlags.Instance);
			//if (fi != null)
			//	fi.SetValue(attr, "DIT IS GELUKT!!!");

			return base.ParseLocalTag(reader, localTag);
		}

	}
}
