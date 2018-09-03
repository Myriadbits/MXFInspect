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
	class MXFEssenceContainerData : MXFInterchangeObject
	{
		[CategoryAttribute("EssenceContainerData"), Description("2701")]
		public MXFUMIDKey LinkedPackageUID { get; set; }
		[CategoryAttribute("EssenceContainerData"), Description("3F06")]
		public UInt32? IndexSID { get; set; }
		[CategoryAttribute("EssenceContainerData"), Description("3F07")]
		public UInt32? BodySID { get; set; }

		public MXFEssenceContainerData(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "EssenceContainerData")
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
				case 0x2701: this.LinkedPackageUID = reader.ReadUMIDKey(); return true;
				case 0x3F06: this.IndexSID = reader.ReadD(); return true;
				case 0x3F07: this.BodySID = reader.ReadD(); return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}

	}
}
