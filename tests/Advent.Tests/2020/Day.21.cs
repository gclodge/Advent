using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day21 : IDailyTest
    {
        public int Number => 21;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownAllergens()
        {
            var input = TestFile.Parse();
            var detector = new AllergenDetector(input);

            int count = detector.CountNonAllergenOccurrence();
            Assert.True(count == 5);

            string danger = detector.GetCanonicalDangerousIngredientList();
            Assert.True(danger == "mxmxvkd,sqjhc,fvjkl");
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();
            var detector = new AllergenDetector(input);

            int count = detector.CountNonAllergenOccurrence();
            Assert.True(count == 2078);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();
            var detector = new AllergenDetector(input);

            string danger = detector.GetCanonicalDangerousIngredientList();
            Assert.True(danger == "lmcqt,kcddk,npxrdnd,cfb,ldkt,fqpt,jtfmtpd,tsch");
        }
    }
}
