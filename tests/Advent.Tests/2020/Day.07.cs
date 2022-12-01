using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day07 : IDailyTest
    {
        public int Number => 7;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownRules()
        {
            string testType = "shiny gold";
            var input = TestFile.Parse();

            var handler = new BaggageHandler(input);
            int count = handler.CountBagsThatCanContain(testType);

            Assert.True(count == 4);
        }

        [Fact]
        public void PartOne()
        {
            string type = "shiny gold";
            var input = InputFile.Parse();

            var handler = new BaggageHandler(input);
            int count = handler.CountBagsThatCanContain(type);

            Assert.True(count == 161);
        }

        [Fact]
        public void Test_KnownContents()
        {
            string testType = "shiny gold";
            var input = TestFile.Parse();

            var handler = new BaggageHandler(input);
            int count = handler.CountBagsWithin(testType);

            Assert.True(count == 32);
        }

        [Fact]
        public void Test_KnownContents_Two()
        {
            string testType = "shiny gold";
            var input = TestHelper.GetTestFile(this, "Test2").Parse();

            var handler = new BaggageHandler(input);
            int count = handler.CountBagsWithin(testType);

            Assert.True(count == 126);
        }

        [Fact]
        public void PartTwo()
        {
            string type = "shiny gold";
            var input = InputFile.Parse();

            var handler = new BaggageHandler(input);
            int count = handler.CountBagsWithin(type);

            Assert.True(count == 30899);
        }
    }
}
