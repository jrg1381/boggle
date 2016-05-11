using System;
using System.Collections.Generic;
using System.Text;

namespace Boggle
{
    internal class BoggleBoard
    {
        private readonly int m_BoardSize;
        private readonly ICharacterGenerator m_Generator;
        private readonly char[,] m_Board;

        internal IEnumerable<BoggleGridEntry> NeighboursOf(BoggleGridEntry gridEntry)
        {
            var x = gridEntry.X;
            var y = gridEntry.Y;
            
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if (SquareIsOutOfRange(dx, dy, x, y)) continue;
                    yield return new BoggleGridEntry(x + dx, y + dy, GetEntry(x + dx, y + dy));
                }
            }
        }

        private bool SquareIsOutOfRange(int dx, int dy, int x, int y)
        {
            if (dx == 0 && dy == 0)
                return true;
            if (x + dx >= m_BoardSize || y + dy >= m_BoardSize || x + dx < 0 || y + dy < 0)
                return true;

            return false;
        }

        internal IEnumerable<BoggleGridEntry> Entries()
        {
            for (var x = 0; x < m_BoardSize; x++)
            {
                for (var y = 0; y < m_BoardSize; y++)
                {
                    var letterAtPosition = GetEntry(x, y);
                    yield return new BoggleGridEntry(x, y, letterAtPosition);
                }
            }
        }

        private char GetEntry(int x, int y)
        {
            return m_Board[x, y];
        }

        internal BoggleBoard(int size = 4, ICharacterGenerator generator = null)
        {
            m_BoardSize = size;
            m_Generator = generator ?? new RandomCharacterGenerator();
            m_Board = new char[m_BoardSize, m_BoardSize];

            PopulateBoard();
        }

        public BoggleBoard(IList<char> letters) : this(IntegerSquareRoot(letters.Count), new CharGenerator(letters) )
        {
        }

        private static int IntegerSquareRoot(int number)
        {
            var answer = Math.Sqrt(number);
            if (!IsCloseEnoughToInteger(answer))
                throw new ArgumentOutOfRangeException(nameof(number), "Not a square number of letters");

            return (int) answer;
        }

        private static bool IsCloseEnoughToInteger(double answer)
        {
            return Math.Abs(Math.Floor(answer) - Math.Ceiling(answer)) <= double.Epsilon;
        }

        class CharGenerator : ICharacterGenerator
        {
            private readonly IList<char> m_Data;
            private int m_Position = 0;

            internal CharGenerator(IList<char> data)
            {
                m_Data = data;
            }

            public char Next()
            {
                if (m_Position >= m_Data.Count)
                    throw new InvalidOperationException();

                return m_Data[m_Position++];
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < m_BoardSize; i++)
            {
                for (var j = 0; j < m_BoardSize; j++)
                {
                    builder.Append(m_Board[i, j]);
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private void PopulateBoard()
        {
            for (var i = 0; i < m_BoardSize; i++)
            {
                for (var j = 0; j < m_BoardSize; j++)
                {
                    m_Board[i, j] = m_Generator.Next();
                }
            }
        }
    }
}