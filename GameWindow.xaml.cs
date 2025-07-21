using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using RandomMazeGame.util;

namespace RandomMazeGame;

/// <summary>
/// Interaction logic for GameWindow.xaml
/// </summary>
public partial class GameWindow : Window
{
	private static readonly double s_fps = 0.01d;
	
    private const int _height = 15;
    private const int _width = 15;
    
    private TextBlock[,] _renderedBoard;
    private MazeBoard _metaMaze;
    
    private int _ticks;
    private DispatcherTimer _tickTimer;

    public GameWindow()
    {
	    InitGame(null, null);
    }

    private void InitGame(Object? sender, EventArgs? e)
    {
        InitializeComponent();
        
        _renderedBoard = new TextBlock[_height, _width]
        {
			{ tbx0_0, tbx0_1, tbx0_2, tbx0_3, tbx0_4, tbx0_5, tbx0_6, tbx0_7, tbx0_8, tbx0_9, tbx0_10, tbx0_11, tbx0_12, tbx0_13, tbx0_14 },
			{ tbx1_0, tbx1_1, tbx1_2, tbx1_3, tbx1_4, tbx1_5, tbx1_6, tbx1_7, tbx1_8, tbx1_9, tbx1_10, tbx1_11, tbx1_12, tbx1_13, tbx1_14 },
			{ tbx2_0, tbx2_1, tbx2_2, tbx2_3, tbx2_4, tbx2_5, tbx2_6, tbx2_7, tbx2_8, tbx2_9, tbx2_10, tbx2_11, tbx2_12, tbx2_13, tbx2_14 },
			{ tbx3_0, tbx3_1, tbx3_2, tbx3_3, tbx3_4, tbx3_5, tbx3_6, tbx3_7, tbx3_8, tbx3_9, tbx3_10, tbx3_11, tbx3_12, tbx3_13, tbx3_14 },
			{ tbx4_0, tbx4_1, tbx4_2, tbx4_3, tbx4_4, tbx4_5, tbx4_6, tbx4_7, tbx4_8, tbx4_9, tbx4_10, tbx4_11, tbx4_12, tbx4_13, tbx4_14 },
			{ tbx5_0, tbx5_1, tbx5_2, tbx5_3, tbx5_4, tbx5_5, tbx5_6, tbx5_7, tbx5_8, tbx5_9, tbx5_10, tbx5_11, tbx5_12, tbx5_13, tbx5_14 },
			{ tbx6_0, tbx6_1, tbx6_2, tbx6_3, tbx6_4, tbx6_5, tbx6_6, tbx6_7, tbx6_8, tbx6_9, tbx6_10, tbx6_11, tbx6_12, tbx6_13, tbx6_14 },
			{ tbx7_0, tbx7_1, tbx7_2, tbx7_3, tbx7_4, tbx7_5, tbx7_6, tbx7_7, tbx7_8, tbx7_9, tbx7_10, tbx7_11, tbx7_12, tbx7_13, tbx7_14 },
			{ tbx8_0, tbx8_1, tbx8_2, tbx8_3, tbx8_4, tbx8_5, tbx8_6, tbx8_7, tbx8_8, tbx8_9, tbx8_10, tbx8_11, tbx8_12, tbx8_13, tbx8_14 },
			{ tbx9_0, tbx9_1, tbx9_2, tbx9_3, tbx9_4, tbx9_5, tbx9_6, tbx9_7, tbx9_8, tbx9_9, tbx9_10, tbx9_11, tbx9_12, tbx9_13, tbx9_14 },
			{ tbx10_0, tbx10_1, tbx10_2, tbx10_3, tbx10_4, tbx10_5, tbx10_6, tbx10_7, tbx10_8, tbx10_9, tbx10_10, tbx10_11, tbx10_12, tbx10_13, tbx10_14 },
			{ tbx11_0, tbx11_1, tbx11_2, tbx11_3, tbx11_4, tbx11_5, tbx11_6, tbx11_7, tbx11_8, tbx11_9, tbx11_10, tbx11_11, tbx11_12, tbx11_13, tbx11_14 },
			{ tbx12_0, tbx12_1, tbx12_2, tbx12_3, tbx12_4, tbx12_5, tbx12_6, tbx12_7, tbx12_8, tbx12_9, tbx12_10, tbx12_11, tbx12_12, tbx12_13, tbx12_14 },
			{ tbx13_0, tbx13_1, tbx13_2, tbx13_3, tbx13_4, tbx13_5, tbx13_6, tbx13_7, tbx13_8, tbx13_9, tbx13_10, tbx13_11, tbx13_12, tbx13_13, tbx13_14 },
			{ tbx14_0, tbx14_1, tbx14_2, tbx14_3, tbx14_4, tbx14_5, tbx14_6, tbx14_7, tbx14_8, tbx14_9, tbx14_10, tbx14_11, tbx14_12, tbx14_13, tbx14_14 }
		};
        
        _metaMaze = new MazeBoard(_height, _width);
        
        InitTickTimer();
        
        //_metaMaze.SetTile(4, TileType.Empty);
    }
    
    //game loop start
    private void InitTickTimer()
    {
	    _ticks = 0;
        
	    _tickTimer = new DispatcherTimer();

	    _tickTimer.Tick += RenderBoard;
        
	    _tickTimer.Interval = TimeSpan.FromSeconds(s_fps);
	    
	    _tickTimer.Start();
    }

    private void RenderBoard(Object? sender, EventArgs? e)
    {
	    for (int i = 0; i < _metaMaze.Count; i++)
	    {
		    Vector2Int pos = _metaMaze.PositionOfIndex(i);
		    TileStyle style = TileStyle.Of(_metaMaze.GetTile(i));

		    _renderedBoard[pos.Y, pos.X].Background = style.Color;
		    _renderedBoard[pos.Y, pos.X].Text = style.Text;
	    }

	    //_metaMaze.PrintRepresentation();
    }
}