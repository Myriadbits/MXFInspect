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

using Myriadbits.MXF.Utils;
using System;
using System.ComponentModel;
using static Myriadbits.MXF.Exceptions.ExceptionExtensions;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MXFUnparseablePack : MXFPack
    {

        private const string CATEGORYNAME = "Unparseable MXFPack";
        private const int CATEGORYPOS = 2;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        public ExceptionInfo Exception { get; private set; }

        public MXFUnparseablePack(MXFPack pack, Exception ex) : base(pack)
        {
            Exception = new ExceptionInfo(ex, true, true);
        }

        public override string ToString()
        {
            return $"ERROR !!! {Key.SMPTEInformation?.Name ?? Key.Name} [len {this.Length.Value}]";
        }
    }
}
