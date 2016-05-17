using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Boggle.Interfaces;

namespace Boggle
{
    internal class WordTree
    {
        private const string c_DefaultWordListFile = @"linuxwords.txt";
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
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var defaultWordList = Path.Combine(directoryName, c_DefaultWordListFile);
            return InitializeFrom(File.ReadLines(defaultWordList));
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