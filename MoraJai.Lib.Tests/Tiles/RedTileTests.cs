namespace MoraJai.Lib.Tests;


public class RedTileTests
{
    [Test]
    public async Task Press_Should_ChangeColorsCorrectly()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.White, TileColor.Black, TileColor.Red },
              { TileColor.Green, TileColor.White, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Black }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var redTile = new RedTile();

        var newGameState = redTile.Press(gameState, 0, 0);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Black, TileColor.Red, TileColor.Red },
              { TileColor.Green, TileColor.Black, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_Should_NotChangeOtherColors()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Green, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Blue, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Black }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var redTile = new RedTile();

        var newGameState = redTile.Press(gameState, 1, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Green, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Blue, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }
}