using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2021;

namespace Advent.Tests._2021
{
    public class Day07 : IDailyTest
    {
        public int Number => 7;
        public int Year => 2021;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var crabs = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            var aligner = new CrabAligner(crabs);

            int cost = aligner.GetCheapestAlignment();
            int gaussCost = aligner.GetCheapestAlignment(isGauss: true);

            int expectedCost = 37;
            int expectedGaussCost = 168;

            Assert.Equal(expectedCost, cost);
            Assert.Equal(expectedGaussCost, gaussCost);
        }

        [Fact]
        public void PartOne()
        {
            var crabs = Input.Parse().Single().Split(',').Select(int.Parse);
            var aligner = new CrabAligner(crabs);

            int cost = aligner.GetCheapestAlignment();
            int expectedCost = 337488;

            Assert.Equal(expectedCost, cost);
        }

        [Fact]
        public void PartTwo()
        {
            var crabs = Input.Parse().Single().Split(',').Select(int.Parse);
            var aligner = new CrabAligner(crabs);

            int cost = aligner.GetCheapestAlignment(isGauss: true);
            int expectedCost = 89647695;

            Assert.Equal(expectedCost, cost);
        }
    }
}
