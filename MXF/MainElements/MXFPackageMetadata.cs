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

using Myriadbits.MXF.KLV;
using System.Diagnostics;
using System.IO;

namespace Myriadbits.MXF
{
    public class MXFPackageMetaData : MXFLocalSet
    {
        public MXFPackageMetaData(MXFPack pack)
            : base(pack)
        {
            switch (this.Key[14])
            {
                case 0x02: this.Key.Name = "Package Metadata Set"; break;
                case 0x03: this.Key.Name = "Picture Metadata Set"; break;
                case 0x04: this.Key.Name = "Sound Metadata Set"; break;
                case 0x05: this.Key.Name = "Data Metadata Set"; break;
                case 0x06: this.Key.Name = "Control Metadata Set"; break;
            }
        }

        protected override bool ReadLocalTagValue(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            byte[] byteArray = reader.ReadBytes((int)localTag.Length.Value);
            
            // Add all meta data see spec: SMPTE ST 331:2011
            switch (localTag.TagValue)
            {
                // Metadata link
                case 0x80:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "Metadata link", localTag.Offset, localTag.Length.Value));
                    break;

                // SMPTE 12M time-code
                case 0x81:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "SMPTE 12M time-code", localTag.Offset, localTag.Length.Value));
                    break;

                // SMPTE 309M date-time stamp
                case 0x82:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "SMPTE 309M date-time stamp", localTag.Offset, localTag.Length.Value));
                    break;

                // UMID
                case 0x83:
                    if (localTag.Length.Value == 64)
                    {
                        ExtendedUMID umid = new ExtendedUMID(byteArray);
                        localTag.Value = umid;
                        return true;
                    }
                    else if (localTag.Length.Value == 32)
                    {
                        ExtendedUMID umid = new ExtendedUMID(byteArray);
                        localTag.Value = umid;
                        return true;
                    }
                    else
                    {
                        // TODO raise an exception, don't eat it
                        Debug.WriteLine("Invalid tag size for UMID. Must be 32 bytes or 64 for extended UMID");
                        return false;
                    }

                // MPEG-2 picture editing
                case 0x84:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "MPEG-2 picture editing", localTag.Offset, localTag.Length.Value));
                    break;

                // 8-channel AES3 editing
                case 0x85:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "8-channel AES3 editing", localTag.Offset, localTag.Length.Value));
                    break;

                // Picture bit-stream splicing
                case 0x86:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "Picture bit-stream splicing", localTag.Offset, localTag.Length.Value));
                    break;

                // MPEG decoder buffer delay
                case 0x87:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "MPEG decoder buffer delay", localTag.Offset, localTag.Length.Value));
                    break;

                // KLV metadata
                case 0x88:
                    var ms = new MemoryStream(byteArray);
                    var klvParser = new MXFPackParser(ms, localTag.ValueOffset);
                    var pack = klvParser.GetNext();
                    if (pack is MXFLocalSet ls)
                    {
                        ls.ReadLocalTagValues();
                    }
                    localTag.AddChild(pack);
                    break;

                // AES3 non-audio metadata
                case 0x89:
                    this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "AES3 non-audio metadata", localTag.Offset, localTag.Length.Value));
                    break;

                default:
                    break;
            }
            return base.ReadLocalTagValue(reader, localTag);
        }

        public override string ToString()
        {
            return Key.Name;
        }
    }
}
