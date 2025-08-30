namespace MoraJai.Lib.Tests;

public class YellowTileTests
{
    [Test]
    public async Task Press_NotTopRow_Should_SwapWithTileAbove()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Yellow, TileColor.Pink, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var yellowTile = new YellowTile();

        var newGameState = yellowTile.Press(gameState, 1, 0);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Yellow, TileColor.Blue, TileColor.Green },
              { TileColor.Red, TileColor.Pink, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_TopRow_Should_HaveNoEffect()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Yellow, TileColor.Blue, TileColor.Green },
              { TileColor.Red, TileColor.Pink, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var yellowTile = new YellowTile();

        var newGameState = yellowTile.Press(gameState, 0, 0);

        await Assert.That(newGameState.GameStateEqual(gameState)).IsTrue();
    }
}
