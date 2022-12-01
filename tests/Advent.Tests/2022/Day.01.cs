using System.Linq;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day01 : IDailyTest
{
    public int Number => 1;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownCalories()
    {
        var lines = new List<string>()
        {
            "1000",
            "2000",
            "3000",
            "",
            "4000",
            "",
            "5000",
            "6000",
            "",
            "7000",
            "8000",
            "9000",
            "",
            "10000",
        };

        var ecc = new ElfCalorieCounter(lines);
        var cals = ecc.CalculateIndividualCalories();
        var Actual = cals.Max();

        int Expected = 24000;
        Assert.Equal(Expected, Actual);
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var ecc = new ElfCalorieCounter(input);
        var cals = ecc.CalculateIndividualCalories();

        int Actual = cals.Max();
        int Expected = 67622;
        Assert.Equal(Expected, Actual);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var ecc = new ElfCalorieCounter(input);
        var cals = ecc.CalculateIndividualCalories();

        var topThree = cals.OrderByDescending(x => x).Take(3);
        
        int Actual = topThree.Sum();
        int Expected = 201491;

        Assert.Equal(Expected, Actual);
    }
}
