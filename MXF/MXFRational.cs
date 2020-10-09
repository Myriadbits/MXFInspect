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

namespace Myriadbits.MXF
{
	//namespace: http://www.smpte-ra.org/reg/2003/2012 	
	//urn:smpte:ul:060e2b34.01040101.03010100.00000000
	public class MXFRational : IEquatable<MXFRational>
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

		public override string ToString()
		{
			return string.Format("{0:.00} ({1}/{2})", this.Value, this.Num, this.Den);
		}

		/// <summary>
		/// Checks whether two rationals are equal?
		/// </summary>
		/// <param name="other">The other rational to be compared</param>
		/// <returns>True if the two rationals are equal</returns>
		public bool Equals(MXFRational other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			if(this.Num == other.Num && this.Den == other.Den)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		///  Checks if this rational is equal to another object
		/// </summary>
		/// <param name="obj">The other object to be compared</param>
		/// <returns>True if the two objects are equal</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(MXFRational)) return false;
			return Equals((MXFRational)obj);
		}

		public override int GetHashCode() { return (this.Num + this.Den).GetHashCode(); }
		public static bool operator ==(MXFRational x, MXFRational y) { return Equals(x, y); }
		public static bool operator !=(MXFRational x, MXFRational y) { return !Equals(x, y); }
	}
}
