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
using System.Linq;

namespace Myriadbits.MXF.KLV
{
    public class KLVLength : ByteArray
    {
        public enum LengthEncodingEnum
        {
            OneByte = 1,
            TwoBytes = 2,
            FourBytes = 4,
            BER
        }

        public enum BERFormEnum
        {
            ShortForm,
            LongForm,
            Indefinite // Not supported
        }

        public LengthEncodingEnum LengthEncoding { get; private set; }

        public long LengthValue { get; private set; }

        public BERFormEnum? BERForm { get; private set; }
        public int? AdditionalOctets { get; private set; }

        public KLVLength(LengthEncodingEnum lengthEncoding, long lengthValue, params byte[] bytes) : base(bytes)
        {
            LengthEncoding = lengthEncoding;
            LengthValue = lengthValue;

            switch (LengthEncoding)
            {
                case LengthEncodingEnum.OneByte:
                case LengthEncodingEnum.TwoBytes:
                case LengthEncodingEnum.FourBytes:
                    long calculatedLengthValue = KLVLengthParser.ToLong(bytes);
                    // TODO do we need to check if each byte does not exceed 0x7F?
                    if (bytes.Length != (int)lengthEncoding)
                    {
                        throw new ArgumentException($"Declared length encoding ({lengthEncoding}) does not correspond to given array length ({bytes.Length})");
                    }
                    else if (calculatedLengthValue != LengthValue)
                    {
                        throw new ArgumentException($"Byte array value ({calculatedLengthValue}) does not match with given length value ({LengthValue})");
                    }
                    break;

                case LengthEncodingEnum.BER:
                    switch (bytes[0])
                    {
                        case > 0x80 when bytes.Length == 1:
                            throw new ArgumentException($"First byte indicates BER Long Form, but byte array consists of only one byte");

                        case > 0x80 when bytes.Length > 1:
                            // TODO check value against array
                            if (KLVLengthParser.ToLong(bytes.Skip(1).ToArray()) != LengthValue)
                            {
                                throw new ArgumentException($"Byte array value ({BitConverter.ToUInt32(bytes)}) does not match with given length value ({LengthValue})");
                            }

                            BERForm = BERFormEnum.LongForm;
                            AdditionalOctets = bytes.Length - 1;
                            break;

                        case 0x80:
                            //TODO is this the correct way to handle this?
                            BERForm = BERFormEnum.Indefinite;
                            throw new NotSupportedException("BER Indefinite Length is not supported");

                        case <= 0x7F when bytes.Length > 1:
                            throw new ArgumentException($"First byte indicates BER Long Form, but byte array consists of only one byte");

                        case <= 0x7F when bytes.Length == 1:
                            if (bytes[0] != LengthValue)
                            {
                                throw new ArgumentException($"Byte value ({bytes[0]}) does not match with given length value ({LengthValue})");
                            }
                            else
                            {
                                BERForm = BERFormEnum.ShortForm;
                                AdditionalOctets = 0;
                            }
                            break;
                    }
                    break;
            }
        }

        public override string ToString()
        {
            if (LengthEncoding == LengthEncodingEnum.BER)
            {
                if (BERForm.Value == BERFormEnum.LongForm)
                {
                    return $"{LengthEncoding} {BERForm.Value}, 1 + {AdditionalOctets} Octets ({LengthValue})";
                }
                else
                {
                    return $"{LengthEncoding} {BERForm.Value}, ({LengthValue})";
                }
            }
            else
            {
                return $"{LengthEncoding}, ({LengthValue})";
            }
        }
    }
}
