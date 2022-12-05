using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day05 : IDailyTest
{
    public int Number => 5;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var lines = new List<string>()
        {
            "    [D]",
            "[N] [C]",
            "[Z] [M] [P]",
            " 1   2   3 ",
            "",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2"
        };

        var crane = new Crane(lines);
        crane.MoveCrates();

        string msgA = crane.GetMessage();
        string ExpectedMsgA = "CMZ";

        Assert.Equal(ExpectedMsgA, msgA);

        crane.Reset();
        crane.MoveCratesPartTwo();

        string msgB = crane.GetMessage();
        string ExpectedMsgB = "MCD";

        Assert.Equal(ExpectedMsgB, msgB);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var crane = new Crane(input);
        crane.MoveCrates();

        string msg = crane.GetMessage();
        string ExpectedMsg = "HNSNMTLHQ";

        Assert.Equal(ExpectedMsg, msg);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var crane = new Crane(input);
        crane.MoveCratesPartTwo();

        string msg = crane.GetMessage();
        string ExpectedMsg = "RNLFDJMCT";

        Assert.Equal(ExpectedMsg, msg);
    }
}
