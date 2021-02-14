#region license
//
// MXFInspect - Myriadbits MXF Viewer. 
// Inspect MXF Files.
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

using Myriadbits.MXF;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public class HexViewer : RichTextBox
    {
        public int BytesPerLine { get; set; } = 16;

        public long DisplayableBytesThreshold { get; set; } = 1000000;

        public bool ShowOffsetAsHex { get; private set; } = false;

        protected MXFObject ObjectToShow { get; set; }

        public HexViewer()
        {
        }

        /// <summary>
        /// Read the data of an mxf object and display it as hex dump
        /// </summary>
        /// <param name="obj"></param>
        public void SetObject(MXFObject obj)
        {
            this.ObjectToShow = obj;
            this.Clear();


            long len = GetObjectLength(obj);

            if (len > DisplayableBytesThreshold)
            {
                // TODO Hexdump should be truncated and not entirely omitted 
                this.Text = "Hexdump not shown due to packet size";
            }
            else if (len > 0)
            {
                byte[] data = new byte[len];
                using (MXFReader reader = new MXFReader((obj.Root() as MXFFile).Filename))
                {
                    reader.Seek(obj.Offset);
                    data = reader.ReadArray(reader.ReadByte, data.Length);
                }
                int maxNumOfDigits = obj.GetMaxOffsetDigitCount();

                this.Text = GetHexDump(obj.Offset, len, maxNumOfDigits, BytesPerLine, data);
            }
        }

        public void RefreshView()
        {
            if (ObjectToShow != null)
            {
                SetObject(ObjectToShow);
            }

        }

        public void SetOffsetStyle(bool showOffsetAsHex)
        {
            this.ShowOffsetAsHex = showOffsetAsHex;
            this.RefreshView();
        }

        private long GetObjectLength(MXFObject obj)
        {
            switch (obj)
            {
                case MXFKLV klv:
                    return klv.DataOffset - klv.Offset + klv.Length;

                case MXFLocalTag tag:
                    return tag.DataOffset - tag.Offset + tag.Size;

                default:
                    return obj.Length;
            }
        }

        private string GetHexDump(long startOffset, long len, int maxNumOfDigits, int bytesPerLine, byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (long currOffset = 0; currOffset < len; currOffset += bytesPerLine)
            {
                int cnt = bytesPerLine;

                // how long is the last line?
                if (len - currOffset < bytesPerLine)
                {
                    cnt = (int)(len - currOffset);
                }

                var slice = new ReadOnlySpan<byte>(data, (int)currOffset, cnt);

                string hex = GetHexValues(slice.ToArray()).PadRight(bytesPerLine * 3);
                string asciisafe = GetASCIISafeString(slice.ToArray());
                string address = FormatAddress(startOffset + currOffset, maxNumOfDigits, ShowOffsetAsHex);

                sb.AppendLine(string.Format("{0}  {1}  {2}", address, hex, asciisafe));
            }

            return sb.ToString();
        }

        private string GetASCIISafeString(byte[] data)
        {
            string ascii = System.Text.Encoding.ASCII.GetString(data);
            return new string(ascii.Select(ch => ch < 0x20 ? '.' : ch).ToArray());
        }

        private string GetHexValues(byte[] data)
        {
            return BitConverter.ToString(data).Replace('-', ' ');
        }

        private string FormatAddress(long addrValue, int maxNumOfDigits, bool showOffsetAsHex)
        {
            string address;
            if (showOffsetAsHex)
            {
                address = string.Format("{0:X}", addrValue);
                address = "0x" + address.PadLeft(maxNumOfDigits, '0');
            }
            else
            {
                address = string.Format("{0:D}", addrValue);
                address = address.PadLeft(maxNumOfDigits, '0');
            }

            return address;
        }
    }
}
