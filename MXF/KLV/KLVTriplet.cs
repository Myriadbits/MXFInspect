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

using Myriadbits.MXF.KLV;
using Myriadbits.MXF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Myriadbits.MXF
{
    public class KLVTriplet<K,L,V> : MXFObject, IKLVTriplet<K, L, V>
        where K : KLVKey
        where L : KLVLengthBase
        where V : ByteArray

    {
        private const string CATEGORYNAME = "MXFPack";
        private const int CATEGORYPOS = 1;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Key part of KLV triplet")]
        public virtual K Key { get; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Length part of KLV triplet")]
        public virtual L Length { get; }

        [Browsable(false)]
        [Description("Value part of KLV triplet")]
        public virtual V Value { get; }

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

        /// <summary>
        /// Offset of the value (=data), i.e. where the payload beginsfrom the beginning of the triplet,
        /// i. e. length of the key +  length of lengthencoding 
        /// </summary>
        [Browsable(false)]
        public long RelativeValueOffset { get; }


        public KLVTriplet(K key, L length, long offset)
        {
            Key = key;
            Length = length;
            Offset = offset;
            ValueOffset = offset + (int)key.KeyLength + length.ArrayLength;
            RelativeValueOffset = key.ArrayLength + length.ArrayLength; 
            TotalLength = (int)key.KeyLength + length.ArrayLength + length.Value;

        }

        //// TODO this should not be the responsibility of the class to read its content
        //public V GetValue(SubStream ss)
        //{
        //    byte[] buffer = new byte[byte]
        //    ss.Seek(Offset, SeekOrigin.Begin);
        //    ss.Read()
            
        //        //return new KLVValue(reader.ReadArray(reader.ReadByte, Length.Value));
        //}
    }

}
