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

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class IndexEntryFlags
    {
        private readonly byte _bitfield;
        
        public bool RandomAccess { get; private set; }
        public bool SequenceHeader { get; private set; }
        public bool ForwardPredictionFlag { get; private set; }
        public bool BackwardPredictionFlag { get; private set; }
        public bool NumericalRangeOverload { get; private set; }
        public bool Reserved { get; private set; }
        public MPEGPictureType MPEGPictureType { get; private set; }

        public IndexEntryFlags(byte bitfield)
        {
            _bitfield = bitfield;
            RandomAccess = (0x80 & bitfield) != 0;
            SequenceHeader = (0x40 & bitfield) != 0;
            ForwardPredictionFlag = (0x20 & bitfield) != 0;
            BackwardPredictionFlag = (0x10 & bitfield) != 0;
            NumericalRangeOverload = (0x08 & bitfield) != 0;
            Reserved = (0x04 & bitfield) != 0;
            MPEGPictureType = (MPEGPictureType)(0x03 & bitfield);
        }

        public override string ToString()
        {
            return Convert.ToString(_bitfield, 2).PadLeft(8, '0');
        }
    }

    public enum MPEGPictureType
    {
        I_Frame = 0x00,
        P_Frame = 0x02,
        B_Frame = 0x03
    }


    public class MXFEntryIndex : MXFObject
    {
        private const string CATEGORYNAME = "IndexEntry";

        [Category(CATEGORYNAME)]
        public UInt64 Index { get; set; }
        [Category(CATEGORYNAME)]
        public sbyte? TemporalOffset { get; set; }
        [Category(CATEGORYNAME)]
        public sbyte? KeyFrameOffset { get; set; }
        [Category(CATEGORYNAME)]
        public IndexEntryFlags Flags { get; set; }
        [Category(CATEGORYNAME)]
        public UInt64? StreamOffset { get; set; }
        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(IntegerArrayConverter))]
        public UInt32[] SliceOffsets { get; set; }
        [Category(CATEGORYNAME)]
        public MXFRational[] PosTable { get; set; }

        public MXFEntryIndex(UInt64 index, MXFReader reader, byte? sliceCount, byte? posTableCount, UInt32 length)
            : base(reader)
        {
            this.m_eType = MXFObjectType.Index;

            this.Length = length;
            this.Index = index;
            this.TemporalOffset = reader.ReadSignedByte();
            this.KeyFrameOffset = reader.ReadSignedByte();
            this.Flags = new IndexEntryFlags(reader.ReadByte());
            this.StreamOffset = reader.ReadUInt64();

            if (sliceCount.HasValue && sliceCount.Value > 0)
            {
                this.SliceOffsets = new UInt32[sliceCount.Value];
                for (int n = 0; n < sliceCount; n++)
                    this.SliceOffsets[n] = reader.ReadUInt32();
            }

            if (posTableCount.HasValue && posTableCount.Value > 0)
            {
                this.PosTable = new MXFRational[posTableCount.Value];
                for (int n = 0; n < posTableCount; n++)
                    this.PosTable[n] = reader.ReadRational();
            }
        }

        /// <summary>
        /// Some output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Index[{0}] - TemporalOffset {1}, KeyFrameOffset {2}, Offset {3}", this.Index, this.TemporalOffset, this.KeyFrameOffset, this.StreamOffset);
        }

    }
}
