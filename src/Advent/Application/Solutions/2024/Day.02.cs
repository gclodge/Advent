namespace Advent.Console.Application.Solutions._2024;

public sealed class RedNosedReports : ISolution
{
    internal record ReportRecord
    {
        public List<int> Values { get; set; } = [];

        public bool IsSafe() => CheckSafety(Values);

        public bool IsSafeWithDampener()
        {
            foreach (int i in Enumerable.Range(0, Values.Count))
            {
                var subset = Values.Where((v, idx) => idx != i).ToList();
                if (CheckSafety(subset)) return true;
            }

            return false;
        }

        public static bool CheckSafety(List<int> values)
        {
            var deltas = Enumerable.Range(0, values.Count - 1).Select(i => values[i + 1] - values[i]);

            if (deltas.Any(x => Math.Abs(x) == 0 || Math.Abs(x) > 3)) return false;

            var bPos = deltas.All(d => d > 0);
            var bNeg = deltas.All(d => d < 0);

            //< If one of these is true, we gucci
            if (bPos || bNeg) return true;

            //< We fukt
            return false;
        }

        public static ReportRecord Parse(string line)
        {
            var vals = line.Split(' ').Select(int.Parse).ToList();

            return new ReportRecord { Values = vals };
        }
    }

    private List<ReportRecord> _reports = [];

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

        _reports = lines.Select(ReportRecord.Parse).ToList();

        Helper.Write($"\t .. Loaded {_reports.Count.Yellow()} 'reports'");
    }

    Task PartOneAsync()
    {
        var safe = _reports.Where(x => x.IsSafe()).ToList();

        Helper.Write($" - Part One: {safe.Count.Yellow()}");

        return Task.CompletedTask;
    }

    Task PartTwoAsync()
    {
        var safe = _reports.Where(x => x.IsSafeWithDampener()).ToList();

        Helper.Write($" - Part Two: {safe.Count.Yellow()}");

        return Task.CompletedTask;
    }
}
