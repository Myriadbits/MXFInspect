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
using Serilog;
using System.Collections;

namespace Myriadbits.MXF.Identifiers
{
    public static class SMPTEULDictionary
    {
        private static bool Initialized { get; set; }
        private static Dictionary<ByteArray, ULDescription> LabelsDictionary { get; set; }
        private static Dictionary<ByteArray, ULDescription> ElementsDictionary { get; set; }
        private static Dictionary<ByteArray, ULDescription> GroupsDictionary { get; set; }
        private static Dictionary<ByteArray, ULDescription> EssencesDictionary { get; set; }

        public static int TotalEntriesCount { get; private set; } = 0;

        public static void Initialize()
        {
            if (!Initialized)
            {
                LabelsDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTEUL_DictionaryComparer());
                ElementsDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTEUL_DictionaryComparer());
                GroupsDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTEUL_DictionaryComparer());
                EssencesDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTEUL_DictionaryComparer());

                FillDictionary(Properties.Resources.Labels, LabelsDictionary);
                Log.ForContext(typeof(SMPTEULDictionary)).Information($"A total of {LabelsDictionary.Count} SMPTE labels register entries added to SMPTE dictionary");

                FillDictionary(Properties.Resources.Elements, ElementsDictionary);
                Log.ForContext(typeof(SMPTEULDictionary)).Information($"A total of {ElementsDictionary.Count} SMPTE elements register entries added to SMPTE dictionary");

                FillDictionary(Properties.Resources.Groups, GroupsDictionary);
                Log.ForContext(typeof(SMPTEULDictionary)).Information($"A total of {GroupsDictionary.Count} SMPTE groups register entries added to SMPTE dictionary");

                FillDictionary(Properties.Resources.Essence, EssencesDictionary);
                Log.ForContext(typeof(SMPTEULDictionary)).Information($"A total of {EssencesDictionary.Count} SMPTE essences register entries added to SMPTE dictionary");

                TotalEntriesCount = LabelsDictionary.Count + ElementsDictionary.Count + GroupsDictionary.Count + EssencesDictionary.Count;
                Log.ForContext(typeof(SMPTEULDictionary)).Information($"SMPTE Dictionary with a total of {TotalEntriesCount} entries loaded");
               
                Initialized = true;
            }
        }

        public static ULDescription GetDescription(ByteArray array)
        {

            Initialize();

            if (LabelsDictionary.TryGetValue(array, out var smpteDescription))
            {
                return smpteDescription;
            }
            else if (ElementsDictionary.TryGetValue(array, out smpteDescription))
            {
                return smpteDescription;
            }
            else if (GroupsDictionary.TryGetValue(array, out smpteDescription))
            {
                return smpteDescription;
            }
            else if (EssencesDictionary.TryGetValue(array, out smpteDescription))
            {
                return smpteDescription;
            }
            else return null;

        }

        public static ByteArray GetByteArrayFromSMPTEULString(string smpteString)
        {
            var bytes = GetBytesFromSMPTEULString(smpteString);
            return new ByteArray(bytes);
        }

        private static void FillDictionary(string s, IDictionary<ByteArray, ULDescription> dict)
        {
            XElement regEntries = XElement.Parse(s);
            string ns = regEntries.Name.NamespaceName;
            AddEntries(dict, regEntries, ns);
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
                        Log.ForContext(typeof(SMPTEULDictionary)).Warning($"Unable to add SMPTE entry {entry} to SMPTE dictionary as entry is already present: {@e}", entry);
                    }
                    Log.ForContext(typeof(SMPTEULDictionary)).Debug($"SMPTE entry {entry} added successfully to SMPTE dictionary");
                    Log.ForContext(typeof(SMPTEULDictionary)).Verbose($"Details of SMPTE entry: {@e}", entry);
                }
                else
                {
                    // TODO if entry not parseable it is null
                    // TODO catch this on a lower level, maybe via an exception
                    Log.ForContext(typeof(SMPTEULDictionary)).Warning($"Unable to parse SMPTE entry {entry}: {@e}", entry);
                }
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

        internal class KeyPartialMatchComparer : IEqualityComparer<ByteArray>
        {
            // if the keys to compare are of the same category (meaning the same hash) compare
            // whether the byte sequence is equal
            public bool Equals(ByteArray x, ByteArray y)
            {
                return x.IsWildCardEqual(y);
            }

            public int GetHashCode(ByteArray obj)
            {
                // hash only the first 12 bytes (prefix is 4 bytes + 5th byte = key category)
                return 0;
            }
        }
    }
}
