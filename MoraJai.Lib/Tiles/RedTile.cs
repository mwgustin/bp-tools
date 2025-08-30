namespace MoraJai.Lib;

public class RedTile : ITile
{
    // Turns all white tiles black, and all black tiles red
    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                newstate.Board[r, c] = state.Board[r, c] switch
                {
                    TileColor.White => TileColor.Black,
                    TileColor.Black => TileColor.Red,
                    _ => state.Board[r, c]
                };
            }
        }

        return newstate;
    }
}