namespace SunamoText._sunamo.SunamoString;

/// <summary>
/// Internal string helper methods for pattern matching and containment checks.
/// </summary>
internal class SH
{
    /// <summary>
    /// Checks whether <paramref name="text"/> matches the given wildcard <paramref name="pattern"/>
    /// using ? for single character and * for multiple characters.
    /// </summary>
    /// <param name="text">The text to match against.</param>
    /// <param name="pattern">The wildcard pattern.</param>
    /// <returns>True if the text matches the pattern.</returns>
    internal static bool MatchWildcard(string text, string pattern)
    {
        return IsMatchRegex(text, pattern, '?', '*');
    }

    /// <summary>
    /// Checks whether <paramref name="text"/> matches the given <paramref name="pattern"/>
    /// using regex-based wildcard matching.
    /// </summary>
    /// <param name="text">The text to match.</param>
    /// <param name="pattern">The pattern with wildcard characters.</param>
    /// <param name="singleWildcard">Character representing a single-character wildcard.</param>
    /// <param name="multipleWildcard">Character representing a multiple-character wildcard.</param>
    /// <returns>True if the text matches the pattern.</returns>
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

    /// <summary>
    /// Counts how many times <paramref name="substring"/> occurs in <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The source text to search in.</param>
    /// <param name="substring">The substring to count occurrences of.</param>
    /// <returns>The number of occurrences.</returns>
    internal static int OccurencesOfStringIn(string text, string substring)
    {
        return text.Split(new[] { substring }, StringSplitOptions.None).Length - 1;
    }

    /// <summary>
    /// Checks whether <paramref name="text"/> contains all strings from <paramref name="list"/>.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="list">The list of strings that must all be contained.</param>
    /// <param name="compareMethod">The comparison method to use.</param>
    /// <returns>True if all strings are contained in the text.</returns>
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

    /// <summary>
    /// Checks whether <paramref name="text"/> contains the given <paramref name="pattern"/>,
    /// supporting negation patterns prefixed with '!'.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The search pattern, possibly prefixed with '!' for negation. Updated to the pattern without prefix.</param>
    /// <returns>True if the containment check passes (including negation logic).</returns>
    internal static bool IsContained(string text, ref string pattern)
    {
        var (isNegation, patternWithoutPrefix) = IsNegationTuple(pattern);
        pattern = patternWithoutPrefix;

        if (isNegation && text.Contains(pattern))
            return false;
        if (!isNegation && !text.Contains(pattern)) return false;

        return true;
    }

    /// <summary>
    /// Determines whether the given <paramref name="pattern"/> starts with a negation prefix '!'
    /// and returns the pattern without the prefix.
    /// </summary>
    /// <param name="pattern">The pattern to check for negation.</param>
    /// <returns>A tuple of (isNegation, patternWithoutPrefix).</returns>
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
