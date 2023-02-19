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

using System.ComponentModel;

namespace Myriadbits.MXF.Identifiers
{
    public enum ULRegistries
    {
        [Description("Metadata Dictionaries")]
        MetadataDictionaries = 0x01,
        [Description("Essence Dictionaries")]
        EssenceDictionaries = 0x02,
        [Description("Control Dictionaries")]
        ControlDictionaries = 0x03,
        [Description("Types Dictionaries")]
        TypesDictionaries = 0x04,

        [Description("Universal Set")]
        UniversalSet = 0x01,

        [Description("Global Set, Length: BER")]
        GlobalSet_BER = 0x02,
        [Description("Global Set, Length: 1 byte")]
        GlobalSet_1Byte = 0x22,
        [Description("Global Set, Length: 2 byte")]
        GlobalSet_2Bytes = 0x42,
        [Description("Global Set, Length: 4 byte")]
        GlobalSet_4Bytes = 0x62,

        [Description("Local Set, Length: BER, TAG: 1 byte")]
        LocalSet_BER_1Byte = 0x03,
        [Description("Local Set, Length: BER, Tag: OID BER")]
        LocalSet_BER_OIDBER = 0x0b,
        [Description("Local Set, Length: BER, Tag: 2 bytes")]
        LocalSet_BER_2Bytes = 0x13,
        [Description("Local Set, Length: BER, Tag: 4 bytes")]
        LocalSet_BER_4Bytes = 0x1b,
        [Description("Local Set, Length: 1 byte, Tag: 1 byte")]
        LocalSet_1Byte_1Byte = 0x23,
        [Description("Local Set, Length: 1 byte, Tag: OID BER")]
        LocalSet_1Byte_OIDBER = 0x2b,
        [Description("Local Set, Length: 1 bytes, Tag: 2 bytes")]
        LocalSet_1Byte_2Bytes = 0x23,
        [Description("Local Set, Length: 1 bytes, Tag: 4 bytes")]
        LocalSet_1Byte_4Bytes = 0x3b,
        [Description("Local Set, Length: 2 bytes, Tag: 1 byte")]
        LocalSet_2Bytes_1Byte = 0x43,
        [Description("Local Set, Length: 2 bytes, Tag: OID BER")]
        LocalSet_2Bytes_OIDBER = 0x4b,
        [Description("Local Set, Length: 2 bytes, Tag: 2 bytes")]
        LocalSet_2Bytes_2Bytes = 0x53,
        [Description("Local Set, Length: 2 bytes, Tag: 4 bytes")]
        LocalSet_2Bytes_4Bytes = 0x5b,
        [Description("Local Set, Length: 4 bytes, Tag: 1 byte")]
        LocalSet_4Bytes_1Byte = 0x63,
        [Description("Local Set, Length: 4 bytes, Tag: OID BER")]
        LocalSet_4Bytes_OIDBER = 0x6b,
        [Description("Local Set, Length: 4 bytes, Tag: 2 bytes")]
        LocalSet_4Bytes_2Bytes = 0x73,
        [Description("Local Set, Length: 4 bytes, Tag: 4 bytes")]
        LocalSet_4Bytes_4Bytes = 0x7b,

        [Description("Variable Length Packs (BER length)")]
        VariableLengthPacks_BER = 0x04,
        [Description("Variable Length Packs (1 byte length)")]
        VariableLengthPacks_1Byte = 0x24,
        [Description("Variable Length Packs (2 bytes length)")]
        VariableLengthPacks_2Bytes = 0x44,
        [Description("Variable Length Packs (4 bytes length)")]
        VariableLengthPacks_4Bytes = 0x64,

        [Description("Defined Length Packs")]
        DefinedLengthPacks = 0x05,

        [Description("Reserved")]
        Reserved = 0x06,

        [Description("Simple wrappers and containers")]
        SimpleWrappersAndContainers,
        [Description("Complex wrappers and containers")]
        ComplexWrappersAndContainers,
    }

}

