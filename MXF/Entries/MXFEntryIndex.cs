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
using Myriadbits.MXF.KLV;

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
        [TypeConverter(typeof(UInt32ArrayConverter))]
        public UInt32[] SliceOffsets { get; set; }
        [Category(CATEGORYNAME)]
        public MXFRational[] PosTable { get; set; }

        public MXFEntryIndex(UInt64 index, IKLVStreamReader reader, long offset, byte? sliceCount, byte? posTableCount, UInt32 length)
            : base(reader)
        {
            this.TotalLength = length;
            this.Offset = offset;
            this.Index = index;
            this.TemporalOffset = reader.ReadSByte();
            this.KeyFrameOffset = reader.ReadSByte();
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
            // calculate correct paddings
            // TODO optimize, could explode in users face
            IEnumerable<MXFEntryIndex> siblings = this.Parent?.Children?.OfType<MXFEntryIndex>();
            long maxIndexEntryCount = siblings?.Count() ?? 0;
            int lenDigitCount = Helper.GetDigitCount(maxIndexEntryCount);
            string indexString = this.Index.ToString().PadLeft(lenDigitCount, '0');

            long maxTempOffset = siblings?.Select(e => Math.Abs(e.TemporalOffset ?? 0)).Max() ?? 0;
            int maxTempOffsetDigitCount = Helper.GetDigitCount(maxTempOffset);
            string tempOffString = this.TemporalOffset.ToString().PadLeft(maxTempOffsetDigitCount + 1);

            long maxKeyFrameOffset = siblings?.Select(e => Math.Abs(e.KeyFrameOffset ?? 0)).Max() ?? 0;
            int maxKeyFrameOffsetDigitCount = Helper.GetDigitCount(maxKeyFrameOffset);
            string keyFrameOffString = this.KeyFrameOffset.ToString().PadLeft(maxKeyFrameOffsetDigitCount + 1);

            long maxStreamOffset = siblings?.Select(e => Math.Abs((long)(e.StreamOffset ?? 0))).Max() ?? 0;
            int maxStreamOffsetDigitCount = Helper.GetDigitCount(maxStreamOffset);
            string streamOffsetString = this.StreamOffset.ToString().PadLeft(maxStreamOffsetDigitCount);

            return $"Index[{indexString}] - TempOffs: {tempOffString}, KeyOffs: {keyFrameOffString}, Offset: {streamOffsetString}";

            //return this.Index.ToString();
        }

    }
}
