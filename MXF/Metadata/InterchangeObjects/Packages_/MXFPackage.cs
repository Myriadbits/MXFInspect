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
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01013400")]
    public class MXFPackage : MXFInterchangeObject
    {
        private const string CATEGORYNAME = "GenericPackage";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.01011510.00000000")]
        public UMID PackageID { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010101.01030302.01000000")]
        public string PackageName { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(DateTimeTypeConverter))]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020110.02050000")]
        public DateTime? ModifiedDate { get; set; }

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(DateTimeTypeConverter))]
        [ULElement("urn:smpte:ul:060e2b34.01010102.07020110.01030000")]
        public DateTime? CreationDate { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010107.05010108.00000000")]
        public UInt16? PackageUsage { get; set; }

        public MXFPackage(MXFPack pack, string metadataName)
            : base(pack, metadataName)
        {
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x4401:
                    this.PackageID = reader.ReadUMIDKey();
                    localTag.Value = this.PackageID;
                    return true;
                case 0x4402:
                    this.PackageName = reader.ReadUTF16String(localTag.Length.Value);
                    localTag.Value = this.PackageName;
                    return true;
                case 0x4403:
                    localTag.AddChildren(reader.GetReferenceSet<MXFTrack>("Track", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x4404:
                    this.ModifiedDate = reader.ReadTimeStamp();
                    localTag.Value = this.ModifiedDate;
                    return true;
                case 0x4405:
                    this.CreationDate = reader.ReadTimeStamp();
                    localTag.Value = this.CreationDate;
                    return true;
                case 0x4406:
                    localTag.AddChildren(reader.GetReferenceSet<MXFTaggedValue>("PackageUserComment", localTag.Offset, localTag.Length.Value));
                    return true;
                // TODO change to KLVData once implemented
                case 0x4407: 
                    localTag.AddChildren(reader.GetReferenceSet<MXFObject>("PackageKLVData", localTag.Offset, localTag.Length.Value));
                    return true;
                case 0x4408: 
                    this.PackageUsage = reader.ReadUInt16();
                    localTag.Value = this.PackageUsage;
                    return true;
                case 0x4409: 
                    localTag.AddChildren(reader.GetReferenceSet<MXFTaggedValue>("PackageAttribute", localTag.Offset, localTag.Length.Value));
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

        public IEnumerable<MXFTrack> GetLinkedTracks()
        {
            return this.Children.OfType<MXFLocalTag>()
                .SingleOrDefault(lt => lt.TagValue == 0x4403)
                .Children.OfType<MXFReference<MXFTrack>>()
                .Select(r => r.Reference);
        }
    }
}
