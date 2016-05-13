using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boggle
{
    internal class OrderedSet
    {
        private readonly HashSet<BoggleGridEntry> m_Set;
        private readonly Stack<BoggleGridEntry> m_Stack;

        internal List<BoggleGridEntry> ToList()
        {
            return m_Stack.ToList();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var entry in m_Stack.Reverse())
                stringBuilder.Append(entry.Letter);
            return stringBuilder.ToString();
        }

        internal OrderedSet()
        {
            m_Set = new HashSet<BoggleGridEntry>();
            m_Stack = new Stack<BoggleGridEntry>();
        }

        internal BoggleGridEntry Pop()
        {
            var item =  m_Stack.Pop();
            m_Set.Remove(item);
            return item;
        }

        internal void Push(BoggleGridEntry item)
        {
            m_Set.Add(item);
            m_Stack.Push(item);
        }

        public bool Contains(BoggleGridEntry item)
        {
            return m_Set.Contains(item);
        }
    }
}