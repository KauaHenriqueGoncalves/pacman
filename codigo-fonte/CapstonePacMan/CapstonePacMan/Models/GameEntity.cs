using CapstonePacMan.Game;

namespace CapstonePacMan.Models;

public abstract class GameEntity
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Size { get; set; } = 28;
    
    public FrameworkElement Sprite { get; protected set; }
 
    public void Draw(Canvas canvas)
    {
        if (!canvas.Children.Contains(Sprite)) canvas.Children.Add(Sprite);
        Canvas.SetLeft(Sprite, X);
        Canvas.SetTop(Sprite, Y);
    }
    
    public abstract void Update(GameMap map);
}

