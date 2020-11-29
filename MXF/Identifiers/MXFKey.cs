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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Myriadbits.MXF
{
    public enum KeyType
    {
        None,
        // Real MXF types
        Partition,
        PackageMetaDataSet,
        Essence,
        IndexSegment,
        MetaData,
        SystemItem,
        PrimerPack,
        Preface,
        Filler,
        RIP
    }


    public enum KeyCategory
    {
        Unknown = 0x00,
        Dictionary_Element = 0x01,
        Group = 0x02,
        Container_Wrapper = 0x03,
        Label = 0x04
    }



    // short keys implemented as struct, as there are loaded
    // over 1000 in the static constructor => so better performance
    public struct MXFShortKey
    {
        UInt64 Key1;
        UInt64 Key2;

        public MXFShortKey(UInt64 key1, UInt64 key2)
        {
            this.Key1 = key1;
            this.Key2 = key2;
        }

        public MXFShortKey(byte[] data)
        {
            // TODO why changing this?
            // Change endianess
            this.Key1 = 0;
            this.Key2 = 0;
            if (data.Length == 16)
            {
                byte[] datar = new byte[16];
                Array.Copy(data, datar, 16);
                Array.Reverse(datar);
                this.Key2 = BitConverter.ToUInt64(datar, 0);
                this.Key1 = BitConverter.ToUInt64(datar, 8);
            }
        }

        public override string ToString()
        {
            return string.Format(string.Format("{0:X16}.{1:X16}", this.Key1, this.Key2));
        }

    };

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFKey : MXFIdentifier, IEquatable<MXFKey>
    {
        private static Dictionary<MXFShortKey, string[]> m_ULDescriptions;


        static MXFKey()
        {
            m_ULDescriptions = new Dictionary<MXFShortKey, string[]>();

            //Parse SMPTE Labels register

            XElement regEntries;
            XNamespace ns = "http://www.smpte-ra.org/schemas/400/2012";

            regEntries = XElement.Parse(MXF.Properties.Resources.Labels);
            ParseEntries(regEntries, ns);

            // Parse SMPTE Elements register

            ns = "http://www.smpte-ra.org/schemas/335/2012";
            regEntries = XElement.Parse(MXF.Properties.Resources.Elements);
            ParseEntries(regEntries, ns);

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
                m_ULDescriptions.Add(shortKey, new string[] { name_string + " - " + definition_string, "", notes_string });
            }
        }

        private static void ParseEntries(XElement regEntries, XNamespace ns)
        {
            foreach (var e in regEntries.Descendants(ns + "Entry"))
            {
                var UL_string = (string)e.Element(ns + "UL") ?? "";
                if (!string.IsNullOrEmpty(UL_string))
                {
                    MXFShortKey shortKey = GetShortKeyFromSMPTEULString(UL_string);
                    string name = (string)e.Element(ns + "Name") ?? "";
                    string definition = (string)e.Element(ns + "Definition") ?? "";
                    string definingDocument = (string)e.Element(ns + "DefiningDocument") ?? "";
                    m_ULDescriptions.Add(shortKey, new string[] { name, definition, definingDocument });
                }

            }
        }

        private static MXFShortKey GetShortKeyFromSMPTEULString(string smpteString)
        {
            const int hexBase = 16;
            string byteString = smpteString.Replace("urn:smpte:ul:", "").Replace(".", "");
            UInt64 value1 = Convert.ToUInt64(byteString.Substring(0, 16), hexBase);
            UInt64 value2 = Convert.ToUInt64(byteString.Substring(16, 16), hexBase);
            return new MXFShortKey(value1, value2);
        }

        [Browsable(false)]
        public KeyType Type { get; set; }
        [Browsable(false)]
        public Type ObjectType { get; set; }

        /// <summary>
        /// The name of this key (if found in SMPTE RP210 or RP224)
        /// </summary>
        [CategoryAttribute("Key"), ReadOnly(true)]
        public string Name { get; set; }

        /// <summary>
        /// Keyfield, describes the type of data
        /// </summary>
        [CategoryAttribute("Key"), ReadOnly(true)]
        public KeyCategory Category
        {
            get
            {
                if (this.Length > 4)
                {
                    return (KeyCategory)this[5];
                }
                else
                {
                    return KeyCategory.Unknown;
                }
            }
        }

        /// <summary>
        /// Create a new key
        /// </summary>
        /// <param name="list"></param>
        public MXFKey(params byte[] list) : base(list)
        {
            this.Type = KeyType.None;
            FindKeyName();
        }

        /// <summary>
        /// Create a new key
        /// </summary>
        /// <param name="list"></param>
        public MXFKey(string name, params byte[] list) : this(list)
        {
            this.Name = name;
            FindKeyName();
        }

        /// <summary>
        /// Create a new key
        /// </summary>
        /// <param name="list"></param>
        public MXFKey(Type objectType, params byte[] list) : this(list)
        {
            this.ObjectType = objectType;
            FindKeyName();
        }

        /// <summary>
        /// Create a new key
        /// </summary>
        /// <param name="list"></param>
        public MXFKey(string name, KeyType type, params byte[] list) : this(name, list)
        {
            this.Type = type;
            FindKeyName();
        }

        /// <summary>
        /// Create a new key by reading from the current file location with a fixed size
        /// </summary>
        /// <param name="firstPart"></param>
        /// <param name="reader"></param>
        public MXFKey(MXFReader reader, UInt32 length) : base(reader, length)
        {
            FindKeyName();
        }

        /// <summary>
        /// Locate the key name (if found)
        /// </summary>
        private void FindKeyName()
        {
            MXFShortKey skey = this.ShortKey;
            if (m_ULDescriptions.ContainsKey(skey))
            {
                this.Name = m_ULDescriptions[skey][0];
            }
        }


        [CategoryAttribute("Key"), ReadOnly(true)]
        public MXFShortKey ShortKey
        {
            get
            {
                return new MXFShortKey(this.GetByteArray().ToArray());
            }
        }


        /// <summary>
        /// Return a description if available
        /// </summary>
        [CategoryAttribute("Key"), ReadOnly(true)]
        public string Description
        {
            get
            {
                MXFShortKey skey = this.ShortKey;
                if (m_ULDescriptions.ContainsKey(skey))
                    return m_ULDescriptions[skey][1];
                return string.Empty;
            }
        }


        /// <summary>
        /// Return a description if available
        /// </summary>
        [CategoryAttribute("Key"), ReadOnly(true)]
        public string Information
        {
            get
            {
                MXFShortKey skey = this.ShortKey;
                if (m_ULDescriptions.ContainsKey(skey))
                    return m_ULDescriptions[skey][2];
                return string.Empty;
            }
        }

        public override string ToString()
        {
                StringBuilder sb = new StringBuilder();
                var bytes = this.GetByteArray();
                if (!string.IsNullOrEmpty(this.Name))
                    sb.Append(this.Name + " - ");
                sb.Append("{ ");
                for (int n = 0; n < this.Length; n++)
                {
                    if (n > 0)
                    {
                        sb.Append(".");
                    }
                        
                    sb.Append(string.Format("{0:X2}", bytes[n]));
                }
                sb.Append(" }");
                return sb.ToString();
            }


        #region Equals

        /// <summary>
        /// Equal keys?
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MXFKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Equals((MXFIdentifier)other);
        }

        /// <summary>
        /// Equal to object?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MXFKey)) return false;
            return Equals((MXFKey)obj);
        }

        public override int GetHashCode() { return base.GetHashCode(); }
        public static bool operator ==(MXFKey x, MXFKey y) { return Equals(x, y); }
        public static bool operator !=(MXFKey x, MXFKey y) { return !Equals(x, y); }

        #endregion
    }
}
