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

using System.ComponentModel;

namespace Myriadbits.MXF
{
	[ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010401.00000000")]
	public class MXFDescriptiveFramework : MXFInterchangeObject
	{
		public readonly UL linkedDMPluginID_Key = new UL(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x05, 0x20, 0x07, 0x01, 0x0c, 0x00, 0x00, 0x00);

		public MXFDescriptiveFramework(MXFReader reader, MXFPack pack)
			: base(reader, pack, "DescriptiveFramework")
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
		{
			if (localTag.Key != null)
			{
				switch (localTag.Key)
				{
					case var _ when localTag.Key == linkedDMPluginID_Key: 
						this.AddChild(reader.ReadReference<MXFDescriptiveMarker>("LinkedDescriptiveFrameworkPluginID")); return true;
				}
			}

			return base.ParseLocalTag(reader, localTag);
		}
	}
}
