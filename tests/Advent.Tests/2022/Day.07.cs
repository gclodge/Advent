using System.Linq;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day07 : IDailyTest
{
    public int Number => 7;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var lines = new List<string>()
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d",
            "$ cd a",
            "$ ls",
            "dir e",
            "29116 f",
            "2557 g",
            "62596 h.lst",
            "$ cd e",
            "$ ls",
            "584 i",
            "$ cd ..",
            "$ cd ..",
            "$ cd d",
            "$ ls",
            "4060174 j",
            "8033020 d.log",
            "5626152 d.ext",
            "7214296 k"
        };

        var browser = new FileSystemBrowser(lines);
        long p1 = browser.CalculatePartOne();
        long p2 = browser.CalculatePartTwo();

        long ExpectedP1 = 95437;
        long ExpectedP2 = 24933642;
        Assert.Equal(ExpectedP1, p1);
        Assert.Equal(ExpectedP2, p2);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var browser = new FileSystemBrowser(input);
        long p1 = browser.CalculatePartOne();

        long ExpectedP1 = 1477771;
        Assert.Equal(ExpectedP1, p1);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var browser = new FileSystemBrowser(input);
        long p2 = browser.CalculatePartTwo();

        long ExpectedP2 = 3579501;
        Assert.Equal(ExpectedP2, p2);
    }
}
