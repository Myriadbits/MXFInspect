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
	/// <summary>
	/// See SMPTE 377-1-2009
	/// </summary>
	public class MXFIndexTableSegment : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "IndexTableSegment";

		[Category(CATEGORYNAME)]
		public UInt32? EditUnitByteCount { get; set; }
		
		[Category(CATEGORYNAME)]
		public UInt32? IndexSID { get; set; }
		
		[Category(CATEGORYNAME)]
		public UInt32? BodySID { get; set; }
		
		[Category(CATEGORYNAME)]
		public MXFRational IndexEditRate { get; set; }
		
		[Category(CATEGORYNAME)]
		public UInt64? IndexStartPosition { get; set; }
		
		[Category(CATEGORYNAME)]
		public MXFPositionType? IndexDuration { get; set; }
		
		[Category(CATEGORYNAME)]
		public byte? SliceCount { get; set; }
		
		[Category(CATEGORYNAME)]
		public byte? PosTableCount { get; set; }
		
		[Category(CATEGORYNAME)]
		public bool? SingleIndexLocation { get; set; }
		
		[Category(CATEGORYNAME)]
		public bool? ForwardIndexDirection { get; set; }
		
		[Category(CATEGORYNAME)]
		public UInt64? ExtStartOffset { get; set; }
		
		[Category(CATEGORYNAME)]
		public UInt64? VBEByteCount { get; set; }
		
		[Category(CATEGORYNAME)]
		public bool? SingleEssenceLocation { get; set; }

		[Browsable(false)]
		public List<MXFEntryIndex> IndexEntries { get; set; }


		public MXFIndexTableSegment(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "IndexTableSegment") //base(headerKLV, "IndexTableSegment", KeyType.IndexSegment)
		{
			this.m_eType = MXFObjectType.Index;
			this.Key.Type = KeyType.IndexSegment;
		}


		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
		{
			switch (localTag.Tag)
			{
				case 0x3F05: this.EditUnitByteCount = reader.ReadUInt32(); return true;
				case 0x3F06: this.IndexSID = reader.ReadUInt32(); return true;
				case 0x3F07: this.BodySID = reader.ReadUInt32(); return true;
				case 0x3F08: this.SliceCount = reader.ReadByte(); return true;
				case 0x3F0C: this.IndexStartPosition = reader.ReadUInt64(); return true;
				case 0x3F0D: this.IndexDuration = reader.ReadUInt64(); return true;
				case 0x3F0E: this.PosTableCount = reader.ReadByte(); return true;
				case 0x3F0F: this.ExtStartOffset = reader.ReadUInt64(); return true;
				case 0x3F10: this.VBEByteCount = reader.ReadUInt64(); return true;
				case 0x3F11: this.SingleIndexLocation = reader.ReadBool(); return true;
				case 0x3F12: this.SingleEssenceLocation = reader.ReadBool(); return true;
				case 0x3F13: this.ForwardIndexDirection = reader.ReadBool(); return true;
				case 0x3F0B: this.IndexEditRate = reader.ReadRational(); return true;
				case 0x3F0A:  // Index entry array
					{
						UInt32 NbIndexEntries = reader.ReadUInt32();
						UInt32 entryLength = reader.ReadUInt32();						
						if (NbIndexEntries > 0)
						{
							this.IndexEntries = new List<MXFEntryIndex>();
							MXFObject indexCollection = new MXFNamedObject("IndexEntries", reader.Position, MXFObjectType.Index);

							for (UInt64 i = 0; i < NbIndexEntries; i++)
							{
								long next = reader.Position + entryLength;

								MXFEntryIndex newEntry = new MXFEntryIndex((ulong) this.IndexStartPosition + i, reader, this.SliceCount, this.PosTableCount, entryLength);
								this.IndexEntries.Add(newEntry); // Also add this entry to the local list

								// And to the child collection
								indexCollection.AddChild(newEntry);

								reader.Seek(next);
							}
							this.AddChild(indexCollection);
						}
					}
					return true;

				case 0x3F09:  // Delta entry array
					{ 
						UInt32 NbDeltaEntries = reader.ReadUInt32();
						UInt32 entryLength = reader.ReadUInt32();
						if (NbDeltaEntries > 0)
						{
							MXFObject deltaCollection = new MXFNamedObject("DeltaEntries", reader.Position, MXFObjectType.Index);
							for (int i = 0; i < NbDeltaEntries; i++)
							{
								long next = reader.Position + entryLength;
								deltaCollection.AddChild(new MXFEntryDelta(reader, entryLength));
								reader.Seek(next);
							}
							this.AddChild(deltaCollection);
						}
					}
					return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}


		public override string ToString()
		{
			return string.Format("Index Table Segment [{0}], BodySID {1}", this.IndexSID, this.BodySID);
		}
	}
}
