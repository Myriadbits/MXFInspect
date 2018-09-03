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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Myriadbits.MXF
{
	public enum MXFObjectType
	{
		Normal,
		Partition,
		Index,
		SystemItem,
		Essence,
		Meta,
		RIP,
		Filler,
		Special
	};

	public enum MXFLogType
	{
		Info,
		Warning,
		Error
	};

	public class MXFObject
	{
		private long m_lOffset = long.MaxValue;	// Offset in bytes from the beginning of the file
		private long m_lLength = -1;			// Length in bytes of this object
		protected MXFObjectType m_eType = MXFObjectType.Normal; // Default to normal type

		[CategoryAttribute("Object"), ReadOnly(true)]
		public long Offset
		{
			get
			{				
				return m_lOffset;
			}
			set 
			{
				m_lOffset = value;
			}
		}

		[CategoryAttribute("Object"), ReadOnly(true)]
		public long Length
		{
			get
			{
				if (m_lLength == -1)
				{
					// Not set, try to get the parent length
					if (this.Parent != null)
						return this.Parent.Length + this.Parent.Offset - this.Offset;
					return 0; // Unknown
				}
				else
					return m_lLength;
			}
			set
			{
				m_lLength = value;
			}
		}

		[Browsable(false)]
		public MXFObjectType Type
		{
			get
			{
				return m_eType;
			}
		}

		[Browsable(false)]		
		public List<MXFObject> Children { get; set; }

		[Browsable(false)]
		public MXFObject Parent { get; set; }

		[Browsable(false)]
		public bool IsLoaded { get; set; }

		/// <summary>
		///Default constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFObject()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFObject(MXFReader reader)
		{
			this.Offset = reader.Position;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reader"></param>
		public MXFObject(long offset)
		{
			this.Offset = offset;
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
		/// Returns the number of children
		/// </summary>
		/// <param name="child"></param>
		/// <returns></returns>
		[Browsable(false)]
		public int ChildCount
		{
			get
			{
				if (this.Children == null)
					return 0;
				return this.Children.Count;
			}
		}

		/// <summary>
		/// Add a child
		/// </summary>
		/// <param name="child"></param>
		/// <returns></returns>
		public MXFObject AddChild(MXFObject child)
		{
			if (this.Children == null)
				this.Children = new List<MXFObject>();
			child.Parent = this;
			this.Children.Add(child);
			if (child.Offset < this.m_lOffset)
				this.m_lOffset = child.Offset;
			return child;
		}

		/// <summary>
		/// Find the top level parent
		/// </summary>
		/// <param name="child"></param>
		/// <returns></returns>
		[Browsable(false)]
		public MXFObject TopParent
		{
			get 
			{
				if (this.Parent != null)
					return this.Parent.TopParent;
				return this;
			}
		}

		public void LogInfo(string format, params object[] args) { this.Log(MXFLogType.Info, format, args); }
		public void LogWarning(string format, params object[] args) { this.Log(MXFLogType.Warning, format, args); }
		public void LogError(string format, params object[] args) { this.Log(MXFLogType.Error, format, args); }



		/// <summary>
		/// Generic log message
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public void Log(MXFLogType type, string format, params object[] args)
		{			
			string s = string.Format("{0}: {1}",  type.ToString(), string.Format(format, args));
			Debug.WriteLine(s);
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (this.Children == null)
				return this.Offset.ToString();
			return string.Format("{0} [{1} items]", this.Offset, this.Children.Count);
		}


		/// <summary>
		/// Is this object visible?
		/// </summary>
		/// <param name="skipFiller"></param>
		/// <returns></returns>
		public bool IsVisible(bool skipFiller)
		{
			if (skipFiller && this.Type == MXFObjectType.Filler)
				return false;
			return true;
		}

		/// <summary>
		/// Find the next object of a specific type
		/// </summary>
		/// <param name="currentObject"></param>
		/// <returns></returns>
		public MXFObject FindNextSibling(Type typeToFind, bool skipFillers)
		{
			MXFObject found = null;
			if (this.Parent != null && this.Parent.HasChildren)
			{
				int index = this.Parent.Children.FindIndex(a => a == this);
				if (index >= 0 && index < this.Parent.Children.Count - 1)
				{
					for (int n = index + 1; n < this.Parent.Children.Count; n++)
					{
						MXFObject child = this.Parent.Children[n];
						if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
						{
							// Yes found next sibling of the same type
							return this.Parent.Children[n];
						}

						// Not the correct type, try its children
						found = this.Parent.Children[n].FindChild(typeToFind, skipFillers);
						if (found != null)
							return found;
					}
				}

				// Hmm still not found, try our grand-parent:
				found = this.Parent.FindNextSibling(typeToFind, skipFillers);
			}
			return found;
		}

		/// <summary>
		/// Find the next object of a specific type
		/// </summary>
		/// <param name="currentObject"></param>
		/// <returns></returns>
		public MXFObject FindPreviousibling(Type typeToFind, bool skipFillers)
		{
			MXFObject found = null;
			if (this.Parent != null && this.Parent.HasChildren)
			{
				int index = this.Parent.Children.FindIndex(a => a == this);
				if (index > 0)
				{
					for (int n = index - 1; n >= 0; n--)
					{
						MXFObject child = this.Parent.Children[n];
						if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
						{
							// Yes found next sibling of the same type
							return this.Parent.Children[n];
						}

						// Not the correct type, try its children
						found = this.Parent.Children[n].FindChildReverse(typeToFind, skipFillers);
						if (found != null)
							return found;
					}
				}

				// Hmm still not found, try our grand-parent:
				found = this.Parent.FindPreviousibling(typeToFind, skipFillers);
			}
			return found;
		}


		/// <summary>
		/// Find the first child of a specific type
		/// </summary>
		/// <param name="currentObject"></param>
		/// <returns></returns>
		public MXFObject FindChild(Type typeToFind, bool skipFillers)
		{
			if (this.Children != null)
			{
				MXFObject found = null;
				foreach(MXFObject child in this.Children)
				{
					if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
						return child;
					if (child.HasChildren)
					{
						found = child.FindChild(typeToFind, skipFillers);
						if (found != null)
							return found;
					}
				}
				return null;
			}
			return null;
		}


		/// <summary>
		/// Find the first child of a specific type
		/// </summary>
		/// <param name="currentObject"></param>
		/// <returns></returns>
		public MXFObject FindChildReverse(Type typeToFind, bool skipFillers)
		{
			if (this.Children != null)
			{
				MXFObject found = null;
				for (int n = this.Children.Count - 1; n >= 0; n--)
				{
					MXFObject child = this.Children[n];
					if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
						return child;
					if (child.HasChildren)
					{
						found = child.FindChildReverse(typeToFind, skipFillers);
						if (found != null)
							return found;
					}
				}
				return null;
			}
			return null;
		}


		/// <summary>
		/// Add this object and all children to the list (recursive)
		/// </summary>
		/// <param name="currentObject"></param>
		/// <returns></returns>
		public void AddToList(List<MXFObject> list)
		{
			list.Add(this);
			if (this.Children != null)
			{
				foreach (MXFObject child in this.Children)
					child.AddToList(list);
			}
		}

		/// <summary>
		/// Returns a specific child
		/// </summary>
		/// <param name="child"></param>
		/// <returns></returns>
		public MXFObject GetChild(int index)
		{
			if (this.Children == null)
				return null;
			if (index >= 0 && index < this.Children.Count)
				return this.Children[index];
			return null;
		}


		/// <summary>
		/// Returns true if a child with a certain offset already exists
		/// </summary>
		/// <param name="currentObject"></param>
		/// <returns></returns>
		public bool ChildExists(MXFObject child)
		{
			if (this.Children != null && child != null)
				return this.Children.Exists(a => a.Offset == child.Offset);
			return false;
		}

		/// <summary>
		/// Load the entire object from disk (when not yet loaded)
		/// </summary>
		public void Load()
		{
			if (!this.IsLoaded)
			{
				this.OnLoad();
				this.IsLoaded = true;
			}
		}

		/// <summary>
		/// Load the entire partition from disk override in derived classes when delay loading is supported
		/// </summary>
		public virtual void OnLoad()
		{
		}
	}
}
