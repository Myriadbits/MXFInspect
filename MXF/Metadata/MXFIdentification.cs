
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFIdentification : MXFMetaData
	{
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public MXFKey GenerationUID { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public string CompanyName { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public MXFKey ProductUID { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public string ProductName { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public string ProductVersionString { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public DateTime ModificationDate { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public string Platform { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public UInt16[] ProductVersion { get; set; }
		[CategoryAttribute("Identification"), ReadOnly(true)]
		public UInt16[] ToolkitVersion { get; set; }

		public MXFIdentification(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Identification")
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
				case 0x3C09: this.GenerationUID = reader.ReadKey(); return true;
				case 0x3C01: this.CompanyName = reader.ReadS(localTag.Size); return true;
				case 0x3C02: this.ProductName = reader.ReadS(localTag.Size); return true;
				case 0x3C03: this.ProductVersion = reader.ReadVersion(); return true;
				case 0x3C04: this.ProductVersionString = reader.ReadS(localTag.Size); return true;
				case 0x3C05: this.ProductUID = reader.ReadKey(); return true;
				case 0x3C06: this.ModificationDate = reader.ReadTimestamp(); return true;
				case 0x3C07: this.ToolkitVersion = reader.ReadVersion(); return true;
				case 0x3C08: this.Platform = reader.ReadS(localTag.Size); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
