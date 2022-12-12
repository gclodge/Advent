using System.IO;
using System.Linq;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day12 : IDailyTest
{
    public int Number => 12;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var input = new string[]
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi"
        };

        var climber = new HillClimber(input);
        var path = climber.FindPath();
        var pathB = climber.FindPath(partOne: false);

        int ExpectedSteps = 31;
        int ExpectedStepsB = 29;
        int ActualSteps = path.Count() - 1;
        int ActualStepsB = pathB.Count() - 1;
        Assert.Equal(ExpectedSteps, ActualSteps);
        Assert.Equal(ExpectedStepsB, ActualStepsB);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();
        var climber = new HillClimber(input);
        var path = climber.FindPath();

        int ExpectedSteps = 391;
        int ActualSteps = path.Count() - 1;
        Assert.Equal(ExpectedSteps, ActualSteps);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();
        var climber = new HillClimber(input);
        var path = climber.FindPath(partOne: false);

        int ExpectedSteps = 386;
        int ActualSteps = path.Count() - 1;
        Assert.Equal(ExpectedSteps, ActualSteps);
    }
}
