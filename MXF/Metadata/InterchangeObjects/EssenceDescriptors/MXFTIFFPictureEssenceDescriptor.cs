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

namespace Myriadbits.MXF
{
	// http://www.smpte-ra.org/reg/395/2014/13/1/aaf 	
	// urn:smpte:ul:060e2b34.027f0101.0d010101.01016900
	public class MXFTIFFPictureEssenceDescriptor : MXFGenericPictureEssenceDescriptor
	{

		/// <summary>
		/// Constructor, set the correct descriptor name
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="headerKLV"></param>
		public MXFTIFFPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "TIFFEssenceDescriptor")
		{
		}
	}
}
