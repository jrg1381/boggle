namespace Boggle
{
    public struct BoggleGridEntry
    {
        internal static BoggleGridEntry EndOfWord => new BoggleGridEntry(0,0);
        internal int X { get; }
        internal int Y { get; }
        internal char Letter { get; }

        internal BoggleGridEntry(int x, int y, char letter = WordTreeNode.EndOfWord)
        {
            X = x;
            Y = y;
            Letter = letter;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Letter: {Letter}";
        }
    }
}