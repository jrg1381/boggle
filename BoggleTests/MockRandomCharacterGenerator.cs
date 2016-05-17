using Boggle.Interfaces;

namespace Boggle
{
    internal class MockRandomCharacterGenerator : ICharacterGenerator
    {
        protected char[] Alphabet;
        private int m_Pos;

        internal MockRandomCharacterGenerator()
        {
            Alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        }

        public char Next()
        {
            var answer = Alphabet[m_Pos];
            m_Pos = (m_Pos + 1) % Alphabet.Length;
            return answer;
        }
    }
}