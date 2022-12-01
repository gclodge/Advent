global using Advent.Domain;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Advent.Tests;

public class TestHelper
{
    static readonly Assembly Self = Assembly.GetExecutingAssembly();

    private const string TestDir = "Inputs";

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
        if (TestDir == null) throw new NullReferenceException($"ERROR :: TestDirectory not set!");

        return Path.Combine(TestDir, $"{dt.Year}", $"Day.{dt.Number.ToString().PadLeft(2, '0')}.{kernel}.txt");
    }
}