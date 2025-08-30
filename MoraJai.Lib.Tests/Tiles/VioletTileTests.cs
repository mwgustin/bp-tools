namespace MoraJai.Lib.Tests;

public class VioletTileTests
{
    [Test]
    public async Task Press_NotBottomRow_Should_SwapWithTileBelow()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Violet, TileColor.Pink, TileColor.Orange },
              { TileColor.Black, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var purpleTile = new VioletTile();

        var newGameState = purpleTile.Press(gameState, 1, 0);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Black, TileColor.Pink, TileColor.Orange },
              { TileColor.Violet, TileColor.White, TileColor.Violet }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_BottomRow_Should_HaveNoEffect()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Black, TileColor.Pink, TileColor.Orange },
              { TileColor.Violet, TileColor.White, TileColor.Violet }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var purpleTile = new VioletTile();

        var newGameState = purpleTile.Press(gameState, 2, 0);

        await Assert.That(newGameState.GameStateEqual(gameState)).IsTrue();
    }
}