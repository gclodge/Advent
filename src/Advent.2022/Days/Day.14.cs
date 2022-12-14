using Advent.Domain;

namespace Advent._2022;

public class Regolith
{
    public const char Rock = '#';
    public const char Sand = 'o';

    private readonly ICollection<string> _input;
    private Grid<char> _grid = new();

    public Regolith(IEnumerable<string> input)
    {
        _input = input.ToList();
    }

    #region Grid Initialization
    const string Split = " -> ";
    const char Delim = ',';
    void InitializeGrid(bool createFloor = false)
    {
        _grid = new();

        foreach (var line in _input)
        {
            var rocks = line.Split(Split);

            foreach (int i in Enumerable.Range(0, rocks.Length - 1))
            {
                var A = rocks[i];
                var B = rocks[i + 1];

                FillSpan(A, B);
            }
        }

        if (createFloor)
        {
            int floorY = _grid.MaxY + 2;
            int xMin = Start.X - floorY * 2;
            int xMax = Start.X + floorY * 2;

            FillHorizontal(floorY, xMin, xMax);
        }
    }

    void FillSpan(string A, string B)
    {
        var a = Parse(A);
        var b = Parse(B);

        int dY = b.y - a.y;
        int dX = b.x - a.x;

        if (dY != 0) FillVertical(a.x, a.y, b.y);
        if (dX != 0) FillHorizontal(a.y, a.x, b.x);
    }

    void FillVertical(int x, int yStart, int yEnd)
    {
        int len = yEnd - yStart;
        foreach (int dy in Enumerable.Range(0, Math.Abs(len) + 1))
        {
            int y = (len < 0) ? yStart - dy : yStart + dy;
            if (!_grid.Contains(x, y)) _grid.AddValue(Rock, x, y);
        }
    }

    void FillHorizontal(int y, int xStart, int xEnd)
    {
        int len = xEnd - xStart;
        foreach (int dx in Enumerable.Range(0, Math.Abs(len) + 1))
        {
            int x = (len < 0) ? xStart - dx : xStart + dx;
            if (!_grid.Contains(x, y)) _grid.AddValue(Rock, x, y);
        }
    }

    static (int x, int y) Parse(string pos)
    {
        var val = pos.Split(Delim);
        return (int.Parse(val[0]), int.Parse(val[1]));
    }
    #endregion

    const int Retries = 5;
    static readonly (int X, int Y) Start = (500, 0);
    public int PartOne()
    {
        InitializeGrid();

        int count = 0;
        int voidCount = 0;

        while (true)
        {
            if (voidCount == Retries) break;

            var (x, y) = DropSand();
            if (y != -1)
            {
                count++;
                _grid.AddValue(Sand, x, y);

                voidCount = 0;
                continue;
            }

            voidCount++;
        }

        return count;
    }

    const int MaxTries = 100000;
    public int PartTwo()
    {
        InitializeGrid(createFloor: true);

        int count = 0;
        int tries = 0;

        while (true)
        {
            if (tries > MaxTries) return -1;

            var pos = DropSand();

            count++;
            tries++;

            if (pos == Start) break;

            _grid.AddValue(Sand, pos.x, pos.y);
        }

        return count;
    }

    (int x, int y) DropSand()
    {
        //< Drop down, then move, then drop
        var stack = new Stack<(int x, int y)>();
        stack.Push(Start);

        while (stack.Count > 0)
        {
            //< Given current pos - scan down for anything that blocks us
            var pos = stack.Pop();

            var blockY = ScanDown(pos.x, pos.y);
            if (blockY == -1) return (-1, -1);

            (int x, int y) L = (pos.x - 1, blockY);
            (int x, int y) R = (pos.x + 1, blockY);

            //< If either diagonal is open, try those, otherwise return pos (at rest)
            if (!_grid.Contains(L.x, L.y))
            {
                stack.Push(L);
                continue;
            }

            if (!_grid.Contains(R.x, R.y))
            {
                stack.Push(R);
                continue;
            }

            //< Want to return the Y-coordinate above where we got blocked
            return (pos.x, blockY - 1);
        }

        return (-1, -1);
    }

    int ScanDown(int x, int y)
    {
        int len = _grid.MaxY - y;
        foreach (int dy in Enumerable.Range(0, len + 1))
        {
            int currY = y + dy;
            if (_grid.Contains(x, currY)) return currY;
        }
        return -1;
    }
}