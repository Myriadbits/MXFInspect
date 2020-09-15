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

namespace Myriadbits.MXF
{
	public class MXFEntryDelta : MXFObject
	{
		[CategoryAttribute("DeltaEntry"), ReadOnly(true)] 
		public sbyte PosTableIndex { get; set; }
		[CategoryAttribute("DeltaEntry"), ReadOnly(true)]
		public byte Slice { get; set; }
		[CategoryAttribute("DeltaEntry"), ReadOnly(true)]
		public UInt32 ElementDelta { get; set; }

		public MXFEntryDelta(MXFReader reader, UInt32 length)
			: base(reader)
		{
			this.m_eType = MXFObjectType.Index;

			this.Length = length;
			this.PosTableIndex = reader.ReadSB();
			this.Slice = reader.ReadB();
			this.ElementDelta = reader.ReadD();
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("DeltaEntry - DeltaPos {0}, Slice {1}, ElementDelta {2}", this.PosTableIndex, this.Slice, this.ElementDelta);
		}
	}
}
