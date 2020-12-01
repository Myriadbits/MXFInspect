﻿#region license
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Myriadbits.MXF
{
	public class MXFLogicalObject : MXFNode<MXFLogicalObject>
	{		
		[Browsable(false)]
		public MXFObject Object { get; set; }

		[Browsable(false)]
		public string Name { get; set; }

		/// <summary>
		///Default constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFLogicalObject(MXFObject obj, string name)
		{
			this.Object = obj;
			this.Name = name;
		}
			
		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (!this.Children.Any())
				return this.Name;
			return string.Format("{0} [{1} items]", this.Name, this.Children.Count);
		}



	}
}
