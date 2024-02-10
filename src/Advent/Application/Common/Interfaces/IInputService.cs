namespace Advent.Console.Application.Common.Interfaces;

public interface IInputService
{
    Task<string> GetInputFileAsync(int year, int day, bool isTest = false);
}
