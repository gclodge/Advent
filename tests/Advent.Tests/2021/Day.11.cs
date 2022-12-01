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
    public class Day11 : IDailyTest
    {
        public int Number => 11;
        public int Year => 2021;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var octo = new OctopusGrid(input);
            var res = octo.Solve();

            int expectedSync = 195;
            int expectedFlashes = 1656;
            Assert.Equal(expectedFlashes, res.flashCount);
            Assert.Equal(expectedSync, res.synchStep);
        }

        [Fact]
        public void PartOne()
        {
            var input = Input.Parse();
            var octo = new OctopusGrid(input);
            var res = octo.Solve();

            int expectedFlashes = 1562;
            Assert.Equal(expectedFlashes, res.flashCount);
        }

        [Fact]
        public void PartTwo()
        {
            var input = Input.Parse();
            var octo = new OctopusGrid(input);
            var res = octo.Solve();

            int expectedSynchStep = 268;
            Assert.Equal(expectedSynchStep, res.synchStep);
        }
    }
}
