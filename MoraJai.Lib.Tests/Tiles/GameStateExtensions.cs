namespace MoraJai.Lib.Tests;


public static class GameStateExtensions
{
    public static bool GameStateEqual(this GameState actual, GameState expected)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (actual.Board[row, col] != expected.Board[row, col])
                {
                    Assert.Fail($"Boards are not equal at position ({row}, {col}). Expected: {expected.Board[row, col]}, Actual: {actual.Board[row, col]}");
                }
            }
        }

        // if (actual.Goal != expected.Goal)
        // {
        //   Assert.Fail($"Goals are not equal. Expected: {expected.Goal}, Actual: {actual.Goal}");
        // }
        for (int i = 0; i < 4; i++)
        {
            if (actual.Goal[i] != expected.Goal[i])
            {
                Assert.Fail($"Goals are not equal at position {i}. Expected: {expected.Goal[i]}, Actual: {actual.Goal[i]}");
            }
        }
        return true;
    }
}