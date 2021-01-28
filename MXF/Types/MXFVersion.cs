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
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFVersion : IEquatable<MXFVersion>
    {
        private const string CATEGORYNAME = "Version";

        [Category(CATEGORYNAME)]
        public UInt16 Major { get; set; }

        [Category(CATEGORYNAME)]
        public UInt16 Minor { get; set; }

        public MXFVersion(UInt16 major, UInt16 minor)
        {
            Major = major;
            Minor = minor;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Major, this.Minor);
        }


        /// <summary>
        /// Checks if two versions are equal
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MXFVersion other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Major == other.Major && this.Minor == other.Minor;
        }

        /// <summary>
        /// Checks if version is equal to another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MXFVersion)) return false;
            return Equals((MXFVersion)obj);
        }

        public static bool operator ==(MXFVersion x, MXFVersion y) { return Equals(x, y); }
        public static bool operator !=(MXFVersion x, MXFVersion y) { return !Equals(x, y); }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Major.GetHashCode();
            hash = (hash * 7) + Minor.GetHashCode();
            return hash;
        }
    }
}
