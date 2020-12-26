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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF
{
    public class MXFReference<T> : MXFObject, IReference<T> where T: MXFObject
    {
        [CategoryAttribute("Reference")]
        public string Name { get; set; }
        [CategoryAttribute("Reference")]
        public T Reference { get; set; }
        [CategoryAttribute("Reference")]
        public MXFUUID Identifier { get; set; }

        public MXFReference(MXFReader reader, string name) : base(reader.Position)
        {
            Name = name;
            Identifier = new MXFUUID(reader);
            Length = Identifier.Length;
        }

        public override string ToString()
        {
            return string.Format("{0} -> [{1}]", this.Name, this.Identifier);
        }

        public bool ResolveReference(IUUIDIdentifiable obj)
        {
            if (Identifier.Equals(obj.GetUUID()))
            {
                if(obj is T)
                {
                    Reference = (T)obj;
                    Debug.WriteLine(string.Format("Reference resolved: {0} -> {1}", this.ToString(), Reference.ToString()));
                    return true;
                }
                Debug.WriteLine(string.Format("Reference not resolveable as types don't match: {0} -> {1}", this.GetType(), Reference.GetType()));
            }
            return false;
        }

        public MXFObject GetReference()
        {
            return Reference;
        }

    }
}
