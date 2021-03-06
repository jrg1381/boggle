using System.Collections.Generic;
using Boggle.Interfaces;

namespace Boggle
{
    internal class WordTreeNode
    {
        internal const char EndOfWord = '\0';
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

            currentNode.AddLetterToChildren(EndOfWord);
        }

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