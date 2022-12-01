using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

namespace Advent.Tests._2020
{
    public class Day01 : IDailyTest
    {
        public int Number => 1;
        public int Year => 2020;

        public string Input => TestHelper.GetInputFile(this);

        private static readonly List<int> TestInputs = new()
        {
            1721,
            979,
            366,
            299,
            675,
            1456
        };

        private const int _SumValue = 2020;

        [Fact]
        public void Test_KnownInputs()
        {
            //< Get the value (A * B) of the two values that sum to 2020
            var val = Functions.FindRecordsThatSumTo(TestInputs, _SumValue, 2);
            //< Ensure we match the known result
            Assert.True(val == 514579);
        }

        [Fact]
        public void PartOne()
        {
            const int numToSum = 2;

            //< Parse all input data - convert to ints
            var input = Input.Parse(int.Parse);
            //< Get the value (A * B) of the two values that sum to 2020
            var val = Functions.FindRecordsThatSumTo(input, _SumValue, numToSum);
            //< Ensure we match the known result
            Assert.True(val == 1019571);
        }

        [Fact]
        public void PartTwo()
        {
            const int numToSum = 3;

            //< Parse all input data - convert to ints
            var input = Input.Parse(int.Parse);
            //< Try to find the local permutation (n = 3) that sums to 2020 - return the multiple of those values
            var val = Functions.FindRecordsThatSumTo(input, _SumValue, numToSum);

            //< Check we match that known result, boah
            Assert.True(val == 100655544);
        }
    }
}
