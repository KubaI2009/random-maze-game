using System.Windows.Media;

namespace RandomMazeGame.util;

public struct TileStyle
{
    private static readonly Dictionary<TileType, TileStyle> s_tileStyles = new Dictionary<TileType, TileStyle>()
    {
        {TileType.Bridge, new TileStyle("", new SolidColorBrush(Colors.SlateGray))},
        {TileType.Pit, new TileStyle("", new SolidColorBrush(Colors.Black))},
        {TileType.Start, new TileStyle("S", new SolidColorBrush(Colors.GreenYellow))},
        {TileType.Finish, new TileStyle("F", new SolidColorBrush(Colors.DodgerBlue))}
    };
    
    private string _text;
    private SolidColorBrush _color;

    public string Text
    {
        get { return _text; }
    }

    public SolidColorBrush Color
    {
        get { return _color; }
    }

    public TileStyle(string text, SolidColorBrush color)
    {
        _text = text;
        _color = color;
    }

    public override string ToString()
    {
        return $"TileStyle: [{_text}, {_color}]";
    }

    public static TileStyle Of(TileType tile)
    {
        try
        {
            return s_tileStyles[tile];
        }
        catch (KeyNotFoundException)
        {
            return s_tileStyles[TileType.Bridge];
        }
    }
}