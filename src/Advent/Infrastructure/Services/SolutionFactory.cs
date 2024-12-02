namespace Advent.Console.Infrastructure.Services;

public sealed class SolutionFactory : ISolutionFactory
{
    public ISolution? CreateSolution(int year, int day)
    {
        return year switch
        {
            2023 => Get2023Solution(day),
            2024 => Get2024Solution(day),
            _ => null
        };
    }

    //< TODO - Register the 'day' & 'year' value of each ISolution in namespaces, instead of manual

    private static ISolution? Get2023Solution(int day)
    {
        return day switch
        {
            1 => new Application.Solutions._2023.Trebuchet(),
            2 => new Application.Solutions._2023.CubeConundrum(),
            3 => new Application.Solutions._2023.GearRatios(),
            4 => new Application.Solutions._2023.ScratchCards(),
            5 => new Application.Solutions._2023.SeedToFertilizer(),
            _ => null
        };
    }

    private static ISolution? Get2024Solution(int day)
    {
        return day switch
        {
            1 => new Application.Solutions._2024.HistorianHysteria(),
            2 => new Application.Solutions._2024.RedNosedReports(),
            _ => null
        };
    }
}