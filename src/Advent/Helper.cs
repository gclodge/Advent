namespace Advent.Console;

public static class Helper
{
    /// <summary>
    /// The 'short' date time format for log file and other indexing
    /// </summary>
    public const string DateFormat = "yyMMdd_HHmmss";
    /// <summary>
    /// ISO8601 DateTime format for text serialization
    /// </summary>
    public const string ISO8601DateFormat = @"yyyy-MM-ddTHH:mm:ss.fffffffZ";

    public static string GetCurrentDateTimeString(string format = DateFormat)
        => DateTime.Now.ToString(format);

    public static string GetEscapedFileName(string file)
        => Path.GetFileName(file).EscapeMarkup();

    public static string GetTimestamp()
    {
        string dt = GetCurrentDateTimeString(ISO8601DateFormat);
        return $"[grey]{dt}:[/]";
    }

    public static string AddTimestamp(string message)
        => $"{GetTimestamp()} {message}";

    public static void Write(string message)
        => AnsiConsole.MarkupLine(AddTimestamp(message));

    public static void WriteDivider(string text)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule($"[yellow]{text}[/]").RuleStyle("grey").LeftJustified());
    }

    public static bool Confirm(string message)
        => AnsiConsole.Confirm(AddTimestamp(message));
}