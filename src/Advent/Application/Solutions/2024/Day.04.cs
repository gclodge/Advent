namespace Advent.Console.Application.Solutions._2024;

public sealed class CeresSearch : ISolution
{
    public record Xmas
    {
        public int Centroid_X { get; set; }
        public int Centroid_Y { get; set; }

        //< Need to store locations of each character, and then the total string
        //< That way we can do part one and part two with the same method (and return a record)
    }

    private readonly Dictionary<int, Dictionary<int, char>> _map = [];

    public async Task RunAsync(string input)
    {
        await ParseInput(input);

        var p1 = await Solve();

        Helper.Write($"Part One: {p1.Yellow()}");

        var mas = await Solve(N: 3, search: "MAS");

        throw new NotImplementedException();
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



    //< Need to find all occurences of XMAS
    //< -> Iterate through maps for an 'X', then check all 4-character direction strings?

    async Task<int> Solve(int N = 4, string search = "XMAS")
    {
        int total = 0;

        foreach (int y in _map.Keys)
        {
            foreach (int x in _map[y].Keys)
            {
                if (_map[y][x] == 'X')
                {
                    var matches = await GetOccurences(x, y, N, search);

                    total += cnt;
                }
            }
        }

        return total;

        
    }

    async Task<List<string>> GetOccurences(int x, int y, int N, string search)
    {
        string backwards = Reverse(search);

        //< TODO - Change these calls to return a record class, that includes the string but also the position of each character
        //<      - Will allow you to compare the 'A' values in part two for a center overlap
        
        var tasks = new List<Task<string>>
        {
            //< Horizontals
            GetDirectionString(x, y, 1, 0, N),
            GetDirectionString(x, y, -1, 0, N),
            //< Verticals
            GetDirectionString(x, y, 0, 1, N),
            GetDirectionString(x, y, 0, -1, N),
            //< Diagonals
            GetDirectionString(x, y, 1, 1, N),
            GetDirectionString(x, y, 1, -1, N),
            GetDirectionString(x, y, -1, 1, N),
            GetDirectionString(x, y, -1, -1, N)
        };

        var results = await Task.WhenAll(tasks);

        var valid = results.Where(r => IsValid(r, N, search, backwards)).ToList();

        return valid;
    }

    static bool IsValid(string input, int N, string forwards, string backwards)
    {
        if (input.Length != N) return false;

        return input == forwards || input == backwards;
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();

        Array.Reverse(charArray);

        return new string(charArray);
    }

    Task<string> GetDirectionString(int x, int y, int dx, int dy, int N)
    {
        var sb = new StringBuilder();

        foreach (int i in Enumerable.Range(0, N))
        {
            int cy = y + (i * dy);
            if (!_map.ContainsKey(cy)) continue;

            if (_map[cy].TryGetValue(x + (i * dx), out char value))
            {
                sb.Append(value);
            }
        }

        return Task.FromResult(sb.ToString());
    }
}
