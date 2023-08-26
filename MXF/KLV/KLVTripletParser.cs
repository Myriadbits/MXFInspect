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
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using Serilog;
using System;
using System.IO;
using System.Threading;

namespace Myriadbits.MXF
{
    public abstract class KLVTripletParser<T, K, L, V> where T : KLVTriplet<K, L, V>
        where K : KLVKey
        where L : ILength
        where V : KLVValue
    {
        private long currentOffset = 0;
        private readonly long baseOffset = 0;

        protected readonly IKLVStreamReader reader;
        protected readonly Stream klvStream;
        protected abstract K ParseKLVKey();
        protected abstract L ParseKLVLength();
        protected abstract T InstantiateKLV(K key, L length, long offset, Stream stream);

        public T Current { get; protected set; }

        public long Offset { get { return currentOffset; } }

        public long RemainingBytesCount { get { return klvStream.Length - currentOffset; } }
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

            var klv = CreateKLV(currentOffset);
            Current = klv;

            // advance to next pack
            SeekToNext();
            return klv;
        }

        public void SeekToNext()
        {
            if (Current != null)
            {
                Seek(currentOffset + Current.TotalLength);
            }
            else
            {
                Seek(0);
            }
        }

        public bool HasNext()
        {
            return RemainingBytesCount > 0;
        }

        protected void Seek(long position)
        {
            reader.Seek(position);
            currentOffset = position;
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
            catch (Exception e)
            {
                throw new KLVKeyParsingException("Exception occured during parsing of key", klvStream.Position, e);
            }
            try
            {
                // TODO before parsing check if we have enough bytes to read
                length = ParseKLVLength();
            }
            catch (Exception e)
            {
                throw new KLVLengthParsingException("Exception occured during parsing of length", klvStream.Position, e);
            }

            long subStreamLength = key.ArrayLength + length.ArrayLength + length.Value;

            // check if substream not longer than the parent stream
            if (RemainingBytesCount < subStreamLength)
            {
                // TODO klvstream is always a filestream, right?
                // this check does not make sense!
                if (klvStream is FileStream)
                {
                    Log.ForContext<KLVTripletParser<T, K, L, V>>().Error($"Substream length longer than parent stream, i.e. file finishes before last klv triplet.\r\nFile finishes @{klvStream.Length} while last KLV triplet with length {subStreamLength} should finish @{offset + subStreamLength}");

                    long truncatedLength = klvStream.Length - offset;
                    Stream truncatedStream = new SubStream(klvStream, offset, truncatedLength);
                    var truncatedKLV = new TruncatedKLV(key, length, baseOffset + currentOffset, truncatedStream);
                    throw new EndOfKLVStreamException("Premature end of file: Last KLV triplet is shorter than declared.", currentOffset, truncatedKLV, null);
                }
            }

            Stream ss = new SubStream(klvStream, offset, subStreamLength);
            return InstantiateKLV(key, length, baseOffset + currentOffset, ss);
        }

        public bool SeekToNextPotentialKey(out long newOffset, long seekThresholdInBytes = 0, CancellationToken ct = default)
        {
            return SeekToBytePattern(out newOffset, UL.ValidULPrefix, seekThresholdInBytes, ct);
        }

        public bool SeekToBytePattern(out long newOffset, byte[] bytePattern, long seekThresholdInBytes = 0, CancellationToken ct = default)
        {
            int foundBytes = 0;
            int bytesRead = 0;

            // TODO implement Boyer-Moore algorithm
            while (!reader.EOF && (seekThresholdInBytes == 0 || bytesRead <= seekThresholdInBytes))
            {
                if (reader.ReadByte() == bytePattern[foundBytes])
                {
                    foundBytes++;

                    if (foundBytes == bytePattern.Length)
                    {
                        // pattern found, reposition to pattern beginning
                        Seek(reader.Position - bytePattern.Length);
                        newOffset = reader.Position;
                        return true;
                    }
                }
                else
                {
                    foundBytes = 0;
                }

                ct.ThrowIfCancellationRequested();
                bytesRead++;
            }

            // TODO what does the caller have to do in this case?
            newOffset = reader.Position;
            return false;
        }
    }
}
