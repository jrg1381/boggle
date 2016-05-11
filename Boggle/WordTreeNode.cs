using System.Collections;
using System.Collections.Generic;

namespace Boggle
{
    internal class WordTreeNode
    {
        class SetWrapper : ISimpleSet<char>
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

        private readonly Dictionary<char, WordTreeNode> m_ChildNodes;

        internal WordTreeNode()
        {
            m_ChildNodes = new Dictionary<char, WordTreeNode>();
            Children = new SetWrapper(m_ChildNodes);
        }

        internal ISimpleSet<char> Children { get; }

        internal bool HasChild(char letter)
        {
            return m_ChildNodes.ContainsKey(letter);
        }

        internal void AddLettersBelowNode(string word)
        {
            var currentNode = this;

            foreach (var letter in word)
            {
                currentNode = currentNode.AddLetterToChildren(letter);
            }

            currentNode.AddLetterToChildren('\0');
        }

        internal bool Winner { get; set; }

        private WordTreeNode AddLetterToChildren(char letter)
        {
            WordTreeNode existingNode;

            if(m_ChildNodes.TryGetValue(letter, out existingNode))
            {
                return existingNode;
            }

            var newNode = new WordTreeNode();
            m_ChildNodes[letter] = newNode;

            return newNode;
        }

        internal WordTreeNode GetChildNode(char letter)
        {
            return m_ChildNodes[letter];
        }
    }
}