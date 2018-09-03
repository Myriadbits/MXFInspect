
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myriadbits.MXFInspect
{
	class MXFContentStorage : MXFMetaData
	{
		public MXFContentStorage(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "ContentStorage")
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
				case 0x1901: ReadKeyList(reader, "Packages", "Package"); return true;
				case 0x1902: ReadKeyList(reader, "EssenceContainer data", "Essencecontainer data"); return true;
			}
			return false; // Not parsed, use default parsing
		}

	}
}
