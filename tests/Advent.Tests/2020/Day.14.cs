using Advent._2020;

using Xunit;

namespace Advent.Tests._2020
{
    
    public class Day14 : IDailyTest
    {
        public int Number => 14;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownMask()
        {
            var input = TestFile.Parse();

            var docker = new Docker(input);
            var val = docker.Initialize();

            Assert.True(val == 165);
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();

            var docker = new Docker(input);
            var val = docker.Initialize();

            Assert.True(val == 9879607673316);
        }

        [Fact]
        public void Test_KnownAddresses()
        {
            var input = TestHelper.GetTestFile(this, "Test2").Parse();

            var docker = new Docker(input);
            var val = docker.Initialize(v2: true);

            Assert.True(val == 208);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();

            var docker = new Docker(input);
            var val = docker.Initialize(v2: true);

            Assert.True(val == 3435342392262);
        }
    }
}
