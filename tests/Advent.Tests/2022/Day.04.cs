using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day04 : IDailyTest
{
    public int Number => 4;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var lines = new List<string>()
        {
            "2-4,6-8",
            "2-3,4-5",
            "5-7,7-9",
            "2-8,3-7",
            "6-6,4-6",
            "2-6,4-8"
        };

        var mgr = new SectionManager(lines);
        var contain = mgr.SelfContained;
        var overlap = mgr.Overlapping;

        int ExpectedContainedCount = 2;
        int ExpectedOverlapCount = 4;
        Assert.Equal(ExpectedContainedCount, contain.Count);
        Assert.Equal(ExpectedOverlapCount, overlap.Count);
    }
        

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var mgr = new SectionManager(input);
        var pairs = mgr.SelfContained;

        int ExpectedCount = 509;
        Assert.Equal(ExpectedCount, pairs.Count);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var mgr = new SectionManager(input);
        var pairs = mgr.Overlapping;

        int ExpectedCount = 870;
        Assert.Equal(ExpectedCount, pairs.Count);
    }
}
