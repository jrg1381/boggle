using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boggle.Interfaces;

namespace Boggle
{
    internal class EmptySet : ISimpleSet<char>
    {
        public bool Contains(char letter)
        {
            return false;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return Enumerable.Empty<char>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<char>().GetEnumerator();
        }
    }
}