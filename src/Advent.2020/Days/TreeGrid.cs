using MathNet.Numerics.LinearAlgebra;

namespace Advent._2020;

public class TreeGrid
{
    public static readonly Vector<double> Origin = GetVector(0, 0);

    public List<string> Rows { get; private set; }

    public List<Vector<double>> Trees { get; private set; } = new List<Vector<double>>();

    public Vector<double> Position { get; private set; } = Origin.Clone();

    public int Height => Rows.Count;
    public int Width => Rows.First().Length;

    public int X => (int)Position[0];
    public int Y => (int)Position[1];

    public TreeGrid(IEnumerable<string> rows)
    {
        Rows = rows.ToList();
    }

    public void ResetPositionAndTrees()
    {
        Position = Origin.Clone();
        Trees = new List<Vector<double>>();
    }

    public void TraverseAndCountTrees(Vector<double> slope)
    {
        ResetPositionAndTrees();

        while (Y < Height)
        {
            //< Move the position
            if (!MoveWithOverflow(slope))
            {
                break;
            }
            //< Check if we a tree mafk
            else if (IsTree(Position))
            {
                Trees.Add(Position.Clone());
            }
        }
    }

    public bool MoveWithOverflow(Vector<double> slope)
    {
        int newX = X + (int)slope[0];
        if (newX >= Width) newX -= Width;

        int newY = Y + (int)slope[1];
        if (newY >= Height) return false;

        Position = GetVector(newX, newY);
        return true;
    }

    private const char Tree = '#';
    public bool IsTree(Vector<double> pos)
    {
        return Rows[Y].ElementAt(X) == Tree;
    }

    public static Vector<double> GetVector(double x, double y)
    {
        return CreateVector.Dense(new double[] { x, y });
    }
}
