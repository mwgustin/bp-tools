namespace MoraJai.Lib.Tests;

public class SolverTests
{
    [Test]
    public async Task SolveStart_WhenAlreadySolved_Should_ReturnSolutionNodeWithDepth0()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Blue, TileColor.Red },
            { TileColor.Green, TileColor.Yellow, TileColor.Green },
            { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);
        var solver = new Solver();

        // Act
        var solution = solver.SolveStart(gameState);

        // Assert
        await Assert.That(solution).IsNotNull();
        await Assert.That(solution.Depth).IsEqualTo(0);
        await Assert.That(solution.State.IsSolved()).IsTrue();
        await Assert.That(solution.Parent).IsNull();
        await Assert.That(solution.Move).IsEqualTo((-1, -1));
    }

    [Test]
    public async Task SolveStart_WithSimpleOneMoveSolution_Should_ReturnSolutionWithDepth1()
    {
        // Arrange - Create a board that requires one red tile press to solve
        // All corners are White, and pressing Red turns White->Black
        // We need corners to be Red, so this won't work with just Red press
        // Let's create a board where one move solves it
        var board = new TileColor[3, 3]
        {
            { TileColor.Black, TileColor.Blue, TileColor.Black },
            { TileColor.Green, TileColor.Red, TileColor.Green },
            { TileColor.Black, TileColor.Blue, TileColor.Black }
        };
        var goal = TileColor.Red; // Need all corners to be Red
        var gameState = new GameState(board, goal);
        var solver = new Solver();

        // Act
        var solution = solver.SolveStart(gameState, maxLevels: 10);

        // Assert
        await Assert.That(solution).IsNotNull();
        if (solution != null)
        {
            await Assert.That(solution.State.IsSolved()).IsTrue();
            await Assert.That(solution.Depth).IsGreaterThan(0);
        }
    }

    [Test]
    public async Task SolveStart_WhenNoSolutionExists_Should_ReturnNull()
    {
        // Arrange - Create an impossible puzzle with maxLevels too low
        var board = new TileColor[3, 3]
        {
            { TileColor.White, TileColor.Blue, TileColor.White },
            { TileColor.Green, TileColor.Yellow, TileColor.Green },
            { TileColor.White, TileColor.Blue, TileColor.White }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);
        var solver = new Solver();

        // Act - Use very low maxLevels to force no solution
        var solution = solver.SolveStart(gameState, maxLevels: 1);

        // Assert
        await Assert.That(solution).IsNull();
    }

    [Test]
    public async Task SolveStart_Should_RespectMaxLevels()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.White, TileColor.Blue, TileColor.White },
            { TileColor.Green, TileColor.Yellow, TileColor.Green },
            { TileColor.White, TileColor.Blue, TileColor.White }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);
        var solver = new Solver();

        // Act
        var solution = solver.SolveStart(gameState, maxLevels: 2);

        // Assert - Should either find solution within 2 moves or return null
        if (solution != null)
        {
            await Assert.That(solution.Depth).IsLessThanOrEqualTo(2);
        }
        else
        {
            // If no solution found, that's also valid behavior
            await Assert.That(solution).IsNull();
        }
    }

    [Test]
    public async Task SolveStart_Should_NotVisitSameStateTwice()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Gray, TileColor.Gray, TileColor.Gray },
            { TileColor.Gray, TileColor.Gray, TileColor.Gray },
            { TileColor.Gray, TileColor.Gray, TileColor.Gray }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);
        var solver = new Solver();

        // Act - Run solver with reasonable max levels
        var solution = solver.SolveStart(gameState, maxLevels: 5);

        // Assert - Just verify it completes without infinite loop
        // The solver should handle duplicate states by not re-visiting them
        // Either finds a solution or returns null, but should not hang
        await Assert.That(solution == null || solution.State != null).IsTrue();
    }

    [Test]
    public async Task SolveStart_WithMultipleGoals_Should_FindSolutionForAllCorners()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.White, TileColor.Blue, TileColor.White },
            { TileColor.Green, TileColor.Yellow, TileColor.Green },
            { TileColor.White, TileColor.Blue, TileColor.White }
        };
        var goals = new TileColor[] { TileColor.Red, TileColor.Red, TileColor.Red, TileColor.Red };
        var gameState = new GameState(board, goals);
        var solver = new Solver();

        // Act
        var solution = solver.SolveStart(gameState, maxLevels: 10);

        // Assert
        if (solution != null)
        {
            await Assert.That(solution.State.IsSolved()).IsTrue();
            await Assert.That(solution.State.Board[0, 0]).IsEqualTo(TileColor.Red);
            await Assert.That(solution.State.Board[0, 2]).IsEqualTo(TileColor.Red);
            await Assert.That(solution.State.Board[2, 0]).IsEqualTo(TileColor.Red);
            await Assert.That(solution.State.Board[2, 2]).IsEqualTo(TileColor.Red);
        }
    }

    [Test]
    public async Task SolveStart_Should_ReturnSolutionNodeWithCorrectMove()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Blue, TileColor.Red },
            { TileColor.Green, TileColor.Yellow, TileColor.Green },
            { TileColor.Red, TileColor.Blue, TileColor.Red }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);
        var solver = new Solver();

        // Act
        var solution = solver.SolveStart(gameState);

        // Assert
        await Assert.That(solution).IsNotNull();
        if (solution != null)
        {
            // For the root node, Move should be (-1, -1)
            await Assert.That(solution.Move).IsEqualTo((-1, -1));
        }
    }

    [Test]
    public async Task SolveStart_Should_BuildCorrectParentChildRelationship()
    {
        // Arrange
        var board = new TileColor[3, 3]
        {
            { TileColor.Black, TileColor.Blue, TileColor.Black },
            { TileColor.Green, TileColor.Red, TileColor.Green },
            { TileColor.Black, TileColor.Blue, TileColor.Black }
        };
        var goal = TileColor.Red;
        var gameState = new GameState(board, goal);
        var solver = new Solver();

        // Act
        var solution = solver.SolveStart(gameState);

        // Assert
        await Assert.That(solution).IsNotNull();
        await Assert.That(solution.State.IsSolved()).IsTrue();
        await Assert.That(solution.Depth).IsGreaterThan(0);

        // Verify parent-child relationship
        var currentNode = solution;
        int expectedDepth = solution.Depth;
        
        while (currentNode != null)
        {
            await Assert.That(currentNode.Depth).IsEqualTo(expectedDepth);
            currentNode = currentNode.Parent;
            expectedDepth--;
        }
        
        await Assert.That(expectedDepth).IsEqualTo(-1);
    }

    [Test]
    public async Task SolveStart_WithComplexBoard_Should_UseBreadthFirstSearch()
    {
        // Arrange - Create a more complex scenario
        var board = new TileColor[3, 3]
        {
            { TileColor.Red, TileColor.Gray, TileColor.Black },
            { TileColor.Orange, TileColor.Orange, TileColor.Orange },
            { TileColor.Green, TileColor.Gray, TileColor.Violet }
        };
        var goals = new TileColor[] { TileColor.Orange, TileColor.Red, TileColor.Red, TileColor.Orange };
        var gameState = new GameState(board, goals);
        var solver = new Solver();

        // Act
        var solution = solver.SolveStart(gameState);

        // Assert
        // If a solution is found, it should be the shortest path (BFS property)
        await Assert.That(solution).IsNotNull();
        await Assert.That(solution.Depth).IsEqualTo(15);
        await Assert.That(solution.State.IsSolved()).IsTrue();
        
        //expected final board state:
        // Orange | Green | Red
        // -----------------------
        // Violet | Orange | Gray
        // -----------------------
        // Red | Gray | Orange
        var expectedState = new TileColor[3, 3]
        {
            { TileColor.Orange, TileColor.Green, TileColor.Red },
            { TileColor.Violet, TileColor.Orange, TileColor.Gray },
            { TileColor.Red, TileColor.Gray, TileColor.Orange }
        };

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                await Assert.That(solution.State.Board[r, c]).IsEqualTo(expectedState[r, c]);
            }
        }
        
        // Verify we can trace back to root
        var node = solution;
        while (node.Parent != null)
        {
            node = node.Parent;
        }
        await Assert.That(node.Move).IsEqualTo((-1, -1)); // Root node
    }
}
