using Microsoft.Extensions.Hosting;

using Advent.Console.Application.Days;

AnsiConsole.Write(new FigletText("Advent").Color(Color.Yellow));
AnsiConsole.MarkupLine($"Advent CLI Version: {Configuration.GetVersion().Yellow()}");

var builder = Host.CreateDefaultBuilder(args);
var config = Configuration.GetConfiguration();

builder.ConfigureServices(services =>
{
    services.AddConsoleServices(config);
});

var registrar = new TypeRegistrar(builder);
var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.SetApplicationName("advent");

    config.AddBranch("run", branch =>
    {
        branch.AddCommand<RunDayCommand>("day")
              .WithDescription("Runs a specific day from a specific year")
              .WithExample(["run", "day", "2023", "1"])
              .WithExample(["run", "day", "2023", "4", "--test"]);
    });

    config.SetExceptionHandler(ex =>
    {
        AnsiConsole.MarkupLine($"ERROR: {ex.Message.Yellow()}");
        return -99;
    });
});

return app.Run(args);