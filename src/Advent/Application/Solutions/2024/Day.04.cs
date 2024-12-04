namespace Advent.Console.Application.Solutions._2024;

public record CeresCharacter
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Value { get; set; }
}

public record CeresMatch
{
    public List<CeresCharacter> Characters { get; set; } = [];

    public string Value => GetValue();

    string GetValue()
    {
        var sb = new StringBuilder();

        foreach (var c in Characters) sb.Append(c.Value);

        return sb.ToString();
    }

    public bool IsValid(int N, string forwards, string backwards)
    {
        var value = GetValue();

        if (value.Length != N) return false;

        return value == forwards || value == backwards;
    }
}

public sealed class CeresSearch : ISolution
{
    private readonly Dictionary<int, Dictionary<int, char>> _map = [];

    public async Task RunAsync(string input)
    {
        await ParseInput(input);

        var p1 = await FindMatches();

        Helper.Write($"Part One: {p1.Count.Yellow()}");

        var mas = await FindMatches(N: 3, search: "MAS", onlyDiags: true);

        var p2 = await CountOverlappingMatches(mas);

        Helper.Write($"Part Two: {p2.Yellow()}");
    }

    async Task ParseInput(string input)
    {
        var lines = await File.ReadAllLinesAsync(input);

        int y = 0;
        foreach (var line in lines)
        {
            if (!_map.ContainsKey(y)) _map.Add(y, []); //< Honestly less readable than new Dictionary<int, char>();

            foreach (int x in Enumerable.Range(0, line.Length))
            {
                _map[y][x] = line[x];
            }

            y++;
        }
    }

    async Task<List<CeresMatch>> FindMatches(int N = 4, string search = "XMAS", bool onlyDiags = false)
    {
        var total = new List<CeresMatch>();

        char kernel = search.First();

        foreach (int y in _map.Keys)
        {
            foreach (int x in _map[y].Keys)
            {
                if (_map[y][x] == kernel)
                {
                    var matches = await GetOccurences(x, y, N, search, onlyDiags);

                    total.AddRange(matches);
                }
            }
        }

        return total;
    }

    async Task<List<CeresMatch>> GetOccurences(int x, int y, int N, string search, bool onlyDiags = false)
    {
        string backwards = Reverse(search);

        var tasks = new List<Task<CeresMatch>>
        {
            //< Diagonals
            GetDirectionString(x, y, 1, 1, N),
            GetDirectionString(x, y, 1, -1, N),
            GetDirectionString(x, y, -1, 1, N),
            GetDirectionString(x, y, -1, -1, N)
        };

        if (!onlyDiags)
        {
            tasks.AddRange(new List<Task<CeresMatch>>
            {
                //< Horizontals
                GetDirectionString(x, y, 1, 0, N),
                GetDirectionString(x, y, -1, 0, N),
                //< Verticals
                GetDirectionString(x, y, 0, 1, N),
                GetDirectionString(x, y, 0, -1, N),
            });
        }

        var results = await Task.WhenAll(tasks);

        var valid = results.Where(r => r.IsValid(N, search, backwards)).ToList();

        return valid;
    }

    static Task<int> CountOverlappingMatches(IEnumerable<CeresMatch> matches)
    {
        var map = new Dictionary<(int x, int y), List<CeresMatch>>();

        foreach (var match in matches)
        {
            var A = match.Characters.Where(c => c.Value == 'A').Single();

            var pos = (A.X, A.Y);

            if (!map.ContainsKey(pos)) map.Add(pos, new List<CeresMatch>());

            map[pos].Add(match);
        }

        var count = map.Values.Count(v => v.Count == 2);

        return Task.FromResult(count);
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();

        Array.Reverse(charArray);

        return new string(charArray);
    }

    Task<CeresMatch> GetDirectionString(int x, int y, int dx, int dy, int N)
    {
        var match = new CeresMatch();

        foreach (int i in Enumerable.Range(0, N))
        {
            int cy = y + (i * dy);
            if (!_map.ContainsKey(cy)) continue;

            int cx = x + (i * dx);
            if (_map[cy].TryGetValue(cx, out char value))
            {
                match.Characters.Add(new CeresCharacter { X = cx, Y = cy, Value = value });
            }
        }

        return Task.FromResult(match);
    }
}
