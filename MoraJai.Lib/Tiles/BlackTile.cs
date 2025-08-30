namespace MoraJai.Lib;


public class BlackTile : ITile
{
    // black tile shifts row right (and wraps around)
    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };
        TileColor temp = state.Board[row, 2];
        newstate.Board[row, 2] = state.Board[row, 1];
        newstate.Board[row, 1] = state.Board[row, 0];
        newstate.Board[row, 0] = temp;
        return newstate;
    }
}
