using Xunit;

using System;
using System.Linq;
using System.Collections.Generic;

using Advent._2020;

namespace Advent.Tests._2020
{
    public class Day04 : IDailyTest
    {
        public int Number => 4;
        public int Year => 2020;

        public string Input => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownPassports()
        {
            var passFile = new PassportFile(TestFile);

            var goodPorts = passFile.Passports.Where(x => x.HasRequiredFields).ToList();

            Assert.True(goodPorts.Count == 2);
        }

        

        [Fact]
        public void Test_CheckDigits()
        {
            const int min = 1920;
            const int max = 2002;
            const int cnt = 4;

            //< 'Good' cases
            Assert.True(Passport.CheckDigits("1921", min, max, cnt));
            Assert.True(Passport.CheckDigits("1999", min, max, cnt));
            Assert.True(Passport.CheckDigits("2001", min, max, cnt));

            //< 'Bad' cases
            Assert.False(Passport.CheckDigits("1919", min, max, cnt));
            Assert.False(Passport.CheckDigits("2003", min, max, cnt));
            Assert.False(Passport.CheckDigits("4", min, max, cnt));
            Assert.False(Passport.CheckDigits("4444444", min, max, cnt));
            Assert.False(Passport.CheckDigits("Pant", min, max, cnt));
        }

        [Fact]
        public void Test_HairColors()
        {
            var knownColors = new List<Tuple<string, bool>>()
            {
                Tuple.Create("#123abc", true),
                Tuple.Create("#123abz", false),
                Tuple.Create("123abc", false),
            };

            foreach (var tup in knownColors)
            {
                Assert.True(Passport.CheckHairColor(tup.Item1) == tup.Item2);
            }
        }

        [Fact]
        public void Test_KnownValidPassports()
        {
            var validFile = new PassportFile(TestHelper.GetTestFile(this, "Test.Valid"));
            Assert.True(validFile.Passports.All(p => p.IsValid));

            var invalidFile = new PassportFile(TestHelper.GetTestFile(this, "Test.Invalid"));
            Assert.True(invalidFile.Passports.All(p => !p.IsValid));
        }

        [Fact]
        public void PartOne()
        {
            var passFile = new PassportFile(Input);

            var goodPorts = passFile.Passports.Where(x => x.HasRequiredFields).ToList();

            Assert.True(goodPorts.Count() == 256);
        }

        [Fact]
        public void PartTwo()
        {
            var passFile = new PassportFile(Input);

            var goodPorts = passFile.Passports.Where(x => x.IsValid).ToList();

            Assert.True(goodPorts.Count == 198);
        }
    }
}
