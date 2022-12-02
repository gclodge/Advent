namespace Advent._2022;

//< A, X -> Rock
//< B, Y -> Paper
//< C, Z -> Scissors

//< Second column unknown, delim in space

enum RpsMove
{
    Rock,
    Paper,
    Scissors
}

public class RockPaperScissors
{
    public const char Delim = ' ';
    private readonly List<string> _input;

    public RockPaperScissors(IEnumerable<string> input)
    {
        _input = input.ToList();
    }

    public long SolvePartOne()
    {
        return _input.Select(PlayPartOne).Sum();
    }

    public long SolvePartTwo()
    {
        return _input.Select(PlayPartTwo).Sum();
    }

    static long PlayPartOne(string line)
    {
        var moves = line.Split(Delim).Select(ParsePartOne).ToList();

        RpsMove mine = moves[1];
        RpsMove theirs = moves[0];

        return GetScore(mine, theirs);
    }

    static long PlayPartTwo(string line)
    {
        //< X lose, Y draw, Z win
        var cmds = line.Split(Delim);

        RpsMove theirs = ParsePartOne(cmds[0]);
        RpsMove mine = ParsePartTwo(cmds[1], theirs);

        return GetScore(mine, theirs);
    }

    static long GetScore(RpsMove mine, RpsMove theirs)
    {
        long resScore = GetResultScore(mine, theirs);
        long moveScore = GetMoveScore(mine);

        return resScore + moveScore;
    }

    static RpsMove ParsePartOne(string val)
    {
        return val.ToLower() switch
        {
            "a" => RpsMove.Rock,
            "x" => RpsMove.Rock,
            "b" => RpsMove.Paper,
            "y" => RpsMove.Paper,
            "c" => RpsMove.Scissors,
            "z" => RpsMove.Scissors,
            _ => throw new ArgumentException($"What does {val} even mean, man?")
        };
    }

    static RpsMove ParsePartTwo(string val, RpsMove theirs)
    {
        return val.ToLower() switch
        {
            "x" => GetCorrespondingMove(theirs, theyWin: true),
            "y" => theirs,
            "z" => GetCorrespondingMove(theirs, theyWin: false),
            _ => throw new ArgumentException($"What does {theirs} even mean, man?")
        };
    }

    static RpsMove GetCorrespondingMove(RpsMove theirs, bool theyWin)
    {
        if (theirs == RpsMove.Rock) return theyWin ? RpsMove.Scissors : RpsMove.Paper;
        if (theirs == RpsMove.Paper) return theyWin ? RpsMove.Rock : RpsMove.Scissors;
        if (theirs == RpsMove.Scissors) return theyWin ? RpsMove.Paper : RpsMove.Rock;

        throw new Exception($"How'd you even get here, my dude?");
    }

    const long WinScore = 6;
    const long DrawScore = 3;
    const long LossScore = 0;
    static long GetResultScore(RpsMove mine, RpsMove theirs)
    {
        if (mine == theirs) return DrawScore;

        if (mine == RpsMove.Rock && theirs == RpsMove.Scissors) return WinScore;
        if (mine == RpsMove.Paper && theirs == RpsMove.Rock) return WinScore;
        if (mine == RpsMove.Scissors && theirs == RpsMove.Paper) return WinScore;

        return LossScore;
    }

    static long GetMoveScore(RpsMove move)
    {
        return move switch
        {
            RpsMove.Rock => 1,
            RpsMove.Paper => 2,
            RpsMove.Scissors => 3,
            _ => throw new ArgumentException($"What does {move} even mean, man?")
        };
    }
}