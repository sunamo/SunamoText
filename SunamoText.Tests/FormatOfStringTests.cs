public class FormatOfStringTests
{
    const string albumsListTemplate = "https://www.facebook.com/|/photos_albums";
    const string albumTemplate = "https://www.facebook.com/media/set/?set=a.|&type=3";



    [Fact]
    public void GetParsedParts2Test()
    {
        var p = FormatOfString.GetParsedParts("a_backup-b", "|_backup-|");
        Assert.Equal<string>(TestData.listAB1, p);
    }

    //
    [Fact]
    public void GetParsedPartsTest()
    {
        var p = FormatOfString.GetParsedParts("{Width=a, Height=b}", "{Width=|, Height=|}");
        Assert.Equal<string>(TestData.listAB1, p);
    }

    [Fact]
    public void HasFormatTest()
    {
        var b = FormatOfString.HasFormat("https://www.facebook.com/media/set/?set=a.742074075847448&type=3", albumTemplate);
        Assert.Equal(true, b);
    }

    [Fact]
    public void HasFormat2Test()
    {
        var b = FormatOfString.HasFormat("https://www.facebook.com/media/set/?set=cba.742074075847448&type=3", albumTemplate);
        Assert.Equal(false, b);
    }
}
