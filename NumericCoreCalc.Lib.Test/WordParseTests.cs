namespace NumericCoreCalc.Lib.Test;

public class WordParseTests
{
    [Fact]
    public void Test1()
    {
        var sut = new WordParse();
        var result = sut.Parse("pigs");
        Assert.Equal(new int[] { 16, 9, 7, 19 }, result);
    }
}