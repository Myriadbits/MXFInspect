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

namespace Myriadbits.MXF
{
	public class MXFPrimerPack : MXFKLV
	{
		private const string CATEGORYNAME = "PrimerPack";

		[Category(CATEGORYNAME)] 
		public UInt32 LocalTagCount { get; set; }

		[Browsable(false)]
		private Dictionary<UInt16, MXFEntryPrimer> m_PrimerKeys = null;
		[Browsable(false)]
		public Dictionary<UInt16, MXFEntryPrimer> AllKeys { get { return m_PrimerKeys; } }

		/// <summary>
		/// Primerpack constructor 
		/// All keys will be passed to the partition and used in the metadatabase class to describe unkown/optional tags
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFPrimerPack(MXFReader reader, MXFKLV headerKLV)
			: base(headerKLV, "PrimerPack", KeyType.PrimerPack)
		{
			this.LocalTagCount = ReadTagList(reader, "LocalTags");
		}

		/// <summary>
		/// Read partition tag list
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="categoryName"></param>
		/// <returns></returns>
		protected UInt32 ReadTagList(MXFReader reader, string categoryName)
		{
			UInt32 nofItems = reader.ReadUInt32();
			UInt32 objectSize = reader.ReadUInt32(); // useless size of objects, always 16 according to specs

			MXFObject keylist = new MXFNamedObject(categoryName, reader.Position);
			if (nofItems > 0 && nofItems < UInt32.MaxValue)
			{
				m_PrimerKeys = new Dictionary<UInt16, MXFEntryPrimer>();
				for (int n = 0; n < nofItems; n++)
				{
					MXFEntryPrimer entry = new MXFEntryPrimer(reader);
					m_PrimerKeys.Add(entry.LocalTag, entry); // Add to our own internal list
					keylist.AddChild(entry); // And add the entry as one of our children
				}
			}
			this.AddChild(keylist);
			return nofItems;
		}


		public override string ToString()
		{
			if (this.LocalTagCount == 0)
				return "PrimerPack";
			return string.Format("PrimerPack [{0} items]", this.LocalTagCount );
		}
	}
}
