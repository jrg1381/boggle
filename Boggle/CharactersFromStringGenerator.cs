using Boggle.Interfaces;

namespace Boggle
{
    internal class CharactersFromStringGenerator : ICharacterGenerator
    {
        protected char[] Alphabet;
        private int m_Pos;

        protected CharactersFromStringGenerator()
        { }

        internal CharactersFromStringGenerator(string boardAsString)
        {
            Alphabet = boardAsString.ToCharArray();
        }

        public char Next()
        {
            var answer = Alphabet[m_Pos];
            m_Pos = (m_Pos + 1) % Alphabet.Length;
            return answer;
        }
    }
}