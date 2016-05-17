using System.Collections;
using System.Collections.Generic;
using Boggle.Interfaces;

namespace Boggle
{
    internal class SetWrapper : ISimpleSet<char>
    {
        private readonly Dictionary<char, WordTreeNode> m_SourceDictionary;

        internal SetWrapper(Dictionary<char, WordTreeNode> sourceDictionary)
        {
            m_SourceDictionary = sourceDictionary;
        }

        public bool Contains(char letter)
        {
            return m_SourceDictionary.ContainsKey(letter);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return m_SourceDictionary.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_SourceDictionary.Keys.GetEnumerator();
        }
    }
}