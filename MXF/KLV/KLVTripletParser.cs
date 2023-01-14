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

using Myriadbits.MXF.Exceptions;
using Myriadbits.MXF.KLV;
using System;
using System.IO;

namespace Myriadbits.MXF
{
    public abstract class KLVTripletParser<T, K, L> : IKLVTripletParser<T, K, L>
        where T : KLVTriplet
        where K : KLVKey
        where L : KLVLengthBase
    {
        protected readonly IKLVStreamReader reader;
        protected long currentKLVOffset = 0;
        protected readonly long baseOffset = 0;
        protected readonly Stream klvStream;
        protected abstract K ParseKLVKey();
        protected abstract L ParseKLVLength();
        protected abstract T InstantiateKLV(K key, L length, long offset, Stream stream);

        public T Current { get; protected set; }

        public KLVTripletParser(Stream stream)
        {
            klvStream = stream;
            reader = new KLVStreamReader(stream);
        }

        public KLVTripletParser(Stream stream, long baseOffset) : this(stream)
        {
            this.baseOffset = baseOffset;
        }

        public virtual T GetNext()
        {

            var klv = CreateKLV(currentKLVOffset);
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

        protected T CreateKLV(long offset)
        {
            K key;
            L length;

            Seek(offset);
            
            try
            {
                // TODO before parsing check if we have enough bytes to read
                key = ParseKLVKey();
            }
            catch(Exception e)
            {
                throw new KLVKeyParsingException(e);
            }

            try
            {
                // TODO before parsing check if we have enough bytes to read
                length = ParseKLVLength();
            }
            catch (Exception e)
            {
                throw new KLVLengthParsingException(e);
            }

            long subStreamLength = key.ArrayLength + length.ArrayLength + length.Value;

            // check if substream not longer than the parent stream
            if (offset + subStreamLength > klvStream.Length)
            {
                throw new KLVStreamException("The parsed length exceeds the stream length");
            }

            Stream ss = new SubStream(klvStream, offset, subStreamLength);
            return InstantiateKLV(key, length, baseOffset + currentKLVOffset, ss);
        }
    }
}
