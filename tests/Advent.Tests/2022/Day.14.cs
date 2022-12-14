using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day14 : IDailyTest
{
    public int Number => 14;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);
    public string Test => TestHelper.GetTestFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var input = new string[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9"
        };

        var regolith = new Regolith(input);
        int sand = regolith.PartOne();
        int floorSand = regolith.PartTwo();

        int ExpectedSand = 24;
        int ExpectedFloorSand = 93;
        Assert.Equal(ExpectedSand, sand);
        Assert.Equal(ExpectedFloorSand, floorSand);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();
        var regolith = new Regolith(input);
        int sand = regolith.PartOne();

        int ExpectedSand = 638;
        Assert.Equal(ExpectedSand, sand);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();
        var regolith = new Regolith(input);
        int sand = regolith.PartTwo();

        int ExpectedSand = 31722;
        Assert.Equal(ExpectedSand, sand);
    }
}
