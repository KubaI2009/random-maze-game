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
        return Random(new CardinalDirection[0]);
    }

    public static CardinalDirection Random(CardinalDirection[] excludedDirections)
    {
        Random random = new Random();
        CardinalDirection[] possibleDirections = GetPossibleDirections(excludedDirections);
        
        return possibleDirections[random.Next(0, possibleDirections.Length)];
    }

    private static CardinalDirection[] GetPossibleDirections(CardinalDirection[] excludedDirections)
    {
        List<CardinalDirection> possibleDirections = new List<CardinalDirection>();

        foreach (CardinalDirection direction in s_opposites.Keys)
        {
            if (!excludedDirections.Contains(direction))
            {
                possibleDirections.Add(direction);
            }
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