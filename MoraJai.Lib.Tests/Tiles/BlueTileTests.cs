namespace MoraJai.Lib.Tests;


public class BlueTileTests
{
    [Test]
    public async Task Press_Should_DelegateToCenterTile_Black()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Black, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var blueTile = new BlueTile();

        var newGameState = blueTile.Press(gameState, 0, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Red, TileColor.Blue },
              { TileColor.Green, TileColor.Black, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_Should_DelegateToCenterTile_Red()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Red, TileColor.Green },
              { TileColor.Red, TileColor.White, TileColor.Black }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var blueTile = new BlueTile();

        var newGameState = blueTile.Press(gameState, 0, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Red, TileColor.Green },
              { TileColor.Red, TileColor.Black, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_Should_DoNothing_When_CenterIsBlue()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Blue, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var blueTile = new BlueTile();

        var newGameState = blueTile.Press(gameState, 0, 1);

        var expectedBoard = initialBoard;
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_Should_DelegateToCenterTile_White()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Gray },
              { TileColor.Green, TileColor.White, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var blueTile = new BlueTile();

        var newGameState = blueTile.Press(gameState, 0, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Gray, TileColor.Blue },
              { TileColor.Green, TileColor.White, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }
}