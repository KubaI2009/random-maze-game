namespace RandomMazeGame.util;

public class MazeCreator
{
    
    private MazeBoard _maze;
    
    private Vector2Int _pos;
    private CardinalDirection _direction;
    private CardinalDirection _sourceDirection;

    private int _stepCount;

    public Vector2Int Pos
    {
        get { return _pos; }
        set { _pos = value; }
    }

    public int X
    {
        get { return Pos.X; }
        set { Pos = new Vector2Int(value, Y); }
    }

    public int Y
    {
        get { return Pos.Y; }
        set { Pos = new Vector2Int(X, value); }
    }

    public CardinalDirection Direction
    {
        get { return _direction; }
        private set { _direction = value; }
    }

    public CardinalDirection SourceDirection
    {
        get { return _sourceDirection; }
        private set { _sourceDirection = value; }
    }

    public MazeCreator(int mazeHeight, int mazeWidth, Vector2Int startingPos, int stepCount)
    {
        _maze = new MazeBoard(mazeHeight, mazeWidth);
        _pos = startingPos;
        _stepCount = stepCount;
        _sourceDirection = CardinalDirection.West;
        _direction = GetRandomDirection();
    }

    public MazeBoard CreateMaze()
    {
        _maze.Clear();
        
        Vector2Int startPos = Pos;
        
        PlaceBridges();
        
        PlaceStartAndFinish(startPos, Pos);
        
        Pos = startPos;
        
        /*Vector2Int testPos = new Vector2Int(13, 5);

        Console.WriteLine(testPos);
        _maze.SetTile(testPos, TileType.Finish);*/
        
        return _maze;
    }

    private void PlaceBridges()
    {
        for (int i = 0; i < 10; i++)
        {
            Direction = GetRandomDirection();
            
            Move();
            
            SourceDirection = Direction.Opposite();
        }

        Console.WriteLine($"--{Pos}--");
    }

    private void PlaceStartAndFinish(Vector2Int startPos, Vector2Int finishPos)
    {
        _maze.SetTile(startPos, TileType.Start);
        _maze.SetTile(finishPos, TileType.Finish);
    }

    public void Move()
    {
        CardinalDirection newSourceDirection = Direction.Opposite();
        
        Console.WriteLine($"Target position - {GetTargetPosition()}");
        Console.WriteLine($"Target direction - {Direction}");
        Console.WriteLine($"Source direction - {SourceDirection}");
        Console.WriteLine($"New source direction - {newSourceDirection}");

        Console.WriteLine(_maze.GetTile(GetTargetPosition()));
        
        if (_maze.GetTile(GetTargetPosition()) != TileType.Pit)
        {
            Hop();
            
            SourceDirection = newSourceDirection;
        
            _maze.PrintRepresentation();
            
            return;
        }
        
        LeaveTrail();
            
        SourceDirection = newSourceDirection;
        
        _maze.PrintRepresentation();
    }

    public void LeaveTrail()
    {
        HopTo(Pos);
        
        for (int i = 0; i < _stepCount; i++)
        {
            HopTo(GetTargetPositionOfVelocity(Direction.NormalizedVelocity));
        }
    }

    public void Hop()
    {
        HopTo(GetTargetPosition());
    }

    public void HopTo(Vector2Int targetPos)
    {
        Console.Write(targetPos);
        
        try
        {
            _maze.SetTile(targetPos, _maze.GetTile(targetPos) == TileType.Pit ? TileType.Bridge : _maze.GetTile(targetPos));
            
            Console.WriteLine();
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine(" error");
            
            return;
        }
        
        Pos = targetPos;
    }

    private CardinalDirection GetRandomDirection()
    {
        List<CardinalDirection> excludedDirections = new List<CardinalDirection>() { SourceDirection };
        
        CardinalDirection direction = CardinalDirection.Random(excludedDirections.ToArray());

        while (!DirectionLeadsSomewhere(direction))
        {
            excludedDirections.Add(direction);
            
            direction = CardinalDirection.Random(excludedDirections.ToArray());
        }
        
        return direction;
        //return CardinalDirection.East;
    }

    private bool DirectionLeadsSomewhere(CardinalDirection direction)
    {
        Vector2Int velocity = direction.GetScaledVelocity(_stepCount);
        Vector2Int newPos = new Vector2Int(X + velocity.X, Y + velocity.Y);

        try
        {
            _maze.GetTile(newPos.Y, newPos.X);
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }

        return true;
    }

    public Vector2Int GetTargetPosition()
    {
        Vector2Int velocity = Direction.GetScaledVelocity(_stepCount);

        //Console.WriteLine($"Target positionnnnnnnnn - {new Vector2Int(X + velocity.X, Y + velocity.Y)}");
        
        return new Vector2Int(X + velocity.X, Y + velocity.Y);
    }

    public Vector2Int GetTargetPositionOfVelocity(Vector2Int velocity)
    {
        return new Vector2Int(X + velocity.X, Y + velocity.Y);
    }
}