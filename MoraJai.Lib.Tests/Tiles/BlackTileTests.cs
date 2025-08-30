namespace MoraJai.Lib.Tests;

public class BlackTileTests
{
    [Test]
    public async Task Press_Should_ShiftTilesInRow()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Black, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var blackTile = new BlackTile();

        var newGameState = blackTile.Press(gameState, 1, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Green, TileColor.Black },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();

    }
}
