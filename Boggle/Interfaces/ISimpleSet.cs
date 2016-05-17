using System.Collections.Generic;

namespace Boggle.Interfaces
{
    internal interface ISimpleSet<T> : IEnumerable<T>
    {
        bool Contains(T item);
    }
}