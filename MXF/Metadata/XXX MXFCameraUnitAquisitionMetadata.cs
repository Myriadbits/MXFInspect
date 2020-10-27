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
    // CameraUnitAcquisitionMetadata 
    // urn:smpte:ul:060e2b34.027f0101.0c020101.02010000
    public class MXFCameraUnitAquisitionMetadata : MXFMetadataBaseclass
    {
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public string CameraSettingFileURI { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")] 
        public string CameraAttributes { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFKey TransferCharacteristic { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFRational CaptureFrameRate { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFKey AutoExposureMode { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFAutoFocusSensingAreaSetting? AutoFocusSensingAreaSetting { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFColorCorrectionFilterWheelSetting? ColorCorrectionFilterWheelSetting { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? NeutralDensityFilterWheelSetting { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? ImageSensorDimensionEffectiveWidth { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? ImageSensorDimensionEffectiveHeight { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFImageSensorReadoutMode? ImageSensorReadoutMode { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt32? ShutterSpeedAngle { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFRational ShutterSpeedTime { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public Int16? CameraMasterGainAdjustment { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? ISOSensitivity { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? ElectricalExtenderMagnification { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? ExposureIndexOfPhotoMeter { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFRational[] ColorMatrix { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public MXFAutoWhiteBalanceMode? AutoWhiteBalanceMode { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? WhiteBalance { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public Int16? CameraMasterBlackLevel { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? CameraKneePoint { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? CameraKneeSlope { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public UInt16? CameraLuminanceDynamicRange { get; set; }
        [CategoryAttribute("CameraUnitAcquisitionMetadata"), Description("")]
        public byte? GammaForCDL { get; set; }
        


        public MXFCameraUnitAquisitionMetadata(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "CameraUnitAcquisitionMetadata")
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
                case 0x8113: this.CameraSettingFileURI = reader.ReadUTF8String(localTag.Size); return true;
                case 0x8114: this.CameraAttributes = reader.ReadUTF8String(localTag.Size); return true;
                case 0x3210: this.TransferCharacteristic = reader.ReadKey(); return true;
                case 0x8106: this.CaptureFrameRate = reader.ReadRational(); return true;
                case 0x8100: this.AutoExposureMode = reader.ReadKey(); return true;
                case 0x8101: this.AutoFocusSensingAreaSetting = (MXFAutoFocusSensingAreaSetting) reader.ReadByte(); return true;
                case 0x8102: this.ColorCorrectionFilterWheelSetting = (MXFColorCorrectionFilterWheelSetting)reader.ReadByte(); return true;
                case 0x8103: this.NeutralDensityFilterWheelSetting = reader.ReadUInt16(); return true;
                case 0x8104: this.ImageSensorDimensionEffectiveWidth = reader.ReadUInt16(); return true;
                case 0x8105: this.ImageSensorDimensionEffectiveHeight = reader.ReadUInt16(); return true;
                case 0x8107: this.ImageSensorReadoutMode = (MXFImageSensorReadoutMode)reader.ReadByte(); return true;
                case 0x8108: this.ShutterSpeedAngle = reader.ReadUInt32(); return true;
                case 0x8109: this.ShutterSpeedTime = reader.ReadRational(); return true;
                case 0x810a: this.CameraMasterGainAdjustment = (short) reader.ReadUInt16(); return true;
                case 0x810b: this.ISOSensitivity = reader.ReadUInt16(); return true;
                case 0x810c: this.ElectricalExtenderMagnification = reader.ReadUInt16(); return true;
                case 0x8115: this.ExposureIndexOfPhotoMeter = reader.ReadUInt16(); return true;
                case 0x8118: this.ColorMatrix = GetRationals(reader, localTag.Size); return true;
                case 0x810d: this.AutoWhiteBalanceMode = (MXFAutoWhiteBalanceMode)reader.ReadByte(); return true;
                case 0x810e: this.WhiteBalance = reader.ReadUInt16(); return true;
                case 0x810f: this.CameraMasterBlackLevel = (short) reader.ReadUInt16(); return true;
                case 0x8110: this.CameraKneePoint = reader.ReadUInt16(); return true;
                case 0x8111: this.CameraKneeSlope = reader.ReadUInt16(); return true;
                case 0x8112: this.CameraLuminanceDynamicRange = reader.ReadUInt16(); return true;
                case 0x8116: this.GammaForCDL = reader.ReadByte(); return true;
            }


            return base.ParseLocalTag(reader, localTag);
        }

        private MXFRational[] GetRationals(MXFReader reader, int count)
        {
            MXFRational[] array = new MXFRational[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = reader.ReadRational();
            }
            return array;
        }
    }
}
