using Advent.Domain;
namespace Advent._2022;

public class CraneMovement
{
    const char Delim = ' ';

    public int Amount { get;}
    public int From { get; }
    public int To { get; }

    public CraneMovement(string line)
    {
        var vals = line.Split(Delim);

        Amount = int.Parse(vals[1]);
        From = int.Parse(vals[3]);
        To = int.Parse(vals[5]);
    }
}

public class CrateStack
{
    public int Count => Stacks.Count;
    public IDictionary<int, Stack<char>> Stacks { get; private set; } = new Dictionary<int, Stack<char>>();

    public void Push(int idx, char val)
    {
        Stacks[idx].Push(val);
    }

    public string GetMessage()
    {
        var vals = Stacks.OrderBy(x => x.Key)
                         .Select(x => x.Value.Peek()).ToArray();

        return new string(vals);
    }

    public void Apply(CraneMovement movement)
    {
        foreach (int _ in Enumerable.Range(0, movement.Amount))
        {
            char crate = Stacks[movement.From].Pop();
            Stacks[movement.To].Push(crate);
        }
    }

    public void ApplyPartTwo(CraneMovement movement)
    {
        var crates = Enumerable.Range(0, movement.Amount)
                               .Select(_ => Stacks[movement.From].Pop())
                               .Reverse();

        foreach (var crate in crates) Stacks[movement.To].Push(crate);
    }

    static readonly HashSet<char> Skippable = new() { (char)32, '[', ']' };
    public static CrateStack Parse(IEnumerable<string> lines)
    {
        var stacks = new CrateStack();
        var crateLines = lines.Take(lines.Count() - 1)
                              .Reverse() //< Have to reverse to stack in correct order
                              .ToList();

        var idxStr = ParseString(lines.Last());
        var idxMap = idxStr.ToDictionary(x => x.idx, x => x.val - '0');

        foreach (var vals in crateLines)
        {
            foreach (var tup in ParseString(vals))
            {
                int idx = idxMap[tup.idx];
                if (!stacks.Stacks.ContainsKey(idx)) stacks.Stacks[idx] = new Stack<char>();

                stacks.Push(idx, tup.val);
            }
        }

        return stacks;
    }

    static List<(char val, int idx)> ParseString(string str)
    {
        var values = new List<(char val, int idx)>();
        for (int i = 0; i < str.Length; i++)
        {
            if (Skippable.Contains(str[i])) continue;

            values.Add((str[i], i));
        }
        return values;
    }
}

public class Crane
{
    private readonly ICollection<string> _input;
    private readonly ICollection<string> _stackInput;
    private readonly ICollection<string> _mvmntInput;

    public CrateStack Stack { get; private set; }
    public ICollection<CraneMovement> Movements { get;}

    public Crane(IEnumerable<string> inputs)
    {
        _input = inputs.ToList();

        var split = Functions.SplitByByElement(_input, "");
        _stackInput = split.First().ToList();
        _mvmntInput = split.Last().ToList();

        Stack = CrateStack.Parse(_stackInput);
        Movements = _mvmntInput.Select(x => new CraneMovement(x)).ToList();
    }

    public void Reset()
    {
        Stack = CrateStack.Parse(_stackInput);
    }

    public void MoveCrates()
    {
        foreach (var movement in Movements) Stack.Apply(movement);
    }

    public void MoveCratesPartTwo()
    {
        foreach (var movement in Movements) Stack.ApplyPartTwo(movement);
    }

    public string GetMessage()
    {
        return Stack.GetMessage();
    }
}