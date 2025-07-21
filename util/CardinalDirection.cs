namespace RandomMazeGame.util;

public class CardinalDirection
{
    //1st coordinate - vertical
    //2nd coordinate - horizontal
    public static readonly CardinalDirection West = new(new Vector2Int(-1, 0), "west");
    public static readonly CardinalDirection North = new(new Vector2Int(0, -1), "north");
    public static readonly CardinalDirection East = new(new Vector2Int(1, 0), "east");
    public static readonly CardinalDirection South = new(new Vector2Int(0, 1), "south");
    
    private static readonly Dictionary<CardinalDirection, CardinalDirection> s_opposites = new Dictionary<CardinalDirection, CardinalDirection>()
    {
        {West, East},
        {North, South},
        {East, West},
        {South, North},
    };
    
    private Vector2Int _normalizedVelocity;
    private string _id;

    public Vector2Int NormalizedVelocity
    {
        get { return _normalizedVelocity; }
    }

    protected CardinalDirection(Vector2Int normalizedVelocity, string id)
    {
        _normalizedVelocity = normalizedVelocity;
        _id = id;
    }

    public Vector2Int GetScaledVelocity(int coefficient)
    {
        return Vector2Int.Product(NormalizedVelocity, coefficient);
    }

    public CardinalDirection Opposite()
    {
        return s_opposites[this];
    }

    public override string ToString()
    {
        return $"CardinalDirection: {_id}";
    }

    public static CardinalDirection Random()
    {
        return Random(s_opposites.Keys.ToArray());
    }

    public static CardinalDirection Random(CardinalDirection[] possibleDirections)
    {
        return possibleDirections[new Random().Next(0, possibleDirections.Length)];
    }

    public static CardinalDirection[] ExcludeDirectionFrom(CardinalDirection[] directions,
        CardinalDirection excludedDirection)
    {
        List<CardinalDirection> possibleDirections = new List<CardinalDirection>();
        
        for (int i = 0; i < directions.Length; i++)
        {
            if (directions[i] == excludedDirection)
            {
                continue;
            }
            
            possibleDirections.Add(directions[i]);
        }
        
        return possibleDirections.ToArray();
    }

    internal static void PrintDebugInfo()
    {
        foreach (CardinalDirection direction in s_opposites.Keys)
        {
            Console.WriteLine($"{direction}: {direction.Opposite()}");
        }
    }
}