using Advent.Domain;

using System.Diagnostics;

using IntervalTree;

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

        public long MinY => Y + ManhattanDist / 2

        public IEnumerable<long> GetXValues(long targetY)
        {
            long dY = Math.Abs(targetY - Y);

            long start = X - (ManhattanDist - dY);
            long len = 2 * (ManhattanDist - dY);

            return Enumerable.Range(0, (int)len).Select(x => start + x);
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

    public int CountBlockedPositions(int Y)
    {
        var tree = GenerateIntervalTree(_sensors);

        var overlap = tree.Query(Y);

        var xValues = overlap.SelectMany(x => x.GetXValues(Y)).ToHashSet();

        return xValues.Count;
    }

    public long FindTuningFrequency(long maxY)
    {
        for (long y = 0; y <= maxY; y++)
        {
            var bounds = GetBoundsAt(y, maxY);

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

            if (!merged || bounds[0][0] != 0 || bounds[0][1] != maxY)
            {
                return (bounds[0][1] + 1) * 4000000 + y;
            }
        }

        return -1;
    }

    List<long[]> GetBoundsAt(long Y, long maxY)
    {
        var bounds = _sensors.Select(s => new long[] { Math.Max(s.MinXAt(Y), 0), Math.Min(s.MaxXAt(Y), maxY) })
                             .Where(x => x[0] <= x[1])
                             .ToList();

        bounds.Sort((x, y) => x[0].CompareTo(y[0]));

        return bounds;
    }

    static IntervalTree<long, Sensor> GenerateIntervalTree(IEnumerable<Sensor> sensors, bool vertical = true)
    {
        var tree = new IntervalTree<long, Sensor>();

        foreach (var sensor in sensors)
        {
            long a = vertical ? sensor.Y - sensor.ManhattanDist : sensor.X + sensor.ManhattanDist;
            long b = vertical ? sensor.Y + sensor.ManhattanDist : sensor.X - sensor.ManhattanDist;

            tree.Add(a, b, sensor);
        }

        return tree;
    }
}