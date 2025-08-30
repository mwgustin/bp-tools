namespace MoraJai.Lib;

public class VioletTile : ITile
{
    //Tile moves south (Swaps place with the tile bellow); 
    // no effect on the bottom row

    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };
        if (row < 2)
        {
            var temp = state.Board[row + 1, col];
            newstate.Board[row + 1, col] = state.Board[row, col];
            newstate.Board[row, col] = temp;
        }
        return newstate;
    }
}
