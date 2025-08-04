namespace RandomMazeGame.util;

public class Player
{
    private Vector2Int _pos;
    private MazeBoard _board;
    
    private int _moveCounter;

    public Vector2Int Pos
    {
        get { return _pos; }
        set { _pos = value; }
    }

    public MazeBoard Board
    {
        get { return _board; }
        private set { _board = value; }
    }

    public int MoveCounter
    {
        get { return _moveCounter; }
        private set { _moveCounter = value; }
    }
    
    public Player(Vector2Int startingPos, MazeBoard board)
    {
        _pos = startingPos;
        _board = board;
    }

    public void Move(Vector2Int displacement)
    {
        if (CanGoTo(Vector2Int.Sum(Pos, displacement)))
        {
            IncrementMoveCounter();
        }
        
        Pos = GetTargetPosition(displacement);
    }

    private void IncrementMoveCounter()
    {
        MoveCounter++;
    }

    public Vector2Int GetTargetPosition(Vector2Int displacement)
    {
        Vector2Int targetPos = Vector2Int.Sum(Pos, displacement);
        
        return CanGoTo(targetPos) ? targetPos : Pos;
    }

    public bool CanGoTo(Vector2Int targetPos)
    {
        try
        {
            if (Board.GetTile(targetPos) == TileType.Pit)
            {
                return false;
            }
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
        
        return true;
    }
}