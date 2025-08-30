namespace MoraJai.Lib.Tests;

public class WhiteTileTests
{

    [Test]
    public async Task Press_Should_TurnAdjacentSameColorTilesGrey_And_ChangeGreyTilesToOriginalColor()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.White, TileColor.Gray, TileColor.Green },
              { TileColor.White, TileColor.Pink, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var whiteTile = new WhiteTile();

        var newGameState = whiteTile.Press(gameState, 0, 0);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Gray, TileColor.White, TileColor.Green },
              { TileColor.Gray, TileColor.Pink, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }
}