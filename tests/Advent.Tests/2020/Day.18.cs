using System;
using System.Linq;
using System.Collections.Generic;

using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day18 : IDailyTest
    {
        public int Number => 18;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        private static readonly List<Tuple<string, int>> KnownEquations = new List<Tuple<string, int>>()
        {
            Tuple.Create("2 * 3 + (4 * 5)", 26),
            Tuple.Create("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437),
            Tuple.Create("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240),
            Tuple.Create("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632),
        };

        private static readonly List<Tuple<string, int>> KnownPrecedenceEquations = new List<Tuple<string, int>>()
        {
            Tuple.Create("1 + (2 * 3) + (4 * (5 + 6))", 51),
            Tuple.Create("2 * 3 + (4 * 5)", 46),
            Tuple.Create("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445),
            Tuple.Create("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060),
            Tuple.Create("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340),
        };

        [Fact]
        public void Test_KnownEquations()
        {
            foreach (var tup in KnownEquations) //< Part one tests
            {
                var eqn = new MathEquation(tup.Item1);
                Assert.True(eqn.Result == tup.Item2);
            }

            foreach (var tup in KnownPrecedenceEquations) //< Part two tests
            {
                var eqn = new MathEquation(tup.Item1, HomeworkType.Advanced);
                Assert.True(eqn.Result == tup.Item2);
            }
        }

        [Fact]
        public void PartOne()
        {
            var input = InputFile.Parse();
            var homework = new MathHomework(input);

            long sum = homework.Equations.Sum(x => x.Result);
            Assert.True(sum == 209335026987);
        }

        [Fact]
        public void PartTwo()
        {
            var input = InputFile.Parse();
            var homework = new MathHomework(input, HomeworkType.Advanced);

            long sum = homework.Equations.Sum(x => x.Result);
            Assert.True(sum == 33331817392479);
        }
    }
}
