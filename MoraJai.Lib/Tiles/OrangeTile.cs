namespace MoraJai.Lib;

public class OrangeTile : ITile
{

    // Orange tile looks at the orthogonally adjacent tiles (up, down, left, right).
    // if there is a majority color among them (2 or more), it changes the orange tile to that color.
    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };

        var adjacentPositions = new (int, int)[]
        {
      (row - 1, col), // Up
      (row + 1, col), // Down
      (row, col - 1), // Left
      (row, col + 1)  // Right
        };

        var colorCount = new Dictionary<TileColor, int>();

        foreach (var (r, c) in adjacentPositions)
        {
            if (r >= 0 && r < 3 && c >= 0 && c < 3)
            {
                var color = state.Board[r, c];
                if (colorCount.ContainsKey(color))
                {
                    colorCount[color]++;
                }
                else
                {
                    colorCount[color] = 1;
                }
            }
        }

        IEnumerable<TileColor> majorityColor = colorCount.OrderByDescending(kv => kv.Value)
                                            .Where(kv => kv.Value >= 2)
                                            .Select(kv => kv.Key);

        if (majorityColor.Count() == 1)
        {
            newstate.Board[row, col] = majorityColor.First();
        }
        return newstate;
    }
}
