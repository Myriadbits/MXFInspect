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
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Win32;
using Serilog;

namespace Myriadbits.MXF.Identifiers
{
    public static class SMPTERegisters
    {
        private static bool Initialized { get; set; }
        private static Dictionary<ByteArray, ULDescription> LabelsDictionary { get; set; }
        private static Dictionary<ByteArray, ULDescription> ElementsDictionary { get; set; }
        private static Dictionary<ByteArray, ULDescription> GroupsDictionary { get; set; }
        private static Dictionary<ByteArray, ULDescription> EssencesDictionary { get; set; }
        private static Dictionary<(byte, byte), DIDDescription> DIDDictionary { get; set; }

        public static int TotalEntriesCount { get; private set; } = 0;

        public static void Initialize()
        {
            if (!Initialized)
            {
                LabelsDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTERegisterComparer());
                ElementsDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTERegisterComparer());
                GroupsDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTERegisterComparer());
                EssencesDictionary = new Dictionary<ByteArray, ULDescription>(new SMPTEEssenceRegisterComparer());

                FillDictionary(Properties.Resources.Labels, LabelsDictionary);
                Log.ForContext(typeof(SMPTERegisters)).Information($"A total of {LabelsDictionary.Count} SMPTE labels register entries added to SMPTE dictionary");

                FillDictionary(Properties.Resources.Elements, ElementsDictionary);
                Log.ForContext(typeof(SMPTERegisters)).Information($"A total of {ElementsDictionary.Count} SMPTE elements register entries added to SMPTE dictionary");

                FillDictionary(Properties.Resources.Groups, GroupsDictionary);
                Log.ForContext(typeof(SMPTERegisters)).Information($"A total of {GroupsDictionary.Count} SMPTE groups register entries added to SMPTE dictionary");

                FillDictionary(Properties.Resources.Essence, EssencesDictionary);
                Log.ForContext(typeof(SMPTERegisters)).Information($"A total of {EssencesDictionary.Count} SMPTE essences register entries added to SMPTE dictionary");

                TotalEntriesCount = LabelsDictionary.Count + ElementsDictionary.Count + GroupsDictionary.Count + EssencesDictionary.Count;
                Log.ForContext(typeof(SMPTERegisters)).Information($"SMPTE Dictionary with a total of {TotalEntriesCount} entries loaded");

                FillDIDDictionary();
                Log.ForContext(typeof(SMPTERegisters)).Information($"A total of {DIDDictionary.Count} ANC Identifier Descriptions added to dictionary");

                Initialized = true;
            }
        }

        public static ULDescription GetULDescription(ByteArray array)
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


        public static DIDDescription GetDIDDescription(byte did, byte sdid)
        {
            Initialize();

            if (DIDDictionary.TryGetValue((did, sdid), out var smpteDescription))
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
                        Log.ForContext(typeof(SMPTERegisters)).Warning($"Unable to add SMPTE entry {entry} to SMPTE dictionary as entry is already present: {@e}", entry);
                    }
                    Log.ForContext(typeof(SMPTERegisters)).Debug($"SMPTE entry {entry} added successfully to SMPTE dictionary");
                    Log.ForContext(typeof(SMPTERegisters)).Verbose($"Details of SMPTE entry: {@e}", entry);
                }
                else
                {
                    // TODO if entry not parseable it is null
                    // TODO catch this on a lower level, maybe via an exception
                    Log.ForContext(typeof(SMPTERegisters)).Warning($"Unable to parse SMPTE entry {entry}: {@e}", entry);
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

        private static void FillDIDDictionary()
        {
            DIDDictionary = new Dictionary<(byte, byte), DIDDescription>();

            string allText = Properties.Resources.ANC_Identifiers;
            string[] allLines = allText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (allLines.Length > 0)
            {
                for (int n = 1; n < allLines.Length; n++) // Start at 1, skip the header
                {
                    string[] parts = allLines[n].Split(';');
                    if (parts.Length > 5)
                    {
                        try
                        {
                            Byte.TryParse(parts[0].Trim(' '), out byte dataType);
                            Byte.TryParse(parts[1].Trim(' ', 'h'), NumberStyles.HexNumber,null, out byte did);
                            Byte.TryParse(parts[2].Trim(' ', 'h'), NumberStyles.HexNumber, null, out byte sdid);

                            var description = new DIDDescription
                            {
                                DataType = dataType,
                                DID = did,
                                SDID = sdid,
                                Status = parts[3],
                                UsedWhere = parts[4],
                                Application = parts[5],
                                LastModifiedTime = parts[6]
                            };

                            DIDDictionary.Add((did, sdid), description);
                        }
                        catch (Exception ex)
                        {
                            Log.ForContext(typeof(SMPTERegisters)).Warning($"Unable to parse ANC Description entry {allLines[n]}: {@ex}", allLines[n]);
                        }
                    }
                }
            }
        }
    }
}
