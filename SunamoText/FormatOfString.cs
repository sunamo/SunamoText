namespace SunamoText;

/// <summary>
/// Provides methods for parsing and validating string formats using pipe-delimited templates.
/// Alternatives: TextFormatData - can check whether on position is expected char (letter, digit, etc.) but then not
/// allow variable length of parsed.
/// </summary>
public class FormatOfString
{
    /// <summary>
    /// Parses variable parts from <paramref name="text"/> using pipe-delimited <paramref name="format"/>.
    /// For example, format "{Width=|, Height=|}" applied to "{Width=100, Height=200}" returns ["100", "200"].
    /// </summary>
    /// <param name="text">The input text to parse.</param>
    /// <param name="format">The pipe-delimited format template where | marks variable parts.</param>
    /// <returns>List of parsed variable parts, or empty list if format does not match.</returns>
    public static List<string> GetParsedParts(string text, string format)
    {
        var formatParts = format.Split('|');

        if (formatParts[0] == text) return new List<string>();

        if (SH.ContainsAll(text, formatParts))
        {
            var result = text.Split(formatParts.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            return result;
        }

        return new List<string>();
    }

    /// <summary>
    /// Checks whether <paramref name="text"/> matches the given pipe-delimited <paramref name="format"/>.
    /// </summary>
    /// <param name="text">The input text to validate.</param>
    /// <param name="format">The pipe-delimited format template.</param>
    /// <param name="isUsingWildcard">When true, pipes are replaced with wildcards for pattern matching.</param>
    /// <returns>True if the text matches the format; otherwise false.</returns>
    public static bool HasFormat(string text, string format, bool isUsingWildcard = false)
    {
        if (isUsingWildcard)
        {
            format = format.Replace('|', '*');
            var result = SH.MatchWildcard(text, format);
            return result;
        }

        var verticalBar = "|";

        var verticalBarCount = SH.OccurencesOfStringIn(format, verticalBar);

        var parsedParts = GetParsedParts(text, format);
        return parsedParts.Count == verticalBarCount;
    }
}
