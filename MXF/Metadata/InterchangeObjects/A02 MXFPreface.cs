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
    public class MXFPreface : MXFInterchangeObject
    {
        private const string CATEGORYNAME = "Preface";

        private readonly MXFKey isRIPPresent_Key = new MXFKey(0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x0e, 0x04, 0x04, 0x05, 0x03, 0x00, 0x00, 0x00, 0x00);

		[Category(CATEGORYNAME)]
        public DateTime? LastModificationDate { get; set; }

        [Category(CATEGORYNAME)]
        public MXFVersion Version { get; set; }

        [Category(CATEGORYNAME)]
        public UInt32? ObjectModelVersion { get; set; }

        [Category(CATEGORYNAME)]
        public MXFKey OperationalPattern { get; set; }

        [Category(CATEGORYNAME)]
        public bool? IsRIPPresent { get; set; }



        public MXFPreface(MXFReader reader, MXFKLV headerKLV)
            : base(reader, headerKLV, "Preface")
        {
            this.Key.Type = KeyType.Preface;
        }

        /// <summary>
        /// Overridden method to process local tags
        /// </summary>
        /// <param name="localTag"></param>
        protected override bool ParseLocalTag(MXFReader reader, MXFLocalTag localTag)
        {
            switch (localTag.Tag)
            {
                case 0x3B02: this.LastModificationDate = reader.ReadTimestamp(); return true;
                case 0x3B05: this.Version = reader.ReadVersion(); return true;
                case 0x3B07: this.ObjectModelVersion = reader.ReadUInt32(); return true;
                case 0x3B03: this.AddChild(reader.ReadReference<MXFContentStorage>("ContentStorage")); return true;
                case 0x3B08: this.AddChild(reader.ReadReference<MXFGenericPackage>("PrimaryPackage")); return true;
                case 0x3B09: this.OperationalPattern = reader.ReadULKey(); return true;
                case 0x3B06: this.AddChild(reader.ReadReferenceSet<MXFIdentification>("Identifications", "Identification")); return true;
                case 0x3B0A: this.AddChild(reader.ReadAUIDSet("EssenceContainers", "EssenceContainer")); return true;
                    // TODO review how the metadataschemes are read (especially if there are no schemes present)
                case 0x3B0B: this.AddChild(reader.ReadAUIDSet("Descriptive Metadata Schemes", "DM scheme")); return true;
                case var _ when localTag.Key == isRIPPresent_Key: this.IsRIPPresent = reader.ReadBool(); return true;
            }
            return base.ParseLocalTag(reader, localTag);
        }

    }
}
