namespace Advent.Console.Application.Solutions._2023;

public sealed class Trebuchet : ISolution
{
    public async Task RunAsync(string input)
    {
        var lines = await ParseInput(input);

         await PartOneAsync(lines);
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
        Helper.Write($"Computing Part One for {input.Count().Yellow()} lines");

        var results = await Task.WhenAll(input.Select(GetCombinedFirstAndLast));

        int sum = results.Sum();

        Helper.Write($"\t Result: {sum.Yellow()}");
    }

    static Task<int> GetCombinedFirstAndLast(string line)
    {
        var nums = line.Where(char.IsNumber).Select(c => c.ToString()).ToList();

        int result = Convert.ToInt32(nums.First() + nums.Last());

        return Task.FromResult(result);
    }
}