using Advent.Domain;

namespace Advent._2022;

record class VolcanoValve(string ID, int FlowRate, HashSet<string> Connections)
{
    public static VolcanoValve Parse(string input)
    {
        var vals = input.Split(';');

        var lhs = vals[0].Split(" has flow rate=");
        string id = lhs[0][6..];
        int flow = int.Parse(lhs[1]);

        var rhs = vals[1][25..];
        var connex = rhs.Split(", ").ToHashSet();

        return new VolcanoValve(id, flow, connex);
    }
}

public class VolcanoSolver
{

    private readonly ICollection<string> _input;
    private readonly ICollection<VolcanoValve> _valves;

    private readonly IDictionary<string, HashSet<string>> _connections;
    private readonly IDictionary<string, List<int>> _visits;

    public VolcanoSolver(IEnumerable<string> input)
    {
        _input = input.ToList();
        _valves = _input.Select(VolcanoValve.Parse).ToList();

        _connections = new Dictionary<string, HashSet<string>>();
        _visits = new Dictionary<string, List<int>>();
    }

    public VolcanoSolver Initialize()
    {
        foreach (var valve in _valves)
        {
            _visits.Add(valve.ID, new());

            if (!_connections.ContainsKey(valve.ID)) _connections.Add(valve.ID, new());

            foreach (var connec in valve.Connections) _connections[valve.ID].Add(connec);
        }

        return this;
    }

    //< See 2021.12
}