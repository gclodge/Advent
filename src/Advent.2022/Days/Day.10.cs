using System.Text;
using Advent.Domain;

namespace Advent._2022;

public class CathodeRayTube
{
    public const int Width = 40;
    public const int Height = 6;

    private readonly ICollection<string> _input;
    private readonly ICollection<(string type, int val)> _cmds;

    private readonly HashSet<int> _active = new();
    private readonly HashSet<int> _steps = new[] { 20, 60, 100, 140, 180, 220 }.ToHashSet();
    private readonly IDictionary<int, int> _sigSteps = new Dictionary<int, int>();

    public int Register { get; private set; } = 1;
    public int Step { get; private set; } = 1;
    public int Position => Step - 1;

    public int SignificantSignalSum => _sigSteps.Values.Sum();

    public CathodeRayTube(IEnumerable<string> input)
    {
        _input = input.ToList();
        _cmds = _input.Select(ParseCommand).ToList();
    }

    public CathodeRayTube ProcessSteps()
    {
        foreach (var (type, val) in _cmds)
        {
            foreach (int _ in Enumerable.Range(0, GetNumSteps(type)))
            {
                if (_steps.Contains(Step)) _sigSteps[Step] = GetSignalStrength(Register, Step);

                if (IsActive(Position, Register)) _active.Add(Position);

                Step++;
            }

            if (type == "addx") Register += val;
        }

        return this;
    }

    static bool IsActive(int pos, int reg)
    {
        return Math.Abs(reg - (pos % Width)) <= 1;
    }

    static int GetSignalStrength(int reg, int step)
    {
        return reg * step;
    }

    static int GetNumSteps(string type)
    {
        return type switch
        {
            "noop" => 1,
            "addx" => 2,
            _ => throw new NotImplementedException($"What even is {type}")
        };
    }

    static (string cmd, int val) ParseCommand(string line)
    {
        var vals = line.Split(' ');
        return (vals[0], vals.Length > 1 ? int.Parse(vals[1]) : 0);
    }

    public void Save(string file)
    {
        var sb = new StringBuilder();

        foreach (var y in Enumerable.Range(0, Height))
        {
            foreach (var x in Enumerable.Range(0, Width))
            {
                int pos = x + (Width * y);
                sb.Append(_active.Contains(pos) ? '#' : '.');
            }
            sb.Append(Environment.NewLine);
        }

        File.WriteAllText(file, sb.ToString());
    }
}