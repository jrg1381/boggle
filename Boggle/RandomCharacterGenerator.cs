using System;
using Boggle.Interfaces;

namespace Boggle
{
    internal class RandomCharacterGenerator : ICharacterGenerator
    {
        private const string c_LetterSource = "abcdefghijklmnopqrstuvwxyzaaeeiioouu";
        private readonly Random m_Random = new Random();

        public char Next()
        {
            return c_LetterSource[m_Random.Next(0, c_LetterSource.Length)];
        }
    }
}