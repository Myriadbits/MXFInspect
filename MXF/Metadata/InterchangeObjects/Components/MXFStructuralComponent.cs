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

using Myriadbits.MXF.Utils;
using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
	[ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01010200")]
	public class MXFStructuralComponent : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "StructuralComponent";
		private const int CATEGORYPOS = 3;

		// TODO this should be a UUID?
		[SortedCategory(CATEGORYNAME, CATEGORYPOS)]
		[ULElement("urn:smpte:ul:060e2b34.01010102.04070100.00000000")]
		public MXFKey DataDefinition { get; set; }

		[SortedCategory(CATEGORYNAME, CATEGORYPOS)]
		[ULElement("urn:smpte:ul:060e2b34.01010102.07020201.01030000")]
		public MXFLengthType? Duration { get; set; }

		public MXFStructuralComponent(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
				case 0x0201: this.DataDefinition = reader.ReadULKey(); return true;
				case 0x0202: this.Duration = reader.ReadUInt64(); return true;
				// TODO replace generic MXFObject with class KLVData once implemented
				case 0x0203: this.AddChild(reader.ReadReferenceSet<MXFObject>("KLV Data", "KLV Data")); return true;
				case 0x0204: this.AddChild(reader.ReadReferenceSet<MXFTaggedValue>("User Comments", "User Comment")); return true;
				case 0x0205: this.AddChild(reader.ReadReferenceSet<MXFTaggedValue>("Attributes", "Attribute")); return true;
            }
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
