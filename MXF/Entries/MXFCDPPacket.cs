#region license
//
// MXF - Myriadbits .NET MXF library. 
// Read MXF Files.
// Copyright (C) 2015 Myriadbits, Jochem Bakker
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// For more information, contact me at: info@myriadbits.com
//
#endregion

using System;
using System.ComponentModel;

namespace Myriadbits.MXF
{
	public enum MXFCDFFrameRate
	{
		Forbidden = 0,
		Rate_24000_1001 = 1,
		Rate_24 = 2, 
		Rate_25 = 3,
		Rate_30000_1001 = 4, 
		Rate_30 = 5, 
		Rate_50 = 6, 
		Rate_60000_1001 = 7, 
		Rate_60 = 8,
		Reserved_9,
		Reserved_10,
		Reserved_11,
		Reserved_12,
		Reserved_13,
		Reserved_14,
		Reserved_15
	};

	public class MXFCDPPacket : MXFObject
	{
		private const string CATEGORYNAME = "CDPPacket";

		private MXFCDFFrameRate FrameRateE { get; set; }

		[Category(CATEGORYNAME)]
		public double? FrameRate { get; set; }

		[Category(CATEGORYNAME)]
		public bool TimeCodePresent { get; set; }
		[Category(CATEGORYNAME)]
		public bool CCDataPresent { get; set; }
		[Category(CATEGORYNAME)]
		public bool SVCInfoPresent { get; set; }
		[Category(CATEGORYNAME)]
		public bool SVCInfoStart { get; set; }
		[Category(CATEGORYNAME)]
		public bool SVCInfoChange { get; set; }
		[Category(CATEGORYNAME)]
		public bool SVCInfoComplete { get; set; }
		[Category(CATEGORYNAME)]
		public bool CaptionServiceActive { get; set; }
		[Category(CATEGORYNAME)]
		public UInt16 SequenceCounter { get; set; }

		[Category(CATEGORYNAME)]
		public MXFTimeStamp TimeCode { get; set; }
		

		public MXFCDPPacket(MXFReader reader)
			: base(reader)
		{
			UInt16 identifier = reader.ReadUInt16();
			if (identifier == 0x9669)
			{
				this.Length = reader.ReadByte();
				this.FrameRateE = (MXFCDFFrameRate) ((reader.ReadByte() & 0xF0) >> 4);

				switch (this.FrameRateE)
				{
					case MXFCDFFrameRate.Rate_24: this.FrameRate = 24; break;
					case MXFCDFFrameRate.Rate_24000_1001: this.FrameRate = 24000.0/1001.0; break;
					case MXFCDFFrameRate.Rate_25: this.FrameRate = 25; break;
					case MXFCDFFrameRate.Rate_30: this.FrameRate = 30; break;
					case MXFCDFFrameRate.Rate_30000_1001: this.FrameRate = 30000.0/1001.0; break;
					case MXFCDFFrameRate.Rate_50: this.FrameRate = 50; break;
					case MXFCDFFrameRate.Rate_60: this.FrameRate = 60; break;
					case MXFCDFFrameRate.Rate_60000_1001: this.FrameRate = 60000.0/1001.0; break;
				}

				byte options = reader.ReadByte();
				this.TimeCodePresent = ((options & 0x80) != 0);
				this.CCDataPresent = ((options & 0x40) != 0);
				this.SVCInfoPresent = ((options & 0x20) != 0);
				this.SVCInfoStart = ((options & 0x10) != 0);
				this.SVCInfoChange = ((options & 0x08) != 0);
				this.SVCInfoComplete = ((options & 0x04) != 0);
				this.CaptionServiceActive = ((options & 0x02) != 0);
				this.SequenceCounter = reader.ReadUInt16();

				byte count = 0;
				long endPos = this.Offset + this.Length;
				while (reader.Position < endPos)
				{
					identifier = reader.ReadByte();
					switch (identifier)
					{
						case 0x71:
							this.TimeCode = new MXFTimeStamp();
							this.TimeCode.ParseSMPTE12M(reader, this.FrameRate.Value);
							break;
						case 0x72:
							count = (byte)(reader.ReadByte() & 0x1F);
							for (int n = 0; n < count; n++)
								this.AddChild(new MXFEntryCCData(reader));
							break;
						case 0x73:
							count = (byte)(reader.ReadByte() & 0x0F);
							for (int n = 0; n < count; n++)
								this.AddChild(new MXFEntrySVCInfo(reader));
							break;
						case 0x74:
							this.AddChild(new MXFCDPFooter(reader));
							break;	
						default:
							this.AddChild(new MXFCDPFuture(reader, (byte) identifier));
							break;
					}
				}
			}
		}

		/// <summary>
		/// Some output
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("CDP Packet - SequenceCnt {0}", this.SequenceCounter);
		}
	}
}
