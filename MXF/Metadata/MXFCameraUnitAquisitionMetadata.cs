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
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    // TODO add InstanceID property and check the inheritance tree, does it really derive from metadatabase class?

    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0c020101.02010000")]
    public class MXFCameraUnitAquisitionMetadata : MXFMetadataBaseclass
    {
        private const string CATEGORYNAME = "CameraUnitAquisitionMetadata";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.01020108.02000000")]
        public string CameraSettingFileURI { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.03020302.02100200")]
        public string CameraAttributes { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.04010201.01010200")]
        public UL TransferCharacteristic { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04010301.03010000")]
        public MXFRational CaptureFrameRate { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01010000")]
        public UL AutoExposureMode { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01020000")]
        public MXFAutoFocusSensingAreaSetting? AutoFocusSensingAreaSetting { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01030000")]
        public MXFColorCorrectionFilterWheelSetting? ColorCorrectionFilterWheelSetting { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01040000")]
        public UInt16? NeutralDensityFilterWheelSetting { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01050000")]
        public UInt16? ImageSensorDimensionEffectiveWidth { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01060000")]
        public UInt16? ImageSensorDimensionEffectiveHeight { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01070000")]
        public MXFImageSensorReadoutMode? ImageSensorReadoutMode { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01080000")]
        public UInt32? ShutterSpeedAngle { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01080100")]
        public MXFRational ShutterSpeedTime { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.01090000")]
        public Int16? CameraMasterGainAdjustment { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.010a0000")]
        public UInt16? ISOSensitivity { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.010b0000")]
        public UInt16? ElectricalExtenderMagnification { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200103.010c0000")]
        public UInt16? ExposureIndexOfPhotoMeter { get; set; }
        
        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(EnumArrayConverter<MXFRational>))]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200103.010d0000")]
        public MXFRational[] ColorMatrix { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.02010000")]
        public MXFAutoWhiteBalanceMode? AutoWhiteBalanceMode { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.02020000")]
        public UInt16? WhiteBalance { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.02030000")]
        public Int16? CameraMasterBlackLevel { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.02040000")]
        public UInt16? CameraKneePoint { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.02050000")]
        public UInt16? CameraKneeSlope { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010d.04200103.02060000")]
        public UInt16? CameraLuminanceDynamicRange { get; set; }
        
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.0101010e.04200103.02070000")]
        public byte? GammaForCDL { get; set; }



        public MXFCameraUnitAquisitionMetadata(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "CameraUnitAquisitionMetadata")
        {
        }


        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x8113: this.CameraSettingFileURI = reader.ReadUTF8String(localTag.Size); return true;
                case 0x8114: this.CameraAttributes = reader.ReadUTF8String(localTag.Size); return true;
                case 0x3210: this.TransferCharacteristic = reader.ReadUL(); return true;
                case 0x8106: this.CaptureFrameRate = reader.ReadRational(); return true;
                case 0x8100: this.AutoExposureMode = reader.ReadUL(); return true;
                case 0x8101: this.AutoFocusSensingAreaSetting = (MXFAutoFocusSensingAreaSetting)reader.ReadByte(); return true;
                case 0x8102: this.ColorCorrectionFilterWheelSetting = (MXFColorCorrectionFilterWheelSetting)reader.ReadByte(); return true;
                case 0x8103: this.NeutralDensityFilterWheelSetting = reader.ReadUInt16(); return true;
                case 0x8104: this.ImageSensorDimensionEffectiveWidth = reader.ReadUInt16(); return true;
                case 0x8105: this.ImageSensorDimensionEffectiveHeight = reader.ReadUInt16(); return true;
                case 0x8107: this.ImageSensorReadoutMode = (MXFImageSensorReadoutMode)reader.ReadByte(); return true;
                case 0x8108: this.ShutterSpeedAngle = reader.ReadUInt32(); return true;
                case 0x8109: this.ShutterSpeedTime = reader.ReadRational(); return true;
                case 0x810a: this.CameraMasterGainAdjustment = (short)reader.ReadUInt16(); return true;
                case 0x810b: this.ISOSensitivity = reader.ReadUInt16(); return true;
                case 0x810c: this.ElectricalExtenderMagnification = reader.ReadUInt16(); return true;
                case 0x8115: this.ExposureIndexOfPhotoMeter = reader.ReadUInt16(); return true;
                case 0x8118: this.ColorMatrix = reader.ReadArray(reader.ReadRational, localTag.Size); return true;
                case 0x810d: this.AutoWhiteBalanceMode = (MXFAutoWhiteBalanceMode)reader.ReadByte(); return true;
                case 0x810e: this.WhiteBalance = reader.ReadUInt16(); return true;
                case 0x810f: this.CameraMasterBlackLevel = (short)reader.ReadUInt16(); return true;
                case 0x8110: this.CameraKneePoint = reader.ReadUInt16(); return true;
                case 0x8111: this.CameraKneeSlope = reader.ReadUInt16(); return true;
                case 0x8112: this.CameraLuminanceDynamicRange = reader.ReadUInt16(); return true;
                case 0x8116: this.GammaForCDL = reader.ReadByte(); return true;
            }


            return base.ParseLocalTag(reader, localTag);
        }
    }
}
