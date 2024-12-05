namespace Advent.Console.Application.Solutions._2024;

public sealed class PrintQueue : ISolution
{
    private List<PageOrderingRule> _rules = [];
    private List<PageUpdate> _updates = [];

    private readonly Dictionary<int, int> _precedenceMap = [];
    private readonly Dictionary<int, List<int>> _valueMap = [];

    public async Task RunAsync(string input)
    {
        await LoadInputAsync(input);

        await PartOneAsync();

        await PartTwoAsync();
    }

    async Task LoadInputAsync(string input)
    {
        var lines = await File.ReadAllLinesAsync(input);

        int idx = lines.ToList().IndexOf(string.Empty);

        _rules = lines.Take(idx).Select(PageOrderingRule.Parse).ToList();
        _updates = lines.Skip(idx + 1).Select(PageUpdate.Parse).ToList();
    }

    Task PartOneAsync()
    {
        Helper.WriteDivider("Part One");
        Helper.Write($"Checking {_updates.Count.Yellow()} updates against {_rules.Count.Yellow()} rules.");

        var ordered = _updates.Where(u => u.IsOrdered(_rules)).ToList();

        Helper.Write($"Found {ordered.Count.Yellow()} ordered udpates.");

        var sum = ordered.Sum(x => x.MiddleValue);

        Helper.Write($"Sum of middles: {sum.Yellow()}");

        return Task.CompletedTask;
    }

    Task PartTwoAsync()
    {
        Helper.WriteDivider("Part Two");
        Helper.Write($"Checking {_updates.Count.Yellow()} updates against {_rules.Count.Yellow()} rules.");

        var invalid = _updates.Where(u => !u.IsOrdered(_rules)).ToList();

        Helper.Write($"Found {invalid.Count.Yellow()} incorrectly ordered udpates.");

        var valid = invalid.Select(i => i.OrderValues(_rules)).ToList();

        var sum = valid.Sum(x => x.MiddleValue);

        Helper.Write($"Sum of middles: {sum.Yellow()}");

        return Task.CompletedTask;
    }
}

public record PageOrderingRule
{
    public int Value { get; set; }

    public int Precedes { get; set; }

    public bool ContainsViolations(List<int> values)
    {
        if (!values.Contains(Value)) return false;

        foreach (int i in Enumerable.Range(0, values.Count - 1))
        {
            if (values[i] == Precedes && values[i + 1] == Value) return true;
        }

        return false;
    }

    public static PageOrderingRule Parse(string line)
    {
        var vals = line.Split('|').Select(int.Parse).ToArray();

        return new PageOrderingRule
        {
            Value = vals[0],
            Precedes = vals[1]
        };
    }
}

public record PageUpdate
{
    public List<int> Values { get; set; } = [];

    public int MiddleValue => Values[Values.Count / 2];

    public bool IsOrdered(IEnumerable<PageOrderingRule> rules)
    {
        return rules.All(r => !r.ContainsViolations(Values));
    }

    public PageUpdate OrderValues(IEnumerable<PageOrderingRule> rules)
    {
        var vals = Values.ToList();

        var violating = rules.Where(r => r.ContainsViolations(vals)).ToList();

        while (violating.Count > 0)
        {
            foreach (var rule in violating)
            {
                int v_idx = vals.IndexOf(rule.Value);
                int p_idx = vals.IndexOf(rule.Precedes);

                vals[p_idx] = rule.Value;
                vals[v_idx] = rule.Precedes;
            }

            violating = rules.Where(r => r.ContainsViolations(vals)).ToList();
        }

        return this with { Values = vals };
    }

    public static PageUpdate Parse(string line)
    {
        var vals = line.Split(',').Select(int.Parse).ToList();

        return new PageUpdate { Values = vals };
    }
}