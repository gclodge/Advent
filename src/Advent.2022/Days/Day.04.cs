namespace Advent._2022;

//< Should have just used an interval tree..

public class SectionAssignment
{
    public const char AssignmentDelim = '-';
    
    private readonly string _input;

    public uint Start { get; }
    public uint End { get; }

    public uint Length => End - Start;

    public SectionAssignment(string input)
    {
        _input = input;

        var vals = _input.Split(AssignmentDelim).Select(uint.Parse);
        Start = vals.First();
        End = vals.Last();
    }

    public bool Contains(SectionAssignment other)
    {
        return other.Start >= Start && other.End <= End;
    }

    public bool Overlaps(SectionAssignment other)
    {
        return Math.Max(Start, other.Start) <= Math.Min(End, other.End);
    }
}

public class SectionPair
{
    public const char PairDelim = ',';

    private readonly string _input;

    SectionAssignment A { get; }
    SectionAssignment B { get; }

    public SectionPair(string line)
    {
        _input = line;

        var vals = _input.Split(PairDelim);

        A = new SectionAssignment(vals[0]);
        B = new SectionAssignment(vals[1]);
    }

    public bool Overlapping()
    {
        return B.Overlaps(A);
    }

    public bool SelfContained()
    {
        return B.Contains(A) || A.Contains(B);
    }
}

public class SectionManager
{
    private readonly List<string> _input;

    public ICollection<SectionPair> Pairs { get; }

    public ICollection<SectionPair> SelfContained => Pairs.Where(x => x.SelfContained()).ToList();
    public ICollection<SectionPair> Overlapping => Pairs.Where(x => x.Overlapping()).ToList();

    public SectionManager(IEnumerable<string> input)
    {
        _input = input.ToList();

        Pairs = _input.Select(x => new SectionPair(x)).ToList();
    }
}