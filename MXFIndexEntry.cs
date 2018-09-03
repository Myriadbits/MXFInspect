using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXFInspect
{
	public class MXFIndexEntry : MXFObject
	{
		[CategoryAttribute("IndexEntry"), ReadOnly(true)]
		public UInt64 Index { get; set; }
		[CategoryAttribute("IndexEntry"), ReadOnly(true)] 
		public byte TemporalOffset { get; set; }
		[CategoryAttribute("IndexEntry"), ReadOnly(true)] 
		public byte KeyFrameOffset { get; set; }
		[CategoryAttribute("IndexEntry"), ReadOnly(true)] 
		public byte Flag { get; set; }
		[CategoryAttribute("IndexEntry"), ReadOnly(true)] 
		public UInt64 StreamOffset { get; set; }

		public MXFIndexEntry(UInt64 index, MXFReader reader)
		{
			this.Index = index;
			this.TemporalOffset = reader.ReadB();
			this.KeyFrameOffset = reader.ReadB();
			this.Flag = reader.ReadB();
			this.StreamOffset = reader.ReadL();
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("Index[{0}] - TemporalOffset {1}, KeyFrameOffset {2}, Offset {3}", this.Index, this.TemporalOffset, this.KeyFrameOffset, this.StreamOffset);
		}

	}
}
