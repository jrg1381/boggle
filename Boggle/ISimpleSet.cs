using System.Collections.Generic;

namespace Boggle
{
    internal interface ISimpleSet<T> : IEnumerable<T>
    {
        bool Contains(T letter);
    }
}