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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    // TODO add InstanceID property and check the inheritance tree, does it really derive from metadatabase class?
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0c020101.01010000")]
    public class MXFLensUnitAquisitionMetadata : MXFMetadataBaseclass
    {
        private const string CATEGORYNAME = "LensUnitAquisitionMetadata";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.03020302.02100100")]
        public string LensAttributes { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200202.01000000")]
        public UInt16? IrisFNumber { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200202.02000000")]
        public UInt16? FocusPositionFromImagePlane { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200202.03000000")]
        public UInt16? FocusPositionFromFrontLensVertex { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200202.04000000")]
        public bool? MacroSetting { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200202.05000000")]
        public UInt16? LensZoom35mmStillCameraEquivalent { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200202.06000000")]
        public UInt16? LensZoomActualFocalLength { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200202.07000000")]
        public UInt16? OpticalExtenderMagnification { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200202.08000000")]
        public UInt16? IrisTNumber { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200202.09000000")]
        public UInt16? IrisRingPosition { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200202.0a000000")]
        public UInt16? FocusRingPosition { get; set; }

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200202.0b000000")]
        public UInt16? ZoomRingPosition { get; set; }



        public MXFLensUnitAquisitionMetadata(MXFPack pack)
            : base(pack, "LensUnitAquisitionMetadata")
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
                case 0x8007:
                    this.LensAttributes = reader.ReadUTF8String(localTag.Length.Value);
                    localTag.Value = this.LensAttributes;
                    return true;
                case 0x8000:
                    this.IrisFNumber = reader.ReadUInt16();
                    localTag.Value = this.IrisFNumber;
                    return true;
                case 0x8001:
                    this.FocusPositionFromImagePlane = reader.ReadUInt16();
                    localTag.Value = this.FocusPositionFromImagePlane;
                    return true;
                case 0x8002:
                    this.FocusPositionFromFrontLensVertex = reader.ReadUInt16();
                    localTag.Value = this.FocusPositionFromFrontLensVertex;
                    return true;
                case 0x8003:
                    this.MacroSetting = reader.ReadBoolean();
                    localTag.Value = this.MacroSetting;
                    return true;
                case 0x8004:
                    this.LensZoom35mmStillCameraEquivalent = reader.ReadUInt16();
                    localTag.Value = this.LensZoom35mmStillCameraEquivalent;
                    return true;
                case 0x8005:
                    this.LensZoomActualFocalLength = reader.ReadUInt16();
                    localTag.Value = this.LensZoomActualFocalLength;
                    return true;
                case 0x8006:
                    this.OpticalExtenderMagnification = reader.ReadUInt16();
                    localTag.Value = this.OpticalExtenderMagnification;
                    return true;
                case 0x8008:
                    this.IrisTNumber = reader.ReadUInt16();
                    localTag.Value = this.IrisTNumber;
                    return true;
                case 0x8009:
                    this.IrisRingPosition = reader.ReadUInt16();
                    localTag.Value = this.IrisRingPosition;
                    return true;
                case 0x800a:
                    this.FocusRingPosition = reader.ReadUInt16();
                    localTag.Value = this.FocusRingPosition;
                    return true;
                case 0x800b:
                    this.ZoomRingPosition = reader.ReadUInt16();
                    localTag.Value = this.ZoomRingPosition;
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }
    }
}
