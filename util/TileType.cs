namespace RandomMazeGame.util;

public class TileType
{
    public static readonly TileType Empty = new TileType('E');
    public static readonly TileType Wall = new TileType('W');
    public static readonly TileType Start = new TileType('S');
    public static readonly TileType Finish = new TileType('F');
    
    private char _symbol;

    public char Symbol
    {
        get { return _symbol; }
    }

    protected TileType(char symbol)
    {
        _symbol = symbol;
    }
}