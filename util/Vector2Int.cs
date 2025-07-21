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

    public void Add(Vector2Int other)
    {
        this = Sum(this, other);
    }

    public void Scale(int coefficient)
    {
        this = Product(this, coefficient);
    }

    public bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
        return $"Vector2Int: [{X}, {Y}]";
    }

    public static Vector2Int Sum(Vector2Int v, Vector2Int w)
    {
        return new Vector2Int(v.X + w.X, v.Y + w.Y);
    }

    public static Vector2Int Product(Vector2Int v, int coefficient)
    {
        return new Vector2Int(v.X * coefficient, v.Y * coefficient);
    }
}