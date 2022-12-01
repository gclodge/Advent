using Advent._2020;

using Xunit;

namespace Advent.Tests._2020
{
    
    public class Day13 : IDailyTest
    {
        public int Number => 13;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownBusses()
        {
            var input = TestFile.Parse();

            var manager = new BusManager(input);
            var bus = manager.GetEarliestBus();

            long wait = bus.GetWaitTime(manager.MinTimestamp);
            long val = wait * bus.ID;

            Assert.True(val == 295);
        }

        [Fact]
        public void Test_KnownMinContinous()
        {
            var input = TestFile.Parse();

            var manager = new BusManager(input);
            var t = manager.GetFirstContinuousDepartureTime();

            Assert.True(t == 1068781);
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();

            var manager = new BusManager(input);
            var bus = manager.GetEarliestBus();

            long wait = bus.GetWaitTime(manager.MinTimestamp);
            long val = wait * bus.ID;

            Assert.True(val == 153);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();

            var manager = new BusManager(input);
            var t = manager.GetFirstContinuousDepartureTime();

            Assert.True(t == 471793476184394);
        }
    }
}
