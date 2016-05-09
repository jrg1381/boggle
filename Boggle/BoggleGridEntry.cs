namespace Boggle
{
    internal struct BoggleGridEntry
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal char Letter { get; set; }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Letter: {Letter}";
        }
    }
}