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
	public class MXFAES3AudioEssenceDescriptor : MXFWaveAudioEssenceDescriptor
	{
		[CategoryAttribute("AES3AudioEssenceDescriptor"), Description("3D0D")]
		public MXFEmphasis? Emphasis { get; set; }
		[CategoryAttribute("AES3AudioEssenceDescriptor"), Description("3D0F")]
		public UInt16? BlockStartOffset { get; set; }
		[CategoryAttribute("AES3AudioEssenceDescriptor"), Description("3D08")]
		public byte? AuxiliaryBitsMode { get; set; }
		[CategoryAttribute("AES3AudioEssenceDescriptor"), Description("3D10")]
		public byte[] ChannelStatusMode { get; set; }
		[CategoryAttribute("AES3AudioEssenceDescriptor"), Description("3D11")]
		public byte[] FixedChannelStatusData { get; set; }
		[CategoryAttribute("AES3AudioEssenceDescriptor"), Description("3D12")]
		public byte[] UserDataMode { get; set; }
		[CategoryAttribute("AES3AudioEssenceDescriptor"), Description("3D13")]
		public byte[] FixedUserData { get; set; }
		//[CategoryAttribute("AES3AudioEssenceDescriptor"), ReadOnly(true)]
		//public UInt32? LinkedTimecodeTrackID { get; set; }
		//[CategoryAttribute("AES3AudioEssenceDescriptor"), ReadOnly(true)]
		//public byte? DataStreamNumber { get; set; }
		
		
		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFAES3AudioEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "AES3 Audio Essence Descriptor")
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
				case 0x3D0D: this.Emphasis = (MXFEmphasis)reader.ReadB(); return true;
				case 0x3D0F: this.BlockStartOffset = reader.ReadW(); return true;
				case 0x3D08: this.AuxiliaryBitsMode = reader.ReadB(); return true;
				case 0x3D10: 
						this.ChannelStatusMode = new byte[localTag.Size];
						reader.Read(this.ChannelStatusMode, localTag.Size);
						return true;
				case 0x3D11:
						this.FixedChannelStatusData = new byte[localTag.Size];
						reader.Read(this.FixedChannelStatusData, localTag.Size);
						return true;
				case 0x3D12:
						this.UserDataMode = new byte[localTag.Size];
						reader.Read(this.UserDataMode, localTag.Size);
						return true;
				case 0x3D13:
						this.FixedUserData = new byte[localTag.Size];
						reader.Read(this.FixedUserData, localTag.Size);
						return true;
			}
			return base.ParseLocalTag(reader, localTag);
		}

	}
}
