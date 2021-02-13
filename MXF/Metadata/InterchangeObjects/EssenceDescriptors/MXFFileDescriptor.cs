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

using Myriadbits.MXF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012500")]
    public class MXFFileDescriptor : MXFGenericDescriptor
    {
        private const string CATEGORYNAME = "FileDescriptor";
        private const int CATEGORYPOS = 3;

        // TODO remove this field, once all specialized subclasses have been implemented
        private static Dictionary<int, string> m_metaTypes = new Dictionary<int, string>();

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010105.06010103.05000000")]
        public UInt32? LinkedTrackId { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04060101.00000000")]
        public MXFRational SampleRate { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.04060102.00000000")]
        public MXFLengthType? ContainerDuration { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010104.01020000")]
        public MXFKey EssenceContainer { get; set; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.06010104.01030000")]
        public MXFKey Codec { get; set; }

        /// <summary>
        /// Static constructor to initialize the static array
        /// </summary>
        static MXFFileDescriptor()
        {
            // Add all meta data 
            m_metaTypes.Add(0x14, "Descriptor: Timecode");
            m_metaTypes.Add(0x23, "Descriptor: Data container");
            m_metaTypes.Add(0x27, "Generic Picture Essence Descriptor");
            m_metaTypes.Add(0x28, "CDCI Essence Descriptor");
            m_metaTypes.Add(0x29, "RGBA Essence Descriptor");
            m_metaTypes.Add(0x42, "Generic Sound Essence Descriptor");
            m_metaTypes.Add(0x43, "Generic Data Essence Descriptor");
            m_metaTypes.Add(0x44, "MultipleDescriptor");
            m_metaTypes.Add(0x47, "Descriptor: AES3");
            m_metaTypes.Add(0x48, "Descriptor: Wave");
            m_metaTypes.Add(0x51, "Descriptor: MPEG 2 Video");
            m_metaTypes.Add(0x5C, "Descriptor: ANC Data Descriptor, SMPTE 436 - 7.3");
            m_metaTypes.Add(0x25, "File Descriptor");
        }

        /// <summary>
        /// Constructor, set the correct descriptor name
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFFileDescriptor(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "Descriptor")
        {
            if (m_metaTypes.ContainsKey(this.Key[14]))
                this.MetaDataName = m_metaTypes[this.Key[14]];
        }

        /// <summary>
        /// Constructor when used as base class
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="headerKLV"></param>
        public MXFFileDescriptor(MXFReader reader, MXFKLV headerKLV, string metadataName)
            : base(reader, headerKLV, metadataName)
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x3006: this.LinkedTrackId = reader.ReadUInt32(); return true;
                case 0x3001: this.SampleRate = reader.ReadRational(); return true;
                case 0x3002: this.ContainerDuration = reader.ReadUInt64(); return true;
                case 0x3004: this.EssenceContainer = reader.ReadULKey(); return true;
                case 0x3005: this.Codec = reader.ReadULKey(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
