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
	public class MXFAUID : MXFNamedObject 
	{
		private const string CATEGORYNAME = "AUID";

		[Category(CATEGORYNAME)]
		public MXFKey Key { get; set; }

		public MXFAUID(MXFReader reader, string name)
			: base(reader.Position)
		{
			this.Name = name;
			this.Key = reader.ReadULKey();
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

			string keyName = this.Key.Name ?? this.Key.ToString();
            return string.Format("{0} [{1}]", this.Name, keyName);
        }
    }
}
