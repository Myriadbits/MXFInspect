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

using Myriadbits.MXF.Identifiers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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

    [TypeConverter(typeof(ExpandableObjectConverter))]
    // TODO rename this class into SMPTEUL = Universal Label
    public class MXFKey : MXFIdentifier, IEquatable<MXFKey>
    {
        private const string CATEGORYNAME = "Key";

        private static Dictionary<MXFShortKey, KeyDescription> knownKeys = KeyDictionary.GetKeys();

        [Browsable(false)]
        public KeyType Type { get; set; }

        [Browsable(false)]
        public bool IsKnown { get; set; } = false;

        /// <summary>
        /// The name of this key (if found in SMPTE RP210 or RP224)
        /// </summary>
        [Category(CATEGORYNAME)]
        public string Name { get; set; }

        /// <summary>
        /// Keyfield, describes the type of data
        /// </summary>
        [Category(CATEGORYNAME)]
        public KeyCategory Category
        {
            get
            {
                if (this.Length > 4)
                {
                    return (KeyCategory)this[4];
                }
                else
                {
                    return KeyCategory.Unknown;
                }
            }
        }

        public MXFKey(params byte[] list) : base(list)
        {
            this.Type = KeyType.None;
            FindKeyName();
        }

        // TODO remove ctor if possible
        public MXFKey(string name, params byte[] list) : this(list)
        {
            this.Name = name;
            FindKeyName();
        }

        /// <summary>
        /// Locate the key name (if found)
        /// </summary>
        private void FindKeyName()
        {
            MXFShortKey skey = this.GetShortKey();
            if (knownKeys.ContainsKey(skey))
            {
                this.Name = knownKeys[skey].Name;
                IsKnown = true;
            }
            else
            {
                IsKnown = false;
            }
        }


        [Category(CATEGORYNAME)]
        public MXFShortKey GetShortKey()
        {
            return new MXFShortKey(this.GetByteArray().ToArray());
        }


        /// <summary>
        /// Return a description if available
        /// </summary>
        [Category(CATEGORYNAME)]
        public string Description
        {
            get
            {
                MXFShortKey skey = this.GetShortKey();
                if (knownKeys.ContainsKey(skey))
                    return knownKeys[skey].Definition;
                return string.Empty;
            }
        }


        /// <summary>
        /// Return a description if available
        /// </summary>
        [Category(CATEGORYNAME)]
        public string Notes
        {
            get
            {
                MXFShortKey skey = this.GetShortKey();
                if (knownKeys.ContainsKey(skey))
                {
                    return knownKeys[skey].Notes;
                }

                return string.Empty;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var bytes = this.GetByteArray();
            if (!string.IsNullOrEmpty(this.Name))
            {
                sb.Append(this.Name + " - ");
            }

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

        // TODO remove operator overloading as they are considered bad (see UUID.Equals...)
        public static bool operator ==(MXFKey x, MXFKey y) { return Equals(x, y); }
        public static bool operator !=(MXFKey x, MXFKey y) { return !Equals(x, y); }

        #endregion
    }
}
