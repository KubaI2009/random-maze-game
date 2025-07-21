namespace RandomMazeGame.util;

public struct Vector2Int
{
    private int _x;
    private int _y;

    public int X
    {
        get { return _x; }
    }

    public int Y
    {
        get { return _y; }
    }

    public Vector2Int(int x, int y)
    {
        _x = x;
        _y = y;
    }
}