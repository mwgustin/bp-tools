namespace MoraJai.Lib.Tests;

public class PinkTileTests
{
    [Test]
    public async Task Press_Center_Should_RotateAdjacentTilesClockwise()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Yellow, TileColor.Pink, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var pinkTile = new PinkTile();

        var newGameState = pinkTile.Press(gameState, 1, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Yellow, TileColor.Red, TileColor.Blue },
              { TileColor.Black, TileColor.Pink, TileColor.Green },
              { TileColor.White, TileColor.Violet, TileColor.Orange }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_TopLeft_Should_RotateAdjacentTilesClockwiseWithWrapAround()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Pink, TileColor.Blue, TileColor.Green },
              { TileColor.Yellow, TileColor.Red, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var pinkTile = new PinkTile();

        var newGameState = pinkTile.Press(gameState, 0, 0);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Pink, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_Side_Should_RotateAdjacentTilesClockwiseWithWrapAround()
    {
        var initialBoard = new TileColor[3, 3]
        {
          { TileColor.Red, TileColor.Blue, TileColor.Green },
          { TileColor.Yellow, TileColor.Orange, TileColor.Pink },
          { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var pinkTile = new PinkTile();

        var newGameState = pinkTile.Press(gameState, 1, 2);

        var expectedBoard = new TileColor[3, 3]
        {
          { TileColor.Red, TileColor.Orange, TileColor.Blue },
          { TileColor.Yellow, TileColor.White, TileColor.Pink },
          { TileColor.Black, TileColor.Violet, TileColor.Green }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }
}
