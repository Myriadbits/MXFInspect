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
using System.Collections.Generic;
using System.Linq;
using static Myriadbits.MXF.KLVKey;
using static Myriadbits.MXF.KLVLength;

namespace Myriadbits.MXF
{
    public class KLVParser
    {
        private readonly MXFReader _reader;
        private long actualOffset = 0;

        public KLVParser(MXFReader reader)
        {
            _reader = reader;
        }

        //private KLVTriplet GetNextKLV1()
        //{
        //    UL ul;

        //    try
        //    {
        //        ul = ParseUL();
        //    }
        //    catch (System.Exception)
        //    {
        //        SeekForNextPotentialKey();
        //        GetNextMXFPack();
        //        throw;
        //    }
        //    finally
        //    {
        //        var length = KLVLengthParser.ParseKLVLength(_reader, LengthEncodings.BER);
        //        //return new KLV(ul, length, null);
        //    }

        //    return null;
        //}

        public KLVTriplet GetNextMXFPack()
        {
            var klv = ParseMXFKLV(actualOffset);

            // TODO really neccessary this type check?
            if (klv.Key is UL ul)
            {
                if (ul.CategoryDesignator == ULCategories.Groups)
                {
                    switch (ul.RegistryDesignator)
                    {
                        //case ULRegistries.MetadataDictionaries:
                        //    break;
                        //case ULRegistries.EssenceDictionaries:
                        //    break;
                        //case ULRegistries.ControlDictionaries:
                        //    break;
                        //case ULRegistries.TypesDictionaries:
                        //    break;
                        //case ULRegistries.GlobalSet_1Byte:
                        //    break;
                        //case ULRegistries.GlobalSet_2Bytes:
                        //    break;
                        //case ULRegistries.GlobalSet_4Bytes:
                        //    break;
                        //case ULRegistries.LocalSet_BER_OIDBER:
                        //    break;
                        //case ULRegistries.LocalSet_BER_2Bytes:
                        //    break;
                        //case ULRegistries.LocalSet_BER_4Bytes:
                        //    break;
                        //case ULRegistries.LocalSet_1Byte_1Byte:
                        //    break;
                        //case ULRegistries.LocalSet_1Byte_OIDBER:
                        //    break;
                        //case ULRegistries.LocalSet1_Byte_4Bytes:
                        //    break;
                        //case ULRegistries.LocalSet_2Bytes_1Byte:
                        //    break;
                        //case ULRegistries.LocalSet_2Bytes_OIDBER:
                        //    break;
                        case ULRegistries.LocalSet_2Bytes_2Bytes:
                            klv.KLVSublist = GetSubKLV(klv, KeyLengths.TwoBytes, LengthEncodings.TwoBytes);
                            break;
                        case ULRegistries.LocalSet_2Bytes_4Bytes:
                            klv.KLVSublist = GetSubKLV(klv, KeyLengths.TwoBytes, LengthEncodings.FourBytes);
                            break;
                        case ULRegistries.LocalSet_4Bytes_1Byte:
                            break;
                        //case ULRegistries.LocalSet_4Bytes_OIDBER:
                        //    break;
                        //case ULRegistries.LocalSet_4Bytes_2Bytes:
                        //    break;
                        //case ULRegistries.LocalSet_4Bytes_4Bytes:
                        //    break;
                        //case ULRegistries.VariableLengthPacks_1Byte:
                        //    break;
                        //case ULRegistries.VariableLengthPacks_2Bytes:
                        //    break;
                        //case ULRegistries.VariableLengthPacks_4Bytes:
                        //    break;
                        //case ULRegistries.DefinedLengthPacks:
                        //    break;
                        //case ULRegistries.Reserved:
                        //    break;
                        //case ULRegistries.SimpleWrappersAndContainers:
                        //    break;
                        //case ULRegistries.ComplexWrappersAndContainers:
                        //    break;
                        case null:
                            break;
                    }
                }
            }

            // advance file position
            actualOffset = actualOffset + klv.TotalLength;
            _reader.Seek(actualOffset);

            return klv;
        }

