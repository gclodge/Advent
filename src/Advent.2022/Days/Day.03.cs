using Advent.Domain;

namespace Advent._2022;

public class Rucksack
{
    public const char Delim = ' ';
    private readonly string _input;

    public int Length => _input.Length;

    public HashSet<char> A { get; }
    public HashSet<char> B { get; }

    public Rucksack(string input)
    {
        _input = input;

        int halfCount = Length / 2;

        A = _input.Take(halfCount).ToHashSet();
        B = _input.Skip(halfCount).ToHashSet();
    }

    public int CalculateSharedItemPriority()
    {
        var shared = A.Single(x => B.Contains(x));
        int res = CalculatePriority(shared);
        return res;

    }

    public static int CalculatePriority(char item)
    {
        var idx = item % 32;
        var add = char.IsUpper(item) ? 26 : 0;
        return idx + add;
    }

    public static int CheckGroups(IEnumerable<Rucksack> sacks)
    {
        var groups = sacks.Split(3).ToList();
        int sum = 0;

        foreach (var group in groups)
        {
            var hist = GetItemHistogram(group);
            var badge = hist.Single(x => x.Value == 3).Key;
            sum += CalculatePriority(badge);
        }

        return sum;
    }

    public static Dictionary<char, int> GetItemHistogram(IEnumerable<Rucksack> group)
    {
        var dict = new Dictionary<char, int>();

        foreach (var sack in group)
        {
            var items = sack.A.Concat(sack.B).ToHashSet();
            foreach (var item in items)
            {
                if (!dict.ContainsKey(item)) dict[item] = 0;

                dict[item] += 1;
            }
        }

        return dict;
    }
}