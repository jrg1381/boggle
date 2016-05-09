namespace Boggle
{
    internal class MockRandomCharacterGenerator : CharactersFromStringGenerator
    {
        internal MockRandomCharacterGenerator()
        {
            Alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        }
    }
}