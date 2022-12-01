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
using Myriadbits.MXF.KLV;
using System;
using System.IO;
using System.Linq;
using static Myriadbits.MXF.KLV.KLVLength;
using static Myriadbits.MXF.KLVKey;

namespace Myriadbits.MXF
{
    public class MXFLocalTagParser : KLVTripletParser<KLVKey, KLVLength, ByteArray>
    {
        public MXFLocalTagParser(Stream stream, long baseOffset) : base(stream, baseOffset)
        {
        }

        public override MXFLocalTag GetNext()
        {
            Seek(currentKLVOffset);

            KLVKey key = ParseTwoBytesLocalTagKey();
            KLVLength length = ParseLocalTagLength();
            SubStream ss = new SubStream(klvStream, currentKLVOffset, key.ArrayLength + length.Value);
            long tagOffset = baseOffset + currentKLVOffset;
            MXFLocalTag tag = new MXFLocalTag(key, length, tagOffset, ss);
            Current = tag;
            
            // advance to next pack
            Seek(currentKLVOffset + tag.TotalLength);
            return tag;
        }

        private KLVKey ParseTwoBytesLocalTagKey()
        {
            var keyLength = KeyLengths.TwoBytes;
            return new KLVKey(keyLength, reader.ReadBytes((int)keyLength));
        }

        private KLVLength ParseLocalTagLength()
        {
            var lengthEncoding = LengthEncodings.TwoBytes;
            return new KLVLength(lengthEncoding, reader.ReadBytes((int)lengthEncoding));
        }
    }
}
