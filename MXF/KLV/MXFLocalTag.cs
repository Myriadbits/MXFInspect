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
using Myriadbits.MXF.Utils;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static Myriadbits.MXF.KLV.KLVLength;
using static Myriadbits.MXF.KLVKey;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFLocalTag : KLVTriplet<KLVKey, KLVLength, KLVValue>
    {
        private const string CATEGORYNAME = "LocalTag";
        private const int CATEGORYPOS = 1;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        // override not really needed but for assigning the sortedcategory attribute
        public override KLVKey Key => base.Key;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        // override not really needed but for assigning the sortedcategory attribute
        public override KLVLength Length => base.Length;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        public AUID AliasUID { get; set; }

        [Browsable(false)]
        public UInt16 TagValue { get; }

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Value of local tag")]
        public object Value { get; set; }

        public MXFLocalTag(KLVKey key, KLVLength length, long offset, Stream stream) : base(key, length, offset, stream)
        {
            //// check passed parameters 
            if (Key.KeyLength == KeyLengths.TwoBytes)
            {
                TagValue = (UInt16)((UInt16)(Key[0] << 8) + Key[1]);
            }
            else if (Key.KeyLength == KeyLengths.OneByte)
            {
                TagValue = (UInt16)Key[0];
            }
            else
            {
                throw new ArgumentException($"The key for a local tag must be 1 or 2 bytes long, instead is: {Key.KeyLength}.");
            }

            // TODO 
            //if (Length.LengthEncoding != LengthEncodings.TwoBytes)
            //{
            //    throw new ArgumentException($"The length encoding for a local tag must be two bytes long, instead is: {Length.LengthEncoding}.");
            //}
        }


        public override string ToString()
        {
            long maxLocalTagLen = this.Parent?.Children?.OfType<MXFLocalTag>()?.Max(lt => lt.Length.Value) ?? 0;
            int lenDigitCount = Helper.GetDigitCount(maxLocalTagLen);
            string lenstring = this.Length.Value.ToString().PadLeft(lenDigitCount, '0');

            StringBuilder sb = new StringBuilder();

            sb.Append($"LocalTag {this.Key} [len {lenstring}] ");

            if (AliasUID is UL ul)
            {
                sb.Append($"-> {ul.Name} ");
            }

            if (this.Children.Any())
            {
                sb.Append($"[{this.Children.Count} items] ");
            }

            if (this.Value != null)
            {
                sb.Append($"= {this.Value}");
            }

            return sb.ToString();
        }
    }
}
