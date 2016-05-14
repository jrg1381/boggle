using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boggle
{
    internal class OrderedSetOfLetters
    {
        private readonly HashSet<BoggleGridEntry> m_Set;
        private readonly Stack<BoggleGridEntry> m_Stack;

        internal List<BoggleGridEntry> ToList()
        {
            return m_Stack.Reverse().ToList();
        }

        /// <summary>
        /// Return the underlying word constructed from the ordered set
        /// </summary>
        /// <returns></returns>
        internal string Word()
        {
            var stringBuilder = new StringBuilder();
            foreach (var entry in m_Stack.Reverse())
                stringBuilder.Append(entry.Letter);
            return stringBuilder.ToString();
        }

        internal OrderedSetOfLetters()
        {
            m_Set = new HashSet<BoggleGridEntry>();
            m_Stack = new Stack<BoggleGridEntry>();
        }

        internal void Pop()
        {
            var item =  m_Stack.Pop();
            m_Set.Remove(item);
        }

        internal void Push(BoggleGridEntry item)
        {
            m_Set.Add(item);
            m_Stack.Push(item);
        }

        internal bool Contains(BoggleGridEntry item)
        {
            return m_Set.Contains(item);
        }
    }
}