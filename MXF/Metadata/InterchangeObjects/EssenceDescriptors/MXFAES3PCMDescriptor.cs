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
	public class MXFAES3PCMDescriptor : MXFWAVEPCMDescriptor
	{
		[CategoryAttribute("AES3 PCM Descriptor"), Description("3D0D")]
		public MXFEmphasis? Emphasis { get; set; }
		[CategoryAttribute("AES3 PCM Descriptor"), Description("3D0F")]
		public UInt16? BlockStartOffset { get; set; }
		[CategoryAttribute("AES3 PCM Descriptor"), Description("3D08")]
		public MXFAuxBitsMode? AuxiliaryBitsMode { get; set; }
		[CategoryAttribute("AES3 PCM Descriptor"), Description("3D10")]
		public MXFChannelStatusMode[] ChannelStatusMode { get; set; }
		[CategoryAttribute("AES3 PCM Descriptor"), Description("3D11")]
        [TypeConverter(typeof(ByteArrayConverter))]
		public byte[] FixedChannelStatusData { get; set; }
		[CategoryAttribute("AES3 PCM Descriptor"), Description("3D12")]
		public MXFUserDataMode[] UserDataMode { get; set; }
		[CategoryAttribute("AES3 PCM Descriptor"), Description("3D13")]
        [TypeConverter(typeof(ByteArrayConverter))]
		public byte[] FixedUserData { get; set; }		
		
		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFAES3PCMDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "AES3 PCM Descriptor")
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
				case 0x3D0D: this.Emphasis = (MXFEmphasis)reader.ReadByte(); return true;
				case 0x3D0F: this.BlockStartOffset = reader.ReadUInt16(); return true;
				case 0x3D08: this.AuxiliaryBitsMode = (MXFAuxBitsMode)reader.ReadByte(); return true;
                case 0x3D10: this.ChannelStatusMode = reader.ReadArray(reader.ReadChannelstatusMode, 8); return true;
                case 0x3D11:
						this.FixedChannelStatusData = reader.ReadArray(reader.ReadByte, localTag.Size);
						return true;
				case 0x3D12:
					this.UserDataMode = reader.ReadArray(reader.ReadUserDataMode, 2); return true;
				case 0x3D13:
						this.FixedUserData = reader.ReadArray(reader.ReadByte, localTag.Size);
						return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}
    }
}
