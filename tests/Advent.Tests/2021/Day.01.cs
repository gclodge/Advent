using Advent.Domain;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2021;

namespace Advent.Tests._2021
{
    public class Day01 : IDailyTest
    {
        public int Number => 1;
        public int Year => 2021;

        public string Input => TestHelper.GetInputFile(this);

        [Fact]
        public void Test_KnownSweeps()
        {
            var values = new List<int>()
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };

            var sweeps = new SonarSweep(values);

            int expected = 7;
            int actual = sweeps.CountIncreasing();

            Assert.Equal(expected, actual);

            int expectedWindows = 5;
            int actualWindows = sweeps.CountIncreasingWindows();

            Assert.Equal(expectedWindows, actualWindows);
        }

        [Fact]
        public void PartOne()
        {
            var inputs = Input.Parse(int.Parse);
            var sweeps = new SonarSweep(inputs);

            int actual = sweeps.CountIncreasing();
            int expected = 1502;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PartTwo()
        {
            var inputs = Input.Parse(int.Parse);
            var sweeps = new SonarSweep(inputs);

            int actual = sweeps.CountIncreasingWindows();
            int expected = 1538;

            Assert.Equal(expected, actual);
        }
    }
}
