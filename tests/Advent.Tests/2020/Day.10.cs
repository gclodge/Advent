using System.Linq;
using System.Collections.Generic;

using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day10 : IDailyTest
    {
        public int Number => 10;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        static readonly List<int> KnownAdapters = new()
        {
            16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4
        };

        [Fact]
        public void Test_KnownAdapters()
        {
            var arr = new JoltageArray(KnownAdapters);
            var diffs = arr.GetAdapterArrayDifferences();

            Assert.True(diffs.Count == 2);
            Assert.True(diffs[1] == 7);
            Assert.True(diffs[3] == 5);
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse(int.Parse).ToList();
            var arr = new JoltageArray(input);

            var diffs = arr.GetAdapterArrayDifferences();
            int res = diffs[1] * diffs[3];

            Assert.True(res == 2170);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse(int.Parse).ToList();
            var arr = new JoltageArray(input);

            long combos = arr.CountPossibleCombinations();
            Assert.True(combos == 24803586664192);
        }
    }
}
