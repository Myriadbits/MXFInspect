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
using Myriadbits.MXF.KLV;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using static Myriadbits.MXF.KLV.KLVLength;
using static Myriadbits.MXF.KLVKey;

namespace Myriadbits.MXF
{
    public class MXFLocalTag : KLVTriplet<KLVKey, KLVLength, ByteArray>
    {
        // TODO add Alias Universal Label?
        public AUID AliasUID { get; set; }

        [Browsable(false)]
        public UInt16 TagValue { get { return (UInt16)((UInt16)(Key[0] << 8) + Key[1]); } }

        public MXFLocalTag(KLVKey key, KLVLength length, long offset, Stream stream) : base(key, length, offset, stream)
        {
            // check passed parameters 
            if (Key.KeyLength != KeyLengths.TwoBytes)
            {
                throw new ArgumentException($"The key for a local tag must be two bytes long, instead is: {Key.KeyLength}.");
            }

            if (Length.LengthEncoding != LengthEncodings.TwoBytes)
            {
                throw new ArgumentException($"The length encoding for a local tag must be two bytes long, instead is: {Length.LengthEncoding}.");
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (AliasUID is UL ul)
            {
                sb.Append($"LocalTag {this.Key:X4} [len {this.Length.Value}] -> {ul.Name} ");
            }
            else
            {
                sb.Append($"LocalTag {this.Key:X4} [len {this.Length.Value}] -> <Unknown tag> ");
            }
            return sb.ToString();
        }
    }
}
