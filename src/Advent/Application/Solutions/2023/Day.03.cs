namespace Advent.Console.Application.Solutions._2023;

public sealed class GearRatios : ISolution
{
    public async Task RunAsync(string input)
    {
        var map = await GearRatioMap.ParseAsync(input);

        await PartOneAsync(map);

        await PartTwoAsync(map);
    }

    static Task PartOneAsync(GearRatioMap map)
    {
        Helper.WriteDivider("Part One");

        var partNums = map.Numbers.Where(n => map.Symbols.Any(s => s.Neighbours(n)))
                                  .ToList();

        var result = partNums.Select(x => int.Parse(x.Value)).Sum();

        Helper.Write($"Found {partNums.Count.Yellow()} 'Part Numbers'");
        Helper.Write($"Result: {result.Yellow()}");

        return Task.CompletedTask;
    }

    static Task PartTwoAsync(GearRatioMap map)
    {
        Helper.WriteDivider("Part Two");

        var gears = map.Symbols.Where(s => s.Value == "*")
                               .Where(g => map.Numbers.Count(n => g.Neighbours(n)) == 2)
                               .ToList();

        int count = 0;
        int sum = 0;
        foreach (var gear in map.Symbols.Where(s => s.Value == "*"))
        {
            var neighs = map.Numbers.Where(gear.Neighbours)
                                    .Select(n => int.Parse(n.Value)).ToList();

            if (neighs.Count == 2)
            {
                int ratio = neighs[0] * neighs[1];
                count += 1;
                sum += ratio;
            }
        }

        Helper.Write($"Found {count.Yellow()} 'Gears'");
        Helper.Write($"Result: {sum.Yellow()}");

        return Task.CompletedTask;
    }
}

internal record GearRatioMap
{
    internal record Number
    {
        public int X_0 { get; set; }
        public int X_1 => X_0 + Length - 1;
        public int Y { get; set; }

        public int Length => Value.Length;
        public string Value { get; set; } = string.Empty;
    }

    internal record Symbol
    {
        public int X { get; set; }
        public int Y { get; set; }

        public string Value { get; set; } = string.Empty;

        public bool Neighbours(Number num)
        {
            var xs = Enumerable.Range(num.X_0, num.Length).ToList();

            return xs.Any(x => Neighbours(x, num.Y));
        }

        public bool Neighbours(int x, int y)
        {
            int dX = Math.Abs(X - x);
            int dY = Math.Abs(Y - y);

            return (dX <= 1 && dY <= 1);
        }
    }

    public List<Number> Numbers { get; set; } = [];
    public List<Symbol> Symbols { get; set; } = [];

    public static async Task<GearRatioMap> ParseAsync(string input)
    {
        var lines = await File.ReadAllLinesAsync(input);
        var map = new GearRatioMap();
         
        foreach (int y in Enumerable.Range(0, lines.Length))
        {
            var sb = new StringBuilder();

            int startX = -1;
            for (int x = 0; x < lines[y].Length; x++)
            {
                char c = lines[y][x];
                
                if (char.IsNumber(c))
                {
                    if (sb.Length == 0) startX = x;
                    sb.Append(c);
                    continue;
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        map.Numbers.Add(new Number
                        {
                            Value = sb.ToString(),
                            X_0 = startX,
                            Y = y
                        });

                        sb.Clear();
                    }

                    if (c != '.')
                    {
                        map.Symbols.Add(new Symbol { Value = c.ToString(), X = x, Y = y });
                    }
                }
            }

            if (sb.Length > 0)
            {
                map.Numbers.Add(new Number
                {
                    Value = sb.ToString(),
                    X_0 = startX,
                    Y = y
                });
            }
        }

        return map;
    }
}