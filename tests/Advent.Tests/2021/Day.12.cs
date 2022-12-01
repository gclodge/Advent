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
    public class Day12 : IDailyTest
    {
        public int Number => 12;
        public int Year => 2021;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var input = Test.Parse();
            var path = new PassagePath(input);
            path.Solve();

            int expectedPaths = 19;
            Assert.Equal(expectedPaths, path.PathCount);

            path = new PassagePath(input)
                        .SetPartTwo();
            path.Solve();
        }

        [Fact]
        public void PartOne()
        {
            var path = new PassagePath(Input.Parse());
            path.Solve();

            int expectedPaths = 5157;
            Assert.Equal(expectedPaths, path.PathCount);
        }

        [Fact]
        public void PartTwo()
        {
            var path = new PassagePath(Input.Parse())
                            .SetPartTwo();
            path.Solve();

            int expectedPaths = 144309;
            Assert.Equal(expectedPaths, path.PathCount);
        }
    }
}
