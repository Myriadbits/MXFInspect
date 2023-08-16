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
using System.ComponentModel;
using System.Linq;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KLVBERLength : KLVLengthBase
    {
        public enum BERForms
        {
            ShortForm,
            LongForm,
            Indefinite // Not supported
        }

        public BERForms BERForm { get; }
        public int AdditionalOctets { get; }

        public KLVBERLength(long lengthValue, params byte[] bytes) : base(bytes)
        {
            Value = lengthValue;

            // Argument validation logic

            switch (bytes[0])
            {
                case > 0x80 when bytes.Length == 1:
                    throw new ArgumentException($"First byte indicates BER Long Form, but byte array consists of only one byte");

                case > 0x80 when bytes.Length > 1:
                    // TODO check value against array
                    if (bytes.Skip(1).ToArray().ToLong() != Value)
                    {
                        throw new ArgumentException($"Byte array value ({BitConverter.ToUInt32(bytes)}) does not match with given length value ({Value})");
                    }

                    BERForm = BERForms.LongForm;
                    AdditionalOctets = bytes.Length - 1;
                    break;

                case 0x80:
                    //TODO is this the correct way to handle this?
                    BERForm = BERForms.Indefinite;
                    throw new NotSupportedException("BER Indefinite Length is not supported");

                case <= 0x7F when bytes.Length > 1:
                    throw new ArgumentException($"First byte indicates BER Long Form, but byte array consists of only one byte");

                case <= 0x7F when bytes.Length == 1:
                    if (bytes[0] != Value)
                    {
                        throw new ArgumentException($"Byte value ({bytes[0]}) does not match with given length value ({Value})");
                    }
                    else
                    {
                        BERForm = BERForms.ShortForm;
                        AdditionalOctets = 0;
                    }
                    break;
            }
        }


        public override string ToString()
        {
            if (BERForm == BERForms.LongForm)
            {
                return $"{Value} (BER {BERForm}, 1 + {AdditionalOctets} Octets)";
            }
            else
            {
                return $"{Value} (BER {BERForm})";
            }
        }

    }
}
