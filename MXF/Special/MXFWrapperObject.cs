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
using System.Linq;

namespace Myriadbits.MXF
{
	// TODO check wether the logical object can be merged into this wrapper class
	public class MXFWrapperObject<T> : MXFNamedObject
	{
		private const string CATEGORYNAME = "ObjectWrapper";

		[Category(CATEGORYNAME)]
		public T Object { get; set; }

		/// <summary>
		///Default constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFWrapperObject(T obj, string name, long offset, long length) : base(name, offset, length)
		{
			this.Object = obj;
		}
			
		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (!this.Children.Any())
				return this.Object.ToString();
			return string.Format("{0} [{1} items]", Object.ToString(), this.Children.Count);
		}



	}
}
