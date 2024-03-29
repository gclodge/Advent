﻿namespace Advent.Domain;

public static class Extensions
{
    /// <summary>
    /// Parse all lines in input text file
    /// </summary>
    /// <param name="file">Text file to be parsed</param>
    /// <returns>Collection of all rows encountered in text file</returns>
    public static IEnumerable<string> Parse(this string file)
    {
        var res = new List<string>();
        using var sr = new StreamReader(file);

        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            if (line != null) res.Add(line);
        }

        return res;
    }

    /// <summary>
    /// Parse all lines in input text file and deserialize with given function
    /// </summary>
    /// <typeparam name="T">Type of object to be created</typeparam>
    /// <param name="file">Text file to be parsed</param>
    /// <param name="deserialize">Deserialization function</param>
    /// <returns>Collection of <typeparamref name="T"/> records</returns>
    public static IEnumerable<T> Parse<T>(this string file, Func<string, T> deserialize)
    {
        return Parse(file).Select(x => deserialize(x));
    }

    public static ulong Sum(this IEnumerable<ulong> elements)
    {
        ulong sum = 0;
        foreach (var ele in elements)
        {
            sum += ele;
        }
        return sum;
    }

    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int size)
    {
        return source.Select((x, i) => new { Index = i, Value = x })
                     .GroupBy(x => x.Index / size)
                     .Select(x => x.Select(v => v.Value));
    }
}