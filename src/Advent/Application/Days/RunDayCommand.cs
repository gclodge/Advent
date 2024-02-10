using System.Diagnostics;

namespace Advent.Console.Application.Days;

public sealed class RunDayCommand : AsyncCommand<RunDayCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<YEAR>")]
        [Description("The four-digit year for the command to be run")]
        public int Year { get; set; } = 2023;

        [CommandArgument(1, "<DAY>")]
        [Description("The integer day-of-december in range [1, 25] to be run")]
        public int Day { get; set; }

        [CommandOption("-t|--test")]
        [Description("Flag that indicates this run should use the test input, if available.")]
        public bool Test { get; set; } = false;

        public string Name => $"{Year} Day {Day}";
    }

    private readonly IInputService _input;
    private readonly ISolutionFactory _solutions;

    public RunDayCommand(
        IInputService input,
        ISolutionFactory solutions)
    {
        _input = input;
        _solutions = solutions;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        Helper.WriteDivider($"Running {settings.Name}");

        var solution = _solutions.CreateSolution(settings.Year, settings.Day);

        if (solution == null)
        {
            Helper.Write($"No solution available for {settings.Name.Yellow()}");
            return -1;
        }

        var input = await _input.GetInputFileAsync(settings.Year, settings.Day, settings.Test);

        Helper.Write($"Starting: {settings.Name.Yellow()}");

        var timer = Stopwatch.StartNew();

        await solution.RunAsync(input);

        timer.Stop();

        Helper.Write($"Done: {settings.Name.Yellow()} in [green]{timer.Elapsed.TotalMilliseconds:0.00} ms[/]");

        return 0;
    }
}
