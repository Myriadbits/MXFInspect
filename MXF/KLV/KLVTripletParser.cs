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
using System;
using System.IO;

namespace Myriadbits.MXF
{
    public abstract class KLVTripletParser<K, L, V>
        where K : KLVKey
        where L : KLVLengthBase
        where V : ByteArray
    {
        protected readonly IKLVStreamReader reader;
        protected long currentKLVOffset = 0;
        protected long baseOffset = 0;
        protected readonly Stream klvStream;
        protected readonly Func<K> keyParsingFunction;
        protected readonly Func<L> lengthEncParsingFunction;

        public KLVTriplet<K, L, V> Current { get; protected set; }



        public KLVTripletParser(Stream stream)
        {
            klvStream = stream;
            reader = new KLVStreamReader(stream);
        }

        public KLVTripletParser(Stream stream, long baseOffset) : this(stream)
        {
            this.baseOffset = baseOffset;
        }

        public virtual KLVTriplet<K, L, V> GetNext()
        {

            var klv = CreateKLV(currentKLVOffset, keyParsingFunction, lengthEncParsingFunction);
            Current = klv;

            // advance to next pack
            Seek(currentKLVOffset + klv.TotalLength);

            return klv;
        }

        public bool HasNext()
        {
            return !reader.EOF;
        }

        protected void Seek(long position)
        {
            reader.Seek(position);
            currentKLVOffset = position;
        }

        protected KLVTriplet<K, L, V> CreateKLV(long offset, Func<K> keyParsingFunction, Func<L> lengthEncParsingFunction)
        {
            Seek(offset);

            K ul = keyParsingFunction();
            L length = lengthEncParsingFunction();
            SubStream ss = new SubStream(klvStream, offset, ul.ArrayLength + length.Value);
            return new KLVTriplet<K, L, V>(ul, length, baseOffset + currentKLVOffset, ss);
        }
    }
}
