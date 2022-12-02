using System.Linq;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day02 : IDailyTest
{
    public int Number => 2;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var lines = new List<string>()
        {
            "A Y",
            "B X",
            "C Z"
        };

        var rps = new RockPaperScissors(lines);
        long scoreP1 = rps.SolvePartOne();
        long scoreP2 = rps.SolvePartTwo();
        long ExpectedP1 = 15;
        long ExpectedP2 = 12;

        Assert.Equal(ExpectedP1, scoreP1);
        Assert.Equal(ExpectedP2, scoreP2);
    }


    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var rps = new RockPaperScissors(input);
        long score = rps.SolvePartOne();
        long Expected = 13526;

        Assert.Equal(Expected, score);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var rps = new RockPaperScissors(input);
        long score = rps.SolvePartTwo();
        long Expected = 14204;

        Assert.Equal(Expected, score);
    }
}
