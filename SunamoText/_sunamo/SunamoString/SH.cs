namespace SunamoText._sunamo.SunamoString;

internal class SH
{
    internal static bool MatchWildcard(string text, string pattern)
    {
        return IsMatchRegex(text, pattern, '?', '*');
    }

    private static bool IsMatchRegex(string text, string pattern, char singleWildcard, char multipleWildcard)
    {
        if (text == pattern) return true;

        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pattern = Regex.Escape(pattern);
        pattern = pattern.Replace(escapedSingle, ".");
        pattern = "^" + pattern.Replace(escapedMultiple, ".*") + "$";
        var regex = new Regex(pattern);
        return regex.IsMatch(text);
    }

    internal static int OccurencesOfStringIn(string text, string substring)
    {
        return text.Split(new[] { substring }, StringSplitOptions.None).Length - 1;
    }

    internal static bool ContainsAll(string text, IList<string> list,
        ContainsCompareMethod compareMethod = ContainsCompareMethod.WholeInput)
    {
        if (compareMethod == ContainsCompareMethod.SplitToWords)
        {
            foreach (var item in list)
                if (!text.Contains(item))
                    return false;
        }
        else if (compareMethod == ContainsCompareMethod.Negations)
        {
            foreach (var item in list)
            {
                var searchPattern = item;
                if (!IsContained(text, ref searchPattern)) return false;
            }
        }
        else if (compareMethod == ContainsCompareMethod.WholeInput)
        {
            foreach (var item in list)
                if (!text.Contains(item))
                    return false;
        }

        return true;
    }

    internal static bool IsContained(string text, ref string pattern)
    {
        var (isNegation, patternWithoutPrefix) = IsNegationTuple(pattern);
        pattern = patternWithoutPrefix;

        if (isNegation && text.Contains(pattern))
            return false;
        if (!isNegation && !text.Contains(pattern)) return false;

        return true;
    }

    internal static (bool, string) IsNegationTuple(string pattern)
    {
        if (pattern[0] == '!')
        {
            pattern = pattern.Substring(1);
            return (true, pattern);
        }

        return (false, pattern);
    }
}
