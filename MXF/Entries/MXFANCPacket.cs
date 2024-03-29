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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public enum MXFANCWrappingType
    {
        Unknown = 0x00,
        VANC = 0x01,
        VANCField1 = 0x02,
        VANCField2 = 0x03,
        VANCProgressiveFrame = 0x04,
        HANCFrame = 0x11,
        HANCField1 = 0x12,
        HANCField2 = 0x13,
        HANCProgressiveFrame = 0x14
    };

    public enum MXFANCPayloadCoding
    {
        Unknown_Coding = 0,
        Coding_8_bit_luma_samples = 4,
        Coding_8_bit_color_difference_samples = 5,
        Coding_8_bit_luma_and_color_difference_samples = 6,
        Coding_10_bit_luma_samples = 7,
        Coding_10_bit_color_difference_samples = 8,
        Coding_10_bit_luma_and_color_difference_samples = 9,
        Coding_8_bit_luma_samples_with_parity_error = 10,
        Coding_8_bit_color_difference_samples_with_parity_error = 11,
        Coding_8_bit_luma_and_color_difference_samples_with_parity_error = 12,
    };

    public class MXFANCPacket : MXFObject
    {
        private const string CATEGORYNAME = "ANCPacket";

        protected static Dictionary<UInt16, string> m_DIDDescription;

        [Category(CATEGORYNAME)]
        public UInt16 LineNumber { get; set; }
        [Category(CATEGORYNAME)]
        public MXFANCWrappingType WrappingType { get; set; }
        [Category(CATEGORYNAME)]
        public MXFANCPayloadCoding PayloadSamplingCoding { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16 PayloadSampleCount { get; set; }
        [Category(CATEGORYNAME)]
        public UInt32 PayloadArrayCount { get; set; }
        private UInt32 PayloadArrayElementLength { get; set; }
        [Category(CATEGORYNAME)]
        public byte DID { get; set; }
        [Category(CATEGORYNAME)]
        public byte SDID { get; set; }
        [Category(CATEGORYNAME)]
        public byte Size { get; set; }
        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        public byte[] Payload { get; set; }
        [Category(CATEGORYNAME)]
        public string ContentDescription { get; set; }

        public MXFANCPacket(IKLVStreamReader reader, long offset)
            : base(offset + reader.Position)
        {
            this.LineNumber = reader.ReadUInt16();
            this.WrappingType = (MXFANCWrappingType)reader.ReadByte();
            this.PayloadSamplingCoding = (MXFANCPayloadCoding)reader.ReadByte();
            this.PayloadSampleCount = reader.ReadUInt16();

            // as per SMPTE 436:2011 the array count(n) is the number of
            // payload data bytes in the array including any padding bytes.
            // The Payload Array Element Length shall be 1.
            this.PayloadArrayCount = reader.ReadUInt32(); // different from PayloadSampleCount because of padding
            this.PayloadArrayElementLength = reader.ReadUInt32(); // always 1 

            this.TotalLength = PayloadArrayCount + 14; //as we have read 14 bytes till here
            if (this.TotalLength > 3)
            {
                // Read the DID
                this.DID = reader.ReadByte();
                this.SDID = reader.ReadByte();
                this.Size = reader.ReadByte();

                DIDDescription description = SMPTERegisters.GetDIDDescription(this.DID, this.SDID);
                if(description != null)
                {
                    this.ContentDescription = $"{description.Application} ({description.UsedWhere})";
                }
                else
                {
                    this.ContentDescription = "<unknown content>";
                }
                
                UInt16 combinedID = (UInt16)((((UInt16)this.DID) << 8) | this.SDID);
                switch (combinedID)
                {
                    case 0x6101: // CDP
                        this.AddChild(new MXFCDPPacket(reader, this.Offset + 17));
                        break;
                    case 0x4105: // AFD
                        break;
                    case 0x4302: // OP47
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Some output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("ANC Packet - Line {0}, Count {1}", this.LineNumber, this.PayloadSampleCount);
        }
    }
}
