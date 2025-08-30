using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MoraJai.Lib;


public record GameState : IEquatable<GameState>
{
    // 3x3 board
    private const int BOARD_SIZE = 3;

    // goal size is 4 (corners)
    private const int GOAL_SIZE = 4;

    public TileColor[,] Board;
    public readonly TileColor[] Goal;
    public GameState(TileColor[,] board, TileColor Goal)
      : this(board, [Goal, Goal, Goal, Goal])
    {
    }
    public GameState(TileColor[,] board, TileColor[] Goal)
    {
        if (board.GetLength(0) != BOARD_SIZE || board.GetLength(1) != BOARD_SIZE)
        {
            throw new ArgumentException("Board must be 3x3");
        }

        if (Goal.Length != GOAL_SIZE)
        {
            throw new ArgumentException("Goal must have exactly 4 colors");
        }

        Board = board;
        this.Goal = Goal;
    }


    // if corners are goal color then return true
    public bool IsSolved()
    {
        return Board[0, 0] == Goal[0] &&
               Board[0, 2] == Goal[1] &&
               Board[2, 0] == Goal[2] &&
               Board[2, 2] == Goal[3];
    }

    public override int GetHashCode()
    {
        StringBuilder sb = new StringBuilder();
        // sb.Append((int)Goal);
        sb.Append(string.Join(",", Goal));
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                sb.Append((int)Board[r, c]);
            }
        }

        return sb.ToString().GetHashCode();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                sb.Append(Board[r, c].ToString().PadRight(7));
                if (c < 2) sb.Append("| ");
            }
            sb.AppendLine();
            if (r < 2) sb.AppendLine(new string('-', 23));
        }
        return sb.ToString();
    }

    // 0,0 | 0,1 | 0,2
    // -----------------
    // 1,0 | 1,1 | 1,2
    // -----------------
    // 2,0 | 2,1 | 2,2
}

