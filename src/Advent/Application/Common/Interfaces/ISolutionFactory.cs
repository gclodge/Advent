namespace Advent.Console.Application.Common.Interfaces;

public interface ISolutionFactory
{
    ISolution? CreateSolution(int year, int day);
}
