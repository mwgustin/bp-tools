namespace MoraJai.Lib;


public enum TileColor
{
    Gray,
    Black,
    Green,
    Pink,
    Yellow,
    Violet,
    White,
    Red,
    Orange,
    Blue,
}



public static class TileColorExtension
{
    public static ITile ToTile(this TileColor color) => color switch
    {
        TileColor.Gray => new GrayTile(),
        TileColor.Black => new BlackTile(),
        TileColor.Green => new GreenTile(),
        TileColor.Pink => new PinkTile(),
        TileColor.Yellow => new YellowTile(),
        TileColor.Violet => new VioletTile(),
        TileColor.White => new WhiteTile(),
        TileColor.Red => new RedTile(),
        TileColor.Orange => new OrangeTile(),
        TileColor.Blue => new BlueTile(),
        _ => throw new ArgumentException($"No tile implementation for color {color}"),
    };
}