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
using System.Diagnostics;
using System.Linq;

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

	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class MXFObject : MXFNode<MXFObject>
    {
		private long m_lOffset = long.MaxValue;	// Offset in bytes from the beginning of the file
		private long m_lLength = -1;			// Length in bytes of this object
		protected MXFObjectType m_eType = MXFObjectType.Normal; // Default to normal type

		[CategoryAttribute("KLV"), ReadOnly(true)]
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

		[CategoryAttribute("KLV"), ReadOnly(true)]
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
		// TODO extract Loaded/Load to an interface
		public bool IsLoaded { get; set; }

		/// <summary>
		///Default constructor needed for derived classes such as MXFFile, ...
		/// </summary>
		/// <param name="reader"></param>
		// TODO can be removed, can it?
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
		/// Adds a child an sets reference of parent to this
		/// </summary>
		/// <param name="child"></param>
		/// <returns></returns>
		public override void AddChild(MXFObject child)
		{
			base.AddChild(child);
			if (child.Offset < this.m_lOffset)
				this.m_lOffset = child.Offset;
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
				foreach (MXFObject child in this.Children)
				{
					if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
						return child;
					if (child.Children.Any())
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
					if (child.Children.Any())
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
		/// Find the next object of a specific type
		/// </summary>
		/// <param name="currentObject"></param>
		/// <returns></returns>
		public MXFObject FindNextSibling(Type typeToFind, bool skipFillers)
		{
			// TODO simplify and if necessary move to another class
			MXFObject found = null;
			if (this.Parent != null && this.Parent.Children.Any())
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
		public MXFObject FindPreviousSibling(Type typeToFind, bool skipFillers)
		{
			MXFObject found = null;
			if (this.Parent != null && this.Parent.Children.Any())
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
				found = this.Parent.FindPreviousSibling(typeToFind, skipFillers);
			}
			return found;
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
			if (this.Children.Any())
				return this.Offset.ToString();
			return string.Format("{0} [{1} items]", this.Offset, this.Children.Count());
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
