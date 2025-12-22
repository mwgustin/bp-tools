namespace MoraJai.Lib.Tests;

public class GameStateTests
{
    [Test]
    public async Task IsSolved_WhenSolved_Should_ReturnTrue()
    {
        var board = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);

        await Assert.That(gameState.IsSolved()).IsTrue();
    }

    [Test]
    public async Task IsSolved_WhenNotSolved_Should_ReturnFalse()
    {
        var board = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);

        await Assert.That(gameState.IsSolved()).IsFalse();
    }

    [Test]
    public async Task IsSolved_WithMultipleGoals_Should_ReturnTrue()
    {
        var board = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Blue, TileColor.Blue, TileColor.Red }
        };
        var goals = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue, TileColor.Red };
        var gameState = new GameState(board, goals);

        await Assert.That(gameState.IsSolved()).IsTrue();
    }

    [Test]
    public async Task Constructor_WhenBoardIsNot3x3_Should_ThrowArgumentException()
    {
        var board = new TileColor[2, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green }
        };
        var goal = TileColor.Red;

        await Assert.ThrowsAsync<ArgumentException>(() => Task.Run(() => new GameState(board, goal)));
    }

    [Test]
    public async Task Constructor_WhenGoalsLengthIsNot4_Should_ThrowArgumentException()
    {
        var board = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goals = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue };

        await Assert.ThrowsAsync<ArgumentException>(() => Task.Run(() => new GameState(board, goals)));
    }


    [Test]
    public async Task GetHashCode_Should_ReturnSameHashCodeForEqualGameStates()
    {
        var board1 = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goals1 = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue, TileColor.Red };
        var gameState1 = new GameState(board1, goals1);

        var board2 = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goals2 = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue, TileColor.Red };
        var gameState2 = new GameState(board2, goals2);

        await Assert.That(gameState1.GetHashCode()).IsEqualTo(gameState2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_Should_ReturnDifferentHashCodeForDifferentGameStates()
    {
        var board1 = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goals1 = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue, TileColor.Red };
        var gameState1 = new GameState(board1, goals1);

        var board2 = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Green },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Blue, TileColor.Blue, TileColor.Red }
        };
        var goals2 = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue, TileColor.Red };
        var gameState2 = new GameState(board2, goals2);

        await Assert.That(gameState1.GetHashCode()).IsNotEqualTo(gameState2.GetHashCode());
    }

    [Test]
    public async Task ToString_Should_ReturnCorrectStringRepresentation()
    {
        var board = new TileColor[3, 3]
        {
              { TileColor.Red, TileColor.Blue, TileColor.Red },
              { TileColor.Green, TileColor.Yellow, TileColor.Green },
              { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goals = new TileColor[] { TileColor.Red, TileColor.Green, TileColor.Blue, TileColor.Red };
        var gameState = new GameState(board, goals);

        // Act & Assert
        await Verify(gameState.ToString());
    }

}
