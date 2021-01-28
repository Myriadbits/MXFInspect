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
	public class MXFEntrySVCInfo : MXFObject
	{
		private const string CATEGORYNAME = "SVC Info";

		[Category(CATEGORYNAME)]
		public byte? CaptionServiceNumber { get; set; }
		[Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
		public byte[] Data { get; set; }
		[Category(CATEGORYNAME)]
		public string DataString { get; set; }

		public MXFEntrySVCInfo(MXFReader reader)
			: base(reader)
		{
			this.Length = 7; // Fixed

			byte b0 = reader.ReadByte();
			if ((b0 & 0x40) != 0)
				this.CaptionServiceNumber = (byte)(b0 & 0x1F);
			else
				this.CaptionServiceNumber = (byte)(b0 & 0x3F);

			this.Data = reader.ReadArray(reader.ReadByte, 6);

			this.DataString = System.Text.Encoding.ASCII.GetString(this.Data);
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("SVC Info - Number {0}, Data: {1}", this.CaptionServiceNumber, this.DataString);
		}
	}
}
