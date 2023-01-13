﻿#region license
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
    [TypeConverter(typeof(ExpandableObjectConverter))]
    // TODO rename this class into SMPTEUL = Universal Label
    public class MXFKey : MXFIdentifier, IEquatable<MXFKey>
    {
        private const string CATEGORYNAME = "Key";

        private static readonly Dictionary<MXFShortKey, KeyDescription> knownKeys = SMPTEULDictionary.GetEntries();

        #region properties

        //TODO make a private setter for this prop
        //[Browsable(false)]
        //public KeyType Type { get; set; }

        /// <summary>
        /// True if found in SMPTE RP210 or RP224
        /// </summary>
        [Browsable(false)]
        public bool IsKnown { get; private set; } = false;

        [Category(CATEGORYNAME)]
        [Description("The name of this key (if found in SMPTE RP210 or RP224)")]
        // TODO make a private setter for this prop
        public string Name { get; set; }

        [Category(CATEGORYNAME)]
        [Description("16-byte size of the UL")]
        public int ULLength { get; private set; }

        [Category(CATEGORYNAME)]
        [Description("Identifies the category of registry described (e.g. Dictionaries)")]
        [TypeConverter(typeof(EnumDescriptionConverter))]
        public ULCategories? CategoryDesignator { get; private set; }

        [Category(CATEGORYNAME)]
        [Description("Identifies the specific register in a category (e.g. Metadata Dictionaries)")]
        [TypeConverter(typeof(EnumDescriptionConverter))]
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
            //this.Type = KeyType.None;
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
                            // Universal sets

                            case 0x01:
                                RegistryDesignator = ULRegistries.UniversalSet;
                                break;

                            // Global sets

                            case 0x02:
                                RegistryDesignator = ULRegistries.GlobalSet_BER;
                                break;
                            case 0x22:
                                RegistryDesignator = ULRegistries.GlobalSet_1Byte;
                                break;
                            case 0x42:
                                RegistryDesignator = ULRegistries.GlobalSet_2Bytes;
                                break;
                            case 0x62:
                                RegistryDesignator = ULRegistries.GlobalSet_4Bytes;
                                break;

                            // Local sets

                            case 0x03:
                                RegistryDesignator = ULRegistries.LocalSet_BER_1Byte;
                                break;
                            case 0x0b:
                                RegistryDesignator = ULRegistries.LocalSet_BER_OIDBER;
                                break;
                            case 0x13:
                                RegistryDesignator = ULRegistries.LocalSet_BER_2Bytes;
                                break;
                            case 0x1b:
                                RegistryDesignator = ULRegistries.LocalSet_BER_4Bytes;
                                break;
                            case 0x23:
                                RegistryDesignator = ULRegistries.LocalSet_1Byte_1Byte;
                                break;
                            case 0x2b:
                                RegistryDesignator = ULRegistries.LocalSet_1Byte_OIDBER;
                                break;
                            case 0x33:
                                RegistryDesignator = ULRegistries.LocalSet_1Byte_2Bytes;
                                break;
                            case 0x3b:
                                RegistryDesignator = ULRegistries.LocalSet_1Byte_4Bytes;
                                break;
                            case 0x43:
                                RegistryDesignator = ULRegistries.LocalSet_2Bytes_1Byte;
                                break;
                            case 0x4b:
                                RegistryDesignator = ULRegistries.LocalSet_2Bytes_OIDBER;
                                break;
                            case 0x53:
                                RegistryDesignator = ULRegistries.LocalSet_2Bytes_2Bytes;
                                break;
                            case 0x5b:
                                RegistryDesignator = ULRegistries.LocalSet_2Bytes_4Bytes;
                                break;
                            case 0x63:
                                RegistryDesignator = ULRegistries.LocalSet_4Bytes_1Byte;
                                break;
                            case 0x6b:
                                RegistryDesignator = ULRegistries.LocalSet_4Bytes_OIDBER;
                                break;
                            case 0x73:
                                RegistryDesignator = ULRegistries.LocalSet_4Bytes_2Bytes;
                                break;
                            case 0x7b:
                                RegistryDesignator = ULRegistries.LocalSet_4Bytes_4Bytes;
                                break;

                            // Variable length packs

                            case 0x04:
                                RegistryDesignator = ULRegistries.VariableLengthPacks_BER;
                                break;
                            case 0x24:
                                RegistryDesignator = ULRegistries.VariableLengthPacks_1Byte;
                                break;
                            case 0x44:
                                RegistryDesignator = ULRegistries.VariableLengthPacks_2Bytes;
                                break;
                            case 0x64:
                                RegistryDesignator = ULRegistries.VariableLengthPacks_4Bytes;
                                break;

                            // DefinedLengthPacks

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

                    case 0x05:
                        CategoryDesignator = ULCategories.RegisteredPrivate;
                        break;

                    case byte b when b >= 0x06 && b <= 0x7e:
                        CategoryDesignator = ULCategories.Reserved;
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


        public MXFKey(UL ul) : base()
        {
            byte[] t = ul.ToArray();
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
                    sb.Append('.');
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

