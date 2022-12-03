using System.Linq;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day03 : IDailyTest
{
    public int Number => 3;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        int Expected = 157;
        int ExpectedBadge = 70;

        var lines = new List<string>()
        {
            "vJrwpWtwJgWrhcsFMMfFFhFp",
            "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg",
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
            "ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw",
        };

        var sacks = lines.Select(x => new Rucksack(x)).ToList();
        var sum = sacks.Sum(x => x.CalculateSharedItemPriority());
        Assert.Equal(Expected, sum);

        var badgeSum = Rucksack.CheckGroups(sacks);
        Assert.Equal(ExpectedBadge, badgeSum);
    }


    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var sacks = input.Select(x => new Rucksack(x)).ToList();
        var sum = sacks.Sum(x => x.CalculateSharedItemPriority());

        int Expected = 7568;
        Assert.Equal(Expected, sum);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var sacks = input.Select(x => new Rucksack(x)).ToList();
        var sum = Rucksack.CheckGroups(sacks);

        int Expected = 2780;
        Assert.Equal(Expected, sum);
    }
}
