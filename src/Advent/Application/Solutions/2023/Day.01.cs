namespace Advent.Console.Application.Solutions._2023;

public sealed class Trebuchet : ISolution
{
    static readonly Dictionary<string, string> _numbers = new()
    {
        { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" }, { "five", "5" },
        { "six", "6" }, { "seven", "7" }, {"eight", "8" }, { "nine", "9" }
    };

    public async Task RunAsync(string input)
    {
        var lines = await ParseInput(input);

        Helper.Write("Processing Inputs");

        await PartOneAsync(lines);

        await PartTwoAsync(lines);
    }

    static async Task<IEnumerable<string>> ParseInput(string input)
    {
        Helper.Write($"Reading: {Path.GetFileName(input).Yellow()}");

        var lines = await File.ReadAllLinesAsync(input);

        Helper.Write($"\t .. Loaded {lines.Length.Yellow()} total lines");

        return lines;
    }

    static async Task PartOneAsync(IEnumerable<string> input)
    {
        var results = await Task.WhenAll(input.Select(GetCombinedFirstAndLast));

        int sum = results.Sum();

        Helper.Write($" - Part One: {sum.Yellow()}");
    }

    static Task<int> GetCombinedFirstAndLast(string line)
    {
        var nums = line.Where(char.IsNumber).Select(c => c.ToString()).ToList();

        int result = Convert.ToInt32(nums.First() + nums.Last());

        return Task.FromResult(result);
    }

    static async Task PartTwoAsync(IEnumerable<string> input)
    {
        var results = await Task.WhenAll(input.Select(GetCombinedWithWords));

        int sum = results.Sum();

        Helper.Write($" - Part Two: {sum.Yellow()}");
    }

    static Task<int> GetCombinedWithWords(string line)
    {
        string first = string.Empty;
        string last = string.Empty;

        for (int i = 0; i < line.Length; i++)
        {
            if (TryGetNumber(line, i, out string val))
            {
                first = val;
                break;
            }
        }

        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (TryGetNumber(line, i, out string val))
            {
                last = val;
                break;
            }
        }

        var result = Convert.ToInt32(first + last);
        return Task.FromResult(result);
    }

    static bool TryGetNumber(string line, int idx, out string value)
    {
        if (char.IsNumber(line[idx]))
        {
            value = line[idx].ToString();
            return true;
        }

        var matching = _numbers.Keys.SingleOrDefault(k => line[idx..].StartsWith(k));
        if (matching != null)
        {
            value = _numbers[matching].ToString();
            return true;
        }

        value = string.Empty;
        return false;
    }
}