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

namespace Myriadbits.MXF
{
	public class MXFSystemMetaDataSet : MXFLocalSet
	{
		public MXFSystemMetaDataSet(MXFPack pack)
			: base(pack)
        {
            this.Key.Name ??= "System Metadata Set (CP)";
			if (this.Key[12] == 0x14)
				this.Key.Name = "System Metadata Set (GC)";

            // TODO rename and parse local tags according to 
            // GC System Scheme Individual Data Definitions
        }

        public override string ToString()
        {
            return $"{this.Key.Name} [len {this.Length.Value}]";
        }
    }
}
