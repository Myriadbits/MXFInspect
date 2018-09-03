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
	public class MXFPreface : MXFInterchangeObject
	{
		[CategoryAttribute("Preface"), Description("3B02")]
		public DateTime? LastModificationDate { get; set; }
		[CategoryAttribute("Preface"), Description("3B03")]
		public MXFRefKey ContentStorageUID { get; set; }
		[CategoryAttribute("Preface"), Description("3B05")]
		public UInt16? Version { get; set; }
		[CategoryAttribute("Preface"), Description("3B07")]
		public UInt32? ObjectModelVersion { get; set; }
		[CategoryAttribute("Preface"), Description("3B08")]
		public MXFRefKey PrimaryPackageUID { get; set; }
		[CategoryAttribute("Preface"), Description("3B09")]
		public MXFKey OperationalPattern { get; set; }

		public MXFPreface(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Preface")
		{
			this.Key.Type = KeyType.Preface;
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
		{
			switch (localTag.Tag)
			{
				case 0x3B02: this.LastModificationDate = reader.ReadTimestamp(); return true;
				case 0x3B05: this.Version = reader.ReadW(); return true;
				case 0x3B07: this.ObjectModelVersion = reader.ReadD(); return true;
				case 0x3B03: this.ContentStorageUID = new MXFRefKey(reader, 16, "ContentStorageUID"); return true;
				case 0x3B08: this.PrimaryPackageUID = new MXFRefKey(reader, 16, "PrimaryPackageUID"); return true;
				case 0x3B09: this.OperationalPattern = new MXFKey(reader, 16); return true;
				case 0x3B06: ReadKeyList(reader, "Identifications", "Identification"); return true;
				case 0x3B0A: ReadKeyList(reader, "Essencecontainers", "Essencecontainer"); return true;
				case 0x3B0B: ReadKeyList(reader, "Descriptive Metadata Schemes", "DM scheme"); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
