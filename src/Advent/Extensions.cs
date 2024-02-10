namespace Advent.Console;

public static class Extensions
{
    /// <summary>
    /// Escape markup & surround input string with SpectreConsole markup of the input colour
    /// </summary>
    /// <param name="str">Input string to be escaped & bound in colour markup</param>
    /// <param name="colour">Raw colour string to be used (ie. 'yellow')</param>
    /// <returns></returns>
    public static string ToColour(this string str, string colour)
        => $"[{colour}]{str.EscapeMarkup()}[/]";

    /// <summary>
    /// Escape markup & colour input text as 'red'
    /// </summary>
    /// <param name="str">Input string to be escaped & bound in colour markup</param>
    /// <returns></returns>
    public static string Red(this string str)
        => str.ToColour("red");

    /// <summary>
    /// Escape markup & colour input text as 'yellow'
    /// </summary>
    /// <param name="str">Input string to be escaped & bound in colour markup</param>
    /// <returns></returns>
    public static string Yellow(this string str)
        => str.ToColour("yellow");

    /// <summary>
    /// Call 'ToString()' then escape markup & colour input text as 'yellow'
    /// </summary>
    /// <param name="obj">Input object to be serialized, escaped, and bound in colour markup</param>
    /// <returns></returns>
    public static string Yellow(this object obj)
        => Yellow(obj.ToString() ?? "");

    /// <summary>
    /// Escape markup & colour input text as 'green'
    /// </summary>
    /// <param name="str">Input string to be escaped & bound in colour markup</param>
    /// <returns></returns>
    public static string Green(this string str)
        => str.ToColour("green");
}
