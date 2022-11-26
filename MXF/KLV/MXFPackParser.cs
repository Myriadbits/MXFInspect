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
using Myriadbits.MXF.KLV;
using System;
using System.IO;
using System.Linq;
using static Myriadbits.MXF.KLVKey;

namespace Myriadbits.MXF
{
    public class MXFPackParser : KLVTripletParser<UL, KLVBERLength, ByteArray>
    {
        private long currentPackNumber = 0;

        public MXFPackParser(Stream stream) : base(stream)
        {
        }

        public override MXFPack GetNext()
        {

            var klv = CreateKLV(currentKLVOffset, ParseUL, ParseBERKLVLength);
            var ss = new SubStream(klvStream, klv.Offset, klv.TotalLength);

            var pack = new MXFPack(klv.Key, klv.Length, klv.Offset, ss);
            // TODO wrap into using/ try...catch
            var byteReader = new KLVStreamReader(ss);
            var typedPack = MXFPackFactory.CreatePack(pack, byteReader);

            typedPack.Number = currentPackNumber++;
            Current = typedPack;

            // advance to next pack
            Seek(currentKLVOffset + klv.TotalLength);

            return typedPack;
        }

        private KLVBERLength ParseBERKLVLength()
        {
            byte[] bytes = new byte[] { reader.ReadByte() };

            switch (bytes[0])
            {
                case <= 0x7F:
                    // short form, size = length
                    return new KLVBERLength(bytes[0], bytes);

                case 0x80:
                    // Indefinite form
                    // LogWarning("KLV length having value 0x80 (=indefinite, not valid according to SMPTE 379M 5.3.4) found at offset {0}!", reader.Position);
                    // TODO is this the correct way to handle this?
                    throw new NotSupportedException("BER Indefinite Form is not supported");

                case > 0x80:

                    // long form: size is number of octets following, 1 + x octets
                    int additionalOctetsCount = bytes[0] - 0x80;

                    // SMPTE 379M 5.3.4 guarantee that additional octets must not exceed 8 bytes
                    if (additionalOctetsCount > 8)
                    {
                        throw new NotSupportedException($"BER Length exceeds 8 octets (not valid according to SMPTE 379M 5.3.4). Found at offset {reader.Position}");
                    }

                    byte[] additionalOctets = reader.ReadBytes(additionalOctetsCount);
                    long lengthValue = additionalOctets.ToLong();
                    bytes = bytes.Concat(additionalOctets).ToArray();
                    return new KLVBERLength(lengthValue, bytes);
            }
        }

        private UL ParseUL()
        {
            return new UL(reader.ReadBytes((int)KeyLengths.SixteenBytes));
        }

        public bool SeekForNextPotentialKey(out long newOffset)
        {
            int foundBytes = 0;

            // TODO implement Boyer-Moore algorithm
            while (!reader.EOF)
            {
                if (reader.ReadByte() == UL.ValidULPrefix[foundBytes])
                {
                    foundBytes++;

                    if (foundBytes == 4)
                    {
                        Seek(reader.Position - 4);
                        newOffset = reader.Position;
                        return true;
                    }
                }
                else
                {
                    foundBytes = 0;
                }
            }
            // TODO what does the caller have to do in this case?
            newOffset = reader.Position;
            return false;
        }

    }
}