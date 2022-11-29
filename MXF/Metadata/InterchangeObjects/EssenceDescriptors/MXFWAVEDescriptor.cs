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

using System.ComponentModel;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    [ULGroup("urn:smpte:ul:060e2b34.027f0101.0d010101.01012c00")]
    public class MXFWAVEDescriptor : MXFFileDescriptor
    {
        private const string CATEGORYNAME = "WAVEDescriptor";

        [Category(CATEGORYNAME)]
        [TypeConverter(typeof(ByteArrayConverter))]
        [ULElement("urn:smpte:ul:060e2b34.01010102.03030302.01000000")]
        public byte[] WaveSummary { get; set; }
        
        public MXFWAVEDescriptor(IKLVStreamReader reader, MXFPack pack)
            : base(reader, pack, "WAVEDescriptor")
        {
        }

        protected override bool ParseLocalTag(IKLVStreamReader reader, MXFLocalTag localTag)
        {
            if (localTag.AliasUID != null)
            {
                switch (localTag.TagValue)
                {
                    case 0x3801: this.WaveSummary = reader.ReadBytes((int)localTag.Length.Value); return true;
                }
            }
            return base.ParseLocalTag(reader, localTag);
        }
    }
}
