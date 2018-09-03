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

using System;
using System.Collections.Generic;

namespace Myriadbits.MXF
{
	public class MXFDescriptiveFramework : MXFGenericPackage
	{
		protected static Dictionary<UInt16, string> m_FWNames = new Dictionary<UInt16, string>();

		static MXFDescriptiveFramework()
		{
			m_FWNames.Add(0x0101, "Production Framework");
			m_FWNames.Add(0x0102, "Clip Framework");
			m_FWNames.Add(0x0103, "Scene Framework");
			m_FWNames.Add(0x1001, "Titles");
			m_FWNames.Add(0x1101, "Identification");
			m_FWNames.Add(0x1201, "Group Relationship");
			m_FWNames.Add(0x1301, "Branding");
			m_FWNames.Add(0x1401, "Event");
			m_FWNames.Add(0x1402, "Publication");
			m_FWNames.Add(0x1501, "Award");
			m_FWNames.Add(0x1601, "Caption Description");
			m_FWNames.Add(0x1701, "Annotation");
			m_FWNames.Add(0x1702, "Setting Period");
			m_FWNames.Add(0x1703, "Scripting");
			m_FWNames.Add(0x1704, "Classification");
			m_FWNames.Add(0x1705, "Shot");
			m_FWNames.Add(0x1706, "Key Point");
			m_FWNames.Add(0x1801, "Participant"); 
			m_FWNames.Add(0x1A02, "Person");
			m_FWNames.Add(0x1A03, "Organisation");
			m_FWNames.Add(0x1A04, "Location");
			m_FWNames.Add(0x1B01, "Address");
			m_FWNames.Add(0x1B02, "Communication");
			m_FWNames.Add(0x1C01, "Contract");
			m_FWNames.Add(0x1C02, "Rights");
			m_FWNames.Add(0x1D01, "Picture Format");
			m_FWNames.Add(0x1E01, "Device parameters");
			m_FWNames.Add(0x1F01, "Name-Value");
			m_FWNames.Add(0x2001, "Processing");
			m_FWNames.Add(0x2002, "Project");
			m_FWNames.Add(0x1901, "Contacts List");
			m_FWNames.Add(0x1708, "Cue Words");
			m_FWNames.Add(0x7F01, "DMS-1 Framework");
			m_FWNames.Add(0x7F02, "Production/Clip Framework");
			m_FWNames.Add(0x7F10, "DMS-1 Set");
			m_FWNames.Add(0x7F11, "TextLanguage");
			m_FWNames.Add(0x7F12, "Thesaurus");
			m_FWNames.Add(0x7F1A, "Contact");
		}

		public MXFDescriptiveFramework(MXFReader reader, MXFKLV headerKLV)
			: base(reader, headerKLV, "DM Framework: <unknown>")
		{
			UInt16 setKey = (UInt16)( ( ((UInt16) headerKLV.Key[13]) << 8) + ((UInt16) headerKLV.Key[14]) );
			if (m_FWNames.ContainsKey(setKey))
				this.MetaDataName = string.Format("DM Framework: {0}", m_FWNames[setKey]);
		}

		public MXFDescriptiveFramework(MXFReader reader, MXFKLV headerKLV, string metadataName)
			: base(reader, headerKLV, metadataName)
		{
		}

		/// <summary>
		/// Overridden method to process local tags
		/// </summary>
		/// <param name="localTag"></param>
		protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
		{
			return base.ParseLocalTag(reader, localTag); 
		}

	}
}
