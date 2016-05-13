using System.Collections.Generic;
using System.Linq;

namespace Boggle
{
    public class BoggleBoardSolver
    {
        private Dictionary<string, List<BoggleGridEntry>> m_WordsFound;
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

        public Dictionary<string, List<BoggleGridEntry>> Solutions()
        {
            if (m_WordsFound != null)
                return m_WordsFound;

            m_WordsFound = new Dictionary<string, List<BoggleGridEntry>>();

            foreach (var entry in m_BoggleBoard.Entries())
            {
                Solve(entry, entry.Letter.ToString());
            }

            return m_WordsFound;
        }

        private void Solve(BoggleGridEntry gridEntry, 
            string wordSoFar, 
            OrderedSet<BoggleGridEntry> squaresVisited = null)
        {
            if (squaresVisited == null)
            {
                squaresVisited = new OrderedSet<BoggleGridEntry>();
                squaresVisited.Push(gridEntry);
            }

            var validNextLetters = m_WordTree.ValidNextLettersForPrefix(wordSoFar);

            var possiblePaths =
                m_BoggleBoard.NeighboursOf(gridEntry)
                    .Where(entry => validNextLetters.Contains(entry.Letter) && !squaresVisited.Contains(entry))
                    .ToArray();

            foreach (var nextStep in possiblePaths)
            {
                if (Equals(nextStep, BoggleGridEntry.EndOfWord))
                {
                    m_WordsFound[wordSoFar] = new List<BoggleGridEntry>();
                    m_WordsFound[wordSoFar] = squaresVisited.ToList();
                    m_WordsFound[wordSoFar].Reverse();
                }
                else
                {
                    squaresVisited.Push(nextStep);
                }

                Solve(nextStep, wordSoFar + nextStep.Letter, squaresVisited);

                if (Equals(nextStep, BoggleGridEntry.EndOfWord))
                    continue;

                squaresVisited.Pop();
            }
        }
    }
}