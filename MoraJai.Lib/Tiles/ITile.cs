namespace MoraJai.Lib;

public interface ITile
{
    public GameState Press(GameState state, int row, int col);
}
