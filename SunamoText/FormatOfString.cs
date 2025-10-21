// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoText;

/// <summary>
///     Alternatives: TextFormatData - can check whether on position is expected char (letter, digit, etc.) but then not
///     allow variable lenght of parsed
/// </summary>
public class FormatOfString
{
    /// <summary>
    ///     A2 = {Width=|, Height=|}
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static List<string> GetParsedParts(string v1, string v2)
    {
        var vb = v2.Split('|'); //SHSplit.Split(v2, "|");

        if (vb[0] == v1) return new List<string>();

        if (SH.ContainsAll(v1, vb))
        {
            var result = v1.Split(vb.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .ToList(); // SHSplit.Split(v1, vb.ToArray());
            return result;
        }

        return new List<string>();
    }

    public static bool HasFormat(string input, string format, bool useWildcard = false)
    {
        if (useWildcard)
        {
            format = format.Replace("|", "*");
            var result = SH.MatchWildcard(input, format);
            return result;
        }

        var vb = "|";

        var countOfVerbar = SH.OccurencesOfStringIn(format, vb);
        //countOfVerbar++;

        //if (format.StartsWith(vb))
        //{
        //    countOfVerbar++;
        //}
        //if (format.EndsWith(vb))
        //{
        //    countOfVerbar++;
        //}

        var parameter = GetParsedParts(input, format);
        return parameter.Count == countOfVerbar;
    }
}