namespace Advent._2022;

public class ElfCalorieCounter
{

    private readonly List<string> _input;

    public ElfCalorieCounter(IEnumerable<string> input)
    {
        _input = input.ToList();
    }

    public IEnumerable<int> CalculateIndividualCalories()
    {
        var res = new List<int>();
        var curr = new List<int>
        {
            int.Parse(_input[0])
        };

        for (int i = 1; i < _input.Count; i++)
        {
            var val = _input[i];
            if (string.IsNullOrEmpty(val))
            {
                res.Add(curr.Sum());
                curr = new List<int>();
            }
            else
                curr.Add(int.Parse(val));
        }

        return res;
    }
}