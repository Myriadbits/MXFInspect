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

namespace Myriadbits.MXF
{
    public enum PartitionType
    {
        Unknown,
        Header,
        Body,
        Footer
    }

    public class MXFPartition : MXFKLV, ILazyLoadable
    {
        [Category("PartitionHeader")]
        public PartitionType PartitionType { get; set; }

        [Category("PartitionHeader")]
        public bool Closed { get; set; }

        [Category("PartitionHeader")]
        public bool Complete { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.03010201.06000000")]
        public UInt16 MajorVersion { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.03010201.07000000")]
        public UInt16 MinorVersion { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010105.03010201.09000000")]
        public UInt32 KagSize { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06101003.01000000")]
        public UInt64 ThisPartition { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06101002.01000000")]
        public UInt64 PreviousPartition { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06101005.01000000")]
        public UInt64 FooterPartition { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.04060901.00000000")]
        public UInt64 HeaderByteCount { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.04060902.00000000")]
        public UInt64 IndexByteCount { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.01030405.00000000")]
        public UInt32 IndexSID { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.06080102.01030000")]
        public UInt64 BodyOffset { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010104.01030404.00000000")]
        public UInt32 BodySID { get; set; }

        [Category("PartitionHeader")]
        [ULElement("urn:smpte:ul:060e2b34.01010105.01020203.00000000")]
        public MXFKey OP { get; set; }

        [Browsable(false)]
        public MXFSystemItem FirstSystemItem { get; set; }

        [Browsable(false)]
        public MXFEssenceElement FirstPictureEssenceElement { get; set; }

        [Browsable(false)]
        public Dictionary<UInt16, MXFEntryPrimer> PrimerKeys { get; set; }

        [Browsable(false)]
        public MXFFile File { get; set; }

        [Browsable(false)]
        public int PartitionNumber { get; set; }

        [Browsable(false)]
        public bool IsLoaded { get; set; }


        public MXFPartition(MXFReader reader, MXFKLV headerKLV)
            : base(headerKLV, "Partition", KeyType.Partition)
        {
            this.m_eType = MXFObjectType.Partition;
            this.IsLoaded = false;

            // Determine the partition type
            switch (this.Key[13])
            {
                case 2: this.PartitionType = PartitionType.Header; break;
                case 3: this.PartitionType = PartitionType.Body; break;
                case 4: this.PartitionType = PartitionType.Footer; break;
                default:
                    this.PartitionType = PartitionType.Unknown;
                    Log(MXFLogType.Error, "unknown partition type");
                    break;
            }

            this.Closed = (this.PartitionType == PartitionType.Footer) || (this.Key[14] & 0x01) == 0x00;
            this.Complete = (this.Key[14] > 2);

            // Make sure we read at the data position
            reader.Seek(this.DataOffset);

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

            this.OP = reader.ReadULKey();

            MXFObject essenceContainers = reader.ReadAUIDSet("Essence Containers", "Essence Container");
            this.AddChild(essenceContainers);
        }


        public override string ToString()
        {
            if (this.PartitionType == PartitionType.Body)
            {
                if (this.FirstSystemItem != null)
                    return string.Format("Body Partition #{0} - {1}", this.PartitionNumber, this.FirstSystemItem.UserDateFullFrameNb);
                else
                    return string.Format("Body Partition #{0}", this.PartitionNumber);
            }
            return string.Format("{0} Partition", Enum.GetName(typeof(PartitionType), this.PartitionType));
        }


        /// <summary>
        /// Load the entire partition from disk (when not yet loaded)
        /// </summary>
        public void Load()
        {
            if (!this.IsLoaded)
            {
                MXFKLVFactory klvFactory = new MXFKLVFactory();
                using (MXFReader reader = new MXFReader(this.File.Filename))
                {
                    // Seek just after this partition
                    reader.Seek(this.DataOffset + this.Length);

                    while (!reader.EOF)
                    {
                        MXFKLV klv = klvFactory.CreateObject(reader, this);

                        if (klv.Key.Type == KeyType.Partition || klv.Key.Type == KeyType.RIP || klv.Key.Type == KeyType.PrimerPack)
                            break; // Next partition or other segment, quit reading							

                        if (!this.Children.Any(a => a.Offset == klv.Offset))
                        {
                            // Normal, just add the new child
                            this.AddChild(klv);
                        }

                        // Next KLV please
                        reader.Seek(klv.DataOffset + klv.Length);
                    }
                }
                this.IsLoaded = true;
            }
        }
    }
}
