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

namespace Myriadbits.MXF
{
	public class MXFAddress : MXFDescriptiveObject
	{
		private const string CATEGORYNAME = "Address";

		public readonly MXFKey commObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x18, 0x00);
		public readonly MXFKey addrNameValueObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x06, 0x01, 0x01, 0x04, 0x05, 0x40, 0x1f, 0x04);
		public readonly MXFKey roomSuiteNumber_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x01, 0x01);
		public readonly MXFKey streetNumber_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x02, 0x01);
		public readonly MXFKey streetName_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x03, 0x01);
		public readonly MXFKey postalTown_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x04, 0x01);
		public readonly MXFKey city_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x05, 0x01);
		public readonly MXFKey stateProvinceCountry_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x06, 0x01);
		public readonly MXFKey postalCode_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x07, 0x01);
		public readonly MXFKey country_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x03, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x08, 0x01);
		public readonly MXFKey roomSuiteName_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x11, 0x01);
		public readonly MXFKey buildingName_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x12, 0x01);
		public readonly MXFKey placeName_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x14, 0x01);
		public readonly MXFKey geoCoordinates_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x15, 0x00);
		public readonly MXFKey astroBodyName_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x07, 0x07, 0x01, 0x20, 0x01, 0x04, 0x01, 0x16, 0x01);

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

		public MXFAddress(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV)
		{
			this.MetaDataName = "Address";
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
		{
			if (localTag.Key != null)
			{
				switch (localTag.Key)
				{
					case var _ when localTag.Key == commObjects_Key:
						this.AddChild(reader.ReadReferenceSet<MXFDescriptiveObject>("CommunicationObjects", "CommunicationObject")); 
						return true;
					// TODO replace generic MXFObject with class NameValue once implemented
					case var _ when localTag.Key == addrNameValueObjects_Key: 
						this.AddChild(reader.ReadReferenceSet<MXFObject>("AddressNameValueObjects", "AddressNameValueObject")); 
						return true;
					case var _ when localTag.Key == roomSuiteNumber_Key: 
						this.RoomSuiteNumber = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == streetNumber_Key: 
						this.StreetNumber = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == streetName_Key: 
						this.StreetNumber = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == postalTown_Key: 
						this.PostalTown = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == city_Key: 
						this.City = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == stateProvinceCountry_Key: 
						this.StateProvinceCounty = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == postalCode_Key: 
						this.PostalCode = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == country_Key: 
						this.Country = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == roomSuiteName_Key: 
						this.RoomSuiteName = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == buildingName_Key: 
						this.BuildingName = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == placeName_Key: 
						this.PlaceName = reader.ReadUTF16String(localTag.Size); 
						return true;
					case var _ when localTag.Key == geoCoordinates_Key: 
						this.GeographicalCoordinates = reader.ReadArray(reader.ReadByte, 12); 
						return true;
					case var _ when localTag.Key == astroBodyName_Key: 
						this.AstronomicalBodyName = reader.ReadUTF16String(localTag.Size); 
						return true;
				}
			}

			return base.ParseLocalTag(reader, localTag);
		}

	}
}
