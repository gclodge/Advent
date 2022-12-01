using System.Linq;

using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day06 : IDailyTest
    {
        public int Number => 6;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();
            var groups = Functions.SplitByByElement(input, "")
                                  .Select(grp => new CustomsGroup(grp))
                                  .ToList();

            int num = groups.Sum(x => x.CountYes);

            Assert.True(num == 7283);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();
            var groups = Functions.SplitByByElement(input, "")
                                  .Select(grp => new CustomsGroup(grp))
                                  .ToList();

            int num = groups.Sum(x => x.CountAllYes);

            Assert.True(num == 3520);
        }
    }
}
