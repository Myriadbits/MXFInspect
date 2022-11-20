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

using System;
using System.Diagnostics;

namespace Myriadbits.MXF
{
    public class MXFPackageMetaData : MXFPack
    {
        private int nofSizeSize = 2;

        public MXFPackageMetaData(IMXFReader reader, MXFPack pack)
            : base(pack.Key, pack.Length, pack.Offset)
        {
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
        private void ParseElements(IMXFReader reader)
        {
            reader.Seek(this.RelativeValueOffset); // Seek to the start of the data

            long end = this.RelativeValueOffset + this.Length.Value;
            byte[] byteArray;

            while (!reader.EOF)
            {
                var (Tag, Size) = GetTag(reader);
                var pos = reader.Position;

                switch (Tag)
                {

                    // Metadata link
                    case 0x80:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "Metadata link", pos, Size));
                        break;

                    // SMPTE 12M time-code
                    case 0x81:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "SMPTE 12M time-code", pos, Size));
                        break;

                    // SMPTE 309M date-time stamp
                    case 0x82:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "SMPTE 309M date-time stamp", pos, Size));
                        break;

                    // UMID
                    case 0x83:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        if (Size == 64)
                        {
                            var umid = new ExtendedUMID(byteArray);
                            this.AddChild(new MXFWrapperObject<ExtendedUMID>(umid, "ExtendedUMID", pos, Size));
                        }
                        else if (Size == 32)
                        {
                            var umid = new UMID(byteArray);
                            this.AddChild(new MXFWrapperObject<UMID>(umid, "UMID", pos, Size));
                        }
                        else
                        {
                            Debug.WriteLine("Invalid tag size for UMID. Must be 32 bytes or 64 for extended UMID");
                        }
                        break;

                    // MPEG-2 picture editing
                    case 0x84:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "MPEG-2 picture editing", pos, Size));
                        break;

                    // 8-channel AES3 editing
                    case 0x85:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "8-channel AES3 editing", pos, Size));
                        break;

                    // Picture bit-stream splicing
                    case 0x86:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "Picture bit-stream splicing", pos, Size));
                        break;

                    // MPEG decoder buffer delay
                    case 0x87:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "MPEG decoder buffer delay", pos, Size));
                        break;

                    // KLV metadata
                    case 0x88:
                        var klvParser = new KLVParser(reader);
                        klvParser.Seek(reader.Position);
                        var pack = klvParser.GetNextMXFPack();
                        this.AddChild(pack);
                        break;

                    // AES3 non-audio metadata
                    case 0x89:
                        byteArray = reader.ReadArray(reader.ReadByte, (int)Size);
                        this.AddChild(new MXFWrapperObject<byte[]>(byteArray, "AES3 non-audio metadata", pos, Size));
                        break;

                    default:
                        break;
                }

                // seek to next tag position
                reader.Seek(pos + Size);
            }
        }

        private (byte Tag, UInt32 Size) GetTag(IMXFReader reader)
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
