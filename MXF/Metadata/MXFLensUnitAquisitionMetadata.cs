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
    // LensUnitAcquisitionMetadata 
    // urn:smpte:ul:060e2b34.027f0101.0c020101.01010000

    // TODO add InstanceID property and check the inheritance tree, does it really derive from metadatabase class?
    public class MXFLensUnitAquisitionMetadata : MXFMetadataBaseclass
    {
        private const string CATEGORYNAME = "LensUnitAquisitionMetadata";

        [Category(CATEGORYNAME)]
        public string LensAttributes { get; set; }
        [Category(CATEGORYNAME)] 
        public UInt16? IrisFNumber { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? FocusPositionFromImagePlane { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? FocusPositionFromFrontLensVertex { get; set; }
        [Category(CATEGORYNAME)]
        public bool? MacroSetting { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? LensZoom35mmStillCameraEquivalent { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? LensZoomActualFocalLength { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? OpticalExtenderMagnification { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? IrisTNumber { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? IrisRingPosition { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? FocusRingPosition { get; set; }
        [Category(CATEGORYNAME)]
        public UInt16? ZoomRingPosition { get; set; }



        public MXFLensUnitAquisitionMetadata(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "LensUnitAquisitionMetadata")
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
                case 0x8007: this.LensAttributes = reader.ReadUTF8String(localTag.Size); return true;
                case 0x8000: this.IrisFNumber = reader.ReadUInt16(); return true;
                case 0x8001: this.FocusPositionFromImagePlane = reader.ReadUInt16();  return true;
                case 0x8002: this.FocusPositionFromFrontLensVertex = reader.ReadUInt16(); return true;
                case 0x8003: this.MacroSetting = reader.ReadBool(); return true;
                case 0x8004: this.LensZoom35mmStillCameraEquivalent = reader.ReadUInt16(); return true;
                case 0x8005: this.LensZoomActualFocalLength= reader.ReadUInt16(); return true;
                case 0x8006: this.OpticalExtenderMagnification = reader.ReadUInt16(); return true;
                case 0x8008: this.IrisTNumber= reader.ReadUInt16(); return true;
                case 0x8009: this.IrisRingPosition= reader.ReadUInt16(); return true;
                case 0x800a: this.FocusRingPosition= reader.ReadUInt16(); return true;
                case 0x800b: this.ZoomRingPosition = reader.ReadUInt16();return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
