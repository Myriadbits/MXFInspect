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
    public abstract class KLVTripletParser<U> : IKLVTripletParser<U> where U : KLVTriplet
    {
        protected readonly IKLVStreamReader reader;
        protected long currentKLVOffset = 0;
        protected long baseOffset = 0;
        protected readonly Stream klvStream;
        protected readonly Func<KLVKey> keyParsingFunction;
        protected readonly Func<KLVLengthBase> lengthEncParsingFunction;

        public U Current { get; protected set; }

        public KLVTripletParser(Stream stream)
        {
            klvStream = stream;
            reader = new KLVStreamReader(stream);
        }

        public KLVTripletParser(Stream stream, long baseOffset) : this(stream)
        {
            this.baseOffset = baseOffset;
        }

        public virtual U GetNext()
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

        protected U CreateKLV(long offset, Func<KLVKey> keyParsingFunction, Func<KLVLengthBase> lengthEncParsingFunction)
        {
            Seek(offset);

            KLVKey key = keyParsingFunction();
            KLVLengthBase length = lengthEncParsingFunction();
            Stream ss = new SubStream(klvStream, offset, key.ArrayLength + length.Value);
            return (U)Activator.CreateInstance(typeof(U), key, length, baseOffset + currentKLVOffset, ss);
        }
    }


    public interface IKLVTripletParser<U> where U : KLVTriplet
    {
        public U Current { get; }
        public bool HasNext();
        public U GetNext();
    }
}
