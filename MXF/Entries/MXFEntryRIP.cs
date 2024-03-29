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

using System;
using System.ComponentModel;
using System.Linq;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public class MXFEntryRIP : MXFObject
    {
        private const string CATEGORYNAME = "RIPEntry";

        [Category(CATEGORYNAME)]
        public UInt32 BodySID { get; set; }

        [Category(CATEGORYNAME)]
        public UInt64 PartitionOffset { get; set; }

        public MXFEntryRIP(IKLVStreamReader reader, long offset)
            : base(offset + reader.Position)
        {
            this.BodySID = reader.ReadUInt32(); // 4 bytes
            this.PartitionOffset = reader.ReadUInt64(); // 8 bytes
            this.TotalLength = 12; // Fixed length 4+8 bytes
        }

        /// <summary>
        /// Some output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            MXFRIP rip = this.Parent as MXFRIP;
            int? ripEntryCount = rip?.Children.Count;
            int? ripEntryNumber = rip?.Children.ToList().IndexOf(this);

            if (ripEntryCount.HasValue && ripEntryNumber.Value >= 0)
            {
                int digitCount = Helper.GetDigitCount(ripEntryCount.Value);
                string ripEntryNumberPadded = ripEntryNumber.Value.ToString().PadLeft(digitCount, '0');
                return $"RIPEntry #{ripEntryNumberPadded} - BodySID {this.BodySID}, PartitionOffset {this.PartitionOffset:N0} (0x{this.PartitionOffset:X})";
            }
            else
            {
                return $"RIPEntry - BodySID {this.BodySID}, PartitionOffset {this.PartitionOffset:N0} (0x{this.PartitionOffset:X})";
            }
        }
    }
}
