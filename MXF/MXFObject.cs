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

using Myriadbits.MXF.KLV;
using Myriadbits.MXF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Myriadbits.MXF
{
    // TODO remove log type
    public enum MXFLogType
    {
        Info,
        Warning,
        Error
    };

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class MXFObject : Node<MXFObject> //ILazyLoadable
    {
        private const string CATEGORYNAME = "Object";
        private const int CATEGORYPOS = 0;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Offset from the beginning of file in terms of bytes")]
        [TypeConverter(typeof(FormattedNumberTypeConverter))]
        public virtual long Offset { get; protected set; } = 0;

        [SortedCategory(CATEGORYNAME, CATEGORYPOS)]
        [Description("Total length of object in term of bytes. If KLV then sum of Key length + Length length + Value length")]
        public virtual long TotalLength { get; protected set; } = 0;

        [Browsable(false)]
        // TODO find better name
        public MXFLogicalObject LogicalWrapper { get; private set; }

        /// <summary>
        ///Default constructor needed for derived classes such as MXFFile, ...
        /// </summary>
        protected MXFObject()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"></param>
        protected MXFObject(IKLVStreamReader reader)
        {
            this.Offset = reader.Position;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"></param>
        protected MXFObject(long offset)
        {
            this.Offset = offset;
        }

        public MXFObject FindNextObjectOfType(Type typeToFind)
        {
            var flatList = this.Root()
                                .Descendants()
                                .Where(o => o.GetType() == typeToFind && o.Offset > this.Offset)
                                .OrderBy(o => o.Offset)
                                .ToList();

            return flatList.FirstOrDefault();
        }

        public MXFObject FindPreviousObjectOfType(Type typeToFind)
        {
            var flatList = this.Root()
                                .Descendants()
                                .Where(o => o.GetType() == typeToFind && o.Offset < this.Offset)
                                .OrderByDescending(o => o.Offset)
                                .ToList();

            return flatList.FirstOrDefault();
        }

        /// <summary>
        /// Some output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Children.Any())
                return this.Offset.ToString();
            return string.Format("{0} [{1} items]", this.Offset, this.Children.Count);
        }

        // TODO find better name, maybe Wrap
        public MXFLogicalObject CreateLogicalObject()
        {
            var wrapper = new MXFLogicalObject(this);
            this.LogicalWrapper = wrapper;
            return wrapper;
        }
    }
}
