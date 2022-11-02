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
using System.Diagnostics;

namespace Myriadbits.MXF.Identifiers.UL
{
    public static class SMPTEULDictionary
    {
        private static Dictionary<ByteArray, ULDescription> Dictionary { get; set; }

        public static Dictionary<ByteArray, ULDescription> GetEntries()
        {
            // if already initialized return it
            if (Dictionary != null)
            {
                return Dictionary;
            }
            else
            {
                Dictionary = new Dictionary<ByteArray, ULDescription>(new SMPTEUL_DictionaryComparer());

                //Parse SMPTE Labels register

                XElement regEntries;
                XNamespace ns = "http://www.smpte-ra.org/schemas/400/2012";

                regEntries = XElement.Parse(Properties.Resources.Labels);
                AddEntries(Dictionary, regEntries, ns);

                // Parse SMPTE Elements register

                ns = "http://www.smpte-ra.org/schemas/335/2012";
                regEntries = XElement.Parse(Properties.Resources.Elements);
                AddEntries(Dictionary, regEntries, ns);

                //Parse SMPTE Groups register

                ns = "http://www.smpte-ra.org/ns/395/2016";
                regEntries = XElement.Parse(Properties.Resources.Groups);
                AddEntries(Dictionary, regEntries, ns);

                var values = Dictionary.Values.OrderBy(s => s.Name).Select(o => o.Name).ToList();

                return Dictionary;
            }
        }

        private static void AddEntries(IDictionary<ByteArray, ULDescription> dict, XElement regEntries, XNamespace ns)
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
                // TODO: heavy performance degrade on this debug statement!?
                //Debug.WriteLine("Unable to parse entry!");
            }
        }

        private static KeyValuePair<ByteArray, ULDescription>? ParseEntry(XNamespace ns, XElement e)
        {
            var UL_string = (string)e.Element(ns + "UL") ?? "";
            if (!string.IsNullOrEmpty(UL_string))
            {
                var byteArray = GetByteArrayFromSMPTEULString(UL_string);

                var keyDescription = new ULDescription
                {
                    Name = e.Element(ns + "Name")?.Value ?? "",
                    Definition = e.Element(ns + "Definition")?.Value ?? "",
                    DefiningDocument = e.Element(ns + "DefiningDocument")?.Value ?? "",
                    IsDeprecated = e.Element(ns + "IsDeprecated")?.Value ?? "",
                    Notes = e.Element(ns + "Notes")?.Value ?? "",
                    Kind = e.Element(ns + "Kind")?.Value ?? "",
                    IsConcrete = e.Element(ns + "IsConcrete")?.Value ?? "",
                    Applications = e.Element(ns + "IsConcrete")?.Value ?? "",
                };

                return new KeyValuePair<ByteArray, ULDescription>(byteArray, keyDescription);
            }
            return null;
        }

        public static ByteArray GetByteArrayFromSMPTEULString(string smpteString)
        {
            var bytes = GetBytesFromSMPTEULString(smpteString);
            return new ByteArray(bytes);
        }

        private static byte[] GetBytesFromSMPTEULString(string smpteString)
        {
            int hexBase = 16;
            var byteArray = new byte[hexBase];
            string ulString = smpteString.Replace("urn:smpte:ul:", "").Replace(".", "");
            for (int i = 0, j = 0; j < ulString.Length; i++, j += 2)
            {
                byteArray[i] = Convert.ToByte(ulString.Substring(j, 2), hexBase);
            }
            return byteArray;
        }
    }
}
