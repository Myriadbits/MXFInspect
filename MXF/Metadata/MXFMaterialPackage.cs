
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFMaterialPackage : MXFMetaData
	{
		[CategoryAttribute("MaterialPackage"), ReadOnly(true)]
		public UInt32 TrackCount { get; set; }
		[CategoryAttribute("MaterialPackage"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey PackageUID { get; set; }
		[CategoryAttribute("MaterialPackage"), ReadOnly(true)]
		public string MaterialPackageName { get; set; }
		[CategoryAttribute("MaterialPackage"), ReadOnly(true)]
		public DateTime CreationDate { get; set; }
		[CategoryAttribute("MaterialPackage"), ReadOnly(true)]
		public DateTime ModifiedDate { get; set; }

		public MXFMaterialPackage(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "MaterialPackage")
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
				case 0x4401: this.PackageUID = reader.ReadRefKey(); return true;
				case 0x4402: this.MaterialPackageName = reader.ReadS(localTag.Size); return true;
				case 0x4403: this.TrackCount = ReadKeyList(reader, "Tracks", "Track"); return true;
				case 0x4404: this.ModifiedDate = reader.ReadTimestamp(); return true;
				case 0x4405: this.CreationDate = reader.ReadTimestamp(); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
