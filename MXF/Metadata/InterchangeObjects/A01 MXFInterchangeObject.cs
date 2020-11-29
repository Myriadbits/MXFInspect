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

namespace Myriadbits.MXF
{
	public class MXFInterchangeObject : MXFMetadataBaseclass
	{
		public readonly MXFKey appPluginObjects_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0c, 0x06, 0x01, 0x01, 0x04, 0x02, 0x0e, 0x00, 0x00);

		[CategoryAttribute("InterchangeObject"), Description("0101")]
		public MXFKey ObjectClass { get; set; }
		[CategoryAttribute("InterchangeObject"), Description("0102")]
		public MXFUUID GenerationUID { get; set; }

		public MXFInterchangeObject(MXFReader reader, MXFKLV headerKLV, string metadataName)
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
				case 0x0102: this.GenerationUID = new MXFUUID(reader); return true;
				case 0x0101: this.ObjectClass = reader.ReadKey(); return true;
					// TODO make specific class
				case var a when localTag.Key == appPluginObjects_Key: ReadReferenceSet<MXFObject>(reader, "Application Plugin Objects", "Application Plugin Object");  return true;
			}
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
