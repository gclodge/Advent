using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day15 : IDailyTest
{
    public int Number => 15;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);
    public string Test => TestHelper.GetTestFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var input = Test.Parse();

        var ess = new EmergencySensorSystem(input);
        long blocked = ess.CountBlockedPositions(20);
        long freq = ess.FindTuningFrequency(20);

        long ExpectedBlocked = 26;
        long ExpectedFreq = 56000011;
        Assert.Equal(ExpectedBlocked, blocked);
        Assert.Equal(ExpectedFreq, freq);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var ess = new EmergencySensorSystem(input);
        long blocked = ess.CountBlockedPositions(2000000);

        long ExpectedBlocked = 5073496;
        Assert.Equal(ExpectedBlocked, blocked);
    }

    [Fact (Skip = "Solution is inefficient AF - need to filter to boundary points only!")]
    public void PartTwo()
    {
        var input = Input.Parse();

        var ess = new EmergencySensorSystem(input);
        long freq = ess.FindTuningFrequency(4000000);

        long ExpectedFreq = 13081194638237;
        Assert.Equal(ExpectedFreq, freq);
    }
}
