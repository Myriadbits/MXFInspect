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

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class Node<T> : INode<T> where T : Node<T>
    {
        [Browsable(false)]
        public List<T> Children { get; set; } = new List<T>();

        [Browsable(false)]
        public T Parent { get; set; }

        [Browsable(false)]
        public INode<T> Root()
        {
            if (this.Parent != null)
                return this.Parent.Root();
            return this;
        }

        [Browsable(false)]
        public IEnumerable<T> Ancestors()
        {
            var parent = this.Parent;
            while (parent != null)
            {
                yield return parent;
                parent = parent.Parent;
            }
        }

        public IEnumerable<T> Descendants()
        {
            if (this.Children.Any())
            {
                var nodes = new Stack<T>(this.Children);
                while (nodes.Any())
                {
                    T node = nodes.Pop();
                    yield return node;
                    if (node.Children.Any())
                    {
                        foreach (var n in node.Children)
                        {
                            nodes.Push(n);
                        }
                    }

                }
            }
            else yield break;
        }

        public virtual void AddChild(T child)
        {
            child.Parent = (T)this;
            this.Children.Add(child);
        }
    }
}