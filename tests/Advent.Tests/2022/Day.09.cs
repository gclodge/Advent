using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day09 : IDailyTest
{
    public int Number => 9;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var linesA = new List<string>()
        {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };

        var linesB = new List<string>()
        {
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20"
        };

        int ExpectedTailPositionsA = 13;
        int ExpectedTailPositionsB = 1;
        int ExpectedTailPositionsC = 36;

        var ropeA = new Rope(linesA).ApplyMovements();
        var ropeB = new Rope(linesA).ApplyMovements(10);
        var ropeC = new Rope(linesB).ApplyMovements(10);

        int countA = ropeA.UniqueTailPositions;
        int countB = ropeB.UniqueTailPositions;
        int countC = ropeC.UniqueTailPositions;

        Assert.Equal(ExpectedTailPositionsA, countA);
        Assert.Equal(ExpectedTailPositionsB, countB);
        Assert.Equal(ExpectedTailPositionsC, countC);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        int ExpectedTailPositions = 6354;
        var rope = new Rope(input).ApplyMovements();
        int count = rope.UniqueTailPositions;

        Assert.Equal(ExpectedTailPositions, count);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        int ExpectedTailPositions = 2651;
        var rope = new Rope(input).ApplyMovements(10);
        int count = rope.UniqueTailPositions;

        Assert.Equal(ExpectedTailPositions, count);
    }
}
