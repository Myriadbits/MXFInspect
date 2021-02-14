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

    public enum ULCategories
    {
        Elements = 0x01,
        Groups = 0x02,
        ContainersAndWrappers = 0x03,
        Labels = 0x04
    }

    public enum ULRegistries
    {
        MetadataDictionaries,
        EssenceDictionaries,
        ControlDictionaries,
        TypesDictionaries,

        UniversalSet,
        GlobalSet,
        LocalSet,
        VariableLengthPacks,
        DefinedLengthPacks,
        Reserved,

        SimpleWrappersAndContainers,
        ComplexWrappersAndContainers,
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    // TODO rename this class into SMPTEUL = Universal Label
    public class MXFKey : MXFIdentifier, IEquatable<MXFKey>
    {
        private const string CATEGORYNAME = "Key";

        private static Dictionary<MXFShortKey, KeyDescription> knownKeys = KeyDictionary.GetKeys();

        #region properties

        [Browsable(false)]
        public KeyType Type { get; set; }

        /// <summary>
        /// True if found in SMPTE RP210 or RP224
        /// </summary>
        [Browsable(false)]
        public bool IsKnown { get; set; } = false;

        [Category(CATEGORYNAME)]
        [Description("The name of this key (if found in SMPTE RP210 or RP224)")]
        public string Name { get; set; }

        [Category(CATEGORYNAME)]
        [Description("16-byte size of the UL")]
        public int ULLength { get; private set; }

        [Category(CATEGORYNAME)]
        [Description("Identifies the category of registry described(e.g.Dictionaries)")]
        public ULCategories? CategoryDesignator { get; private set; }

        [Category(CATEGORYNAME)]
        [Description("Identifies the specific register in a category (e.g. Metadata Dictionaries)")]
        public ULRegistries? RegistryDesignator { get; private set; }

        [Category(CATEGORYNAME)]
        [Description("Designator of the structure variant within the given registry designator")]
        public byte? StructureDesignator { get; private set; }

        [Category(CATEGORYNAME)]
        [Description("Version of the given register which first defines the item specified by the Item Designator")]
        public byte? VersionNumber { get; private set; }

        [Category(CATEGORYNAME)]
        [Description("Unique identification of the particular item within the context of the UL Designator")]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] ItemDesignator { get; private set; }

        #endregion

        public MXFKey(params byte[] list) : base(list)
        {
            this.Type = KeyType.None;
            FindKeyName();

            ULLength = this.Length;

            if (this.Length > 5)
            {
                switch (this[4])
                {
                    case 0x01:
                        CategoryDesignator = ULCategories.Elements;
                        switch (this[5])
                        {
                            case 0x01:
                                RegistryDesignator = ULRegistries.MetadataDictionaries;
                                break;
                            case 0x02:
                                RegistryDesignator = ULRegistries.EssenceDictionaries;
                                break;
                            case 0x03:
                                RegistryDesignator = ULRegistries.ControlDictionaries;
                                break;
                            case 0x04:
                                RegistryDesignator = ULRegistries.TypesDictionaries;
                                break;
                            default:
                                RegistryDesignator = null;
                                break;
                        }
                        break;

                    case 0x02:
                        CategoryDesignator = ULCategories.Groups;
                        switch (this[5])
                        {
                            case 0x01:
                                RegistryDesignator = ULRegistries.UniversalSet;
                                break;
                            case 0x02:
                                RegistryDesignator = ULRegistries.GlobalSet;
                                break;
                            case 0x03:
                                RegistryDesignator = ULRegistries.LocalSet;
                                break;
                            case 0x04:
                                RegistryDesignator = ULRegistries.VariableLengthPacks;
                                break;
                            case 0x05:
                                RegistryDesignator = ULRegistries.DefinedLengthPacks;
                                break;
                            case 0x06:
                                RegistryDesignator = ULRegistries.Reserved;
                                break;
                            default:
                                RegistryDesignator = null;
                                break;
                        }
                        break;

                    case 0x03:
                        CategoryDesignator = ULCategories.ContainersAndWrappers;
                        switch (this[5])
                        {
                            case 0x01:
                                RegistryDesignator = ULRegistries.SimpleWrappersAndContainers;
                                break;
                            case 0x02:
                                RegistryDesignator = ULRegistries.ComplexWrappersAndContainers;
                                break;
                        }
                        break;

                    case 0x04:
                        CategoryDesignator = ULCategories.Labels;
                        break;

                    default:
                        CategoryDesignator = null;
                        break;
                }
            }

            StructureDesignator = this.Length > 6 ? this[6] : null;
            VersionNumber = this.Length > 7 ? this[7] : null;
            ItemDesignator = this.Length > 8 ? this.GetByteArray().Skip(8).ToArray() : null;
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
                {
                    return knownKeys[skey].Definition;
                }
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

