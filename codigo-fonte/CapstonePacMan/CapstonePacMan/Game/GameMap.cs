using Windows.Foundation;
using Microsoft.UI;
using Microsoft.UI.Xaml.Shapes;

namespace CapstonePacMan.Game;

public class GameMap
{
    private int TileSize { get; set; } = 38;
    private readonly int[,] _map =
    {
        // 0 1 2 3 4 5 6 7 8 9 10 11 12 13 14
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, // 0
        {1,0,0,0,0,0,0,1,2,0,0,0,0,0,1}, // 1
        {1,0,1,1,1,1,0,1,0,1,1,1,1,0,1}, // 2
        {1,0,1,0,0,0,0,0,0,0,0,0,1,0,1}, // 3
        {1,0,1,0,1,1,1,1,1,1,1,0,1,0,1}, // 4
        {1,0,1,0,0,0,0,1,0,0,0,0,1,0,1}, // 5
        {1,0,1,1,1,1,0,1,0,1,1,1,1,0,1}, // 6
        {1,0,0,0,0,0,0,1,2,0,0,0,0,0,1}, // 7
        {1,0,1,1,1,1,1,1,1,1,1,1,1,0,1}, // 8
        {1,2,0,0,0,0,0,1,0,0,0,0,0,2,1}, // 9
        {1,0,1,0,1,1,1,1,1,1,1,0,1,0,1}, //10
        {1,0,1,0,0,0,0,0,0,0,0,0,1,0,1}, //11
        {1,0,1,1,1,1,0,1,0,1,1,1,1,0,1}, //12
        {1,0,0,0,0,0,0,1,0,0,0,0,0,0,1}, //13
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}, //14
    };
    
    public List<Pill> Pills { get; private set; } = [];
    
    public void Draw(Canvas canvas)
    {
        for (var y = 0; y < _map.GetLength(0); y++)
        {
            for (var x = 0; x < _map.GetLength(1); x++)
            {
                if (_map[y, x] == 1)
                {
                    var wall = new Rectangle
                    {
                        Width = TileSize,
                        Height = TileSize,
                        Fill = new SolidColorBrush(Colors.Blue)
                    };
                    Canvas.SetLeft(wall, x * TileSize);
                    Canvas.SetTop(wall, y * TileSize);
                    canvas.Children.Add(wall);
                }
            }
        }
    }
    
    public void CreatePills(Canvas canvas)
    {
        Pills.Clear();

        for (var y = 0; y < _map.GetLength(0); y++)
        {
            for (var x = 0; x < _map.GetLength(1); x++)
            {
                if (_map[y, x] == 0)
                {
                    var pill = new Pill(x, y, TileSize, false);
                    Pills.Add(pill);
                    pill.Draw(canvas);
                }
                else if (_map[y, x] == 2)
                {
                    var pill = new Pill(x, y, TileSize, true);
                    Pills.Add(pill);
                    pill.Draw(canvas);
                }
            }
        }
    }
    
    public bool IsWall(Rect rect)
    {
        var leftTile   = (int)(rect.Left   / TileSize);
        var rightTile  = (int)(rect.Right  / TileSize);
        var topTile    = (int)(rect.Top    / TileSize);
        var bottomTile = (int)(rect.Bottom / TileSize);

        for (var y = topTile; y <= bottomTile; y++)
        {
            for (var x = leftTile; x <= rightTile; x++)
            {
                if (x < 0 || y < 0 ||
                    x >= _map.GetLength(1) ||
                    y >= _map.GetLength(0))
                    return true;

                if (_map[y, x] == 1)
                    return true;
            }
        }
        return false;
    }
}
