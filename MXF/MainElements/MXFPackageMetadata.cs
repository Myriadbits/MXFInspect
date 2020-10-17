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
using System.Collections.Generic;

namespace Myriadbits.MXF
{	
	public class MXFPackageMetaData : MXFKLV
	{
		private static Dictionary<int, string> m_itemTypes = new Dictionary<int, string>();
		private int nofSizeSize = 2;

		/// <summary>
		/// Static constructor to initialize the static array
		/// </summary>
		static MXFPackageMetaData()
		{
			// Add all meta data see spec: SMPTE ST 331:2011
			m_itemTypes.Add(0x80, "Metadata link");
			m_itemTypes.Add(0x81, "SMPTE 12M time-code");
			m_itemTypes.Add(0x82, "SMPTE 309M date-time stamp");
			m_itemTypes.Add(0x83, "UMID");
			m_itemTypes.Add(0x84, "MPEG-2 picture editing");
			m_itemTypes.Add(0x85, "8-channel AES3 editing");
			m_itemTypes.Add(0x86, "Picture bit-stream splicing");
			m_itemTypes.Add(0x87, "MPEG decoder buffer delay");
			m_itemTypes.Add(0x88, "KLV metadata");
			m_itemTypes.Add(0x89, "AES3 non-audio metadata");
		}

		public MXFPackageMetaData(MXFReader reader, MXFKLV headerKLV)
			: base(headerKLV, "PackageMetadata", KeyType.PackageMetaDataSet)
		{
			this.m_eType = MXFObjectType.Essence; // I will count this as essence data
			if (this.Key[5] == 0x63)
				nofSizeSize = 4;
			switch (this.Key[14])
			{
				case 0x02: this.Key.Name = "Package Metadata set"; break;
				case 0x03: this.Key.Name = "Picture Metadata set"; break;
				case 0x04: this.Key.Name = "Sound Metadata set"; break;
				case 0x05: this.Key.Name = "Data Metadata set"; break;
				case 0x06: this.Key.Name = "Control Metadata set"; break;
			}
				

			reader.Seek( this.DataOffset); // Seek to the start of the data

			long end = this.DataOffset + this.Length;
			while (reader.Position < end)
			{
				byte type = reader.ReadByte();
				UInt32 size = 0;
				if (nofSizeSize == 2) 
					size = reader.ReadUInt16();
				else
					size = reader.ReadUInt32();
				long startPos = reader.Position;
				if (m_itemTypes.ContainsKey(type))
				{
					this.AddChild(new MXFData(m_itemTypes[type], reader, size));
				}
				reader.Seek(startPos + size);
			}
		}
		
		public override string ToString()
		{
			return string.Format("Package Metadata [len {0}]", this.Length);
		}
	}
}