        private List<KLVTriplet> GetSubKLV(KLVTriplet klv, KeyLengths keyLength, LengthEncodings encoding)
        {
            var subKLVList = new List<KLVTriplet>();
            var offset = klv.ValueOffset;
            long summedLength = 0;
            while (summedLength < klv.Length.Value)
            {
                var subKLV = ParseKLV(keyLength, encoding, offset);

                if (subKLV.Offset + subKLV.TotalLength > klv.Offset + klv.TotalLength)
                {
                    throw new System.Exception("SubKLV out range");
                }
                else {
                    subKLVList.Add(subKLV);
                    offset += subKLV.TotalLength;
                    summedLength += subKLV.TotalLength;
                }
            }
            return subKLVList;
        }

        private KLVTriplet ParseKLV(KeyLengths keyLength, LengthEncodings encoding, long offset)
        {
            // move to file pos
            _reader.Seek(offset);

            var key = ParseKLVKey(keyLength);
            var length = ParseKLVLength(encoding);
            var value = _reader.ReadArray(_reader.ReadByte, length.Value);
            return new KLVTriplet(key, length, offset, value);
        }

        private KLVTriplet ParseMXFKLV(long offset)
        {
            // move to file pos
            _reader.Seek(offset);

            var ul = ParseUL();
            var length = ParseKLVLength(LengthEncodings.BER);
            return new KLVTriplet(ul, length, actualOffset);
        }

        private bool SeekForNextPotentialKey()
        {
            int foundBytes = 0;

            // TODO implement Boyer-Moore algorithm
            while (!_reader.EOF)
            {
                if (_reader.ReadByte() == UL.ValidULPrefix[foundBytes])
                {
                    foundBytes++;

                    if (foundBytes == 4)
                    {
                        _reader.Seek(_reader.Position - 4);
                        return true;
                    }
                }
                else
                {
                    foundBytes = 0;
                }
            }
            // TODO what does the caller have to do in this case?
            return false;
        }

        private KLVKey ParseKLVKey(KeyLengths keyLength)
        {
            return new KLVKey(keyLength, _reader.ReadArray(_reader.ReadByte, (int)keyLength));
        }

        private UL ParseUL()
        {
            return new UL(_reader.ReadArray(_reader.ReadByte, 16));
        }

        private KLVLength ParseKLVLength(LengthEncodings encoding)
        {

            switch (encoding)
            {
                case LengthEncodings.OneByte:
                case LengthEncodings.TwoBytes:
                case LengthEncodings.FourBytes:
                    return ParseSimpleKLVLength((int)encoding);

                case LengthEncodings.BER:
                    return ParseBERKLVLength();

                default:
                    return null;
            }
        }

        private KLVLength ParseSimpleKLVLength(int numOfBytes)
        {
            byte[] bytes = _reader.ReadArray(_reader.ReadByte, numOfBytes);
            long lengthValue = bytes.ToLong();
            return new KLVLength((LengthEncodings)numOfBytes, lengthValue, bytes);
        }

        private KLVLength ParseBERKLVLength()
        {
            byte[] bytes = new byte[] { _reader.ReadByte() };

            switch (bytes[0])
            {
                case <= 0x7F:
                    // short form, size = length
                    return new KLVLength(LengthEncodings.BER, bytes[0], bytes);

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
                        throw new NotSupportedException($"BER Length exceeds 8 octets (not valid according to SMPTE 379M 5.3.4). Found at offset {_reader.Position}");
                    }

                    byte[] additionalOctets = _reader.ReadArray(_reader.ReadByte, additionalOctetsCount);
                    long lengthValue = additionalOctets.ToLong();
                    bytes = bytes.Concat(additionalOctets).ToArray();
                    return new KLVLength(LengthEncodings.BER, lengthValue, bytes);
            }
        }

    }
}
