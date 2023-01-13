﻿#region license
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

using System.Linq;

namespace Myriadbits.MXF.KLV
{
    public class KLV
    {
        public KLVKey Key { get; private set; }

        public KLVLength Length { get; private set; }

        public KLVValue Value { get; private set; }



        public KLV(KLVKey key, KLVLength length, KLVValue value)
        {
            Key = key;
            Length = length;
        }

        //public KLV(KLVKey key, KLVLength length)
        //{
        //    b = new KLVKey(0x00);

        //    b[1] = 0x00;

        //    foreach (var s in b)
        //    {
        //        s = 5;
        //    }
        //}

        public void test()
        {
            b = new KLVKey(0x00);
        }
    }

}
