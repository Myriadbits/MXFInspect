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

namespace Myriadbits.MXF
{
	[ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012400")]
	public class MXFGenericDescriptor : MXFInterchangeObject
	{
		private readonly MXFKey subDescriptorKey = new MXFKey(0x06,0x0E,0x2B,0x34,0x01,0x01,0x01,0x09,0x06,0x01,0x01,0x04,0x06,0x10,0x00,0x00);
		public MXFGenericDescriptor(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
				case 0x2F01:
					this.AddChild(reader.ReadReferenceSet<MXFLocator>("Locators", "Locator")); 
					return true;
				case var _ when localTag.Key == subDescriptorKey: 
					this.AddChild(reader.ReadReferenceSet<MXFSubDescriptor>("SubDescriptors", "SubDescriptor")); 
					return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
