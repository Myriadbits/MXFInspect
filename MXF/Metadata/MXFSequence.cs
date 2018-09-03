
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFSequence : MXFMetaData
	{
		[CategoryAttribute("SourcePackage"), ReadOnly(true)]
		public UInt64 Duration { get; set; }		
		[CategoryAttribute("SourcePackage"), ReadOnly(true)]
		public MXFRefKey DataDefinitionUID { get; set; }
		[CategoryAttribute("SourcePackage"), ReadOnly(true)]
		public UInt32 StructuralComponentsCount { get; set; }
		[CategoryAttribute("SourcePackage"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey DescriptorUID { get; set; }

		public MXFSequence(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Sequence")
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
				case 0x0202: this.Duration = reader.ReadL(); return true;
				case 0x0201: this.DataDefinitionUID = new MXFRefKey(reader); return true;
				case 0x1001: this.StructuralComponentsCount = ReadKeyList(reader, "Structural components", "Structural component"); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
