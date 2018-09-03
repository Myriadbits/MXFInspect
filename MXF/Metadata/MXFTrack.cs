
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFTrack : MXFMetaData
	{
		[CategoryAttribute("Track"), ReadOnly(true)]
		public UInt32 ID { get; set; }
		[CategoryAttribute("Track"), ReadOnly(true)]
		public UInt32 Number { get; set; }
		[CategoryAttribute("Track"), ReadOnly(true)]
		public UInt32 RateNum { get; set; }
		[CategoryAttribute("Track"), ReadOnly(true)]
		public UInt32 RateDen { get; set; }
		[CategoryAttribute("Track"), ReadOnly(true)]
		public double Rate { get; set; }
		[CategoryAttribute("Track"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey SequenceUID { get; set; }
		[CategoryAttribute("Track"), ReadOnly(true)]
		public string TrackName { get; set; }

		public MXFTrack(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Track")
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
				case 0x4801: this.ID = reader.ReadD(); return true;
				case 0x4802: this.TrackName = reader.ReadS(localTag.Size); return true;
				case 0x4803: this.SequenceUID = reader.ReadRefKey(); return true;
				case 0x4804: this.Number = reader.ReadD(); return true;
				case 0x4B01:
					this.RateNum = reader.ReadD();
					this.RateDen = reader.ReadD();
					if (this.RateDen != 0)
						this.Rate = ((double) this.RateNum) / ((double) this.RateDen);
					return true;

			}
			return false; // Not parsed, use default parsing
		}

	}
}
