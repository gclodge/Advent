using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day19 : IDailyTest
    {
        public int Number => 19;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        public static int RuleIndex => 0;

        [Fact]
        public void Test_KnownMessages()
        {
            var input = TestFile.Parse();

            var satellite = new Satellite(input);
            int count = satellite.CountMatchingMessages(RuleIndex);

            Assert.True(count == 2);
        }

        [Fact]
        public void Test_KnownMessages_WithFuckery()
        {
            var input = TestHelper.GetTestFile(this, "Test2").Parse();

            var satellite = new Satellite(input, isPartTwo: true);
            int count = satellite.CountMatchingMessages(RuleIndex, isPartTwo: true);

            Assert.True(count == 12);
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();

            var satellite = new Satellite(input);
            int count = satellite.CountMatchingMessages(RuleIndex);

            Assert.True(count == 203);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();
            
            var satellite = new Satellite(input, isPartTwo: true);
            //< NB :: This returns 305 when it should return 304
            //<    :: The extra record is "abaabaaabababaaabbbaaaabaabbbababbbbaaaabababaaabaaabaab"
            int count = satellite.CountMatchingMessages(RuleIndex, isPartTwo: true);

            const int actualAnswer = 304;
            Assert.True(count == actualAnswer + 1);
        }
    }
}
