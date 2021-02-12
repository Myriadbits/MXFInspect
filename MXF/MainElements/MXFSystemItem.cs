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
	[Flags]
	public enum SystemBitmap
	{
		Control = 0x01,
		Data = 0x02,
		Sound = 0x04,
		Picture = 0x08,
		UserDateTime = 0x10,
		CreationDateTime = 0x20,
		SMPTELabel = 0x40,
		FECActive = 0x80
	};

	public enum SystemStreamStatus
	{
		Undefined = 0,
		HeadPackage = 1,
		StartStreamPackage = 2,
		MidStreamPackage = 3,
		EndStreamPackage = 4,
		StreamTailPackage = 5,
		StreamStartEndPackage = 6,
		Reserved = 7
	};

	public enum SystemTransferMode
	{
		Synchronous = 0,
		Isochronous = 1,
		Asynchronous = 2,
		LowLatency = 3,
	};

	public enum SystemTimingMode
	{
		Normal = 0,
		Advanced = 1,
		Dual = 2,
		Reserved = 3,
	};

	public class MXFSystemItem : MXFKLV
	{
		private const string CATEGORYNAME = "SystemItem";

		[Category(CATEGORYNAME)]
		public SystemBitmap SystemBitmap { get; set; }
		
		[Category(CATEGORYNAME)]
		public double PackageRate { get; set; }
		
		[Category(CATEGORYNAME)]
		public SystemStreamStatus StreamStatus { get; set; }
		
		[Category(CATEGORYNAME)]
		public bool LowLatencyMode { get; set; }
		
		[Category(CATEGORYNAME)]
		public SystemTransferMode TransferMode { get; set; }
		
		[Category(CATEGORYNAME)]
		public SystemTimingMode TimingMode { get; set; }
		
		[Category(CATEGORYNAME)]
		public UInt16 ChannelHandle { get; set; }
		
		[Category(CATEGORYNAME)]
		public UInt16 ContinuityCount { get; set; }

		[Category(CATEGORYNAME)]
		public MXFKey SMPTE { get; set; }

		[Category(CATEGORYNAME)]
		public string CreationDate { get; set; }
		
		[Category(CATEGORYNAME)]
		public MXFTimeStamp UserDate { get; set; }
		
		[Category(CATEGORYNAME)]
		public string UserDateFullFrameNb { get; set; }

		[Browsable(false)]
		public bool Indexed { get; set; }

		[Category(CATEGORYNAME)]
		public long EssenceOffset
		{
			get
			{
				if (this.Partition == null) return this.Offset; // Unknown
				if (this.Partition.FirstSystemItem == null) return this.Offset; // Unknown
				return (this.Offset - this.Partition.FirstSystemItem.Offset) + ((long)this.Partition.BodyOffset);
			}
		}

		public MXFSystemItem(MXFReader reader, MXFKLV headerKLV)
			: base(headerKLV, "SystemItem (CP)", KeyType.SystemItem)
		{
			this.m_eType = MXFObjectType.SystemItem;
			if (this.Key[12] == 0x14)
				this.Key.Name = "SystemItem (GC)";

			reader.Seek(this.DataOffset); // Seek to the start of the data

			// Parse system bitmap
			this.SystemBitmap = (SystemBitmap)reader.ReadByte();

			// Parse Content package rate
			byte rate = reader.ReadByte();
			int rateIndex = (rate & 0x1E) >> 1;
			int[] rates = new int[16] {0, 24, 25, 30, 48, 50, 60, 72, 75, 90, 96, 100, 120, 0, 0, 0 };
			int rateNonDrop = 1;
			if (rateIndex < 16)
				rateNonDrop = rates[rateIndex];
			this.PackageRate = rateNonDrop;
			if ((rate & 0x01) == 0x01) // 1.001 divider active?
				this.PackageRate = this.PackageRate / 1.001;


			// Parse Content Package Type
			byte type = reader.ReadByte();
			this.StreamStatus = (SystemStreamStatus)((type & 0xE0) >> 5);
			this.LowLatencyMode = ((type & 0x10) == 0x10);
			this.TransferMode = (SystemTransferMode)((type & 0x0C) >> 2);
			this.TimingMode = (SystemTimingMode)(type & 0x03);

			this.ChannelHandle = reader.ReadUInt16();
			this.ContinuityCount = reader.ReadUInt16();

			this.SMPTE = reader.ReadULKey(); // Always read even if zero

			MXFTimeStamp creationTimeStamp = reader.ReadBCDTimeCode(this.PackageRate);
			this.CreationDate = creationTimeStamp.ToString();

			this.UserDate = reader.ReadBCDTimeCode(this.PackageRate);
			this.UserDateFullFrameNb = this.UserDate.GetString(true);
		}


		public override string ToString()
		{
			return string.Format("{0}, Count {1} [{2}]", this.Key.Name, this.ContinuityCount, this.UserDateFullFrameNb);
		}		
	}
}
