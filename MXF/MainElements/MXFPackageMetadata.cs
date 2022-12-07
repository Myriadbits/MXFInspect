#region licenseBER ShortForm
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
using System;
using System.Diagnostics;
using System.IO;

namespace Myriadbits.MXF
{
    public class MXFPackageMetaData : MXFPack
    {
        private int nofSizeSize = 2;

        public MXFPackageMetaData(MXFPack pack)
            : base(pack)
        {
            IKLVStreamReader reader = this.GetReader();
            if (this.Key[5] == 0x63)
                nofSizeSize = 4;
            switch (this.Key[14])
            {
                case 0x02: this.Key.Name = "Package Metadata set"; break;
                case 0x03: this.Key.Name = "Picture Metadata set"; break;
                case 0x04: this.Key.Name = "Sound Metadata set"; break;
                case 0x05: this.Key.Name = "Data Metadata set"; break;
                case 0x06: this.Key.Name = "Control Metadata set"; break;
            }

            ParseElements(reader);
        }


        // Add all meta data see spec: SMPTE ST 331:2011
        private void ParseElements(IKLVStreamReader reader)
        {
            reader.Seek(this.RelativeValueOffset); // Seek to the start of the data

            byte[] byteArray;

            while (!reader.EOF)
            {
                var (tag, size) = GetTag(reader);
                var pos = reader.Position;
                byteArray = reader.ReadBytes((int)size);

                switch (tag)
                {
                    
                    // Metadata link
                    case 0x80:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "Metadata link", pos, size));
                        break;

                    // SMPTE 12M time-code
                    case 0x81:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "SMPTE 12M time-code", pos, size));
                        break;

                    // SMPTE 309M date-time stamp
                    case 0x82:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "SMPTE 309M date-time stamp", pos, size));
                        break;

                    // UMID
                    case 0x83:
                        if (size == 64)
                        {
                            var umid = new ExtendedUMID(byteArray);
                            this.AddChild(new MXFWrapperObject<ExtendedUMID>(umid, "ExtendedUMID", pos, size));
                        }
                        else if (size == 32)
                        {
                            var umid = new UMID(byteArray);
                            this.AddChild(new MXFWrapperObject<UMID>(umid, "UMID", pos, size));
                        }
                        else
                        {
                            // TODO raise an exception, don't eat it
                            Debug.WriteLine("Invalid tag size for UMID. Must be 32 bytes or 64 for extended UMID");
                        }
                        break;

                    // MPEG-2 picture editing
                    case 0x84:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "MPEG-2 picture editing", pos, size));
                        break;

                    // 8-channel AES3 editing
                    case 0x85:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "8-channel AES3 editing", pos, size));
                        break;

                    // Picture bit-stream splicing
                    case 0x86:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "Picture bit-stream splicing", pos, size));
                        break;

                    // MPEG decoder buffer delay
                    case 0x87:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "MPEG decoder buffer delay", pos, size));
                        break;

                    // KLV metadata
                    case 0x88:
                        var ms = new MemoryStream(byteArray);
                        var klvParser = new MXFPackParser(ms, this.Offset + pos);
                        var pack = klvParser.GetNext();
                        this.AddChild(pack);
                        break;

                    // AES3 non-audio metadata
                    case 0x89:
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "AES3 non-audio metadata", pos, size));
                        break;

                    default:
                        break;
                }

                // seek to next tag position
                reader.Seek(pos + size);
            }
        }

        private (byte Tag, UInt32 Size) GetTag(IKLVStreamReader reader)
        {
            byte tag = reader.ReadByte();
            UInt32 size = 0;
            if (nofSizeSize == 2)
            {
                size = reader.ReadUInt16();
            }
            else
            {
                size = reader.ReadUInt32();
            }


            return (tag, size);
        }
    }
}
