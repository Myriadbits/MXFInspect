using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXFInspect
{
	public class MXFIndexTableSegment : MXFMetaData
	{
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt32 EditUnitByteCount { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt32 IndexSID { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt32 BodySID { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt32 IndexEditRateNum { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt32 IndexEditRateDen { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt64 IndexStartPosition { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt64 IndexDuration { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt32 NbIndexEntries { get; set; }
		[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		public UInt32 NbDeltaEntries { get; set; }

		//[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		//public List<MXFIndexEntry> IndexEntries { get; set; }

		//[CategoryAttribute("IndexTableSegment"), ReadOnly(true)] 
		//public List<MXFDeltaEntry> DeltaEntries { get; set; }


		public MXFIndexTableSegment(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "IndexTableSegment") //base(headerKLV, "IndexTableSegment", KeyType.IndexSegment)
		{
		}


		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
		{
			switch (localTag.Tag)
			{
				case 0x3F05: this.EditUnitByteCount = reader.ReadD(); return true;
				case 0x3F06: this.IndexSID = reader.ReadD(); return true;
				case 0x3F07: this.BodySID = reader.ReadD(); return true;
				case 0x3F0C: this.IndexStartPosition = reader.ReadL(); return true;
				case 0x3F0D: this.IndexDuration = reader.ReadL(); return true;
				case 0x3F0B:
					this.IndexEditRateNum = reader.ReadD();
					this.IndexEditRateDen = reader.ReadD();
					return true;
				case 0x3F0A:  // Index entry array
					{
						this.NbIndexEntries = reader.ReadD();
						UInt32 entryLength = reader.ReadD();

						if (this.NbIndexEntries > 0)
						{
							MXFObject indexCollection = this.AddChild("IndexEntries");

							for (UInt64 i = 0; i < this.NbIndexEntries; i++)
							{
								long next = reader.Position + entryLength;

								indexCollection.AddChild(new MXFIndexEntry(this.IndexStartPosition + i, reader));

								reader.Seek(next);
							}
						}
					}
					return true;

				case 0x3F09:  // Delta entry array
					{ 
						this.NbDeltaEntries = reader.ReadD();
						UInt32 entryLength = reader.ReadD();

						if (this.NbDeltaEntries > 0)
						{
							MXFObject deltaCollection = this.AddChild("DeltaEntries");
							for (int i = 0; i < this.NbDeltaEntries; i++)
							{
								long next = reader.Position + entryLength;

								deltaCollection.AddChild(new MXFDeltaEntry(reader));

								reader.Seek(next);
							}
						}
					}
					return true;
			}
			return false; // Not parsed, use default parsing
		}


		public override string ToString()
		{
			return string.Format("Index Table Segment {0} [len {1}]", this.Offset, this.Length);
		}
	}
}
