namespace Advent.Console.Application.Solutions._2023;

public sealed class CubeConundrum : ISolution
{

    public async Task RunAsync(string input)
    {
        var games = await ParseGames(input);

        await PartOneAsync(games);

        await PartTwoAsync(games);
    }

    static async Task<IEnumerable<CubeGame>> ParseGames(string input)
    {
        Helper.Write($"Reading: {Path.GetFileName(input).Yellow()}");

        var lines = await File.ReadAllLinesAsync(input);

        var games = lines.Select(x => new CubeGame(x)).ToList();

        Helper.Write($"\t .. Loaded {lines.Length.Yellow()} total Games");

        return games;
    }

    static Task PartOneAsync(IEnumerable<CubeGame> games)
    {
        var ids = games.Where(g => g.IsPossible(12, 13, 14))
                       .Select(x => x.ID).ToList();

        Helper.WriteDivider("Part One");
        Helper.Write($"Result: {ids.Sum().Yellow()}");

        return Task.CompletedTask;
    }

    static Task PartTwoAsync(IEnumerable<CubeGame> games)
    {
        var pows = games.Select(g => g.GetPower()).ToList();

        Helper.WriteDivider("Part Two");
        Helper.Write($"Result: {pows.Sum().Yellow()}");

        return Task.CompletedTask;
    }
}

public record CubeGame
{
    public int ID { get; set; }

    public List<CubeGameSubset> Subsets { get; set; }

    public CubeGame(string line)
    {
        var arr = line.Split(": ");
        ID = int.Parse(arr[0].Split(" ").Last());
        Subsets = arr[1].Split("; ")
                        .Select(x => new CubeGameSubset(x))
                        .ToList();
    }

    public bool IsPossible(int r, int g, int b)
    {
        return Subsets.All(s => s.Red <= r && s.Green <= g && s.Blue <= b);
    }

    public int GetPower()
    {
        int r = Subsets.Max(s => s.Red);
        int g = Subsets.Max(s => s.Green);
        int b = Subsets.Max(s => s.Blue);

        return r * g * b;
    }
}

public record CubeGameSubset
{
    public int Red { get; set; }
    public int Blue { get; set; }
    public int Green { get; set; }

    public CubeGameSubset(string input)
    {
        foreach (var val in input.Split(", "))
        {
            var res = val.Split(" ");
            int count = int.Parse(res[0]);
            string key = res[1];

            if (key == "red") Red = count;
            else if (key == "green") Green = count;
            else if (key == "blue") Blue = count;
        }
    }
}