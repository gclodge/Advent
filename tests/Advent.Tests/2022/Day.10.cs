using System.IO;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2022;

namespace Advent.Tests._2022;

public class Day10 : IDailyTest
{
    public int Number => 10;
    public int Year => 2022;

    public string Input => TestHelper.GetInputFile(this);
    public string Test => TestHelper.GetTestFile(this);

    [Fact]
    public void Test_KnownInputs()
    {
        var input = Test.Parse();

        var crt = new CathodeRayTube(input).ProcessSteps();
        int sig = crt.SignificantSignalSum;

        int ExpectedSig = 13140;
        Assert.Equal(ExpectedSig, sig);

        //< Need to print the image and find out what it is
        string imgPath = Path.ChangeExtension(Test, ".Result.txt");
        crt.Save(imgPath);

        Assert.True(File.Exists(imgPath));
    }

    [Fact]
    public void PartOne()
    {
        var input = Input.Parse();

        var crt = new CathodeRayTube(input).ProcessSteps();
        int sig = crt.SignificantSignalSum;

        int ExpectedSig = 12460;
        Assert.Equal(ExpectedSig, sig);
    }

    [Fact]
    public void PartTwo()
    {
        var input = Input.Parse();

        var crt = new CathodeRayTube(input).ProcessSteps();
        int sig = crt.SignificantSignalSum;

        int ExpectedSig = 12460;
        Assert.Equal(ExpectedSig, sig);

        //< Need to print the image and find out what it is
        string imgPath = Path.ChangeExtension(Input, ".Result.txt");
        crt.Save(imgPath);

        //< EZFPRAKL
        Assert.True(File.Exists(imgPath));
    }
}
