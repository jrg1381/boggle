﻿using System;
using System.Linq;
using NUnit.Framework;

namespace Boggle
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void WordTreeWithCommonPrefixEntries()
        {
            var wordTree = WordTree.InitializeFrom(new [] { "this", "that", "other" });
            CollectionAssert.AreEquivalent(new[] { 'i', 'a' },wordTree.ValidNextLettersForPrefix("th"));
            CollectionAssert.AreEquivalent(new[] { 'r' }, wordTree.ValidNextLettersForPrefix("othe"));
        }

        [Test]
        public void WordTreeWithMaximalLengthCommonPrefix()
        {
            var wordTree = WordTree.InitializeFrom(new[] { "mope","mop" });
            CollectionAssert.AreEquivalent(new[] { 'e', }, wordTree.ValidNextLettersForPrefix("mop"));
        }

        [Test]
        public void NeighboursOfTopLeftAreCorrect()
        {
            var boggleBoard = new BoggleBoard(4, new MockRandomCharacterGenerator());
            var neighbours = boggleBoard.NeighboursOf(new BoggleGridEntry(0,0));
            CollectionAssert.AreEquivalent(new[]
            {
                new BoggleGridEntry(0, 1, 'b'),
                new BoggleGridEntry(1, 0, 'e'),
                new BoggleGridEntry(1, 1, 'f')
            }, neighbours);
        }

        [Test]
        public void NeighboursOfBottomRightAreCorrect()
        {
            var boggleBoard = new BoggleBoard(4, new MockRandomCharacterGenerator());
            var neighbours = boggleBoard.NeighboursOf(new BoggleGridEntry(3,3));
            CollectionAssert.AreEquivalent(new[]
            {
                new BoggleGridEntry(2, 2, 'k'),
                new BoggleGridEntry(2, 3, 'l'),
                new BoggleGridEntry(3, 2, 'o')
            }, neighbours);
        }

        [Test]
        public void NeighboursOfACentralSquareAreCorrect()
        {
            var boggleBoard = new BoggleBoard(4, new MockRandomCharacterGenerator());
            var neighbours = boggleBoard.NeighboursOf(new BoggleGridEntry(2,2));
            CollectionAssert.AreEquivalent(new[]
            {
                 new BoggleGridEntry(1, 1, 'f'),
                 new BoggleGridEntry(1, 2, 'g'),
                 new BoggleGridEntry(1, 3, 'h'),
                 new BoggleGridEntry(2, 1, 'j'),
                 new BoggleGridEntry(2, 3, 'l'),
                 new BoggleGridEntry(3, 1, 'n'),
                 new BoggleGridEntry(3, 2, 'o'),
                 new BoggleGridEntry(3, 3, 'p'),
            }, neighbours);
        }

        [Test]
        public void InitializeWordTreeFromRealDictionary()
        {
            var wordTree = WordTree.InitializeFromDefaultWordList();
            CollectionAssert.AreEquivalent(new[] { 's' }, wordTree.ValidNextLettersForPrefix("thing"));
        }

        [Test]
        public void Solve()
        {
            var board = new BoggleBoard(12);
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

        [Test]
        public void NoBacktrackingOverUsedLettersMoreThanOneStepBack()
        {
            // xhak
            // bfnc
            // erqn
            // bnus
            //
            // backtracking would find the word 'unsure'. This is impossible because 'unsure' contains two 'u's
            // and the input data only contains one
            var board = new BoggleBoard(4, new CharactersFromStringGenerator("xhakbfncerqnbnus"));
            var boggleSolver = new BoggleBoardSolver(board);
            var solutions = boggleSolver.Solutions();
            CollectionAssert.DoesNotContain(solutions, "unsure", "Wrong words found in search");
        }

    }
}
