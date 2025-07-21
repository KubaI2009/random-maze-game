using System.Collections;

namespace RandomMazeGame.util;

public class MazeBoard
{
    private TileType[,] _board;

    public int Height
    {
        get { return _board.GetLength(0); }
    }

    public int Width
    {
        get { return _board.GetLength(1); }
    }

    public int Count
    {
        get { return Height * Width; }
    }
    
    public MazeBoard(int height, int width)
    {
        _board = CreateBoard(height, width);
    }

    public TileType GetTile(int i)
    {
        return GetTile(PositionOfIndex(i));
    }

    public TileType GetTile(Vector2Int pos)
    {
        return GetTile(pos.Y, pos.X);
    }

    public TileType GetTile(int y, int x)
    {
        return _board[y, x];
    }

    public void SetTile(int i, TileType tile)
    {
        SetTile(PositionOfIndex(i), tile);
    }

    public void SetTile(Vector2Int pos, TileType tile)
    {
        SetTile(pos.Y, pos.X, tile);
    }

    public void SetTile(int y, int x, TileType tile)
    {
        _board[y, x] = tile;
    }

    public Vector2Int PositionOfIndex(int i)
    {
        return new Vector2Int(i % Width, i / Width);
    }

    public void PrintRepresentation()
    {
        PrintSeparator();
        
        for (int i = 0; i < _board.Length; i++)
        {
            Vector2Int pos = PositionOfIndex(i);
            
            Console.Write(_board[pos.Y, pos.X].Symbol);

            if (i % Width == Width - 1)
            {
                Console.WriteLine();
                continue;
            }

            Console.Write(' ');
        }
        
        PrintSeparator();
    }

    private void PrintSeparator()
    {
        for (int i = 0; i < Width - 1; i++)
        {
            Console.Write("--");
        }

        Console.WriteLine("-");
    }

    public void Clear()
    {
        for (int i = 0; i < _board.Length; i++)
        {
            SetTile(PositionOfIndex(i), TileType.Pit);
        }
    }

    public bool IsFilled()
    {
        return IsFilled(1);
    }

    public bool IsFilled(int stepLength)
    {
        for (int i = 0; i < _board.Length; i++)
        {
            Vector2Int pos = PositionOfIndex(i);

            if (pos.X % stepLength == 0
                && pos.Y % stepLength == 0
                && GetTile(pos) == TileType.Pit)
            {
                return false;
            }
        }
        
        return true;
    }

    public void AddRandomConnections(int stepLength, int maxAmount)
    {
        for (int i = 0; i < _board.Length; i++)
        {
            if (maxAmount <= 0)
            {
                return;
            }
            
            Vector2Int pos = PositionOfIndex(i);

            if (pos.X % stepLength == 0
                && pos.Y % stepLength == 0
                && GetTile(pos) == TileType.Pit
                && new Random().Next(10) == 0)
            {
                SetTile(pos, TileType.Bridge);
                
                maxAmount--;
            }
        }

        /*if (amount > 0)
        {
            AddRandomConnections(stepLength, amount);
        }*/
    }

    private static TileType[,] CreateBoard(int height, int width)
    {
        TileType[,] board = new TileType[height, width];

        for (int i = 0; i < height * width; i++)
        {
            int y = i / width;
            int x = i % width;
            
            board[y, x] = TileType.Pit;
        }
        
        return board;
    }
}