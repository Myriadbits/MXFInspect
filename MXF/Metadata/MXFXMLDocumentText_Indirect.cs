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

namespace Myriadbits.MXF.Metadata
{
    public class MXFXMLDocumentText_Indirect : MXFPack
    {
        private const string CATEGORYNAME = "XML Document Text(Indirect)";

        [Category(CATEGORYNAME)]
        [MultiLine]
        [ULElement("urn:smpte:ul:060e2b34.01010105.03010220.01000000")]
        public string Text { get; set; }

        public MXFXMLDocumentText_Indirect(MXFPack pack)
            : base(pack)
        {
            // TODO inline this method or roll out for all mxf packs for
            // better error handling
            Initialize(this.GetReader());
        }

        private void Initialize(IKLVStreamReader reader)
        {
            // Make sure we read at the data position
            reader.Seek(this.RelativeValueOffset);
            Text = reader.ReadUTF8String((int)this.Length.Value);
        }
    }
}
