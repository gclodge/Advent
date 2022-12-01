using System;
using System.Collections.Generic;

using Advent._2020;

using Xunit;

namespace Advent.Tests._2020
{
    public class Day15 : IDailyTest
    {
        public int Number => 15;
        public int Year => 2020;

        public static readonly int[] Input = new int[] { 1, 20, 11, 6, 12, 0 };

        #region Test Cases
        public static readonly List<Tuple<int[], int>> KnownResults_2020 = new List<Tuple<int[], int>>()
        {
            Tuple.Create(new int[] { 1, 3, 2 }, 1),
            Tuple.Create(new int[] { 2, 1, 3 }, 10),
            Tuple.Create(new int[] { 1, 2, 3 }, 27),
            Tuple.Create(new int[] { 2, 3, 1 }, 78),
            Tuple.Create(new int[] { 3, 2, 1 }, 438),
            Tuple.Create(new int[] { 3, 1, 2 }, 1836),
        };

        public static readonly List<Tuple<int[], int>> KnownResults_30mil = new List<Tuple<int[], int>>()
        {
            Tuple.Create(new int[] { 0, 3, 6 }, 175594),
            Tuple.Create(new int[] { 1, 3, 2 }, 2578),
            Tuple.Create(new int[] { 2, 1, 3 }, 3544142),
            Tuple.Create(new int[] { 1, 2, 3 }, 261214),
            Tuple.Create(new int[] { 2, 3, 1 }, 6895259),
            Tuple.Create(new int[] { 3, 2, 1 }, 18),
            Tuple.Create(new int[] { 3, 1, 2 }, 362),
        };
        #endregion

        [Fact]
        public void Test_KnownResults_2020()
        {
            int target = 2020;
            foreach (var tup in KnownResults_2020)
            {
                var word = MemoryGame.FindWordAtTurn(tup.Item1, target);
                Assert.True(word == tup.Item2);
            }
        }

        [Fact]
        public void PartOne()
        {
            int target = 2020;
            int word = MemoryGame.FindWordAtTurn(Input, target);

            Assert.True(word == 1085);
        }

        [Fact]
        public void PartTwo()
        {
            int target = 30000000;
            int word = MemoryGame.FindWordAtTurn(Input, target);
            //< ~2s run time, not ideal but functional for the day
            Assert.True(word == 10652);
        }
    }
}
