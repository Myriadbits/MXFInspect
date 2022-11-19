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
using System;
using System.ComponentModel;
using System.Text;

namespace Myriadbits.MXF
{
    public class MXFLocalTag : MXFObject
    {
        private const string CATEGORYNAME = "LocalTag";

        [Category(CATEGORYNAME)]
        public long DataOffset { get; set; }

        [Category(CATEGORYNAME)]
        public UInt16 Tag { get; set; }

        [Category(CATEGORYNAME)]
        public UInt16 Size { get; set; }

        [Category(CATEGORYNAME)]
        public AUID Key { get; set; }

        [Category(CATEGORYNAME)]
        public object Value { get; set; }


        public MXFLocalTag(IMXFReader reader)
            : base(reader)
        {
            this.Tag = reader.ReadUInt16();
            this.Size = reader.ReadUInt16();
            this.DataOffset = reader.Position;
            this.TotalLength = this.Size;
        }

        /// <summary>
        /// Parse this tag
        /// </summary>
        /// <param name="reader"></param>
        public void Parse(IMXFReader reader)
        {
            if (this.Size == 1)
                this.Value = reader.ReadByte();
            else if (this.Size == 2)
                this.Value = reader.ReadUInt16();
            else if (this.Size == 4)
                this.Value = reader.ReadUInt32();
            else if (this.Size == 8)
                this.Value = reader.ReadUInt64();
            else
            {
                this.Value = reader.ReadArray(reader.ReadByte, this.Size);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Key is UL ul)
            {
                sb.Append($"LocalTag 0x{this.Tag:X4} -> {ul.Name} [len {this.Size}]");
            }
            else
            {
                sb.Append($"LocalTag 0x{this.Tag:X4} -> <Unknown tag> [len {this.Size}]");
            }
            return sb.ToString();
        }

    }
}
