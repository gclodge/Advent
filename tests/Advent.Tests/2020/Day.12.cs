using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    
    public class Day12 : IDailyTest
    {
        public int Number => 12;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownCommands()
        {
            var input = TestFile.Parse();

            var shipCapt = new ShipCaptain(input);
            shipCapt.Navigate();

            Assert.True(shipCapt.Distance == 25);
        }

        [Fact]
        public void Test_KnownCommands_WithWaypoint()
        {
            var input = TestFile.Parse();

            var shipCapt = new ShipCaptain(input, ShipNavigationType.Waypoint);
            shipCapt.Navigate();

            Assert.True(shipCapt.Distance == 286);
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();

            var shipCapt = new ShipCaptain(input);
            shipCapt.Navigate();

            Assert.True(shipCapt.Distance == 562);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();

            var shipCapt = new ShipCaptain(input, ShipNavigationType.Waypoint);
            shipCapt.Navigate();

            Assert.True(shipCapt.Distance == 101860);
        }
    }
}
