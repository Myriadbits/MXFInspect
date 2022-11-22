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
    public class MXFEntryPrimer : MXFObject
	{
		private const string CATEGORYNAME = "PrimerEntry";

		[Category(CATEGORYNAME)]
		public UInt16 Tag { get; set; }

		[Category(CATEGORYNAME)]
		public AUID AliasUID { get; set; }

		public MXFEntryPrimer(IKLVStreamReader reader, long offset)
			: base(reader)
		{
			this.Offset = offset;
			this.Tag = reader.ReadUInt16();
			this.AliasUID = reader.ReadAUID();
			this.TotalLength = 18; // Fixed length (16 bytes key + 2 bytes tag)
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("Tag 0x{0:X4} -> {1}", this.Tag, this.AliasUID.ToString());
		}
	}
}
