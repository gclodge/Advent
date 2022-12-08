namespace Advent.Domain;

public class Grid<T>
{
    private readonly Dictionary<int, Dictionary<int, T>> Map = new();

    public int MinX { get; private set; } = int.MaxValue;
    public int MinY { get; private set; } = int.MaxValue;
    public int MaxX { get; private set; } = int.MinValue;
    public int MaxY { get; private set; } = int.MinValue;

    public int Width => (MaxX - MinX);
    public int Height => (MaxY - MinY);

    public IEnumerable<int> X_Indices => Enumerable.Range(MinX, Width + 1);
    public IEnumerable<int> Y_Indices => Enumerable.Range(MinY, Height + 1);

    public IEnumerable<int> Left(int posX) => X_Indices.Where(x => x < posX);
    public IEnumerable<int> Right(int posX) => X_Indices.Where(x => x > posX);
    public IEnumerable<int> Below(int posY) => Y_Indices.Where(y => y > posY);
    public IEnumerable<int> Above(int posY) => Y_Indices.Where(y => y < posY);

    public bool Contains(int x, int y)
    {
        if (!Map.ContainsKey(y)) return false;
        if (!Map[y].ContainsKey(x)) return false;

        return true;
    }

    public void AddValue(T val, int x, int y)
    {
        CheckExtrema(x, y);

        if (!Map.ContainsKey(y)) Map.Add(y, new Dictionary<int, T>());

        Map[y].Add(x, val);
    }

    public void SetValue(T val, int x, int y)
    {
        if (!Contains(x, y)) throw new ArgumentException($"Grid doesnt not contain ({x},{y})");

        Map[y][x] = val;
    }

    public T GetValue(int x, int y)
    {
        if (!Contains(x, y)) throw new ArgumentException($"Grid doesnt not contain ({x},{y})");

        return Map[y][x];
    }

    void CheckExtrema(int x, int y)
    {
        MinX = Math.Min(MinX, x);
        MinY = Math.Min(MinY, y);
        MaxX = Math.Max(MaxX, x);
        MaxY = Math.Max(MaxY, y);
    }
}