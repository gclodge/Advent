﻿using System.Reflection;

using Microsoft.Extensions.Configuration;

namespace Advent.Console.Common;

public static class Configuration
{
    public static string GetVersion()
    {
        var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        return assemblyVersion ?? "Unknown";
    }

    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .AddEnvironmentVariables("Stseelhtun")
            .Build();
    }
}
