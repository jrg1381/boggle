using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boggle
{
    internal class OrderedSet<T> : ISimpleSet<T>
    {
        private readonly HashSet<T> m_Set;
        private readonly Stack<T> m_Stack;

        internal List<T> ToList()
        {
            return m_Stack.ToList();
        }

        internal OrderedSet()
        {
            m_Set = new HashSet<T>();
            m_Stack = new Stack<T>();
        }

        internal T Pop()
        {
            var item =  m_Stack.Pop();
            m_Set.Remove(item);
            return item;
        }

        internal void Push(T item)
        {
            m_Set.Add(item);
            m_Stack.Push(item);
        }

        public bool Contains(T item)
        {
            return m_Set.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_Set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) m_Set).GetEnumerator();
        }
    }
}