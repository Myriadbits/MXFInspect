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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Myriadbits.MXF
{
	public class MXFLogicalObject
	{		
		[Browsable(false)]
		public MXFObject Object { get; set; }

		[Browsable(false)]		
		public List<MXFLogicalObject> Children { get; set; }

		[Browsable(false)]
		public MXFLogicalObject Parent { get; set; }

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
		/// Find the first child with this type
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public MXFLogicalObject this[Type type]
		{
			get
			{
				if(this.Children != null)
                {
					foreach (MXFLogicalObject lo in this.Children)
					{
						if (lo.Object != null && lo.Object.GetType() == type)
							return lo;
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Get all childs cast to the required type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lo"></param>
		/// <returns></returns>
		public static List<T> GetChilds<T>(MXFLogicalObject lo)
		{
			if (lo.HasChildren)
				return lo.Children.Select(a => a.Object).Cast<T>().ToList();
			return null;
		}

		/// <summary>
		/// Get all childs cast to the required type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lo"></param>
		/// <returns></returns>
		public static List<MXFLogicalObject> GetLogicChilds<T>(MXFLogicalObject lo)
		{
			if (lo.HasChildren)
				return lo.Children.Where(a => a.Object is T).ToList();
			return null;
		}

		/// <summary>
		/// Get all childs cast to the required type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lo"></param>
		/// <returns></returns>
		public static T GetFirstChild<T>(MXFLogicalObject lo)
		{
			if (lo.HasChildren)
				return lo.Children.Select(a => a.Object).Cast<T>().FirstOrDefault();
			return default(T);
		}
			
		/// <summary>
		/// Add a child
		/// </summary>
		/// <param name="child"></param>
		/// <returns></returns>
		[Browsable(false)]
		public bool HasChildren
		{
			get
			{
				if (this.Children == null)
					return false;
				return this.Children.Count > 0;
			}
		}

		
		/// <summary>
		/// Add a child
		/// </summary>
		/// <param name="child"></param>
		/// <returns></returns>
		public MXFLogicalObject AddChild(MXFLogicalObject child)
		{
			if (this.Children == null)
				this.Children = new List<MXFLogicalObject>();
			child.Parent = this;
			this.Children.Add(child);
			return child;
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (this.Children == null)
				return this.Name;
			return string.Format("{0} [{1} items]", this.Name, this.Children.Count);
		}



	}
}
