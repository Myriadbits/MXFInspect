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
using System.ComponentModel;
using System.Text;

namespace Myriadbits.MXF
{
	public class MXFLocalTag : MXFObject
	{
		private const string CATEGORYNAME = "LocalTag";

		[Category(CATEGORYNAME)]
		public long DataOffset { get; set; }
		[Category(CATEGORYNAME)]
		public UInt16 Tag { get; set; }
		[Category(CATEGORYNAME)]
		public UInt16 Size { get; set; }
		[Category(CATEGORYNAME)]
		public string Name { get; set; }
		[Category(CATEGORYNAME)]
		public MXFKey Key { get; set; }
		[Category(CATEGORYNAME)]
		public object Value { get; set; }
		[Category(CATEGORYNAME)]
		public object ValueString { get; set; }

		public MXFLocalTag(MXFReader reader)
			: base(reader)
		{
			this.Tag = reader.ReadUInt16();
			this.Size = reader.ReadUInt16();
			this.DataOffset = reader.Position;
			this.Length = this.Size;
		}

		/// <summary>
		/// Parse this tag
		/// </summary>
		/// <param name="reader"></param>
		public void Parse(MXFReader reader)
		{
			if (this.Size == 1)
				this.Value = reader.ReadByte();
			else if (this.Size == 2)
				this.Value = reader.ReadUInt16();
			else if (this.Size == 4)
				this.Value = reader.ReadUInt32();
			else if (this.Size == 8)
				this.Value = reader.ReadUInt64();
			else
			{
				byte[] data = new byte[this.Size];
				for (int n = 0; n < this.Size; n++)
					data[n] = reader.ReadByte();
				this.Value = data;
			}
		}

		public override string ToString()
		{
			string name = this.Name;
			if (string.IsNullOrEmpty(name))
				name = "<Unknown localtag>";

			if (this.Value != null)
			{
				Type valueType = this.Value.GetType();
				if (valueType.IsArray)
				{
					byte[] data = this.Value as byte[];
					StringBuilder hex = new StringBuilder();
					foreach (byte b in data)
						hex.AppendFormat("{0:x2}, ", b);
					//this.ValueString = hex.ToString();
					this.ValueString = System.Text.Encoding.BigEndianUnicode.GetString(data);
					return string.Format("{0} 0x{1:X4} = {2}", name, this.Tag, hex.ToString());
				}
				else
				{
					return string.Format("{0} 0x{1:X4} = {2}", name, this.Tag, this.Value);
				}
			}
			return string.Format("{0} 0x{1:X4} = {2}", name, this.Tag, this.Value);
		}

	}
}
