using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day18 : IDailyTest
{
    public int Number => 18;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);
    public string Test => TestHelper.GetTestFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var input = Test.Parse();

        var scanner = new LavaDropletScanner(input);
        int sides = scanner.CountSharedSides();

        int ExpectedSides = 64;
        Assert.Equal(ExpectedSides, sides);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var scanner = new LavaDropletScanner(input);
        int sides = scanner.CountSharedSides();

        int ExpectedSides = 4482;
        Assert.Equal(ExpectedSides, sides);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();
    }
}
