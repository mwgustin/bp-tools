namespace MoraJai.Lib.Tests;


public class GreenTileTests
{
    [Test]
    public async Task Press_00_Should_FlyToOppositeCorner()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Green, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Black, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var greenTile = new GreenTile();

        var newGameState = greenTile.Press(gameState, 0, 0);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Black, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Green }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();

    }

    [Test]
    public async Task Press_01_Should_FlyToOppositeSide()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Green, TileColor.Red },
              { TileColor.Red, TileColor.Black, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var greenTile = new GreenTile();

        var newGameState = greenTile.Press(gameState, 0, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Black, TileColor.Red },
              { TileColor.Red, TileColor.Green, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_10_Should_FlyToOppositeSide()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Black, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var greenTile = new GreenTile();

        var newGameState = greenTile.Press(gameState, 1, 0);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Black, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_11_Should_FlyToItself()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Green, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var greenTile = new GreenTile();

        var newGameState = greenTile.Press(gameState, 1, 1);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Green, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }

    [Test]
    public async Task Press_22_Should_FlyToOppositeCorner()
    {
        var initialBoard = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Black, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Green }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(initialBoard, goal);
        var greenTile = new GreenTile();

        var newGameState = greenTile.Press(gameState, 2, 2);

        var expectedBoard = new TileColor[3, 3]
        {
              { TileColor.Green, TileColor.Blue, TileColor.Red },
              { TileColor.Red, TileColor.Black, TileColor.Red },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var expectedGameState = new GameState(expectedBoard, goal);

        await Assert.That(newGameState.GameStateEqual(expectedGameState)).IsTrue();
    }
}