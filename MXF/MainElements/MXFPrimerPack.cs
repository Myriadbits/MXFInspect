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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public class MXFPrimerPack : MXFPack
	{
		private const string CATEGORYNAME = "PrimerPack";

		[Category(CATEGORYNAME)] 
		public UInt32 PrimerEntriesCount { get; set; }

		[Browsable(false)]
		private readonly Dictionary<UInt16, MXFPrimerEntry> primerEntries = new Dictionary<UInt16, MXFPrimerEntry>();
		
		[Browsable(false)]
		public IReadOnlyDictionary<UInt16, MXFPrimerEntry> PrimerEntries { get { return primerEntries; } }

		/// <summary>
		/// Primerpack constructor 
		/// All keys will be passed to the partition and used in the metadatabase class to describe unknown/optional tags
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="pack"></param>
		public MXFPrimerPack(MXFPack pack)
			: base(pack)
        {
            IKLVStreamReader reader = this.GetReader();
            this.PrimerEntriesCount = ReadPrimerEntries(reader);
		}

		/// <summary>
		/// Read partition tag list
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="categoryName"></param>
		/// <returns></returns>
		protected UInt32 ReadPrimerEntries(IKLVStreamReader reader)
		{
			reader.Seek(this.RelativeValueOffset);
			UInt32 numOfPrimerEntries = reader.ReadUInt32();

            // TODO useless size of objects, always 2(=tag) + 16(=UL) -> 18 according to specs
            UInt32 primerEntrySize = reader.ReadUInt32(); 

			// TODO do a minimum of checks here
			// TODO allow duplicate entries and complain afterwards when validating
			if (numOfPrimerEntries > 0 && numOfPrimerEntries < UInt32.MaxValue)
			{
				for (int n = 0; n < numOfPrimerEntries; n++)
				{
					MXFPrimerEntry entry = new MXFPrimerEntry(reader, this.Offset + reader.Position);
					primerEntries.Add(entry.Tag, entry); // Add to our own internal list
					this.AddChild(entry); // And add the entry as one of our children
				}
			}
			return numOfPrimerEntries;
		}


		public override string ToString()
		{
			return (this.PrimerEntriesCount == 0) ? "PrimerPack" : $"PrimerPack [{this.PrimerEntriesCount} items]";
		}
	}
}
