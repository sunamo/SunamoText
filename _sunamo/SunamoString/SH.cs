namespace SunamoText._sunamo.SunamoString;

internal class SH
{
    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, AllChars.q, AllChars.asterisk);
    }

    private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat) return true;

        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, AllStrings.dot);
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        var reg = new Regex(pat);
        return reg.IsMatch(str);
    }

    internal static int OccurencesOfStringIn(string source, string p_2)
    {
        return source.Split(new[] { p_2 }, StringSplitOptions.None).Length - 1;
    }

    internal static bool ContainsAll(string input, IList<string> allWords,
        ContainsCompareMethod ccm = ContainsCompareMethod.WholeInput)
    {
        if (ccm == ContainsCompareMethod.SplitToWords)
        {
            foreach (var item in allWords)
                if (!input.Contains(item))
                    return false;
        }
        else if (ccm == ContainsCompareMethod.Negations)
        {
            foreach (var item in allWords)
            {
                var c = item;
                if (!IsContained(input, ref c)) return false;
            }
        }
        else if (ccm == ContainsCompareMethod.WholeInput)
        {
            foreach (var item in allWords)
                if (!input.Contains(item))
                    return false;
        }

        return true;
    }

    internal static bool IsContained(string item, ref string contains)
    {
        var (negation, contains2) = IsNegationTuple(contains);
        contains = contains2;

        if (negation && item.Contains(contains))
            return false;
        if (!negation && !item.Contains(contains)) return false;

        return true;
    }

    internal static bool IsContained(string item, string contains)
    {
        var (negation, contains2) = IsNegationTuple(contains);
        contains = contains2;

        if (negation && item.Contains(contains))
            return false;
        if (!negation && !item.Contains(contains)) return false;

        return true;
    }

    internal static (bool, string) IsNegationTuple(string contains)
    {
        if (contains[0] == '!')
        {
            contains = contains.Substring(1);
            return (true, contains);
        }

        return (false, contains);
    }

    internal static string JoinNL(List<string> l)
    {
        StringBuilder sb = new();
        foreach (var item in l) sb.AppendLine(item);
        var r = string.Empty;
        r = sb.ToString();
        return r;
    }

    internal static List<string> SplitCharMore(string s, params char[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitMore(string s, params string[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNone(string text, params string[] deli)
    {
        return text.Split(deli, StringSplitOptions.None).ToList();
    }

    internal static string NullToStringOrDefault(object n)
    {
        return n == null ? " " + Consts.nulled : AllStrings.space + n;
    }

    internal static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext)) return name.Substring(0, name.Length - ext.Length);
        return name;
    }
}