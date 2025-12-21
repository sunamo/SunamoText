namespace SunamoText;

public class UnicodeHelper
{
    public static StringBuilder stringBuilder = new();

    public static StringBuilder DeescapeDecodeUnicode(string str)
    {
        stringBuilder.Clear();
        // var splitted = Regex.Split(str, @"\\u([a-fA-F\d]{4})");
        //  
        // foreach (var s in splitted)
        // {
        //     try
        //     {
        //         if (s.Length == 4)
        //         {
        //             var decoded = ((char)Convert.ToUInt16(s, 16)).ToString();
        //             stringBuilder.Append(decoded);
        //         }
        //         else
        //         {
        //             stringBuilder.Append(s);
        //         }
        //     }
        //     catch (Exception exception)
        //     {
        //         stringBuilder.Append(s);
        //     }
        // }
        //
        // return stringBuilder;

        stringBuilder.Append(Regex.Replace(
            str,
            @"\\[Uu]([0-9A-Fa-f]{4})",
            m => char.ToString(
                (char)ushort.Parse(m.Groups[1].Value, NumberStyles.AllowHexSpecifier))));
        return stringBuilder;
    }
}