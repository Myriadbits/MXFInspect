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
using System.ComponentModel;

namespace Myriadbits.MXF
{
    public class MXFKLV : MXFObject
    {
        private static MXFKey s_MxfKlvKey = new MXFKey(0x06, 0x0e, 0x2b, 0x34);

        [CategoryAttribute("KLV"), ReadOnly(true)]
        public MXFKey Key { get; set; }

        [CategoryAttribute("KLV"), ReadOnly(true)]
        public long DataOffset { get; set; } // Points just after the KLV

        [Browsable(false)]
        public MXFPartition Partition { get; set; }

        [CategoryAttribute("KLV"), ReadOnly(true)]
        public MXFBER BER { get; set; }

        /// <summary>
        /// Create the KLV key
        /// </summary>
        /// <param name="reader"></param>
        public MXFKLV(MXFReader reader)
            : base(reader)
        {
            this.Key = CreateAndValidateKey(reader);
            this.BER = DecodeBerLength(reader);
            this.Length = this.BER.Size;
            this.DataOffset = reader.Position;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="reader"></param>
        public MXFKLV(MXFKLV klv, string name, KeyType type)
        {
            this.Offset = klv.Offset;
            this.Key = klv.Key;
            this.Key.Name = name;
            this.Key.Type = type;
            this.BER = klv.BER;
            this.Length = klv.Length;
            this.DataOffset = klv.DataOffset;
            this.Partition = klv.Partition;
        }


        /// <summary>
        /// Validate if the current position is a valid SMPTE key
        /// </summary>
        private MXFKey CreateAndValidateKey(MXFReader reader)
        {
            byte iso = reader.ReadByte();
            byte len = reader.ReadByte();
            byte smp = 0, te = 0;
            bool valid = false;
            if (iso == 0x06) // Do not check length when not iso
            {
                smp = reader.ReadByte();
                te = reader.ReadByte();
                valid = (smp == 0x2B && te == 0x34); // SMPTE define
            }

            MXFKey key = new MXFKey(iso, len, smp, te, reader);

            if (!valid)
            {
                //throw new ApplicationException(string.Format("Invalid SMPTE Key found at offset {0}! Incorrect MXF file!", reader.Position - 4));
                LogError("Invalid SMPTE Key found at offset {0}! Key: {1}", reader.Position - 4, key.Name);
            }
            return key;
        }

        /// <summary>
        /// Decode the length
        /// </summary>
        /// <param name="reader"></param>
        private MXFBER DecodeBerLength(MXFReader reader)
        {
            long size = reader.ReadByte();

            if (size <= 0x7F)
            {
                // short form, size = length
                return new MXFBER(0, size);
            }
            else if (size > 0x80)
            {
                // long form: size is number of octets following, 1 + x octets
                int additionalOctets = (int)size - 0x80;

                // SMPTE 379M 5.3.4 guarantee that additional octets must not exceed 8 bytes
                if (additionalOctets > 8)
                {
                    LogWarning("KLV length has more than 8 octets (not valid according to SMPTE 379M 5.3.4) found at offset {0}!", reader.Position);
                }
                size = 0;
                for (int i = 0; i < additionalOctets; i++)
                {
                    size = size << 8 | reader.ReadByte();
                }

                return new MXFBER(additionalOctets, size);
            }
            else
            {
                // size is 0x80, which means indefinite
                LogWarning("KLV length having value 0x80 (=indefinite, not valid according to SMPTE 379M 5.3.4) found at offset {0}!", reader.Position);
                return new MXFBER(-1, -1);
            };
        }

        /// <summary>
        /// Some output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Children != null && this.Children.Count > 0)
                return string.Format("{0} [len {1}]", this.Key.Name, this.Children.Count);
            return string.Format("{0} [len {1}]", this.Key.Name, this.Length);
        }
    }
}
