namespace MoraJai.Lib;

public class GrayTile : ITile
{
    public GameState Press(GameState state, int row, int col)
    {
        // Gray tile does nothing
        return state;
    }
}
