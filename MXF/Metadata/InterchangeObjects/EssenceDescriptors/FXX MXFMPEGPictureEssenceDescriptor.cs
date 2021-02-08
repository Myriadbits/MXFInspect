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
    public class MXFMPEGPictureEssenceDescriptor : MXFCDCIPictureEssenceDescriptor
    {
        private const string CATEGORYNAME = "MPEGPictureEssenceDescriptor";

        private readonly MXFKey bitRate_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x0b, 0x00, 0x00);
        private readonly MXFKey identicalGOPIndicator_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x07, 0x00, 0x00);
        private readonly MXFKey maxGOPSize_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x08, 0x00, 0x00);
        private readonly MXFKey maxBPictureCount_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x09, 0x00, 0x00);
        private readonly MXFKey constantBPictureFlag_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x03, 0x00, 0x00);
        private readonly MXFKey codedContentScanningKind_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x04, 0x00, 0x00);
        private readonly MXFKey profileAndLevel_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x0a, 0x00, 0x00);
        private readonly MXFKey singleSequenceFlag_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x02, 0x00, 0x00);
        private readonly MXFKey closedGOP_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x06, 0x00, 0x00);
        private readonly MXFKey lowDelay_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05, 0x04, 0x01, 0x06, 0x02, 0x01, 0x05, 0x00, 0x00);

        [Category(CATEGORYNAME)]
        public bool? SingleSequenceFlag { get; set; }

        [Category(CATEGORYNAME)]
        public bool? LowDelayIndicator { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? BitRate { get; set; }

        [Category(CATEGORYNAME)]
        public bool? IdenticalGOPIndicator { get; set; }

        [Category(CATEGORYNAME)]
        public bool? ConstantBPictureFlag { get; set; }

        [Category(CATEGORYNAME)]
        public bool? ClosedGOPIndicator { get; set; }

        [Category(CATEGORYNAME)]
        public UInt16? MaximumGOPSize { get; set; }

        [Category(CATEGORYNAME)]
        public UInt16? MaximumBPictureCount { get; set; }

        [Category(CATEGORYNAME)]
        public byte? ProfileAndLevel { get; set; }

        [Category(CATEGORYNAME)]
        public MXFCodedContentScanning? CodedContentScanningKind { get; set; }


        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFMPEGPictureEssenceDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV)
        {
            this.MetaDataName = "MPEG Picture Essence Descriptor";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            if (localTag.Key != null)
            {
                switch (localTag.Key)
                {
                    case var _ when localTag.Key == bitRate_Key: this.BitRate = reader.ReadUInt32(); return true;
                    case var _ when localTag.Key == identicalGOPIndicator_Key: this.IdenticalGOPIndicator = reader.ReadBool(); return true;
                    case var _ when localTag.Key == maxGOPSize_Key: this.MaximumGOPSize = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == maxBPictureCount_Key: this.MaximumBPictureCount = reader.ReadUInt16(); return true;
                    case var _ when localTag.Key == constantBPictureFlag_Key: this.ConstantBPictureFlag = reader.ReadBool(); return true;
                    case var _ when localTag.Key == codedContentScanningKind_Key: this.CodedContentScanningKind = (MXFCodedContentScanning)reader.ReadByte(); return true;
                    case var _ when localTag.Key == profileAndLevel_Key: this.ProfileAndLevel = reader.ReadByte(); return true;
                    case var _ when localTag.Key == singleSequenceFlag_Key: this.SingleSequenceFlag = reader.ReadBool(); return true;
                    case var _ when localTag.Key == closedGOP_Key: this.ClosedGOPIndicator = reader.ReadBool(); return true;
                    case var _ when localTag.Key == lowDelay_Key: this.LowDelayIndicator = reader.ReadBool(); return true;
                }
            }

            return base.ParseLocalTag(reader, localTag);
        }

    }
}
