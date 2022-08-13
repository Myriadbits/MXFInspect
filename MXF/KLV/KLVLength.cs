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

namespace Myriadbits.MXF.KLV
{
    public class KLVLength : ByteArray
    {
        public enum LengthEncoding
        {
            OneByte = 1,
            TwoBytes = 2,
            FourBytes = 4,
            BER_ShortForm,
            BER_LongForm,
            BER_Indefinite

        }

        public KLVLength(LengthEncoding lengthEncoding, params byte[] bytes) : base(bytes)
        {
            switch (lengthEncoding)
            {
                case LengthEncoding.OneByte:
                case LengthEncoding.TwoBytes:
                case LengthEncoding.FourBytes:
                    // TODO do we need to check if each byte does not exceed 0x7F?
                    if (bytes.Length != (int)LengthEncoding.OneByte)
                    {
                        throw new ArgumentException($"Array length ({bytes.Length}) does not correspond to given length encoding ({lengthEncoding})");
                    }
                    break;

                case LengthEncoding.BER_ShortForm:
                    if (bytes.Length != 1)
                    {
                        throw new ArgumentException($"Array must consist of one byte in BER Short Form");
                    }
                    else if (bytes.Length > 0x7F)
                    {
                        throw new ArgumentException($"Byte must not exceed 0x7F (127) in BER Short Form)");
                    }
                    break;

                case LengthEncoding.BER_LongForm:
                    if (bytes.Length <= 1)
                    {
                        throw new ArgumentException($"Array must consist of at least two bytes in BER Long Form");
                    }
                    else if (bytes[1] <= 0x80)
                    {
                        throw new ArgumentException($"Invalid first byte for BER Long Form. Must be greater than 0x80 (128)");
                    }
                    break;

                case LengthEncoding.BER_Indefinite:
                    //TODO is this the correct way to handle this?
                    throw new NotSupportedException("BER Indefinite Length is not supported");

            }

        }
    }

}
