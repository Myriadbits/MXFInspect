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

using System.ComponentModel;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011900")]
	public class MXFControlPoint : MXFInterchangeObject
	{
		private const string CATEGORYNAME = "ControlPoint";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05300508.00000000")]
		public MXFEditHint EditHint { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.0530050d.00000000")]
		public object ControlPointValue { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020103.10020100")]
		public MXFRational ControlPointTime { get; set; }


		public MXFControlPoint(MXFPack pack)
			: base(pack, "ControlPoint")
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
		{
			switch (localTag.TagValue)
			{
				case 0x1a04: 
					this.EditHint = (MXFEditHint)reader.ReadByte();
                    localTag.Value = this.EditHint; 
					return true;
				case 0x1a02: 
					this.ControlPointValue = reader.ReadArray<byte>(reader.ReadByte, localTag.Length.Value);
					localTag.Value = this.ControlPointValue;
					return true;
				case 0x1a03: 
					this.ControlPointTime = reader.ReadRational(); 
					localTag.Value = this.ControlPointTime;
					return true;
			}	
			return base.ReadLocalTagValue(reader, localTag);
		}

	}
}
