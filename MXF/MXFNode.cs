using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class MXFNode<T> : INode<T> where T : MXFNode<T>
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