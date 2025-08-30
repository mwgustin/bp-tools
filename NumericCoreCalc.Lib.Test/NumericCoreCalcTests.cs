namespace NumericCoreCalc.Lib.Test;

public class NumericCoreCalcTests
{
    [Fact]
    public void Book1Test()
    {
        // first example from the book
        // 86455
        // Becomes 8, 6, 45, 5; Becomes + 8, x 6, ÷ 45, - 5; Core = 18
        //arrange
        int[][] x = [[8, 6, 45, 5]];
        var sut = new NumericCoreCalculator();


        //act
        var result = sut.Calculate(x);
        //assert
        Assert.Equal(18, result);
    }

    [Fact]
    public void Book2Test()
    {
        // second example from the book
        // Becomes 3, 6, 1, 4; Becomes + 3, x 6, ÷ 1, - 4; Core = 14"
        //arrange
        int[][] x = [[3, 6, 1, 4]];
        var sut = new NumericCoreCalculator();

        //act
        var result = sut.Calculate(x);
        //assert
        Assert.Equal(14, result);
    }

    [Fact]
    public void WineCellarTest()
    {
        // M|CC|XI|II (wine cellar)
        //arrange
        int[][] x = [[1000, 200, 11, 2]];

        var sut = new NumericCoreCalculator();

        //act
        var result = sut.Calculate(x);
        //assert
        Assert.Equal(53, result);
    }

    [Fact]
    public void HovelTest()
    {
        // 45292 (hovel)
        //arrange
        int[][] x = [[45, 2, 9, 2]];
        var sut = new NumericCoreCalculator();

        //act
        var result = sut.Calculate(x);
        //assert
        Assert.Equal(8, result);
    }
}
