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
	public enum CCDataType
	{
		NTSC_CC_FIELD_1 = 0, 
		NTSC_CC_FIELD_2 = 1, 
		DTVCC_PACKET_DATA = 2, 
		DTVCC_PACKET_START = 3
	};

	public class MXFEntryCCData : MXFObject
	{
		private const string CATEGORYNAME = "CCData";

		[Category(CATEGORYNAME)] 
		public bool? Valid { get; set; }
		[Category(CATEGORYNAME)]
		public CCDataType? CCType { get; set; }
		[Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
		public byte[] Data { get; set; }		

		public MXFEntryCCData(MXFReader reader)
			: base(reader)
		{
			this.Length = 3; // Fixed
			byte b0 = reader.ReadByte();
			if ((b0 & 0xF8) == 0xF8) // Valid marker bits?
			{
				this.Valid = ((b0 & 0x04) != 0);
				this.CCType = (CCDataType)(b0 & 0x03);
				this.Data = new byte[2];
				this.Data[0] = reader.ReadByte();
				this.Data[1] = reader.ReadByte();
			}	
			
			// When this object is not valid, set the type to filler
			if (this.Valid.HasValue && !this.Valid.Value)
				this.m_eType = MXFObjectType.Filler;			
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (this.Valid.HasValue)
			{
				if (this.Valid.Value)
					return string.Format("CC_Data, Type {0}, Data: 0x{1:X2}, 0x{2:X2}", this.CCType, this.Data[0], this.Data[1]);
				else
					return string.Format("CC_Data, <padding>");
			}
			return "<empty CC_Data>";
		}
	}
}
