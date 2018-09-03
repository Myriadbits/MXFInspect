
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFSourcePackage : MXFMetaData
	{
		[CategoryAttribute("SourcePackage"), ReadOnly(true)]
		public UInt32 TracksCount { get; set; }
		[CategoryAttribute("SourcePackage"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey PackageUID { get; set; }
		[CategoryAttribute("SourcePackage"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey ULUID { get; set; }
		[CategoryAttribute("SourcePackage"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey DescriptorUID { get; set; }

		public MXFSourcePackage(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "SourcePackage")
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
				case 0x4403:
					this.TracksCount = ReadKeyList(reader, "Tracks", "Track");
					return true;

				case 0x4401: // UMID
					this.ULUID = new MXFRefKey(reader);
					this.PackageUID = new MXFRefKey(reader);
					return true;
	
				case 0x4701: // Descriptor
					this.DescriptorUID = new MXFRefKey(reader);
					return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
