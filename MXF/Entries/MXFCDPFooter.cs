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
	public class MXFCDPFooter : MXFObject
	{
		private const string CATEGORYNAME = "CDPFooter";

		[Category(CATEGORYNAME)] 
		public UInt16? SequenceCounter { get; set; }
		[Category(CATEGORYNAME)]
		public byte? PacketChecksum { get; set; }


		public MXFCDPFooter(MXFReader reader)
			: base(reader)
		{
			this.SequenceCounter = reader.ReadUInt16();
			this.PacketChecksum = reader.ReadByte();
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("CDP Footer, Sequence {0}, Checksum {1}", this.SequenceCounter, this.PacketChecksum);
		}
	}
}
