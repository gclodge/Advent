using System.Collections.Generic;

using Advent._2020;

using Xunit;
using MathNet.Numerics.LinearAlgebra;

namespace Advent.Tests._2020
{
    public class Day03 : IDailyTest
    {
        public int Number => 3;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);

        [Fact]
        public void PartOne()
        {
            var rows = InputFile.Parse();

            var slope = TreeGrid.GetVector(3, 1);

            var grid = new TreeGrid(rows);
            grid.TraverseAndCountTrees(slope);

            Assert.True(grid.Trees.Count == 228);
        }

        [Fact]
        public void PartTwo()
        {
            var slopes = new List<Vector<double>>()
            {
                TreeGrid.GetVector(1, 1),
                TreeGrid.GetVector(3, 1),
                TreeGrid.GetVector(5, 1),
                TreeGrid.GetVector(7, 1),
                TreeGrid.GetVector(1, 2),
            };

            var rows = InputFile.Parse();
            var grid = new TreeGrid(rows);

            var results = new List<int>();
            foreach (var slope in slopes)
            {
                grid.TraverseAndCountTrees(slope);

                results.Add(grid.Trees.Count);
            }

            double val = 1;
            foreach (var res in results)
            {
                val *= res;
            }

            Assert.True(val == 6818112000);
        }
    }
}
