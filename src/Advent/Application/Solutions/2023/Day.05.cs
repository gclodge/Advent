namespace Advent.Console.Application.Solutions._2023;

public sealed class SeedToFertilizer : ISolution
{
    private List<uint> _seeds = [];
    private readonly HashSet<SeedMap> _maps = [];

    public async Task RunAsync(string input)
    {
        await LoadInputs(input);

        await PartOneAsync();

        await PartTwoAsync();
    }

    async Task LoadInputs(string input)
    {
        var lines = await File.ReadAllLinesAsync(input);

        _seeds = lines[0].Split(": ").Last().Split(' ').Select(uint.Parse).ToList();

        var curr = new List<string>();
        foreach (var line in lines.Where(l => !string.IsNullOrEmpty(l)).Skip(1))
        {
            if (line.EndsWith(':') && curr.Count > 0)
            {
                _maps.Add(new SeedMap(curr));
                curr.Clear();
                curr.Add(line);

                continue;
            }

            curr.Add(line);
        }

        if (curr.Count > 0) _maps.Add(new SeedMap(curr));
    }

    async Task PartOneAsync()
    {
        Helper.WriteDivider("Part One");

        var results = await Task.WhenAll(_seeds.Select(Transform));

        Helper.Write($"Result: {results.Min().Yellow()}");
    }

    async Task PartTwoAsync()
    {
        Helper.WriteDivider("Part Two");

        //< Can't brute force, must do interval math to find out what values to check

        throw new NotImplementedException();
    }

    Task<uint> Transform(uint value)
    {
        uint curr = value;

        foreach (var map in _maps) curr = map.Transform(curr);

        return Task.FromResult(curr);
    }

    static IEnumerable<uint> ParseRange(uint start, uint length)
    {
        var vals = new List<uint>();
        for (uint i = 0; i < length; i++)
        {
            vals.Add(start + i);
        }
        return vals;
    }
}

internal record SeedMap
{
    internal record Range
    {
        public uint DestinationStart { get; set; }
        public uint SourceStart { get; set; }
        public uint Length { get; set; }

        public bool Touches(uint value)
            => value >= SourceStart && value <= (SourceStart + Length);

        public uint Transform(uint value)
        {
            if (value >= SourceStart && value <= SourceStart + Length)
            {
                return DestinationStart + (value - SourceStart);
                
            }

            return value;
        }


        internal static Range Parse(string line)
        {
            var vals = line.Split(' ').Select(uint.Parse).ToArray();

            return new Range
            {
                DestinationStart = vals[0],
                SourceStart = vals[1],
                Length = vals[2]
            };
        }
    }

    public string Name { get; set; }

    public readonly HashSet<Range> Ranges;

    public SeedMap(IEnumerable<string> lines)
    {
        Name = lines.First();
        Ranges = lines.Skip(1).Select(Range.Parse).ToHashSet();
    }

    public uint Transform(uint value)
    {
        var touching = Ranges.FirstOrDefault(r => r.Touches(value));

        return touching == null ? value : touching.Transform(value);
    }
}

