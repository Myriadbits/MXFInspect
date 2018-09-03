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
using System.Text;

namespace Myriadbits.MXF
{
	public class MXFData : MXFObject
	{
		[CategoryAttribute("Data"), ReadOnly(true)]
		public byte[] Data { get; set; }
		[CategoryAttribute("Data"), ReadOnly(true)]
		public string DataString { get; set; }		
		[CategoryAttribute("Data"), ReadOnly(true)]
		public UInt32 Size { get; set; }
		[CategoryAttribute("Data"), ReadOnly(true)]
		public string Name { get; set; }

		/// <summary>
		///Default constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFData(string name, MXFReader reader, UInt32 size)
			: base(reader)
		{
			this.Size = size;
			this.Name = name;
			this.Data = new byte[size];
			for (int n = 0; n < size; n++)
				this.Data[n] = reader.ReadB();

			StringBuilder sb = new StringBuilder();
			for (int n = 0; n < this.Data.Length; n++)
			{
				if (n > 0)
					sb.Append(", ");
				sb.Append(string.Format("{0:X02}", this.Data[n]));
			}
			this.DataString = sb.ToString();
		}

		
		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0} [len {1}]", this.Name, this.Size);
		}
	}
}
