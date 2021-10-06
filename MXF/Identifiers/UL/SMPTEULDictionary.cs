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
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Myriadbits.MXF.Identifiers
{
    public static class SMPTEULDictionary
    {
        private static Dictionary<MXFShortKey, KeyDescription> Dictionary { get; set; }

        public static Dictionary<MXFShortKey, KeyDescription> GetEntries()
        {
            // if already initialized return it
            if (Dictionary != null)
            {
                return Dictionary;
            }
            else
            {
                Dictionary = new Dictionary<MXFShortKey, KeyDescription>(new SMPTEULDictionaryComparer());

                //Parse SMPTE Labels register

                XElement regEntries;
                XNamespace ns = "http://www.smpte-ra.org/schemas/400/2012";

                regEntries = XElement.Parse(MXF.Properties.Resources.Labels);
                AddEntries(Dictionary, regEntries, ns);

                // Parse SMPTE Elements register

                ns = "http://www.smpte-ra.org/schemas/335/2012";
                regEntries = XElement.Parse(MXF.Properties.Resources.Elements);
                AddEntries(Dictionary, regEntries, ns);

                //Parse SMPTE Groups register

                ns = "http://www.smpte-ra.org/ns/395/2016";
                regEntries = XElement.Parse(MXF.Properties.Resources.Groups);
                AddEntries(Dictionary, regEntries, ns);

                var values = Dictionary.Values.OrderBy(s => s.Name).Select(o => o.Name).ToList();

                return Dictionary;
            }
        }

        private static void AddEntries(IDictionary<MXFShortKey, KeyDescription> dict, XElement regEntries, XNamespace ns)
        {
            foreach (var e in regEntries.Descendants(ns + "Entry"))
            {
                var entry = ParseEntry(ns, e);
                if (entry.HasValue)
                {
                    if (dict.TryAdd(entry.Value.Key, entry.Value.Value) == false)
                    {
                        // TODO: raise an exception! (or at least log)
                        Debug.WriteLine("Entry already present!");
                    }
                }
            }
        }

        private static KeyValuePair<MXFShortKey, KeyDescription>? ParseEntry(XNamespace ns, XElement e)
        {
            var UL_string = (string)e.Element(ns + "UL") ?? "";
            if (!string.IsNullOrEmpty(UL_string))
            {
                MXFShortKey shortKey = GetShortKeyFromSMPTEULString(UL_string);

                var keyDescription = new KeyDescription
                {
                    Name = (string)e.Element(ns + "Name") ?? "",
                    Definition = (string)e.Element(ns + "Definition") ?? "",
                    DefiningDocument = (string)e.Element(ns + "DefiningDocument") ?? "",
                    IsDeprecated = (string)e.Element(ns + "IsDeprecated") ?? "",
                    Notes = (string)e.Element(ns + "Notes") ?? "",
                };

                return new KeyValuePair<MXFShortKey, KeyDescription>(shortKey, keyDescription);
            }
            return null;
        }

        public static MXFShortKey GetShortKeyFromSMPTEULString(string smpteString)
        {
            var bytes = GetByteArrayFromSMPTEULString(smpteString);
            return new MXFShortKey(bytes);
        }

        public static byte[] GetByteArrayFromSMPTEULString(string smpteString)
        {
            const int hexBase = 16;
            byte[] retVal = new byte[16];
            string byteString = smpteString.Replace("urn:smpte:ul:", "").Replace(".", "");
            for (int i = 0, j = 0; j < byteString.Length - 2; i++, j += 2)
            {
                retVal[i] = Convert.ToByte(byteString.Substring(j, 2), hexBase);
            }
            return retVal;
        }
    }
}
