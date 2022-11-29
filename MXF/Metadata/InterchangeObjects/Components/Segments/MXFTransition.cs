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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011700")]
	public class MXFTransition : MXFSegment
	{
		private const string CATEGORYNAME = "Transition";

		[Category(CATEGORYNAME)]
		[ULElement("urn:smpte:ul:060e2b34.01010102.07020103.01060000")]
		public MXFPosition? CutPoint { get; set; }
		
		public MXFTransition(IKLVStreamReader reader, MXFPack pack, string metadataName)
			: base(reader, pack, "Transition")
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
		{
            switch (localTag.TagValue)
            {
				case 0x1801: this.AddChild(reader.ReadReference<MXFOperationGroup>("TransitionOperation")); return true;
				case 0x1802: this.CutPoint = reader.ReadUInt64(); return true;
			}
			
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
