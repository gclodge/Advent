using System.Text;
using Advent.Domain;

namespace Advent._2022;

public class HillNode
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public char Elevation { get; private set; }

    public static HillNode Create(int x, int y, char elev)
    {
        return new HillNode
        {
            X = x,
            Y = y,
            Elevation = elev
        };
    }

    public override string ToString() => $"({X}, {Y}): {Elevation}";
}

public class HillClimber
{
    public const char StartChar = 'S';
    public const char EndChar = 'E';
    public const char MinElevChar = 'a';

    private readonly ICollection<string> _input;
    private readonly ICollection<(int x, int y)> _starts 
        = new List<(int x, int y)>();

    private readonly IDictionary<(int x, int y), HillNode> _elevMap 
        = new Dictionary<(int x, int y), HillNode>();

    static readonly (int, int)[] Neighbours = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

    public (int x, int y) Start { get; private set; }
    public (int x, int y) End { get; private set; }

    public HillClimber(IEnumerable<string> input)
    {
        _input = input.ToList();
        ParseInputGrid();
    }

    void ParseInputGrid()
    {
        foreach (int y in Enumerable.Range(0, _input.Count))
        {
            string line = _input.ElementAt(y);
            foreach (int x in Enumerable.Range(0, line.Length))
            {
                char c = line[x];
                var node = HillNode.Create(x, y, c);
                _elevMap.Add((x, y), node);

                if (c == StartChar)
                {
                    Start = (x, y);
                    _starts.Add(Start);
                }
                else if (c == EndChar) End = (x, y);
                else if (c == MinElevChar) _starts.Add((x, y));
            }
        }
    }

    private static int GetEffectiveValue(char c) => c switch
    {
        'S' => 'a',
        'E' => 'z',
        _ => c
    };

    private static int ComputeHeuristic(HillNode curr, HillNode end)
    {
        return Math.Abs(curr.X - end.X) + Math.Abs(curr.Y - end.Y);
    }

    private static int? ComputeEdgeWeight(HillNode curr, HillNode neigh)
    {
        var nEff = GetEffectiveValue(neigh.Elevation);
        var cEff = GetEffectiveValue(curr.Elevation);

        int? score = nEff > cEff + 1 ? null : (int?)cEff - nEff + 1;

        return score;
    }

    public IEnumerable<HillNode> FindPath(bool partOne = true)
    {
        HillNode target = _elevMap[End];
        if (partOne) return FindPathAStar(_elevMap[Start], target);

        var paths = _starts.Select(p => FindPathAStar(_elevMap[p], target))
                           .Where(p => p.Any()).ToList();

        return paths.OrderBy(p => p.Count()).First();
    }

    public IEnumerable<HillNode> FindPathAStar(HillNode start, HillNode target)
    {
        var pq = new PriorityQueue<HillNode, int>();
        pq.Enqueue(start, 0);

        Dictionary<HillNode, HillNode> from = new();
        Dictionary<HillNode, int> gScore = new()
        {
            { start, 0 }
        };
        Dictionary<HillNode, int> fScore = new()
        {
            { start, ComputeHeuristic(start, target) }
        };

        while (pq.Count > 0)
        {
            var curr = pq.Dequeue();
            if (curr == target) return ReconstructPath(from, curr);

            foreach (var neigh in GetNeighbours(curr).ToList())
            {
                int? weight = ComputeEdgeWeight(curr, neigh);
                if (weight is null) continue;

                int tentativeScore = gScore[curr] + weight.Value;
                if (!gScore.ContainsKey(neigh) || tentativeScore < gScore[neigh])
                {
                    from[neigh] = curr;
                    gScore[neigh] = tentativeScore;
                    fScore[neigh] = tentativeScore + ComputeHeuristic(neigh, target);
                    pq.Enqueue(neigh, fScore[neigh]);
                }
            }
        }

        return new List<HillNode>(); //< No path, return empty
    }

    private static List<HillNode> ReconstructPath(Dictionary<HillNode, HillNode> from, HillNode curr)
    {
        List<HillNode> path = new() { curr };
        while (from.ContainsKey(curr))
        {
            curr = from[curr];
            path.Add(curr);
        }
        path.Reverse();
        return path;
    }

    IEnumerable<HillNode> GetNeighbours(HillNode node)
    {
        foreach ((int x, int y) in Neighbours)
        {
            var pos = (node.X + x, node.Y + y);
            if (_elevMap.ContainsKey(pos)) yield return _elevMap[pos];
        }
    }
}