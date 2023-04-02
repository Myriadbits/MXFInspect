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

using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01013000")]
	public class MXFIdentification : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "Identification";

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.02010000")]
        public string ApplicationSupplierName { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.03010000")]
        public string ApplicationName { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.04000000")]
        public MXFProductVersion ApplicationVersion { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.05010000")]
        public string ApplicationVersionString { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.07000000")]
        public AUID ApplicationProductId { get; set; }

		[Category(CATEGORYNAME)]
        [TypeConverter(typeof(DateTimeTypeConverter))]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020110.02030000")]
        public DateTime? FileModificationDate { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.0a000000")]
        public MXFProductVersion ToolkitVersion { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.06010000")]
        public string ApplicationPlatform { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200701.01000000")]
        public AUID GenerationID { get; set; }

		public MXFIdentification(MXFPack pack)
			: base(pack, "Identification")
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
		{
			switch (localTag.TagValue)
			{
				case 0x3C09: 
					this.GenerationID = reader.ReadUUID();
                    localTag.Value = this.GenerationID; 
					return true;
				case 0x3C01: 
					this.ApplicationSupplierName = reader.ReadUTF16String(localTag.Length.Value); 
					localTag.Value = this.ApplicationSupplierName;
					return true;
				case 0x3C02: 
					this.ApplicationName = reader.ReadUTF16String(localTag.Length.Value); 
					localTag.Value = this.ApplicationName;
					return true;
				case 0x3C03: 
					this.ApplicationVersion = reader.ReadProductVersion();
						localTag.Value = this.ApplicationVersion;
					return true;
				case 0x3C04: 
					this.ApplicationVersionString = reader.ReadUTF16String(localTag.Length.Value); 
					localTag.Value = this.ApplicationVersionString;
					return true;
				case 0x3C05: 
					this.ApplicationProductId = reader.ReadAUID(); 
					localTag.Value = this.ApplicationProductId;
					return true;
				case 0x3C06: 
					this.FileModificationDate = reader.ReadTimeStamp();
					localTag.Value = this.FileModificationDate;
					return true;
				case 0x3C07: 
					this.ToolkitVersion = reader.ReadProductVersion(); 
					localTag.Value = this.ToolkitVersion;
					return true;
				case 0x3C08: 
					this.ApplicationPlatform = reader.ReadUTF16String(localTag.Length.Value); 
					localTag.Value = this.ApplicationPlatform;
					return true;
			}
			return base.ReadLocalTagValue(reader, localTag);
		}

	}
}
