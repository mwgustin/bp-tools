namespace MoraJai.Lib;

public class YellowTile : ITile
{

    // Tile moves north (Swaps place with the tile above); 
    // no effect on the top row

    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };
        if (row > 0)
        {
            var temp = state.Board[row - 1, col];
            newstate.Board[row - 1, col] = state.Board[row, col];
            newstate.Board[row, col] = temp;
        }
        return newstate;
    }
}

