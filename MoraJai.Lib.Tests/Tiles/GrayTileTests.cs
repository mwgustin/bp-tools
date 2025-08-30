namespace MoraJai.Lib.Tests;

public class GrayTileTests
{
    [Test]
    public async Task Press_Should_Not_Change_GameState()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var grayTile = new GrayTile();

        var newGameState = grayTile.Press(gameState, 1, 1);

        await Assert.That(newGameState).IsEqualTo(gameState);
    }
}