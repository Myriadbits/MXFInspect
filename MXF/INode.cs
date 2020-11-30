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

        T GetChild(int index);

        void AddChild(T child);

    }
}