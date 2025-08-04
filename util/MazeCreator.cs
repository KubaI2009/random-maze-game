namespace RandomMazeGame.util;

public class MazeCreator
{
    
    private MazeBoard _maze;
    
    private Vector2Int _pos;
    private CardinalDirection _direction;

    private int _stepLength;

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

    public MazeCreator(int mazeHeight, int mazeWidth, Vector2Int startingPos, int stepLength)
    {
        _maze = new MazeBoard(mazeHeight, mazeWidth);
        _pos = startingPos;
        _stepLength = stepLength;
        _direction = GetRandomDirection();
    }

    public MazeBoard CreateMaze()
    {
        _maze.Clear();
        
        Vector2Int startPos = Pos;
        
        PlaceBridges();
        
        _maze.AddRandomConnections(_stepLength, 5);
        
        PlaceStartAndFinish(startPos, GetFinishPos());
        
        Pos = startPos;
        
        /*Vector2Int testPos = new Vector2Int(13, 5);

        Console.WriteLine(testPos);
        _maze.SetTile(testPos, TileType.Finish);*/
        
        return _maze;
    }

    private void PlaceBridges()
    {
        while (!_maze.IsFilled(_stepLength))
        {
            Direction = GetRandomDirection();
            
            Move();
        }

        //Console.WriteLine($"--{Pos}--");
    }

    private void PlaceStartAndFinish(Vector2Int startPos, Vector2Int finishPos)
    {
        _maze.SetTile(startPos, TileType.Start);
        _maze.SetTile(finishPos, TileType.Finish);
    }

    public void Move()
    {
        CardinalDirection newSourceDirection = Direction.Opposite();
        
        //Console.WriteLine($"Target position - {GetTargetPosition()}");
        //Console.WriteLine($"Target direction - {Direction}");

        //Console.WriteLine(_maze.GetTile(GetTargetPosition()));
        
        if (_maze.GetTile(GetTargetPosition()) != TileType.Pit)
        {
            Hop();
        
            //_maze.PrintRepresentation();
            
            return;
        }
        
        LeaveTrail();
        
        //_maze.PrintRepresentation();
    }

    public void LeaveTrail()
    {
        HopTo(Pos);
        
        for (int i = 0; i < _stepLength; i++)
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
        //Console.Write(targetPos);
        
        try
        {
            _maze.SetTile(targetPos, _maze.GetTile(targetPos) == TileType.Pit ? TileType.Bridge : _maze.GetTile(targetPos));
            
            //Console.WriteLine();
        }
        catch (IndexOutOfRangeException)
        {
            //Console.WriteLine(" error");
            
            return;
        }
        
        Pos = targetPos;
    }

    private CardinalDirection GetRandomDirection()
    {
        List<CardinalDirection> possibleDirections = new List<CardinalDirection>()
        {
            CardinalDirection.West,
            CardinalDirection.North,
            CardinalDirection.East,
            CardinalDirection.South
        };
        
        CardinalDirection direction;
        
        while (true)
        {
            if (possibleDirections.Count == 0)
            {
                possibleDirections = new List<CardinalDirection>()
                {
                    CardinalDirection.West,
                    CardinalDirection.North,
                    CardinalDirection.East,
                    CardinalDirection.South
                };

                while (true)
                {
                    direction = CardinalDirection.Random(possibleDirections.ToArray());

                    if (DirectionLeadsSomewhere(direction))
                    {
                        return direction;
                    }
            
                    possibleDirections.Remove(direction);
                }
            }
            
            direction = CardinalDirection.Random(possibleDirections.ToArray());

            if (DirectionIsValid(direction))
            {
                return direction;
            }
            
            possibleDirections.Remove(direction);
        }
    }

    private bool DirectionIsValid(CardinalDirection direction)
    {
        return DirectionLeadsSomewhere(direction) && DirectionLeadsIntoPit(direction);
    }

    private bool DirectionLeadsIntoPit(CardinalDirection direction)
    {
        Vector2Int velocity = direction.GetScaledVelocity(_stepLength);
        Vector2Int newPos = new Vector2Int(X + velocity.X, Y + velocity.Y);

        try
        {
            return _maze.GetTile(newPos.Y, newPos.X) == TileType.Pit;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    private bool DirectionLeadsSomewhere(CardinalDirection direction)
    {
        Vector2Int velocity = direction.GetScaledVelocity(_stepLength);
        Vector2Int targetPos = Vector2Int.Sum(Pos, velocity);

        try
        {
            _maze.GetTile(targetPos);
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }

        return true;
    }

    public Vector2Int GetTargetPosition()
    {
        Vector2Int velocity = Direction.GetScaledVelocity(_stepLength);

        //Console.WriteLine($"Target positionnnnnnnnn - {new Vector2Int(X + velocity.X, Y + velocity.Y)}");
        
        return new Vector2Int(X + velocity.X, Y + velocity.Y);
    }

    public Vector2Int GetTargetPositionOfVelocity(Vector2Int velocity)
    {
        return new Vector2Int(X + velocity.X, Y + velocity.Y);
    }

    private Vector2Int GetFinishPos()
    {
        //Console.WriteLine("Getting finish position");
        
        for (int i = _maze.Count - 1; i >= 0; i--)
        {
            Vector2Int pos = _maze.PositionOfIndex(i);

            //Console.WriteLine(pos);

            if (_maze.GetTile(pos) == TileType.Bridge)
            {
                return pos;
            }
        }
        
        return new Vector2Int(0, 0);
    }
}