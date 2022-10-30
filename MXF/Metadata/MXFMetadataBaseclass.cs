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
using System.ComponentModel;

namespace Myriadbits.MXF
{
    /// <summary>
    /// Meta data class (might) consists of multiple local tags
    /// </summary>
    public class MXFMetadataBaseclass : MXFLocalSet
    {
        private const string CATEGORYNAME = "Metadata";

        [Category(CATEGORYNAME)]
        [Description("Type of metadata node")]
        [Browsable(false)]
        public string MetaDataName { get; protected set; }

        public MXFMetadataBaseclass(MXFReader reader, MXFPack pack, string metaDataName)
            : base(reader, pack)
        {
            MetaDataName = metaDataName;
        }

        public override string ToString()
        {
            return string.Format("{0} [len {1}]", this.MetaDataName, this.Length.Value);
        }
    }
}
