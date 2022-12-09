namespace Advent._2022;

internal record Position(int X, int Y)
{
    internal Position Move(Movement move)
    {
        return move.Direction switch
        {
            "R" => new Position(X + 1, Y),
            "L" => new Position(X - 1, Y),
            "U" => new Position(X, Y + 1),
            "D" => new Position(X, Y - 1),
            _ => throw new NotImplementedException()
        };
    }

    internal Position MoveToHead(Position head)
    {
        var d = (head.X - X, head.Y - Y);

        return d switch
        {
            (2, 2)   => new Position(X + 1, Y + 1),
            (-2, -2) => new Position(X - 1, Y - 1),
            (-2, 2)  => new Position(X - 1, Y + 1),
            (2, -2)  => new Position(X + 1, Y - 1),
            (var x, -2) => new Position(X + x, Y - 1),
            (var x, 2) => new Position(X + x, Y + 1),
            (-2, var y) => new Position(X - 1, Y + y),
            (2, var y) => new Position(X + 1, Y + y),
            _ => this
        };
    }
}

internal record Movement(string Direction, int Magnitude)
{
    internal static Movement Parse(string line)
    {
        var vals = line.Split(' ');
        return new Movement(vals[0], int.Parse(vals[1]));
    }
}

public class Rope
{
    private readonly ICollection<string> _input;
    private readonly ICollection<Movement> _movements;

    private readonly HashSet<Position> _tailPos = new();

    private Position _head = new(0, 0);
    private Position[] _tails = Array.Empty<Position>();

    public int UniqueTailPositions => _tailPos.Count;

    public Rope(IEnumerable<string> input)
    {
        _input = input.ToList();
        _movements = _input.Select(x => Movement.Parse(x)).ToList();
    }

    public Rope ApplyMovements(int ropeSize = 2)
    {
        _tailPos.Add(_head);
        _tails = Enumerable.Range(0, ropeSize - 1).Select(_ => new Position(0, 0)).ToArray();

        foreach (var movement in _movements)
        {
            foreach (int _ in Enumerable.Range(0, movement.Magnitude))
            {
                _head = _head.Move(movement);

                foreach (int i in Enumerable.Range(0, _tails.Length))
                {
                    Position head = i == 0 ? _head : _tails[i - 1];
                    _tails[i] = _tails[i].MoveToHead(head);
                }

                _tailPos.Add(_tails.Last());
            }
        }

        return this;
    }
}