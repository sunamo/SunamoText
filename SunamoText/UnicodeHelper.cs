namespace SunamoText;

public class UnicodeHelper
{
    public static StringBuilder ResultStringBuilder { get; set; } = new();

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
