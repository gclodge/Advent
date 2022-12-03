global using Advent.Domain;

using System;
using System.IO;

namespace Advent.Tests;

public class TestHelper
{
    public const string InputDirectoryRootVarName = "ADVENT_INPUT_DIR_ROOT";

    public static string GetInputDirectory()
    {
        var dir = Environment.GetEnvironmentVariable(InputDirectoryRootVarName);
        if (dir == null) throw new ArgumentNullException($"{InputDirectoryRootVarName} is not set - cannot locate input directory");

        return dir;
    }

    public static string GetInputFile(IDailyTest dt)
    {
        return GetFile(dt, "Input");
    }

    public static string GetTestFile(IDailyTest dt, string? kernel = null)
    {
        return GetFile(dt, (kernel == null) ? "Test" : kernel);
    }

    private static string GetFile(IDailyTest dt, string kernel)
    {
        return Path.Combine(GetInputDirectory(), $"{dt.Year}", $"Day.{dt.Number.ToString().PadLeft(2, '0')}.{kernel}.txt");
    }
}