namespace SunamoText;

// Alternatives: TextFormatData - can check whether on position is expected char (letter, digit, etc.) but then not
// allow variable length of parsed.
public class FormatOfString
{
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
