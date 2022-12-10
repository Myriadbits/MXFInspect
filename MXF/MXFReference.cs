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
using System.Diagnostics;
using Myriadbits.MXF.KLV;

namespace Myriadbits.MXF
{
    public class MXFReference<T> : MXFObject, IReference<T> where T : MXFObject
    {
        private const string CATEGORYNAME = "Reference";

        [Category(CATEGORYNAME)]
        public string Name { get; set; }
        [Category(CATEGORYNAME)]
        [Browsable(false)]
        public T Reference { get; set; }
        [Category(CATEGORYNAME)]
        public UUID Identifier { get; set; }

        public MXFReference(IKLVStreamReader reader, long offset, string name) : base(offset)
        {
            Name = name;
            Identifier = reader.ReadUUID();
            TotalLength = (long)Identifier.KeyLength;
        }

        public override string ToString()
        {
            return string.Format("{0} -> [{1}]", this.Name, this.Identifier);
        }

        public bool ResolveReference(IUUIDIdentifiable obj)
        {
            // TODO: save the resolve status (=i.e. the return value of this function) into a property,
            // so that it can be checked by a validator later on (if there are unresolved ones.
            if (Identifier.Equals(obj.GetUUID()))
            {
                if (obj is T)
                {
                    Reference = (T)obj;
                    Debug.WriteLine(string.Format("Reference resolved: {0} -> {1}", this.ToString(), Reference.ToString()));
                    return true;
                }
                Debug.WriteLine(string.Format("Reference not resolveable as types don't match: {0} -> {1}", this.GetType(), obj.GetType()));
            }
            return false;
        }

        public MXFObject GetReference()
        {
            return Reference;
        }

    }
}
