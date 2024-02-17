namespace Advent.Console.Infrastructure.Services;

public sealed class SolutionFactory : ISolutionFactory
{
    public ISolution? CreateSolution(int year, int day)
    {
        return year switch
        {
            2023 => Get2023Solution(day),
            _ => null
        };
    }

    private static ISolution? Get2023Solution(int day)
    {
        return day switch
        {
            1 => new Application.Solutions._2023.Trebuchet(),
            2 => new Application.Solutions._2023.CubeConundrum(),
            _ => null
        };
    }
}