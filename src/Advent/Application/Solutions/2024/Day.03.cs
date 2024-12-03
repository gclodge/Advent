using System.Text.RegularExpressions;

namespace Advent.Console.Application.Solutions._2024;

public sealed class TobogganMultiplier : ISolution
{
    internal record Instruction
    {
        public int A { get; set; }
        public int B { get; set; }

        public int Position { get; set; }

        public int Result => A * B;

        public static Instruction Parse(string line, int pos)
        {
            var parts = line.Split(',');

            var a = int.Parse(parts[0].Replace("mul(", ""));
            var b = int.Parse(parts[1].Replace(")", ""));

            return new Instruction { A = a, B = b, Position = pos };
        }

        public bool IsEnabled(IEnumerable<DisableRange> ranges)
            => !ranges.Any(r => r.Covers(this));
    }

    internal record DisableRange
    {
        public int A { get; set; }
        public int B { get; set; }

        public bool Covers(Instruction ins) => A <= ins.Position && B >= ins.Position;
    }

    private readonly string _regex = @"mul\(\d{1,3},\d{1,3}\)";

    public async Task RunAsync(string input)
    {
        //var data = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

        Helper.Write($"Loading input from: {input.Yellow()}");

        var data = await File.ReadAllTextAsync(input);

        var matches = Regex.Matches(data, _regex);

        var ins = matches.Select(x => Instruction.Parse(x.Value, x.Index)).ToList();

        Helper.Write($" - Found {ins.Count.Yellow()} instructions");

        Helper.Write($"Part One: {ins.Sum(x => x.Result).Yellow()}");

        var ranges = await LoadDisableRanges(data);

        var enabled = ins.Where(i => i.IsEnabled(ranges)).ToList();

        Helper.Write($" - Found {enabled.Count.Yellow()} 'enabled' instructions");

        Helper.Write($"Part Two: {enabled.Sum(x => x.Result).Yellow()}");
    }

    static Task<List<DisableRange>> LoadDisableRanges(string data)
    {
        var ranges = new List<DisableRange>();

        string dont_val = "don't()";
        string do_val = "do()";

        int a = data.IndexOf(dont_val);

        if (a == -1) return Task.FromResult(ranges);

        while (a != -1)
        {
            int b = data.IndexOf(do_val, a + 1);

            if (b == -1)
            {
                ranges.Add(new DisableRange { A = a, B = data.Length - 1 });
                return Task.FromResult(ranges);
            }

            ranges.Add(new DisableRange { A = a, B = b });

            a = data.IndexOf(dont_val, b + 1);
        }

        return Task.FromResult(ranges);
    }
}
