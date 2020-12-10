using System;
using System.Collections.Generic;

namespace Myriadbits.MXF
{
    public interface INode<T>
    {
        List<T> Children { get; set; }
        T Parent { get; set; }

        INode<T> Root();

        IEnumerable<T> Descendants();

        void AddChild(T child);

    }
}