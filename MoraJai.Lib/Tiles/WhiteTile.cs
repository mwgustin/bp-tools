namespace MoraJai.Lib;

public class WhiteTile : ITile
{
    // Turns itself and any orthogonally adjacent tiles of the same color grey, 
    // and changes any grey tiles orthogonally-adjacent to those tiles 
    // to the color of the pressed tile
    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };

        //bc of rules with blue, this isn't always guaranteed to be white
        var originalColor = state.Board[row, col];

        var adjacentPositions = new (int, int)[]
        {
      (row - 1, col), // Up
      (row + 1, col), // Down
      (row, col - 1), // Left
      (row, col + 1)  // Right
        };

        foreach (var (r, c) in adjacentPositions)
        {
            if (r >= 0 && r < 3 && c >= 0 && c < 3)
            {
                if (state.Board[r, c] == originalColor)
                {
                    newstate.Board[r, c] = TileColor.Gray;
                }
                else if (state.Board[r, c] == TileColor.Gray)
                {
                    newstate.Board[r, c] = originalColor;
                }
            }
        }
        newstate.Board[row, col] = TileColor.Gray;
        return newstate;
    }
}