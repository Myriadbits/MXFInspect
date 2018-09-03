
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFEssenceContainerData : MXFMetaData
	{
		[CategoryAttribute("EssenceContainerData"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey LinkedPackageUID { get; set; }
		[CategoryAttribute("EssenceContainerData"), ReadOnly(true)]
		public UInt32 IndexSID { get; set; }
		[CategoryAttribute("EssenceContainerData"), ReadOnly(true)]
		public UInt32 BodySID { get; set; }

		public MXFEssenceContainerData(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "EssenceContainerData")
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
				case 0x2701: this.LinkedPackageUID = reader.ReadRefKey(); return true;
				case 0x3F06: this.IndexSID = reader.ReadD(); return true;
				case 0x3F07: this.BodySID = reader.ReadD(); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
