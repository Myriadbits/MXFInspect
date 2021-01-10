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
using System.Collections.Generic;
using System.Xml.Linq;

namespace Myriadbits.MXF.Identifiers
{
    public static class KeyDictionary
    {
        public static Dictionary<MXFShortKey, string[]> GetKeys()
        {
            var dict = new Dictionary<MXFShortKey, string[]>();

            //Parse SMPTE Labels register

            XElement regEntries;
            XNamespace ns = "http://www.smpte-ra.org/schemas/400/2012";

            regEntries = XElement.Parse(MXF.Properties.Resources.Labels);
            AddEntries(dict, regEntries, ns);

            // Parse SMPTE Elements register

            ns = "http://www.smpte-ra.org/schemas/335/2012";
            regEntries = XElement.Parse(MXF.Properties.Resources.Elements);
            AddEntries(dict, regEntries, ns);

            //Parse SMPTE Groups register

            ns = "http://www.smpte-ra.org/ns/395/2016";
            regEntries = XElement.Parse(MXF.Properties.Resources.Groups);

            foreach (var el in regEntries.Element(ns + "Entries").Elements(ns + "Entry"))
            {
                UInt64 value1 = 0;
                UInt64 value2 = 0;
                string UL_string = "";
                string name_string = "";
                string definition_string = "";
                string notes_string = "";
                var x = el.Element(ns + "UL");
                if (x != null) UL_string = x.Value.Replace("urn:smpte:ul:", "").Replace(".", "");
                else continue; // No UL --> ignore this entry
                //Debug.WriteLine(UL_string);
                value1 = Convert.ToUInt64(UL_string.Substring(0, 16), 16);
                value2 = Convert.ToUInt64(UL_string.Substring(16, 16), 16);
                MXFShortKey shortKey = new MXFShortKey(value1, value2);
                x = el.Element(ns + "Name");
                if (x != null) name_string = x.Value;
                x = el.Element(ns + "Definition");
                if (x != null) definition_string = x.Value;
                x = el.Element(ns + "Notes");
                if (x != null) notes_string = x.Value;
                //Debug.WriteLine(shortKey.ToString() + name_string +  definition_string + defining_document_string);
                dict.Add(shortKey, new string[] { name_string + " - " + definition_string, "", notes_string });
            }

            return dict;
        }

        private static void AddEntries(IDictionary<MXFShortKey, string[]> dict, XElement regEntries, XNamespace ns)
        {
            foreach (var e in regEntries.Descendants(ns + "Entry"))
            {
                var entry = ParseEntry(ns, e);
                if (entry.HasValue)
                {
                    dict.Add(entry.Value);
                }
            }
        }

        private static KeyValuePair<MXFShortKey, string[]>? ParseEntry(XNamespace ns, XElement e)
        {
            var UL_string = (string)e.Element(ns + "UL") ?? "";
            if (!string.IsNullOrEmpty(UL_string))
            {
                MXFShortKey shortKey = GetShortKeyFromSMPTEULString(UL_string);
                string name = (string)e.Element(ns + "Name") ?? "";
                string definition = (string)e.Element(ns + "Definition") ?? "";
                string definingDocument = (string)e.Element(ns + "DefiningDocument") ?? "";
                return new KeyValuePair<MXFShortKey, string[]>(shortKey, new string[] { name, definition, definingDocument });
            }
            return null;
        }

        private static MXFShortKey GetShortKeyFromSMPTEULString(string smpteString)
        {
            const int hexBase = 16;
            string byteString = smpteString.Replace("urn:smpte:ul:", "").Replace(".", "");
            UInt64 value1 = Convert.ToUInt64(byteString.Substring(0, 16), hexBase);
            UInt64 value2 = Convert.ToUInt64(byteString.Substring(16, 16), hexBase);
            return new MXFShortKey(value1, value2);
        }
    }
}
