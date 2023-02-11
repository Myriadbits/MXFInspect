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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public class MXFCDPFuture : MXFObject
	{
		private const string CATEGORYNAME = "CDPFuture";

		[Category(CATEGORYNAME)]
		public byte? SectionID { get; set; }
		[Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
		public byte[] Data { get; set; }


		public MXFCDPFuture(IKLVStreamReader reader, long offset, byte sectionID)
			: base(offset)
		{
			this.SectionID = sectionID;
			this.TotalLength = reader.ReadByte();
			this.Data = reader.ReadBytes((int)this.TotalLength);
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("CDP FutureExtension, SectionID {0}", this.SectionID);
		}
	}
}
