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

        //public static IEnumerable<PropertyDescriptor> ConcatWithChildPropDescriptors(IEnumerable<PropertyDescriptor> propDescriptors)
        //{
        //    var childs = GetChildPropDescriptors(propDescriptors).Except(propDescriptors);
        //    if (childs.Any())
        //    {
        //        return propDescriptors.Distinct().Concat(ConcatWithChildPropDescriptors(propDescriptors));
        //    }
        //    else return propDescriptors;
        //}

        //public static IEnumerable<PropertyDescriptor> GetChildPropDescriptors(IEnumerable<PropertyDescriptor> propDescriptors)
        //{
        //    return propDescriptors.SelectMany(o => o.GetChildProperties()
        //                                        .Cast<PropertyDescriptor>())
        //                        .Where(prop => prop.IsBrowsable);
        //}

        //public static IEnumerable<PropertyDescriptor> GetAllChilds(PropertyDescriptor propDescriptors)
        //{
        //    var children = propDescriptors.GetChildProperties().Cast<PropertyDescriptor>().Where(prop => prop.IsBrowsable);

        //    if (children.Any())
        //    {
        //        var childStack = new Stack<PropertyDescriptor>(children);
        //        while (childStack.Any())
        //        {
        //            PropertyDescriptor pi = childStack.Pop();
        //            yield return pi;
        //            if (GetAllChilds(pi).Any())
        //            {
        //                foreach (var n in GetAllChilds(pi))
        //                {
        //                    childStack.Push(n);
        //                }
        //            }

        //        }
        //    }
        //    else yield break;

        //}

        public static IEnumerable<PropertyDescriptor> GetChildPropDescriptor(PropertyDescriptor propDescriptor)
        {
            return propDescriptor.GetChildProperties().Cast<PropertyDescriptor>().Where(prop => prop.IsBrowsable);
        }

        public static IEnumerable<PropertyDescriptor> GetAllChildsProperties(PropertyDescriptor pDescriptor, List<PropertyDescriptor> alreadyVisited)
        {
            var children = GetChildPropDescriptor(pDescriptor).Except(alreadyVisited);

            if (children.Any())
            {

                //foreach (var item in children)
                //{
                //    Debug.WriteLine($"Children: {item.DisplayName}");
                //}

                var childStack = new Stack<PropertyDescriptor>(children);
                while (childStack.Any())
                {
                    PropertyDescriptor pi = childStack.Pop();

                    yield return pi;

                    var cChilds = GetAllChildsProperties(pi, alreadyVisited);
                    if (cChilds.Any())
                    {
                        foreach (var n in cChilds)
                        {
                            if (!alreadyVisited.Contains(n))
                                { childStack.Push(n); }
                        }
                        alreadyVisited.AddRange(children);
                    }
                }
            }
            else yield break;
        }
    }
}