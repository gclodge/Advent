using System.IO;
using System.Linq;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day11 : IDailyTest
{
    public int Number => 11;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);
    public string Test => TestHelper.GetTestFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var input = Test.Parse();

        var mmA = new MonkeyManager(input).ParseMonkeys();
        var mmB = new MonkeyManager(input).ParseMonkeys();
        var histA = mmA.GenerateInspectionHistogram();
        var histB = mmB.GenerateInspectionHistogram(10000, divide: false);
        var topTwoA = histA.OrderByDescending(x => x).Take(2);
        var topTwoB = histB.OrderByDescending(x => x).Take(2);

        int Expected0 = 101;
        int Expected3 = 105;
        int ExpectedMonkeyBusiness = 10605;

        long ExpectedFirstB = 52166;
        long ExpectedSecondB = 52013;
        long ExpectedMonkeyBusinessB = 2713310158;

        int ActualMonkeyBusinessA = topTwoA.First() * topTwoA.Last();
        long ActualMonkeyBusinessB = (long)topTwoB.First() * (long)topTwoB.Last();

        Assert.Equal(Expected0, histA[0]);
        Assert.Equal(Expected3, histA[3]);
        Assert.Equal(ExpectedFirstB, topTwoB.First());
        Assert.Equal(ExpectedSecondB, topTwoB.Last());
        Assert.Equal(ExpectedMonkeyBusiness, ActualMonkeyBusinessA);
        Assert.Equal(ExpectedMonkeyBusinessB, ActualMonkeyBusinessB);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var mm = new MonkeyManager(input).ParseMonkeys();
        var hist = mm.GenerateInspectionHistogram();
        var topTwo = hist.OrderByDescending(x => x).Take(2);

        int ExpectedMonkeyBusiness = 50616;
        int ActualMonkeyBusiness = topTwo.First() * topTwo.Last();

        Assert.Equal(ExpectedMonkeyBusiness, ActualMonkeyBusiness);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var mm = new MonkeyManager(input).ParseMonkeys();
        var hist = mm.GenerateInspectionHistogram(10000, divide: false);
        var topTwo = hist.OrderByDescending(x => x).Take(2);

        long ExpectedMonkeyBusiness = 11309046332;
        long ActualMonkeyBusiness = (long)topTwo.First() * (long)topTwo.Last();

        Assert.Equal(ExpectedMonkeyBusiness, ActualMonkeyBusiness);
    }
}
