using System.Linq;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day08 : IDailyTest
{
    public int Number => 8;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var lines = new List<string>()
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };

        var trees = new TreeMap(lines);
        int visible = trees.CountVisible;
        int score = trees.HighestScenicScore;

        int ExpectedVisible = 21;
        int ExpectedScore = 8;
        Assert.Equal(ExpectedVisible, visible);
        Assert.Equal(ExpectedScore, score);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var trees = new TreeMap(input);
        int visible = trees.CountVisible;

        int ExpectedVisible = 1711;
        Assert.Equal(ExpectedVisible, visible);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var trees = new TreeMap(input);
        int score = trees.HighestScenicScore;

        int ExpectedScore = 301392;
        Assert.Equal(ExpectedScore, score);
    }
}
