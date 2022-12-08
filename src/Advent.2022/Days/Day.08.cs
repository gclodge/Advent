using Advent.Domain;

namespace Advent._2022;

public class TreeMap
{
    private readonly ICollection<string> _input;

    private readonly Grid<int> _grid = new();

    public int CountVisible { get; private set; } = 0;
    public int HighestScenicScore { get; private set; } = int.MinValue;

    public TreeMap(IEnumerable<string> input)
    {
        _input = input.ToList();

        PopulateGrid();
        ScanTrees();
    }

    void PopulateGrid()
    {
        foreach (int y in Enumerable.Range(0, _input.Count))
        {
            string line = _input.ElementAt(y);

            foreach (int x in Enumerable.Range(0, line.Length))
            {
                //< Fuck char->int, all my homies hate char->int
                _grid.AddValue(line[x] - '0', x, y);
            }
        }
    }

    void ScanTrees()
    {
        foreach (int y in _grid.X_Indices)
        {
            foreach (int x in _grid.Y_Indices)
            {
                var (visible, score) = ScanTree(x, y);
                if (!visible) continue;

                CountVisible++;
                HighestScenicScore = Math.Max(HighestScenicScore, score);
            }
        }
    }

    (bool visible, int score) ScanTree(int posX, int posY)
    {
        if (posX == _grid.MinX || posX == _grid.MaxX) return (true, 0);
        if (posY == _grid.MinY || posY == _grid.MaxY) return (true, 0);

        if (!ScanVisibility(posX, posY)) return (false, 0);

        return (true, GetScore(posX, posY));
    }

    bool ScanVisibility(int posX, int posY)
    {
        int height = _grid.GetValue(posX, posY);

        if (_grid.Left(posX).All(x => _grid.GetValue(x, posY) < height)) return true;
        if (_grid.Right(posX).All(x => _grid.GetValue(x, posY) < height)) return true;
        if (_grid.Above(posY).All(y => _grid.GetValue(posX, y) < height)) return true;
        if (_grid.Below(posY).All(y => _grid.GetValue(posX, y) < height)) return true;

        return false;
    }

    int GetScore(int posX, int posY)
    {
        if (posX == _grid.MinX || posX == _grid.MaxX) return 0;
        if (posY == _grid.MinY || posY == _grid.MaxY) return 0;

        int height = _grid.GetValue(posX, posY);

        int left = ScanVisibility(_grid.Left(posX).OrderByDescending(x => x), posY, height);
        int right = ScanVisibility(_grid.Right(posX), posY, height);
        int above = ScanVisibility(_grid.Above(posY).OrderByDescending(y => y), posX, height, isHoriz: false);
        int below = ScanVisibility(_grid.Below(posY), posX, height, isHoriz: false);

        return left * right * above * below;
    }

    int ScanVisibility(IEnumerable<int> idx, int pos, int height, bool isHoriz = true)
    {
        if (!idx.Any()) return 0;

        int count = 0;
        foreach (int i in idx)
        {
            var val = isHoriz ? _grid.GetValue(i, pos) : _grid.GetValue(pos, i);
            if (val < height) count += 1;
            else
            {
                count += 1;
                break;
            }
        }

        return count;
    }
}