﻿#region license
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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01013800")]
	public class MXFTrack : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "GenericTrack";

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.01070101.00000000")]
        public UInt32? TrackID { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.01040103.00000000")]
        public UInt32? EssenceTrackNumber { get; set; }

		[Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.01070102.01000000")]
        public string TrackName { get; set; }


		public MXFTrack(IKLVStreamReader reader, MXFPack pack)
			: base(reader, pack, "Generic Track")
		{
		}


		public MXFTrack(IKLVStreamReader reader, MXFPack pack, string metadataName)
			: base(reader, pack, metadataName)
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
		{
			switch (localTag.Tag)
			{
				case 0x4801: this.TrackID = reader.ReadUInt32(); return true;
				case 0x4802: this.TrackName = reader.ReadUTF16String(localTag.Size); return true;
				case 0x4803: this.AddChild(reader.ReadReference<MXFSegment>("TrackSegment")); return true;
				case 0x4804: this.EssenceTrackNumber = reader.ReadUInt32(); return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}