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

using System.ComponentModel;
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
	// TODO add ULElement attributes
	[ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010401.011b0100")]
    public class MXFAddress : MXFDescriptiveObject
	{
		private const string CATEGORYNAME = "Address";

		public readonly UL commObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x18, 0x00);
		public readonly UL addrNameValueObjects_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x1f, 0x04);
		public readonly UL roomSuiteNumber_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x01, 0x01);
		public readonly UL streetNumber_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x02, 0x01);
		public readonly UL streetName_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x03, 0x01);
		public readonly UL postalTown_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x04, 0x01);
		public readonly UL city_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x05, 0x01);
		public readonly UL stateProvinceCountry_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x06, 0x01);
		public readonly UL postalCode_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x07, 0x01);
		public readonly UL country_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x08, 0x01);
		public readonly UL roomSuiteName_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x11, 0x01);
		public readonly UL buildingName_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x12, 0x01);
		public readonly UL placeName_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x14, 0x01);
		public readonly UL geoCoordinates_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x15, 0x00);
		public readonly UL astroBodyName_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x16, 0x01);

		[Category(CATEGORYNAME)]
		public string RoomSuiteNumber { get; set; }
		[Category(CATEGORYNAME)]
		public string StreetNumber { get; set; }
		[Category(CATEGORYNAME)]
		public string StreetName { get; set; }
		[Category(CATEGORYNAME)]
		public string PostalTown { get; set; }
		[Category(CATEGORYNAME)]
		public string City { get; set; }
		[Category(CATEGORYNAME)]
		public string StateProvinceCounty { get; set; }
		[Category(CATEGORYNAME)]
		public string PostalCode { get; set; }
		[Category(CATEGORYNAME)]
		public string Country { get; set; }
		[Category(CATEGORYNAME)]
		public string RoomSuiteName { get; set; }
		[Category(CATEGORYNAME)]
		public string BuildingName { get; set; }
		[Category(CATEGORYNAME)]
		public string PlaceName { get; set; }
		[Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
		public byte[] GeographicalCoordinates { get; set; }
		[Category(CATEGORYNAME)]
		public string AstronomicalBodyName { get; set; }

		public MXFAddress(MXFPack pack)
			: base(pack)
		{
			this.MetaDataName = "Address";
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
		{
			if (localTag.AliasUID != null)
			{
				switch (localTag.AliasUID)
				{
					case var _ when localTag.AliasUID == commObjects_Key:
						localTag.AddChildren(reader.GetReferenceSet<MXFDescriptiveObject>("CommunicationObject", localTag.Offset, localTag.Length.Value));
						return true;
					// TODO replace generic MXFObject with class NameValue once implemented
					case var _ when localTag.AliasUID == addrNameValueObjects_Key: 
						localTag.AddChildren(reader.GetReferenceSet<MXFObject>("AddressNameValueObject", localTag.Offset, localTag.Length.Value));
                        return true;
					case var _ when localTag.AliasUID == roomSuiteNumber_Key: 
						this.RoomSuiteNumber = reader.ReadUTF16String(localTag.Length.Value);
						localTag.Value = this.RoomSuiteNumber;
						return true;
					case var _ when localTag.AliasUID == streetNumber_Key: 
						this.StreetNumber = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.StreetNumber;
                        return true;
					case var _ when localTag.AliasUID == streetName_Key: 
						this.StreetName = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.StreetName;
                        return true;
					case var _ when localTag.AliasUID == postalTown_Key: 
						this.PostalTown = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.PostalTown;
                        return true;
					case var _ when localTag.AliasUID == city_Key: 
						this.City = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.City;
                        return true;
					case var _ when localTag.AliasUID == stateProvinceCountry_Key: 
						this.StateProvinceCounty = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.StateProvinceCounty;
                        return true;
					case var _ when localTag.AliasUID == postalCode_Key: 
						this.PostalCode = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.PostalCode;
                        return true;
					case var _ when localTag.AliasUID == country_Key: 
						this.Country = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.Country;
                        return true;
					case var _ when localTag.AliasUID == roomSuiteName_Key: 
						this.RoomSuiteName = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.RoomSuiteName;
                        return true;
					case var _ when localTag.AliasUID == buildingName_Key: 
						this.BuildingName = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.BuildingName;
                        return true;
					case var _ when localTag.AliasUID == placeName_Key: 
						this.PlaceName = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.PlaceName;
                        return true;
					case var _ when localTag.AliasUID == geoCoordinates_Key: 
						this.GeographicalCoordinates = reader.ReadBytes(12);
                        localTag.Value = this.GeographicalCoordinates;
                        return true;
					case var _ when localTag.AliasUID == astroBodyName_Key: 
						this.AstronomicalBodyName = reader.ReadUTF16String(localTag.Length.Value);
                        localTag.Value = this.AstronomicalBodyName;
                        return true;
				}
			}

			return base.ReadLocalTagValue(reader, localTag);
		}

	}
}
