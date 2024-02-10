using Themis.Geometry;
using Themis.Geometry.Lines;
using Themis.Index.QuadTree;
//using MathNet.Numerics.LinearAlgebra;

namespace Advent._2022;

public class EmergencySensorSystem
{
    class Sensor
    {
        public long X { get; private set; }
        public long Y { get; private set; }

        public long BeaconX { get; private set; }
        public long BeaconY { get; private set; }

        public long ManhattanDist => Math.Abs(BeaconX - X) + Math.Abs(BeaconY - Y);

        public long MinXAt(long y) => X - ManhattanDist + Math.Abs(Y - y);
        public long MaxXAt(long y) => X + ManhattanDist - Math.Abs(Y - y);
        public long MinYAt(long x) => Y - ManhattanDist + Math.Abs(X - x);
        public long MaxYAt(long x) => Y + ManhattanDist - Math.Abs(X - x);

        public LineSegment[] GetEdges()
        {
            var btm = new long[2] { X, MinYAt(X) };
            var top = new long[2] { X, MaxYAt(X) };
            var left = new long[2] { MinXAt(Y), Y };
            var right = new long[2] { MaxXAt(X), Y };

            return new LineSegment[4]
            {
                Generate(left, top),
                Generate(top, right),
                Generate(right, btm),
                Generate(btm, left)
            };
        }

        static LineSegment Generate(long[] min, long[] max)
        {
            var A = min.Select(l => (double)l).ToVector();
            var B = max.Select(l => (double)l).ToVector();

            return new LineSegment(A, B);
        }

        public static Sensor Generate(string line)
        {
            var vals = line.Split(':');

            var sensorVals = vals[0].Split(',');
            var beaconVals = vals[1].Split(',');

            string sensX = sensorVals[0][12..];
            string sensY = sensorVals[1][3..];

            string beacX = beaconVals[0][24..];
            string beacY = beaconVals[1][3..];

            return new Sensor
            {
                X = long.Parse(sensX),
                Y = long.Parse(sensY),
                BeaconX = long.Parse(beacX),
                BeaconY = long.Parse(beacY)
            };
        }
    }

    private readonly ICollection<string> _input;
    private readonly ICollection<Sensor> _sensors;

    public EmergencySensorSystem(IEnumerable<string> input)
    {
        _input = input.ToList();
        _sensors = _input.Select(x => Sensor.Generate(x)).ToList();
    }

    public long CountBlockedPositions(int Y)
    {
        var ranges = _sensors.Select(s => new long[] { s.MinXAt(Y), s.MaxXAt(Y) }).ToList();
        ranges.Sort((x, y) => x[0].CompareTo(y[0]));

        (_, long[] res) = Merge(ranges);

        long count = Math.Abs(res[1] - res[0]);
        return count;
    }

    public long ThemisFindFrequency(long maxValue)
    {
        var edges = _sensors.SelectMany(s => s.GetEdges()).ToList();

        var tree = new QuadTree<LineSegment>();
        edges.ForEach(seg => tree.Add(seg, seg.Envelope));

        foreach (var edge in edges)
        {
            var query = tree.QueryDistinct(edge.Envelope).ToArray();

            //< Now what? Look for intersection..
        }


        //< Foreach seg, query tree to get covering segments, get intersections, bounce if point found!

        throw new NotImplementedException();
    }


    public long FindTuningFrequency(long maxY)
    {
        for (long y = 0; y <= maxY; y++)
        {
            var bounds = GetBoundsAt(y, maxY);

            (bool merged, long[] res) = Merge(bounds);

            if (!merged || res[0] != 0 || res[1] != maxY) return (res[1] + 1) * 4000000 + y;
        }

        return -1;
    }

    static (bool merged, long[] res) Merge(List<long[]> bounds)
    {
        bool merged = true;

        while (merged && bounds.Count > 1)
        {
            merged = false;

            if (bounds[0][0] <= bounds[1][0] && bounds[0][1] >= bounds[1][0])
            {
                bounds[0][1] = Math.Max(bounds[0][1], bounds[1][1]);
                bounds.RemoveAt(1);
                merged = true;
            }
        }

        return (merged, bounds[0]);
    }

    List<long[]> GetBoundsAt(long Y, long maxY)
    {
        var bounds = _sensors.Select(s => new long[] { Math.Max(s.MinXAt(Y), 0), Math.Min(s.MaxXAt(Y), maxY) })
                             .Where(x => x[0] <= x[1])
                             .ToList();

        bounds.Sort((x, y) => x[0].CompareTo(y[0]));

        return bounds;
    }
}