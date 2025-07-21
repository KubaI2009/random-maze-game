namespace RandomMazeGame.util;

public class TileType
{
    public static readonly TileType Bridge = new TileType('+');
    public static readonly TileType Pit = new TileType('=');
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

    public override string ToString()
    {
        return $"TileType: {Symbol}";
    }
}