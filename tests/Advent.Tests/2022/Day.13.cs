using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day13 : IDailyTest
{
    public int Number => 13;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);
    public string Test => TestHelper.GetTestFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var input = Test.Parse();

        var anal = new DistressSignalAnalyzer(input);
        int P1 = anal.PartOne();
        int P2 = anal.PartTwo();

        int ExpectedP1 = 13;
        int ExpectedP2 = 140;
        Assert.Equal(ExpectedP1, P1);
        Assert.Equal(ExpectedP2, P2);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();
        var anal = new DistressSignalAnalyzer(input);
        int P1 = anal.PartOne();

        int ExpectedP1 = 5623;
        Assert.Equal(ExpectedP1, P1);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();
        var anal = new DistressSignalAnalyzer(input);
        int P2 = anal.PartTwo();

        int ExpectedP2 = 20570;
        Assert.Equal(ExpectedP2, P2);
    }
}
