using Myriadbits.MXFInspect;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public static class ReadOnlyPropertyGridHelpers
    {

        public static IEnumerable<PropertyDescriptor> ConcatWithChildPropDescriptors(IEnumerable<PropertyDescriptor> propDescriptors)
        {
            var childs = GetChildPropDescriptors(propDescriptors).Except(propDescriptors);
            if (childs.Any())
            {
                return propDescriptors.Distinct().Concat(ConcatWithChildPropDescriptors(propDescriptors));
            }
            else return propDescriptors;
        }

        public static IEnumerable<PropertyDescriptor> GetChildPropDescriptors(IEnumerable<PropertyDescriptor> propDescriptors)
        {
            return propDescriptors.SelectMany(o => o.GetChildProperties()
                                                .Cast<PropertyDescriptor>())
                                .Where(prop => prop.IsBrowsable);
        }

        public static IEnumerable<PropertyDescriptor> GetChildPropDescriptor(PropertyDescriptor propDescriptor)
        {
            return propDescriptor.GetChildProperties().Cast<PropertyDescriptor>().Where(prop => prop.IsBrowsable);
        }

        public static IEnumerable<PropertyDescriptor> GetAllChilds(PropertyDescriptor propDescriptors)
        {
            var children = propDescriptors.GetChildProperties().Cast<PropertyDescriptor>().Where(prop => prop.IsBrowsable);

            if (children.Any())
            {
                var childStack = new Stack<PropertyDescriptor>(children);
                while (childStack.Any())
                {
                    PropertyDescriptor pi = childStack.Pop();
                    yield return pi;
                    if (GetAllChilds(pi).Any())
                    {
                        foreach (var n in GetAllChilds(pi))
                        {
                            childStack.Push(n);
                        }
                    }

                }
            }
            else yield break;

        }
        public static IEnumerable<PropertyDescriptor> GetAllChilds1(PropertyDescriptor pDescriptor, List<PropertyDescriptor> alreadyVisited)
        {
            var children = GetChildPropDescriptor(pDescriptor).Except(alreadyVisited);

            foreach (var item in children)
            {
                Debug.WriteLine(item.DisplayName);
            }

            if (children.Any())
            {
               var childs = new Stack<PropertyDescriptor>(children);
                while (childs.Any())
                {
                    PropertyDescriptor pi = childs.Pop();

                    yield return pi;
                    var cChilds = GetAllChilds1(pi, alreadyVisited);
                    if (cChilds.Any())
                    {
                        foreach (var n in cChilds)
                        {
                            childs.Push(n);
                        }
                    }
                    alreadyVisited.AddRange(children);
                }
            }
            else yield break;
        }

        //            if (this.Children.Any())
        //    {
        //    var nodes = new Stack<T>(this.Children);
        //    while (nodes.Any())
        //    {
        //        T node = nodes.Pop();
        //        yield return node;
        //        if (node.Children.Any())
        //        {
        //            foreach (var n in node.Children)
        //            {
        //                nodes.Push(n);
        //            }
        //        }

        //    }
        //}
        //    else yield break;
        //}
    }
}