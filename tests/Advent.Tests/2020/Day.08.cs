using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day08 : IDailyTest
    {
        public int Number => 8;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownProgram()
        {
            var input = TestFile.Parse();

            var handheld = new HandheldGameSystem(input);
            var res = handheld.Run();

            Assert.True(res.Accumulator == 5);
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();
            var handheld = new HandheldGameSystem(input);

            var res = handheld.Run();

            Assert.True(res.Accumulator == 1782);
        }

        [Fact]
        public void Test_KnownProgramWithSwap()
        {
            var input = TestFile.Parse();

            var handheld = new HandheldGameSystem(input);
            var res = handheld.Run();

            Assert.True(res.Accumulator == 5);
        }

        [Fact]
        public void PartTwo()
        {
            //< Parse the data into memory - generate an initial Handheld
            var input = InputFile.Parse();
            var handheld = new HandheldGameSystem(input);
            //< Get the indices we may want to alter
            var indicesToTry = handheld.GetIndicesToTry(new string[] { "nop", "jmp" });

            int goodVal = -1;
            foreach (var idx in indicesToTry)
            {
                //< Reset the HandheldGameSystem back to 'normal' (the lazy way)
                handheld = new HandheldGameSystem(input);
                //< Run it with the swap
                var res = handheld.RunWithSwap(idx);
                if (!res.WasInfiniteLoop)
                {
                    goodVal = res.Accumulator;
                    break;
                }
            }

            Assert.True(goodVal == 797);
        }
    }
}
