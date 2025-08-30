namespace NumericCoreCalc.Lib.Test;


public class ParseTests
{
    [Fact]
    public void Book1ParseTest()
    {
        //arrange
        var x = "86455";
        int[][] expected = [
          [ 8, 6, 4, 55 ],
      [ 8, 6, 45, 5 ],
      [ 8, 64, 5, 5 ],
      [86, 4, 5, 5 ]
        ];
        var sut = new Parse();
        //act
        var result = sut.Partitions(x);
        //assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Book2ParseTest()
    {
        //arrange
        var x = "3614";
        int[][] expected = [
          [ 3, 6, 1, 4 ]
        ];
        var sut = new Parse();
        //act
        var result = sut.Partitions(x);
        //assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void HovelParseTest()
    {
        var x = "45292";

        var sut = new Parse();

        //act
        var result = sut.Partitions(x);
        //assert
        Assert.Contains([45, 2, 9, 2], result);
    }
}