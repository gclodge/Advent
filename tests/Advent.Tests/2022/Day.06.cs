using System.Linq;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day06 : IDailyTest
{
    public int Number => 6;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var lines = new List<(string str, int idxA, int idxB)>()
        {
            ("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7, 19),
            ("bvwbjplbgvbhsrlpgdmjqwftvncz", 5, 23),
            ("nppdvjthqldpwncqszvftbrmjlhg", 6, 23),
            ("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10, 29),
            ("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11, 26)
        };

        foreach (var line in lines)
        {
            var buff = new DatastreamBuffer(line.str);
            int idxA = buff.GetFirstStartOfPacketMarker(4);
            int idxB = buff.GetFirstStartOfPacketMarker(14);

            Assert.Equal(line.idxA, idxA);
            Assert.Equal(line.idxB, idxB);
        }
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse().Single();

        var buff = new DatastreamBuffer(input);
        int ActualIdx = buff.GetFirstStartOfPacketMarker(4);
        int ExpectedIdx = 1850;

        Assert.Equal(ExpectedIdx, ActualIdx);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse().Single();

        var buff = new DatastreamBuffer(input);
        int ActualIdx = buff.GetFirstStartOfPacketMarker(14);
        int ExpectedIdx = 2823;

        Assert.Equal(ExpectedIdx, ActualIdx);
    }
}
