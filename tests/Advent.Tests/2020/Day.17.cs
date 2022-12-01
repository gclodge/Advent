using System;
using System.Linq;

using Advent._2020;

using Xunit;

namespace Advent.Tests._2020
{
    
    public class Day17 : IDailyTest
    {
        public int Number => 17;
        public int Year => 2020;

        public string Input => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_GetOffsetsForDimension()
        {
            var dims = new int[] { 2, 3, 4 };
            foreach (var dim in dims)
            {
                var offsets = ConwayCube.GetOffsets(dim).ToList();
                int targetCount = (int)Math.Pow(3, dim);

                Assert.True(offsets.Count() == targetCount);
            }
        }

        [Fact]
        public void Test_GetNeighbours3D()
        {
            var cube = new ConwayCube(new int[] { 0, 0, 0 });
            var neighs = cube.GetNeighbours().ToList();

            int targetCount = (int)Math.Pow(3, cube.Dimensions) - 1;
            Assert.True(neighs.Count == targetCount);
        }

        [Fact]
        public void Test_KnownCubes()
        {
            var input = TestFile.Parse();
            var controller = new ConwayController(input);
            controller.SimulateSteps(6);

            Assert.True(controller.NumActive == 112);
        }

        [Fact]
        public void Test_KnownCubes_4D()
        {
            var input = TestFile.Parse();
            var controller = new ConwayController(input, dimensions: 4);
            controller.SimulateSteps(6);

            Assert.True(controller.NumActive == 848);
        }

        [Fact]
        public void PartOne()
        {
            var input = Input.Parse();
            var controller = new ConwayController(input);
            controller.SimulateSteps(6);

            Assert.True(controller.NumActive == 293);
        }

        [Fact]
        public void PartTwo()
        {
            var input = Input.Parse();
            var controller = new ConwayController(input, dimensions: 4);
            controller.SimulateSteps(6);

            Assert.True(controller.NumActive == 1816);
        }
    }
}
