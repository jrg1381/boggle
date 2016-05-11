using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Boggle
{
    class EmptySet : ISimpleSet<char>
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

    internal class WordTree
    {
        private const string c_DefaultWordListFile = @"C:\Users\james.gilmore\documents\visual studio 2015\Projects\Boggle\Boggle\linuxwords.txt";
        private readonly WordTreeNode m_RootNode;
        private static readonly ISimpleSet<char> s_EmptySet = new EmptySet();

        private WordTree()
        {
            m_RootNode = new WordTreeNode();
        }

        internal static WordTree InitializeFrom(IEnumerable<string> words)
        {
            var newTree = new WordTree();
            newTree.Initialize(words);
            return newTree;
        }

        internal static WordTree InitializeFromDefaultWordList()
        {
            return InitializeFrom(File.ReadLines(c_DefaultWordListFile));
        }

        private void Initialize(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                m_RootNode.AddLettersBelowNode(word);
            }
        }

        internal ISimpleSet<char> ValidNextLettersForPrefix(string prefix)
        {
            var currentNode = m_RootNode;

            foreach (var letter in prefix)
            {
                if (!currentNode.HasChild(letter))
                {
                    return s_EmptySet;
                }

                currentNode = currentNode.GetChildNode(letter);
            }

            return currentNode.Children;
        }
    }
}