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

namespace Myriadbits.MXF
{
	public class MXFEntryRIP : MXFObject
	{
		[CategoryAttribute("RIPEntry"), ReadOnly(true)]
		public UInt32 BodySID { get; set; }
		[CategoryAttribute("RIPEntry"), ReadOnly(true)]
		public UInt64 PartitionOffset { get; set; }

		public MXFEntryRIP(MXFReader reader)
			: base(reader)
		{
			this.m_eType = MXFObjectType.RIP;
			this.BodySID = reader.ReadD();
			this.PartitionOffset = reader.ReadL();
			this.Length = 12; // Fixed length
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("RIPEntry - BodySID {0}, ParitionOffset {1}", this.BodySID, this.PartitionOffset);
		}
	}
}
