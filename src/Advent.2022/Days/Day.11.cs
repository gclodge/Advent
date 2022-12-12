namespace Advent._2022;

class Monkey
{
    public ICollection<long> Items { get; private set; } = new List<long>();

    public long Modulo { get; private set; } = 0;

    public long TestValue { get; private set; } = 0;

    public long? WorryValue { get; private set; } = 0;
    public char WorryOperator { get; private set; } = '+';

    public int TrueTarget { get; private set; } = -1;
    public int FalseTarget { get; private set; } = -1;

    public void Catch(long item)
    {
        Items.Add(item);
    }

    public long AdjustWorry(long item)
    {
        long mult = WorryValue ?? item;

        return WorryOperator switch
        {
            '+' => item + mult,
            '*' => item * mult,
            _ => throw new NotImplementedException($"bruh, what is {WorryOperator}")
        };
    }

    public IEnumerable<(long value, int idx)> InspectItems(bool divide = true)
    {
        List<(long value, int idx)> items = new();

        foreach (var item in Items)
        {
            long adjusted = AdjustWorry(item);
            long worry = divide ? adjusted / 3 : adjusted % Modulo;
            int target = (worry % TestValue == 0) ? TrueTarget : FalseTarget;
            items.Add((worry, target));
        }

        //< Need to remove all items, as we throw 'em
        Items.Clear();

        return items;
    }

    public Monkey SetModulo(long modulo)
    {
        Modulo = modulo;
        return this;
    }

    public static Monkey Parse(IEnumerable<string> lines)
    {
        var items = lines.ElementAt(1).Split(':').Last()
                                      .Split(',').Select(x => x.Trim())
                                      .ToList();

        var ops = lines.ElementAt(2)[23..].Split(' ');
        int? opVal = ops.Last() == "old" ? null : int.Parse(ops.Last());
        char op = ops.First().Single();

        int testVal = int.Parse(lines.ElementAt(3)[21..]);
        int trueTgt = int.Parse(lines.ElementAt(4)[28..]);
        int falseTgt = int.Parse(lines.ElementAt(5)[30..]);

        return new Monkey
        {
            Items = items.Select(long.Parse).ToList(),
            WorryOperator = op,
            WorryValue = opVal,
            TestValue = testVal,
            TrueTarget = trueTgt,
            FalseTarget = falseTgt
        };
    }
}

public class MonkeyManager
{
    private readonly List<string> _input;

    private List<Monkey> _monkeys;
    private long _modulo;

    public MonkeyManager(IEnumerable<string> input)
    {
        _input = input.ToList();
        _monkeys = new();
    }

    public MonkeyManager ParseMonkeys()
    {
        List<string> curr = new();
        for (int i = 0; i < _input.Count; i++)
        {
            var val = _input[i];
            if (!string.IsNullOrEmpty(val))
            {
                curr.Add(val);
                continue;
            }

            _monkeys.Add(Monkey.Parse(curr));
            curr = new();
        }

        if (curr.Any()) _monkeys.Add(Monkey.Parse(curr));

        _modulo = _monkeys.Aggregate(1L, (x, y) => x * y.TestValue);
        _monkeys = _monkeys.Select(m => m.SetModulo(_modulo)).ToList();

        return this;
    }

    public int[] GenerateInspectionHistogram(int rounds = 20, bool divide = true)
    {
        int[] hist = new int[_monkeys.Count];

        foreach (int _ in Enumerable.Range(0, rounds))
        {
            foreach (int i in Enumerable.Range(0, _monkeys.Count))
            {
                hist[i] += _monkeys[i].Items.Count;

                var items = _monkeys[i].InspectItems(divide);

                foreach (var (value, idx) in items) _monkeys[idx].Catch(value);
            }
        }

        return hist;
    }
}