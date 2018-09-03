
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFPreface : MXFMetaData
	{
		[CategoryAttribute("Preface"), ReadOnly(true)]
		public DateTime LastModificationDate { get; set; }
		[CategoryAttribute("Preface"), ReadOnly(true)]
		public UInt16 Version { get; set; }
		[CategoryAttribute("Preface"), ReadOnly(true)]
		public UInt32 ObjectModelVersion { get; set; }
		[CategoryAttribute("Preface"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey ContentStorageUID { get; set; }
		[CategoryAttribute("Preface"), ReadOnly(true)]
		public MXFKey OperationalPatternUL { get; set; }
		[CategoryAttribute("Preface"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey PrimaryPackageUID { get; set; }

		public MXFPreface(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Preface")
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
				case 0x3B02: this.LastModificationDate = reader.ReadTimestamp(); return true;
				case 0x3B05: this.Version = reader.ReadW(); return true;
				case 0x3B07: this.ObjectModelVersion = reader.ReadD(); return true;
				case 0x3B03: this.ContentStorageUID = new MXFRefKey(reader, 16, "ContentStorageUID"); return true;
				case 0x3B08: this.PrimaryPackageUID = new MXFRefKey(reader, 16, "PrimaryPackageUID"); return true;
				case 0x3B09: this.OperationalPatternUL = new MXFKey(reader, 16); return true;
				case 0x3B06: ReadKeyList(reader, "Identifications", "Identification"); return true;
				case 0x3B0A: ReadKeyList(reader, "Essencecontainers", "Essencecontainer"); return true;
				case 0x3B0B: ReadKeyList(reader, "Metadata Schemes", "Metadata scheme"); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
