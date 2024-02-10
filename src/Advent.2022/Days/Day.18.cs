using Advent.Domain;

namespace Advent._2022;

public class LavaDropletScanner
{
    private readonly ICollection<string> _input;
    private readonly ICollection<(int x, int y, int z)> _positions;

    private readonly IDictionary<int, Grid<(int x, int y, int z)>> _grids;

    public LavaDropletScanner(IEnumerable<string> input)
    {
        _input = input.ToList();
        _positions = _input.Select(line => ParsePosition(line)).ToList();

        _grids = new Dictionary<int, Grid<(int x, int y, int z)>>();
        foreach (var (x, y, z) in _positions)
        {
            if (!_grids.ContainsKey(z)) _grids.Add(z, new Grid<(int x, int y, int z)>());

            _grids[z].AddValue((x, y, z), x, y);
        }
    }

    static (int x, int y, int z) ParsePosition(string line)
    {
        var vals = line.Split(',').Select(int.Parse).ToArray();
        return (vals[0], vals[1], vals[2]);
    }

    const int TotalSides = 6;
    public int CountSharedSides()
    {
        int count = 0;
        foreach (var pos in _positions)
        {
            int horiz = CheckHorizontalNeighbours(pos);
            int vert = CheckVerticalNeighbours(pos);

            count += TotalSides - (horiz + vert);
        }

        return count;
    }

    int CheckHorizontalNeighbours((int x, int y, int z) pos)
    {
        int count = 0;

        if (_grids[pos.z].Contains(pos.x - 1, pos.y)) count += 1;
        if (_grids[pos.z].Contains(pos.x + 1, pos.y)) count += 1;
        if (_grids[pos.z].Contains(pos.x, pos.y + 1)) count += 1;
        if (_grids[pos.z].Contains(pos.x, pos.y - 1)) count += 1;

        return count;
    }

    int CheckVerticalNeighbours((int x, int y, int z) pos)
    {
        int count = 0;
        int aboveZ = pos.z + 1;
        int belowZ = pos.z - 1;

        if (_grids.ContainsKey(aboveZ) && _grids[aboveZ].Contains(pos.x, pos.y)) count += 1;
        if (_grids.ContainsKey(belowZ) && _grids[belowZ].Contains(pos.x, pos.y)) count += 1;

        return count;
    }

    public int CountExternalEdges()
    {
        //< Need to flood fill the exterior on each Z - then count the boundary edges?



        throw new NotImplementedException();
    }

    static IEnumerable<(int x, int y, int z)> GenerateNeighbours((int x, int y, int z) pos)
    {
        return new[]
        {
            (pos.x - 1, pos.y, pos.z),
            (pos.x + 1, pos.y, pos.z),
            (pos.x, pos.y + 1, pos.z),
            (pos.x, pos.y - 1, pos.z),
            (pos.x, pos.y, pos.z + 1),
            (pos.x, pos.y, pos.z - 1)
        };
    }
}