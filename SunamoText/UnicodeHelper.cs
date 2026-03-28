namespace SunamoText;

/// <summary>
/// Provides utility methods for working with Unicode escape sequences in strings.
/// </summary>
public class UnicodeHelper
{
    /// <summary>
    /// Shared StringBuilder instance used for building decoded results.
    /// </summary>
    public static StringBuilder ResultStringBuilder { get; set; } = new();

    /// <summary>
    /// Decodes Unicode escape sequences (e.g. \u00E9) in the given <paramref name="text"/> into their character representations.
    /// </summary>
    /// <param name="text">The input text containing Unicode escape sequences to decode.</param>
    /// <returns>A StringBuilder containing the decoded text.</returns>
    public static StringBuilder DeescapeDecodeUnicode(string text)
    {
        ResultStringBuilder.Clear();

        ResultStringBuilder.Append(Regex.Replace(
            text,
            @"\\[Uu]([0-9A-Fa-f]{4})",
            match => char.ToString(
                (char)ushort.Parse(match.Groups[1].Value, NumberStyles.AllowHexSpecifier))));
        return ResultStringBuilder;
    }
}
