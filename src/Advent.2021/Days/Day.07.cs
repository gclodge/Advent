namespace Advent._2021;

public class CrabAligner
{
    public int Min => PositionMap.Keys.Min();
    public int Max => PositionMap.Keys.Max();

    Dictionary<int, int> PositionMap;

    public CrabAligner(IEnumerable<int> crabs)
    {
        PositionMap = crabs.GroupBy(c => c)
                           .ToDictionary(c => c.Key, c => c.Count());
    }

    public int GetCheapestAlignment(bool isGauss = false)
    {
        return Enumerable.Range(Min, Max - Min + 1)
                         .Select(c => GetCost(c, isGauss))
                         .OrderBy(c => c) //< Order descending
                         .First();
    }

    int GetCost(int pos, bool isGauss = false)
    {
        return PositionMap.Select(kvp => GetPositionOffset(pos, kvp.Key, isGauss) * kvp.Value)
                          .Sum();
    }


    /// <summary>
    /// We use Gaussian Summation to calculate the sum of the pattern [n, n - 1, .., 1, 0] = n * (n + 1) / 2
    /// <para>See: https://letstalkscience.ca/educational-resources/backgrounders/gauss-summation</para>
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="index"></param>
    /// <param name="gauss"></param>
    /// <returns></returns>
    static int GetPositionOffset(
        int pos,
        int index,
        bool gauss = false)
    {
        int offset = Math.Abs(pos - index);
        return gauss ? offset * (offset + 1) / 2 : offset;
    }
}