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


using System.Linq;

namespace Myriadbits.MXF
{	
	public class MXFRIP : MXFKLV
	{
		public MXFRIP(MXFReader reader, MXFKLV headerKLV)
			: base(headerKLV, "RIP", KeyType.RIP)
		{
			this.m_eType = MXFObjectType.RIP;
			Initialize(reader);
		}

		private void Initialize(MXFReader reader)
		{
			// Make sure we read at the data position
			reader.Seek(this.DataOffset);

			// Read all local tags
			long klvEnd = this.DataOffset + this.Length;
			while (reader.Position + 12 < klvEnd)
			{
				// Add to the collection
				AddChild(new MXFEntryRIP(reader));
			}
		}

		public override string ToString()
		{
			if (!this.Children.Any())
				return string.Format("RIP [0 items]");
			return string.Format("RIP [{0} items]", this.Children.Count);
		}

		public MXFEntryRIP GetPartition(int partitionIndex)
		{
			return this.Children.ElementAtOrDefault(partitionIndex) as MXFEntryRIP;
		}
	}
}
