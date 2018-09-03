
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFSourceClip : MXFMetaData
	{
		[CategoryAttribute("SourceClip"), ReadOnly(true)]
		public UInt64 Duration { get; set; }
		[CategoryAttribute("SourceClip"), ReadOnly(true)]
		public UInt64 StartPosition { get; set; }
		[CategoryAttribute("SourceClip"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey SourcePackageID { get; set; }
		[CategoryAttribute("SourceClip"), ReadOnly(true)]
		public UInt32 SourceTrackId { get; set; }

		public MXFSourceClip(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "SourceClip")
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
				case 0x1201: this.StartPosition = reader.ReadL(); return true;
				case 0x0202: this.Duration = reader.ReadL(); return true;
				case 0x1101:
					reader.Skip(16); // Only read last 16 bytes
					this.SourcePackageID = new MXFRefKey(reader);
					return true;
				case 0x1102: this.SourceTrackId = reader.ReadD(); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
