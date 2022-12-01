using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day09 : IDailyTest
    {
        public int Number => 9;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownPreamble()
        {
            var input = TestFile.Parse(long.Parse);
            var xmas = new XmasEncoder(input, 5);

            var badVal = xmas.CheckFirstInvalidValue();
            Assert.True(badVal.HasValue && badVal.Value == 127);
        }

        private const long _PartOneResult = 15353384;
        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse(long.Parse);
            var xmas = new XmasEncoder(input, 25);

            var badVal = xmas.CheckFirstInvalidValue();

            Assert.True(badVal.HasValue);
            Assert.Equal(_PartOneResult, badVal);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse(long.Parse);
            var xmas = new XmasEncoder(input, 25);

            var weakness = xmas.FindEncryptionWeakness(_PartOneResult);
            long Expected = 2466556;

            Assert.True(weakness.HasValue);
            Assert.Equal(Expected, weakness);
        }
    }
}
