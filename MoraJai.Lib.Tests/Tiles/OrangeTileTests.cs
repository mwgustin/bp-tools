namespace MoraJai.Lib.Tests;

public class OrangeTileTests
{
    [Test]
    public async Task Press_Should_Change_OrangeTile_To_Majority_Color()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Orange, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Red },
              { TileColor.Blue, TileColor.Red, TileColor.Green }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var orangeTile = new OrangeTile();

        var newGameState = orangeTile.Press(gameState, 0, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Red, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Red },
              { TileColor.Blue, TileColor.Red, TileColor.Green }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_Should_Change_OrangeTile_To_Majority_Color_Different_Case()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Blue, TileColor.Red, TileColor.Red },
              { TileColor.Blue, TileColor.Orange, TileColor.Red },
              { TileColor.Green, TileColor.Red, TileColor.Green }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var orangeTile = new OrangeTile();

        var newGameState = orangeTile.Press(gameState, 1, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Blue, TileColor.Red, TileColor.Red },
              { TileColor.Blue, TileColor.Red, TileColor.Red },
              { TileColor.Green, TileColor.Red, TileColor.Green }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_With_Multiple_Majority_Colors_Should_Not_Change_OrangeTile()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Red, TileColor.Red },
              { TileColor.Blue, TileColor.Orange, TileColor.Blue },
              { TileColor.Green, TileColor.Red, TileColor.Green }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var orangeTile = new OrangeTile();

        var newGameState = orangeTile.Press(gameState, 0, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Red, TileColor.Red },
              { TileColor.Blue, TileColor.Orange, TileColor.Blue },
              { TileColor.Green, TileColor.Red, TileColor.Green }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_Should_Not_Change_OrangeTile_When_No_Majority_Color()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Orange, TileColor.Blue },
              { TileColor.Green, TileColor.Yellow, TileColor.Red },
              { TileColor.Blue, TileColor.Green, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var orangeTile = new OrangeTile();

        var newGameState = orangeTile.Press(gameState, 0, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Orange, TileColor.Blue },
              { TileColor.Green, TileColor.Yellow, TileColor.Red },
              { TileColor.Blue, TileColor.Green, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }
}