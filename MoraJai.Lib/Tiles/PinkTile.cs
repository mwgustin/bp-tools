namespace MoraJai.Lib;

public class PinkTile : ITile
{
    // All adjacent tiles (including diagonally-adjacent tiles) 
    // rotate around the pressed tile clockwise, teleporting around to the next 
    // valid position when they would otherwise up outside the box edge
    // If the tile is not diagonal or orthogonally adjacent, nothing happens.
    //
    // example: 
    // pink tile is at center (1,1),
    // all tiles rotate clockwise around it.
    //
    // another example:
    // if pink tile is at (0,0), the tiles at (0,1), (1,1), and (1,0) rotate clockwise, wrapping around but ignoring 0,0
    // so that 0,1 goes to 1,1, 1,1 goes to 1,0, and 1,0 goes to 0,1
    // but nothing else changes.

    public GameState Press(GameState gameState, int row, int col)
    {
        GameState newstate = gameState with { Board = (TileColor[,])gameState.Board.Clone() };

        var positions = new (int, int)[]
        {
      (row - 1, col - 1), (row - 1, col), (row - 1, col + 1),
      (row, col + 1),
      (row + 1, col + 1), (row + 1, col), (row + 1, col - 1),
      (row, col - 1)
        };

        var validPositions = positions
          .Where(pos => pos.Item1 >= 0 && pos.Item1 < 3 && pos.Item2 >= 0 && pos.Item2 < 3)
          .ToArray();

        var colors = validPositions
          .Select(pos => gameState.Board[pos.Item1, pos.Item2])
          .ToArray();

        for (int i = 0; i < validPositions.Length; i++)
        {
            var (r, c) = validPositions[i];
            newstate.Board[r, c] = colors[(i - 1 + validPositions.Length) % validPositions.Length];
        }

        return newstate;
    }
}
