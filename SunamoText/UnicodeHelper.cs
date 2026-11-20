namespace SunamoText;

/// <summary>
/// Represents the unicode helper type.
/// </summary>
public class UnicodeHelper
{
    /// <summary>
    /// Gets or sets the result string builder.
    /// </summary>
    public static StringBuilder ResultStringBuilder { get; set; } = new();

    /// <summary>
    /// Deescape decode unicode.
    /// </summary>
    /// <returns>The deescape decode unicode result.</returns>
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
