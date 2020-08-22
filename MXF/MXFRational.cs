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

namespace Myriadbits.MXF
{
	public class MXFRational
	{
		public UInt32 Num { get; set; }
		public UInt32 Den { get; set; }

		public double Value 
		{ 
			get 
			{
				if (this.Den == 0)
					return 0;
				return ((double)this.Num) / ((double)this.Den);
			}
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(MXFRational)) return false;
			var other = (MXFRational)obj;
			return this.Num == other.Num && this.Den == other.Den;
		}
		public static bool operator ==(MXFRational x, MXFRational y) { return Equals(x, y); }
		public static bool operator !=(MXFRational x, MXFRational y) { return !Equals(x, y); }

		public override string ToString()
		{
			return string.Format("{0:.00} ({1}/{2})", this.Value, this.Num, this.Den);
		}
	}
}
