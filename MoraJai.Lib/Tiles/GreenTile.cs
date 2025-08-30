namespace MoraJai.Lib;

public class GreenTile : ITile
{
    // green tiles "fly" to the opposite side of the board.
    // 0,0 goes to 2,2, 
    // 0,1 goes to 2,1,
    // 0,2 goes to 2,0,
    // 1,0 goes to 1,2,

    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };
        var temp = state.Board[2 - row, 2 - col];
        newstate.Board[2 - row, 2 - col] = state.Board[row, col];
        newstate.Board[row, col] = temp;
        return newstate;
    }
}