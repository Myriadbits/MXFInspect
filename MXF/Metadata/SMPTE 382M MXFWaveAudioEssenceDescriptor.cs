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
	public class MXFWaveAudioEssenceDescriptor : MXFGenericSoundEssenceDescriptor
	{
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D0A")]
		public UInt16? BlockAlign { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D0B")]
		public byte? SequenceOffset { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D09")]
		public UInt32? AveragesBytesPerSecond { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D32")]
		public MXFKey ChannelAssignment { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D29")]
		public UInt32? PeakEnvelopeVersion { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D2A")]
		public UInt32? PeakEnvelopeFormat { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D2B")]
		public UInt32? PointsPerPeakValue { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D2C")]
		public UInt32? PeakEnvelopeBlockSize { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D2D")]
		public UInt32? PeakChannels { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D2E")]
		public UInt32? PeakFrames { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D2F")]
		public UInt64? PeakOfPeaksPosition { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D30")]
		public DateTime? PeakEnvelopeTimestamp { get; set; }
		[CategoryAttribute("WaveAudioEssenceDescriptor"), Description("3D31")]
		public byte[] PeakEnvelopeData { get; set; }
		
		
		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFWaveAudioEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "Wave Audio Essence Descriptor")
		{
		}

		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFWaveAudioEssenceDescriptor(MXFReader reader, MXFKLV headerKLV, string metadataName)
			: base(reader, headerKLV, metadataName)
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
				case 0x3D0A: this.BlockAlign = reader.ReadW(); return true;
				case 0x3D0B: this.SequenceOffset = reader.ReadB(); return true;
				case 0x3D09: this.AveragesBytesPerSecond = reader.ReadD(); return true;
				case 0x3D32: this.ChannelAssignment = reader.ReadKey(); return true;
				case 0x3D29: this.PeakEnvelopeVersion = reader.ReadD(); return true;
				case 0x3D2A: this.PeakEnvelopeFormat = reader.ReadD(); return true;
				case 0x3D2B: this.PointsPerPeakValue = reader.ReadD(); return true;
				case 0x3D2C: this.PeakEnvelopeBlockSize = reader.ReadD(); return true;
				case 0x3D2D: this.PeakChannels = reader.ReadD(); return true;
				case 0x3D2E: this.PeakFrames = reader.ReadD(); return true;
				case 0x3D2F: this.PeakOfPeaksPosition = reader.ReadL(); return true;
				case 0x3D30: this.PeakEnvelopeTimestamp = reader.ReadTimestamp(); return true;
				case 0x3D31: 
						this.PeakEnvelopeData = new byte[localTag.Size];
						reader.Read(this.PeakEnvelopeData, localTag.Size); 
						return true;

			}
			return base.ParseLocalTag(reader, localTag);
		}

	}
}
