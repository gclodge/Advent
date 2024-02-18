namespace Advent.Console.Application.Solutions._2023;

public sealed class ScratchCards : ISolution
{
    public async Task RunAsync(string input)
    {
        var cards = await ParseCards(input);

        await PartOneAsync(cards);

        await PartTwoAsync(cards);
    }

    static async Task<IEnumerable<ScratchCard>> ParseCards(string input)
    {
        Helper.Write($"Reading: {Path.GetFileName(input).Yellow()}");

        var lines = await File.ReadAllLinesAsync(input);

        var cards = lines.Select(x => new ScratchCard(x)).ToList();

        Helper.Write($"\t .. Loaded {lines.Length.Yellow()} total ScratchCards");

        return cards;
    }

    static Task PartOneAsync(IEnumerable<ScratchCard> cards)
    {
        var points = cards.Sum(c => c.GetPoints());

        Helper.WriteDivider("Part One");
        Helper.Write($"Result: {points.Yellow()}");

        return Task.CompletedTask;
    }

    static Task PartTwoAsync(IEnumerable<ScratchCard> cards)
    {
        var hist = new int[byte.MaxValue];

        foreach (var card in cards)
        {
            //< Ensure we have at least the 'original' card added to the histogram
            hist[card.ID] += 1;
            //< Get the number of winners for the loop
            int num = card.GetNumWinners();
            //< Get the number of the current card so we know how many to add of each successive card
            int count = hist[card.ID];
            foreach (int i in Enumerable.Range(1, num))
            {
                hist[card.ID + i] += count;
            }
        }

        Helper.WriteDivider("Part Two");
        Helper.Write($"Result: {hist.Sum().Yellow()}");

        return Task.CompletedTask;
    }
}

internal record ScratchCard
{
    public int ID { get; set; }

    public HashSet<int> Winning { get; set; } = [];
    public HashSet<int> Numbers { get; set; } = [];

    public ScratchCard(string line)
    {
        var arr = line.Split(": ");
        ID = int.Parse(arr[0].Split(" ").Last());

        var nums = arr[1].Split(" | ");
        Winning = ParseNumbers(nums[0]);
        Numbers = ParseNumbers(nums[1]);
    }

    static HashSet<int> ParseNumbers(string input)
    {
        return input.Split(" ").Where(x => !string.IsNullOrEmpty(x))
                               .Select(int.Parse)
                               .ToHashSet();
    }

    public int GetNumWinners()
        => Numbers.Where(n => Winning.Contains(n)).Count();

    public int GetPoints()
        => (int)Math.Pow(2, GetNumWinners() - 1);
}