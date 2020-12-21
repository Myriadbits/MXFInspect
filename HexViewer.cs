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
        public HexViewer()
        {
            BytesPerLine = 16;
        }

        /// <summary>
        /// Read the data of an mxf object and display it as hex dump
        /// </summary>
        /// <param name="obj"></param>
        public void SetObject(MXFObject obj)
        {
            this.Clear();

            // Cast to KLV
            long readerOffset = obj.Offset;
            long len = (int)obj.Length;
            MXFKLV klv = obj as MXFKLV;
            if (klv != null)
            {
                // Determine real length including BER + Key
                len = (klv.DataOffset - readerOffset) + klv.Length;
            }
            MXFLocalTag lt = obj as MXFLocalTag;
            if (lt != null)
            {
                len = (lt.DataOffset - readerOffset) + lt.Size;
            }

            if (len > 0)
            {
                byte[] data = new byte[len];
                using (MXFReader reader = new MXFReader((obj.Root() as MXFFile).Filename))
                {
                    reader.Seek(readerOffset);
                    data = reader.ReadArray(reader.ReadByte, data.Length);
                }

                this.Text = GetHexDump(readerOffset, len, BytesPerLine, data);
            }
            else
            {
                this.Text = "";
            }
        }

        private string GetHexDump(long startOffset, long len, int bytesPerLine, byte[] data)
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

                // TODO: padding of offset not hardcoded
                sb.AppendLine(string.Format("{0:0000000000}  {1}  {2}", startOffset + currOffset, hex, asciisafe));
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

    }
}
