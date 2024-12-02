namespace Advent.Console.Application.Solutions._2024;

public sealed class HistorianHysteria : ISolution
{
    private List<int> A { get; set; } = [];
    private List<int> B { get; set; } = [];

    public async Task RunAsync(string input)
    {
        await ParseInputs(input);

        await PartOneAsync();

        await PartTwoAsync();
    }

    async Task ParseInputs(string input)
    {
        Helper.Write($"Reading: {Path.GetFileName(input).Yellow()}");

        var lines = await File.ReadAllLinesAsync(input);

        foreach (var line in lines)
        {
            var vals = line.Split("   ").Select(int.Parse).ToList();

            A.Add(vals[0]);
            B.Add(vals[1]);
        }

        Helper.Write($"\t .. Loaded {lines.Length.Yellow()} lines for {A.Count.Yellow()} pairs.");
    }

    Task PartOneAsync()
    {
        var orderedA = A.OrderBy(x => x).ToList();
        var orderedB = B.OrderBy(x => x).ToList();

        var dists = new List<int>();
        foreach (int i in Enumerable.Range(0, orderedA.Count))
        {
            dists.Add(Math.Abs(orderedA[i] - orderedB[i]));
        }

        int sum = dists.Sum();

        Helper.Write($" - Part One: {sum.Yellow()}");

        return Task.CompletedTask;
    }

    Task PartTwoAsync()
    {
        var hist = new Dictionary<int, int>();

        foreach (int b in B)
        {
            hist.TryAdd(b, 0);

            hist[b] += 1;
        }

        var vals = new List<int>();

        foreach (int a in A)
        {
            var seen = hist.TryGetValue(a, out int count);

            vals.Add(seen ? a * count : 0);
        }

        var sum = vals.Sum();

        Helper.Write($" - Part Two: {sum.Yellow()}");

        return Task.CompletedTask;
    }
}
