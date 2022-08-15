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


using System;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using static Myriadbits.MXF.KLV.KLVLength;

namespace Myriadbits.MXF.KLV
{
    public static class KLVLengthParser
    {
        public static KLVLength ParseKLVLength(MXFReader reader, LengthEncodingEnum encoding)
        {

            switch (encoding)
            {
                case LengthEncodingEnum.OneByte:
                case LengthEncodingEnum.TwoBytes:
                case LengthEncodingEnum.FourBytes:
                    return ParseSimpleKLVLength(reader, (int)encoding);

                case LengthEncodingEnum.BER:
                    return ParseBERKLVLength(reader);

                default:
                    return null;
            }
        }

        private static KLVLength ParseSimpleKLVLength(MXFReader reader, int numOfBytes)
        {
            byte[] bytes = reader.ReadArray(reader.ReadByte, numOfBytes);
            long lengthValue = ToLong(bytes);
            return new KLVLength((LengthEncodingEnum)numOfBytes, lengthValue, bytes);
        }

        private static KLVLength ParseBERKLVLength(MXFReader reader)
        {
            byte[] bytes = new byte[] { reader.ReadByte() };

            switch (bytes[0])
            {
                case <= 0x7F:
                    // short form, size = length
                    return new KLVLength(LengthEncodingEnum.BER, bytes[0], bytes);

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

                    byte[] additionalOctets = reader.ReadArray(reader.ReadByte, additionalOctetsCount);
                    long lengthValue = ToLong(additionalOctets);
                    bytes = bytes.Concat(additionalOctets).ToArray();
                    return new KLVLength(LengthEncodingEnum.BER, lengthValue, bytes);
            }
        }

        public static long ToLong(byte[] theBytes)
        {
            long lengthValue = 0;
            for (int i = 0; i < theBytes.Length; i++)
            {
                lengthValue = lengthValue << 8 | theBytes[i];
            }
            return lengthValue;
        }
    }
}
