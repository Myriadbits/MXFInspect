﻿//
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
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class MXFRefKey : MXFNamedObject 
	{
		[CategoryAttribute("ReferenceKey"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFObject Reference { get; set; }

		[CategoryAttribute("ReferenceKey"), ReadOnly(true)]
		public MXFKey Key { get; set; }


		/// <summary>
		/// Create a new reference key by reading from the current file location
		/// </summary>
		/// <param name="firstPart"></param>
		/// <param name="reader"></param>
		public MXFRefKey(MXFReader reader)
			: base(reader.Position)
		{
			this.Key = new MXFKey(reader, 16);
			this.Length = 16;
		}

		/// <summary>
		/// Named Reference key
		/// </summary>
		/// <param name="reader"></param>
		public MXFRefKey(MXFReader reader, UInt32 size, string name)
			: base(reader.Position)
		{
			this.Name = name;
			this.Key = new MXFKey(reader, size);
			this.Length = this.Key.Length;
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Name))
				return this.Key.Name;
			return string.Format("{0} [{1}]", this.Name, this.Key.ToString());
		}
	}
}
