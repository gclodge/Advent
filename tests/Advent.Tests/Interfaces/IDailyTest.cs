using Xunit;

namespace Advent.Tests;

public interface IDailyTest
{
    int Number { get; }

    int Year { get; }

    [Fact]
    void PartOne();

    [Fact]
    void PartTwo();
}