using Microsoft.Extensions.Options;

namespace Advent.Console.Infrastructure.Services;

public sealed class InputService : IInputService
{
    private readonly InputOptions _options;

    public InputService(IOptions<InputOptions> options)
    {
        _options = options.Value;
    }

    public Task<string> GetInputFileAsync(int year, int day, bool isTest = false)
    {
        var file = GetFile(year, day, isTest);

        return Task.FromResult(file);
    }

    public string GetFile(int year, int day, bool isTest = false)
    {
        string dd = day.ToString().PadLeft(2, '0');
        string yyyy = year.ToString();
        var file = Path.Combine(_options.SourceDirectory, yyyy, $"Day.{dd}.txt");

        if (!File.Exists(file)) throw new FileNotFoundException($"Couldn't find input file: {file}");

        return isTest ? Path.ChangeExtension(file, ".Test.txt") : file;
    }
}
