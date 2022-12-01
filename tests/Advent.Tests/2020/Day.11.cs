using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day11 : IDailyTest
    {
        public int Number => 11;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownAdjacentSeats()
        {
            var input = TestFile.Parse();

            var map = new SeatingMap(input);
            map.SimulateSeating();

            Assert.True(map.Occupied == 37);
        }

        [Fact]
        public void Test_KnownVisibleSeats()
        {
            var input = TestFile.Parse();

            var map = new SeatingMap(input, tolerance: 5, searchFirstVisible: true);
            map.SimulateSeating();

            Assert.True(map.Occupied == 26);
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();

            var map = new SeatingMap(input);
            map.SimulateSeating();

            Assert.True(map.Occupied == 2265);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();

            var map = new SeatingMap(input, tolerance: 5, searchFirstVisible: true);
            map.SimulateSeating();

            Assert.True(map.Occupied == 2045);
        }
    }
}
