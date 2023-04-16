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
using System.IO;
using System.Linq;
using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public enum PartitionType
    {
        Unknown,
        Header, //(02h)
        Body,   //(03h)
        Footer  //(04h)
    }

    public enum PartitionStatus
    {
        [Description("Open & Incomplete")]
        OpenIncomplete = 0x01,
        [Description("Closed & Incomplete")]
        ClosedIncomplete = 0x02,
        [Description("Open & Complete")]
        OpenComplete = 0x03,
        [Description("Closed & Complete")]
        ClosedComplete = 0x04,
        [Description("Generic Stream Partition")]
        GenericStreamPartition = 0x11,
        [Description("Invalid")]
        Invalid,
    }

    public class MXFPartition : MXFPack, ILazyLoadable
    {
        private const string CATEGORYNAME = "PartitionHeader";

        [Category(CATEGORYNAME)]
        public PartitionType PartitionType { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.01020203.00000000")]
        public UL OperationalPattern { get; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(EnumDescriptionConverter))]
        public PartitionStatus Status { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.01020210.02010000")]
        [TypeConverter(typeof(AUIDArrayConverter))]
        public AUID[] EssenceContainers { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.01030404.00000000")]
        public UInt32 BodySID { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.01030405.00000000")]
        public UInt32 IndexSID { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.03010201.06000000")]
        public UInt16 MajorVersion { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.03010201.07000000")]
        public UInt16 MinorVersion { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.03010201.09000000")]
        public UInt32 KagSize { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06101003.01000000")]
        public UInt64 ThisPartition { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06101002.01000000")]
        public UInt64 PreviousPartition { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06101005.01000000")]
        public UInt64 FooterPartition { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.04060901.00000000")]
        public UInt64 HeaderByteCount { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.04060902.00000000")]
        public UInt64 IndexByteCount { get; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06080102.01030000")]
        public UInt64 BodyOffset { get; }

        [Browsable(false)]
        public MXFSystemMetaDataPack FirstSystemItem { get; set; }

        [Browsable(false)]
        public MXFEssenceElement FirstPictureEssenceElement { get; set; }

        [Browsable(false)]
        public MXFFile File { get; set; }

        [Browsable(false)]
        public int PartitionNumber { get; set; }

        [Browsable(false)]
        public bool IsLoaded { get; set; }


        public MXFPartition(MXFPack pack)
            : base(pack)
        {
            IKLVStreamReader reader = this.GetReader();
            this.IsLoaded = false;

            // Determine the partition type
            switch (this.Key[13])
            {
                case 0x02: this.PartitionType = PartitionType.Header; break;
                case 0x03: this.PartitionType = PartitionType.Body; break;
                case 0x04: this.PartitionType = PartitionType.Footer; break;
                default:
                    this.PartitionType = PartitionType.Unknown;
                    // TODO remove
                    //Log(MXFLogType.Error, "unknown partition type");
                    break;
            }

            switch (this.Key[14])
            {
                case 0x01:
                    this.Status = PartitionStatus.OpenIncomplete;
                    break;
                case 0x02:
                    this.Status = PartitionStatus.ClosedIncomplete;
                    break;
                case 0x03:
                    this.Status = PartitionStatus.OpenComplete;
                    break;
                case 0x04:
                    this.Status = PartitionStatus.ClosedComplete;
                    break;
                case 0x11:
                    this.Status = PartitionStatus.GenericStreamPartition;
                    break;
                default:
                    this.Status = PartitionStatus.Invalid;
                    break;
            }

            // Make sure we read at the data position
            reader.Seek(this.RelativeValueOffset);

            this.MajorVersion = reader.ReadUInt16();
            this.MinorVersion = reader.ReadUInt16();

            this.KagSize = reader.ReadUInt32();
            this.ThisPartition = reader.ReadUInt64();
            this.PreviousPartition = reader.ReadUInt64();
            this.FooterPartition = reader.ReadUInt64();
            this.HeaderByteCount = reader.ReadUInt64();
            this.IndexByteCount = reader.ReadUInt64();
            this.IndexSID = reader.ReadUInt32();
            this.BodyOffset = reader.ReadUInt64();
            this.BodySID = reader.ReadUInt32();

            this.OperationalPattern = reader.ReadUL();

            uint itemCount = reader.ReadUInt32();
            uint itemLength = reader.ReadUInt32(); // not really needed
            this.EssenceContainers = reader.ReadArray(reader.ReadUL, itemCount);
            //MXFNamedObject essenceContainers = new MXFNamedObject("EssenceContainers", this.Offset + reader.Position, this.TotalLength - reader.Position);
            //essenceContainers.AddChildren(reader.ReadAUIDSet("EssenceContainer", this.Offset, this.TotalLength - reader.Position));
            //this.AddChild(essenceContainers);
        }


        public override string ToString()
        {
            var root = this.Root() as MXFFile;
            int partitionCount = root?.GetPartitions().Count() ?? 0;
            int digitCount = Helper.GetDigitCount(partitionCount);
            string partitionNumberPadded = this.PartitionNumber.ToString().PadLeft(digitCount, '0');

            if (this.PartitionType == PartitionType.Body)
            {
                if (this.FirstSystemItem != null)
                    return $"Body Partition #{partitionNumberPadded} - {this.FirstSystemItem.UserDateFullFrameNb}";
                else
                    return $"Body Partition #{partitionNumberPadded}";
            }
            return $"{Enum.GetName<PartitionType>(this.PartitionType)} Partition";
        }


        /// <summary>
        /// Load the entire partition from disk (when not yet loaded)
        /// </summary>
        public void Load()
        {
            //if (!this.IsLoaded)
            //{
            //    MXFPackFactory klvFactory = new MXFPackFactory();
            //    using (IMXFReader reader = new MXFReader(this.File.Filename))
            //    {
            //        // Seek just after this partition
            //        reader.Seek(this.ValueOffset + this.Length.Value);

            //        while (!reader.EOF)
            //        {
            //            MXFPack pack = klvFactory.CreatePack(reader, this);

            //            if (pack is MXFPartition or MXFRIP or MXFPrimerPack)
            //            {
            //                break; // Next partition or end of file (RIP) other segment, quit reading							
            //            }
            //            if (!this.Children.Any(a => a.Offset == pack.Offset))
            //            {
            //                // Normal, just add the new child
            //                this.AddChild(pack);
            //            }

            //            // Next KLV please
            //            reader.Seek(pack.ValueOffset + pack.Length.Value);
            //        }
            //    }
            //    this.IsLoaded = true;
            //}
        }
    }
}
