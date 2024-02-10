namespace Advent.Console.Application.Common.Models;

public sealed class InputOptions
{
    public const string Name = "Input";

    public const string DefaultDirectory = @"C:\_test\Advent";

    public string SourceDirectory { get; set; } = DefaultDirectory;
}