
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	public class MXFGenericDescriptor : MXFMetaData
	{
		private static Dictionary<int, string> m_metaTypes = new Dictionary<int, string>();

		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 SubDescriptorCount { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true), TypeConverter(typeof(ExpandableObjectConverter))]
		public MXFRefKey EssenceContainerUL { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 LinkedTrackId { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public MXFRefKey EssenceCodecId { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 Width { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 Height { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public byte FrameLayout { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 AspectRatioNum { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 AspectRatioDen { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public byte FieldDominance { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 ComponentDepth { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 HorizontalSubSampling { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 VerticalSubSampling { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 SampleRateNum { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 SampleRateDen { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 ChannelCount { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public UInt32 BitsPerSample { get; set; }
		[CategoryAttribute("Descriptor"), ReadOnly(true)]
		public byte ActiveFormat { get; set; }


		/// <summary>
		/// Static constructor to initialize the static array
		/// </summary>
		static MXFGenericDescriptor()
		{
			// Add all meta data 
			m_metaTypes.Add(0x14, "Descriptor: Timecode");
			m_metaTypes.Add(0x23, "Descriptor: Data container");
			m_metaTypes.Add(0x27, "Generic Picture Essence Descriptor");
			m_metaTypes.Add(0x28, "CDCI Essence Descriptor");
			m_metaTypes.Add(0x29, "RGBA Essence Descriptor");
			m_metaTypes.Add(0x42, "Generic Sound Essence Descriptor");
			m_metaTypes.Add(0x43, "Generic Data Essence Descriptor");
			m_metaTypes.Add(0x44, "MultipleDescriptor");
			m_metaTypes.Add(0x47, "Descriptor: AES3");
			m_metaTypes.Add(0x48, "Descriptor: Wave");
			m_metaTypes.Add(0x51, "Descriptor: MPEG 2 Video");
			m_metaTypes.Add(0x5C, "Descriptor: ANC Data Descriptor, SMPTE 436 - 7.3");
			m_metaTypes.Add(0x2F, "Preface");
			m_metaTypes.Add(0x30, "Identification");
			m_metaTypes.Add(0x39, "Event track (DM)");
			m_metaTypes.Add(0x41, "DM Segment");
			m_metaTypes.Add(0x45, "DM Source clip");
			m_metaTypes.Add(0x60, "Package marker object");
			m_metaTypes.Add(0x25, "File Descriptor");
			m_metaTypes.Add(0x32, "Network Locator");
			m_metaTypes.Add(0x33, "Text Locator");
			m_metaTypes.Add(0x61, "Application Plug-in object");
			m_metaTypes.Add(0x62, "Application Referenced object");

		}

		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFGenericDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Descriptor")
		{
			if (m_metaTypes.ContainsKey(this.Key[14]))
				this.MetaDataName = m_metaTypes[this.Key[14]];
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
		{
			switch (localTag.Tag)
			{
				case 0x3F01: this.SubDescriptorCount = ReadKeyList(reader, "SubDescriptors", "SubDescriptor"); return true;
				case 0x3004: this.EssenceContainerUL = new MXFRefKey(reader); return true;
				case 0x3006: this.LinkedTrackId = reader.ReadD(); return true;
				case 0x3201: this.EssenceCodecId = new MXFRefKey(reader); return true;
				case 0x3202: this.Height = reader.ReadD(); return true;
				case 0x3203: this.Width = reader.ReadD(); return true;
				case 0x320C: this.FrameLayout = reader.ReadB(); return true;
				case 0x320E:
					this.AspectRatioNum = reader.ReadD();
					this.AspectRatioDen = reader.ReadD();
					return true;
				case 0x3212: this.FieldDominance = reader.ReadB(); return true;
				case 0x3218: this.ActiveFormat = reader.ReadB(); return true;
				case 0x3301: this.ComponentDepth = reader.ReadD(); return true;
				case 0x3302: this.HorizontalSubSampling = reader.ReadD(); return true;
				case 0x3308: this.VerticalSubSampling = reader.ReadD(); return true;
				case 0x3D01: this.BitsPerSample = reader.ReadD(); return true;
				case 0x3D03:
					this.SampleRateNum = reader.ReadD();
					this.SampleRateDen = reader.ReadD();
					return true;
				case 0x3D06: this.EssenceCodecId = new MXFRefKey(reader); return true;
				case 0x3D07: this.ChannelCount = reader.ReadD(); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
