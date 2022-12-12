using System.Text;
using Advent.Domain;

namespace Advent._2022;

public class HillClimber
{
    private readonly ICollection<string> _input;

    public HillClimber(IEnumerable<string> input)
    {
        _input = input.ToList();
    }
}