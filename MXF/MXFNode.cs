using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Myriadbits.MXF
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class MXFNode
    {
        [Browsable(false)]
        public List<MXFObject> Children { get; set; } = new List<MXFObject>();

        [Browsable(false)]
        public MXFObject Parent { get; set; }

        [Browsable(false)]
        public MXFNode Root()
        {
            if (this.Parent != null)
                return this.Parent.Root();
            return this;
        }

        public IEnumerable<MXFObject> Descendants()
        {
            if (this.Children.Any())
            {
                var nodes = new Stack<MXFObject>(this.Children);
                while (nodes.Any())
                {
                    MXFObject node = nodes.Pop();
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

        /// <summary>
        /// Find the first child of a specific type
        /// </summary>
        /// <param name="currentObject"></param>
        /// <returns></returns>
        public MXFObject FindChild(Type typeToFind, bool skipFillers)
        {
            if (this.Children != null)
            {
                MXFObject found = null;
                foreach (MXFObject child in this.Children)
                {
                    if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
                        return child;
                    if (child.Children.Any())
                    {
                        found = child.FindChild(typeToFind, skipFillers);
                        if (found != null)
                            return found;
                    }
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// Find the first child of a specific type
        /// </summary>
        /// <param name="currentObject"></param>
        /// <returns></returns>
        public MXFObject FindChildReverse(Type typeToFind, bool skipFillers)
        {
            if (this.Children != null)
            {
                MXFObject found = null;
                for (int n = this.Children.Count - 1; n >= 0; n--)
                {
                    MXFObject child = this.Children[n];
                    if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
                        return child;
                    if (child.Children.Any())
                    {
                        found = child.FindChildReverse(typeToFind, skipFillers);
                        if (found != null)
                            return found;
                    }
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// Find the next object of a specific type
        /// </summary>
        /// <param name="currentObject"></param>
        /// <returns></returns>
        public MXFObject FindNextSibling(Type typeToFind, bool skipFillers)
        {
            MXFObject found = null;
            if (this.Parent != null && this.Parent.Children.Any())
            {
                int index = this.Parent.Children.FindIndex(a => a == this);
                if (index >= 0 && index < this.Parent.Children.Count - 1)
                {
                    for (int n = index + 1; n < this.Parent.Children.Count; n++)
                    {
                        MXFObject child = this.Parent.Children[n];
                        if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
                        {
                            // Yes found next sibling of the same type
                            return this.Parent.Children[n];
                        }

                        // Not the correct type, try its children
                        found = this.Parent.Children[n].FindChild(typeToFind, skipFillers);
                        if (found != null)
                            return found;
                    }
                }

                // Hmm still not found, try our grand-parent:
                found = this.Parent.FindNextSibling(typeToFind, skipFillers);
            }
            return found;
        }

        /// <summary>
        /// Find the next object of a specific type
        /// </summary>
        /// <param name="currentObject"></param>
        /// <returns></returns>
        public MXFObject FindPreviousSibling(Type typeToFind, bool skipFillers)
        {
            MXFObject found = null;
            if (this.Parent != null && this.Parent.Children.Any())
            {
                int index = this.Parent.Children.FindIndex(a => a == this);
                if (index > 0)
                {
                    for (int n = index - 1; n >= 0; n--)
                    {
                        MXFObject child = this.Parent.Children[n];
                        if (child.GetType() == typeToFind && child.IsVisible(skipFillers))
                        {
                            // Yes found next sibling of the same type
                            return this.Parent.Children[n];
                        }

                        // Not the correct type, try its children
                        found = this.Parent.Children[n].FindChildReverse(typeToFind, skipFillers);
                        if (found != null)
                            return found;
                    }
                }

                // Hmm still not found, try our grand-parent:
                found = this.Parent.FindPreviousSibling(typeToFind, skipFillers);
            }
            return found;
        }

        /// <summary>
        /// Returns a specific child
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public MXFObject GetChild(int index)
        {
            if (this.Children.Any())
                return null;
            if (index >= 0 && index < this.Children.Count)
                return this.Children[index];
            return null;
        }
    }
}