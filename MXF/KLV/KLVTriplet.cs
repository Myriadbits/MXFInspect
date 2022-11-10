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

using Myriadbits.MXF.KLV;
using Myriadbits.MXF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    public class KLVTriplet : MXFObject 
    {
        private const string CATEGORYNAME = "MXFPack";
        private const int CATEGORYPOS = 1;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Key part of KLV triplet")]
        public virtual KLVKey Key { get; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Length part of KLV triplet")]
        public virtual KLVLengthBase Length { get; }

        [Browsable(false)]
        [Description("Value part of KLV triplet")]
        public byte[] Value { get; }

        [Browsable(false)]
        public List<KLVTriplet> KLVSublist { get; set; }

        /// <summary>
        /// Offset from beginning of the file (i.e. position of start of key within file)
        /// </summary>
        public override long Offset { get; protected set; }

        /// <summary>
        /// Total length of KLV (= sum of lengths of key, KLV-length and value)
        /// </summary>
        [Description("Total length of KLV in bytes (Key length + Length length + Value length)")]
        public override long TotalLength { get; set; }

        /// <summary>
        /// Offset of the value (=data), i.e. where the payload begins.
        /// </summary>
        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Offset of the value part of the KLV in term of bytes from beginning of the file")]
        public long ValueOffset { get; }


        public KLVTriplet(KLVKey key, KLVLengthBase length, long offset)
        {
            Key = key;
            Length = length;
            Offset = offset;
            ValueOffset = offset + (int)key.KeyLength + length.ArrayLength;
            TotalLength = (int)key.KeyLength + length.ArrayLength + length.Value;
        }

        public KLVTriplet(KLVKey key, KLVLengthBase length, long offset, byte[] value) : this(key, length, offset)
        {
            // if passed value differs in length w.r.t to the declared length throw
            if (value.LongLength != length.Value)
            {
                throw new ArgumentException($"Size of value ({value.LongLength}) does not match with declared length of KLV ({length.Value})");
            }
            Value = value;
        }

        // TODO this should not be the responsibility of the class to read its content
        public KLVValue GetValue(MXFReader reader)
        {
            reader.Seek(ValueOffset);
            return new KLVValue(reader.ReadArray(reader.ReadByte, Length.Value));
        }
    }

}
