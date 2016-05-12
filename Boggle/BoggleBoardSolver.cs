using System.Collections.Generic;
using System.Linq;

namespace Boggle
{
    public class BoggleBoardSolver
    {
        private List<string> m_WordsFound;
        private readonly WordTree m_WordTree;
        private readonly BoggleBoard m_BoggleBoard;

        public BoggleBoardSolver(IList<char> letters) : this(new BoggleBoard(letters))
        {
        }

        internal BoggleBoardSolver(BoggleBoard boggleBoard = null, WordTree wordTree = null)
        {
            m_WordTree = wordTree ??
                         WordTree.InitializeFromDefaultWordList();

            m_BoggleBoard = boggleBoard ?? new BoggleBoard();
        }

        public IEnumerable<string> Solutions()
        {
            if (m_WordsFound != null)
                return m_WordsFound;

            m_WordsFound = new List<string>();

            foreach (var entry in m_BoggleBoard.Entries())
            {
                Solve(entry, entry.Letter.ToString());
            }

            return m_WordsFound;
        }

        private void Solve(BoggleGridEntry gridEntry, string wordSoFar, HashSet<BoggleGridEntry> squaresVisited = null)
        {
            if(squaresVisited == null)
                squaresVisited = new HashSet<BoggleGridEntry>();

            var validNextLetters = m_WordTree.ValidNextLettersForPrefix(wordSoFar);

            var possiblePaths =
                m_BoggleBoard.NeighboursOf(gridEntry)
                    .Where(entry => validNextLetters.Contains(entry.Letter) && !squaresVisited.Contains(entry))
                    .ToArray();

            foreach (var nextStep in possiblePaths)
            {
                if (Equals(nextStep, BoggleGridEntry.EndOfWord))
                {
                    m_WordsFound.Add(wordSoFar);
                }
                else
                {
                    squaresVisited.Add(gridEntry);
                }

                Solve(nextStep, wordSoFar + nextStep.Letter, squaresVisited);
            }

            foreach (var visitedSquare in possiblePaths)
            {
                squaresVisited.Remove(visitedSquare);
            }
        }
    }
}