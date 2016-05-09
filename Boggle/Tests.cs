using System;
using System.Linq;
using NUnit.Framework;

namespace Boggle
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ConstructAndInitializeWordTree()
        {
            var wordTree = WordTree.InitializeFrom(new [] { "this", "that", "other" });
            CollectionAssert.AreEquivalent(new[] { 'i', 'a' },wordTree.ValidNextLettersForPrefix("th"));
            CollectionAssert.AreEquivalent(new[] { 'r' }, wordTree.ValidNextLettersForPrefix("othe"));
        }

        [Test]
        public void ConstructAndInitializeWordTree2()
        {
            var wordTree = WordTree.InitializeFrom(new[] { "mope","mop" });
            CollectionAssert.AreEquivalent(new[] { 'e', }, wordTree.ValidNextLettersForPrefix("mop"));
        }

        [Test]
        public void ConstructBoard()
        {
            var boggleBoard = new BoggleBoard(4, new MockRandomCharacterGenerator());
            var neighbours = boggleBoard.NeighboursOf(new BoggleGridEntry { X = 0, Y = 0});
            CollectionAssert.AreEquivalent(new[] {
                new BoggleGridEntry {X = 0, Y = 1, Letter = 'b'},
                new BoggleGridEntry {X = 1, Y = 0, Letter = 'e'},
                new BoggleGridEntry {X = 1, Y = 1, Letter = 'f'}
 }, neighbours);
        }

        [Test]
        public void ConstructBoard2()
        {
            var boggleBoard = new BoggleBoard(4, new MockRandomCharacterGenerator());
            var neighbours = boggleBoard.NeighboursOf(new BoggleGridEntry { X = 3, Y = 3 });
            CollectionAssert.AreEquivalent(new[] {
                new BoggleGridEntry {X = 2, Y = 2, Letter = 'k'},
                new BoggleGridEntry {X = 2, Y = 3, Letter = 'l'},
                new BoggleGridEntry {X = 3, Y = 2, Letter = 'o'}
 }, neighbours);
        }

        [Test]
        public void ConstructBoard3()
        {
            var boggleBoard = new BoggleBoard(4, new MockRandomCharacterGenerator());
            var neighbours = boggleBoard.NeighboursOf(new BoggleGridEntry { X = 2, Y = 2 });
            CollectionAssert.AreEquivalent(new[]
            {
                new BoggleGridEntry {X = 1, Y = 1, Letter = 'f'},
                new BoggleGridEntry {X = 1, Y = 2, Letter = 'g'},
                new BoggleGridEntry {X = 1, Y = 3, Letter = 'h'},
                new BoggleGridEntry {X = 2, Y = 1, Letter = 'j'},
                new BoggleGridEntry {X = 2, Y = 3, Letter = 'l'},
                new BoggleGridEntry {X = 3, Y = 1, Letter = 'n'},
                new BoggleGridEntry {X = 3, Y = 2, Letter = 'o'},
                new BoggleGridEntry {X = 3, Y = 3, Letter = 'p'},
            }, neighbours);
        }

        [Test]
        public void RealDictionary()
        {
            var wordTree = WordTree.InitializeFromDefaultWordList();
            CollectionAssert.AreEquivalent(new[] { 's' }, wordTree.ValidNextLettersForPrefix("thing"));
        }

        [Test]
        public void Solve()
        {
            var board = new BoggleBoard(50);
            var boggleSolver = new BoggleBoardSolver(board);
            var solutions = boggleSolver.Solutions().OrderByDescending(x => x.Length);
            Console.WriteLine(board.ToString());
            Console.WriteLine(String.Join(Environment.NewLine, solutions));
        }

        [Test]
        public void NoBacktrackingOverUsedLetters()
        {
            // backtracking would find the word 'tot'
            var board = new BoggleBoard(2, new CharactersFromStringGenerator("tozz"));
            var boggleSolver = new BoggleBoardSolver(board);
            var solutions = boggleSolver.Solutions();
            CollectionAssert.AreEquivalent(new[] { "to"}, solutions, "Wrong words found in search");
        }

        // xhak
        // bfnc
        // erqn
        // bnus

        [Test]
        public void NoBacktrackingOverUsedLettersLongerExample()
        {
            // backtracking would find the word 'unsure'. This is impossible because 'unsure' contains two 'u's
            // and the input data only contains one
            var board = new BoggleBoard(4, new CharactersFromStringGenerator("xhakbfncerqnbnus"));
            var boggleSolver = new BoggleBoardSolver(board);
            var solutions = boggleSolver.Solutions();
            CollectionAssert.DoesNotContain(solutions, "unsure", "Wrong words found in search");
        }

    }
}
