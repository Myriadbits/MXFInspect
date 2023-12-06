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

using Myriadbits.MXF.Identifiers;
using Myriadbits.MXF.KLV;
using System.ComponentModel;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01011e00")]
    public class MXFPluginDefinition : MXFDefinitionObject
    {
        private const string CATEGORYNAME = "MXFPluginDefinition";

        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.010a0101.01010000")]
        public string DeviceManufacturerName { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.010a0101.03000000")]
        public AUID ManufacturerID { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03030301.02010000")]
        public string PluginVersionString { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03030301.03000000")]
        public MXFVersion PluginVersion { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200901.00000000")]
        // TODO create "PluginCategoryType" type
        public object PluginCategory { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200902.00000000")]
        public AUID PluginPlatform { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200903.00000000")]
        public MXFVersion MinPlatformVersion { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200904.00000000")]
        public MXFVersion MaxPlatformVersion { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200905.00000000")]
        public AUID Engine { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200906.00000000")]
        public MXFVersion MinEngineVersion { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200907.00000000")]
        public MXFVersion MaxEngineVersion { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200908.00000000")]
        public AUID PluginAPI { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.05200909.00000000")]
        public MXFVersion MinPluginAPI { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.0520090a.00000000")]
        public MXFVersion MaxPluginAPI { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.0520090b.00000000")]
        public bool SoftwareOnly { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.0520090c.00000000")]
        public bool Accelerator { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.0520090d.00000000")]
        public bool Authentication { get; set; }
        [Category(CATEGORYNAME)]
        [ULElement("urn:smpte:ul:060e2b34.01010102.0520090f.00000000")]
        public AUID ImplementedClass { get; set; }


        public MXFPluginDefinition(MXFPack pack)
            : base(pack)
        {
            this.MetaDataName = "PluginDefinition";
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            switch (localTag.TagValue)
            {
                case 0x2206:
                    DeviceManufacturerName = reader.ReadUTF16String(localTag.Length.Value);
                    localTag.Value = DeviceManufacturerName;
                    return true;

                case 0x2208:
                    ManufacturerID = reader.ReadAUID();
                    localTag.Value = ManufacturerID;
                    return true;

                case 0x2205:
                    PluginVersionString = reader.ReadUTF16String(localTag.Length.Value);
                    localTag.Value = PluginVersionString;
                    return true;

                case 0x2204:
                    PluginVersion = reader.ReadVersion();
                    localTag.Value = PluginVersion;
                    return true;

                case 0x2203:
                    PluginCategory = reader.ReadBytes(16);
                    localTag.Value = PluginCategory;
                    return true;

                case 0x2209:
                    PluginPlatform = reader.ReadAUID();
                    localTag.Value = PluginPlatform;
                    return true;

                case 0x220a:
                    MinPlatformVersion = reader.ReadVersion();
                    localTag.Value = MinPlatformVersion;
                    return true;

                case 0x220b:
                    MaxPlatformVersion = reader.ReadVersion();
                    localTag.Value = MaxPlatformVersion;
                    return true;

                case 0x220c:
                    Engine = reader.ReadAUID();
                    localTag.Value = Engine;
                    return true;

                case 0x220d:
                    MinEngineVersion = reader.ReadVersion();
                    localTag.Value = MinEngineVersion;
                    return true;

                case 0x220e:
                    MaxEngineVersion = reader.ReadVersion();
                    localTag.Value = MaxEngineVersion;
                    return true;

                case 0x220f:
                    PluginAPI = reader.ReadAUID();
                    localTag.Value = PluginAPI;
                    return true;

                case 0x2210:
                    MinPluginAPI = reader.ReadVersion();
                    localTag.Value = MinPluginAPI;
                    return true;

                case 0x2211:
                    MaxPluginAPI = reader.ReadVersion();
                    localTag.Value = MaxPluginAPI;
                    return true;

                case 0x2212:
                    SoftwareOnly = reader.ReadBoolean();
                    localTag.Value = SoftwareOnly;
                    return true;

                case 0x2213:
                    Accelerator = reader.ReadBoolean();
                    localTag.Value = Accelerator;
                    return true;

                case 0x2214:
                    localTag.AddChild(reader.ReadReference<MXFLocator>("PluginLocators", localTag.Offset));
                    return true;

                case 0x2215:
                    Authentication = reader.ReadBoolean();
                    localTag.Value = Authentication;
                    return true;

                case 0x2216:
                    ImplementedClass = reader.ReadAUID();
                    localTag.Value = ImplementedClass;
                    return true;

                case 0x2207:
                    localTag.AddChild(reader.ReadReference<MXFNetworkLocator>("ManufacturerInfo", localTag.Offset));
                    return true;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

    }
}
