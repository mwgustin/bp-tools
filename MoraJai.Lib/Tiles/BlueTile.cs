namespace MoraJai.Lib;

public class BlueTile : ITile
{
    // Behaves the same way as the center tile (1,1); 
    // No effect if the center tile is blue.

    public GameState Press(GameState state, int row, int col)
    {
        GameState newstate = state with { Board = (TileColor[,])state.Board.Clone() };

        var centerTile = state.Board[1, 1];

        if (centerTile == TileColor.Blue)
        {
            return newstate;
        }

        ITile tileBehavior = centerTile.ToTile();
        return tileBehavior.Press(newstate, row, col);
    }
}

