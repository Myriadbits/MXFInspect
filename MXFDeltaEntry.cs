using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXFInspect
{
	public class MXFDeltaEntry : MXFObject
	{
		[CategoryAttribute("DeltaEntry"), ReadOnly(true)] 
		public byte DeltaPos { get; set; }
		[CategoryAttribute("DeltaEntry"), ReadOnly(true)]
		public byte Slice { get; set; }
		[CategoryAttribute("DeltaEntry"), ReadOnly(true)]
		public UInt32 ElementDelta { get; set; }

		public MXFDeltaEntry(MXFReader reader)
		{
			this.DeltaPos = reader.ReadB();
			this.Slice = reader.ReadB();
			this.ElementDelta = reader.ReadD();
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("DeltaEntry - DeltaPos {0}, Slice {1}, ElementDelta {2}", this.DeltaPos, this.Slice, this.ElementDelta);
		}
	}
}
