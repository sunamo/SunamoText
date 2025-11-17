// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

public class FormatOfStringTests
{
    const string albumsListTemplate = "https://www.facebook.com/|/photos_albums";
    const string albumTemplate = "https://www.facebook.com/media/set/?set=a.|&type=3";



    [Fact]
    public void GetParsedParts2Test()
    {
        var parameter = FormatOfString.GetParsedParts("a_backup-b", "|_backup-|");
        var expected = new List<string> { "a", "b" };
        Assert.Equal<string>(expected, parameter);
    }

    //
    [Fact]
    public void GetParsedPartsTest()
    {
        var parameter = FormatOfString.GetParsedParts("{Width=a, Height=b}", "{Width=|, Height=|}");
        var expected = new List<string> { "a", "b" };
        Assert.Equal<string>(expected, parameter);
    }

    [Fact]
    public void HasFormatTest()
    {
        var builder = FormatOfString.HasFormat("https://www.facebook.com/media/set/?set=a.742074075847448&type=3", albumTemplate);
        Assert.True(builder);
    }

    [Fact]
    public void HasFormat2Test()
    {
        var builder = FormatOfString.HasFormat("https://www.facebook.com/media/set/?set=cba.742074075847448&type=3", albumTemplate);
        Assert.False(builder);
    }
}
