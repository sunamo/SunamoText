/// <summary>
/// Tests for the <see cref="FormatOfString"/> class.
/// </summary>
public class FormatOfStringTests
{
    const string albumTemplate = "https://www.facebook.com/media/set/?set=a.|&type=3";

    /// <summary>
    /// Tests parsing parts from a backup-style format string.
    /// </summary>
    [Fact]
    public void GetParsedParts2Test()
    {
        var parsedParts = FormatOfString.GetParsedParts("a_backup-b", "|_backup-|");
        var expected = new List<string> { "a", "b" };
        Assert.Equal<string>(expected, parsedParts);
    }

    /// <summary>
    /// Tests parsing parts from a width/height format string.
    /// </summary>
    [Fact]
    public void GetParsedPartsTest()
    {
        var parsedParts = FormatOfString.GetParsedParts("{Width=a, Height=b}", "{Width=|, Height=|}");
        var expected = new List<string> { "a", "b" };
        Assert.Equal<string>(expected, parsedParts);
    }

    /// <summary>
    /// Tests that a matching URL is recognized as having the album format.
    /// </summary>
    [Fact]
    public void HasFormatTest()
    {
        var isMatchingFormat = FormatOfString.HasFormat("https://www.facebook.com/media/set/?set=a.742074075847448&type=3", albumTemplate);
        Assert.True(isMatchingFormat);
    }

    /// <summary>
    /// Tests that a non-matching URL is not recognized as having the album format.
    /// </summary>
    [Fact]
    public void HasFormat2Test()
    {
        var isMatchingFormat = FormatOfString.HasFormat("https://www.facebook.com/media/set/?set=cba.742074075847448&type=3", albumTemplate);
        Assert.False(isMatchingFormat);
    }
}
