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
using System.Collections.Generic;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    // TODO rename into MXFANCElement?
    public class MXFANCFrameElement : MXFEssenceElement
    {
        public MXFANCFrameElement(MXFPack pack)
            : base(pack)
        {
            IKLVStreamReader reader = this.GetReader();
            reader.Seek(this.RelativeValueOffset);
            // TODO make reading more robust if we encounter end of stream
            // i.e. if declared klv length is wrong
            if (!reader.EOF)
            {
                UInt16 nofPackets = reader.ReadUInt16();
                for (int n = 0; n < nofPackets; n++)
                {
                    MXFANCPacket newpacket = new MXFANCPacket(reader);
                    this.AddChild(newpacket);
                }
            }
        }

        public override string ToString()
        {
            return $"{this.Key.Name ?? "ANC Frame Element"} [packets {this.Children.Count}]";
        }
    }
}
