using Windows.Foundation;
using CapstonePacMan.Game;
using CapstonePacMan.Models.Enum;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;

namespace CapstonePacMan.Models;

public class PacMan : GameEntity
{
    public static event Action<Pill>? OnEating;
    public static event Action<Ghost>? OnCollidedWithGhost;
    
    // Rotação do PacMan
    private readonly RotateTransform _rotateTransform;
    private Direction CurrentDirection { get; set; } = Direction.Right;
    public Direction DesiredDirection { get; set; } = Direction.Right;
    private double Speed { get; set; } = 4;
    private static readonly BitmapImage PacManIdleImage;

    static PacMan()
    {
        try
        {
            PacManIdleImage = new BitmapImage(
                new Uri("ms-appx:///Assets/Sprites/PacManIdle.png")
            );
        }
        catch (UriFormatException ex)
        {
            Console.WriteLine("URI do sprite do PacMan é inválida.");
            Console.WriteLine(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao carregar o sprite do PacMan.");
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public PacMan(double x, double y)
    {
        X = x;
        Y = y;

        _rotateTransform = new RotateTransform
        {
            Angle = 0, // direita
            CenterX = Size / 2,
            CenterY = Size / 2
        };

        try
        {
            Sprite = new Ellipse
            {
                Width = Size,
                Height = Size,
                RenderTransform = _rotateTransform,
                Fill = new ImageBrush
                {
                    ImageSource = PacManIdleImage
                }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao criar o sprite do PacMan.");
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    private static (double x, double y) DirectionToVector(Direction dir)
    {
        return dir switch
        {
            Direction.Left  => (-1, 0),
            Direction.Right => (1, 0),
            Direction.Up    => (0, -1),
            Direction.Down  => (0, 1),
            _ => (0, 0)
        };
    }
    
    private void TryChangeDirection(GameMap map)
    {
        if (DesiredDirection == CurrentDirection) return;

        var (dx, dy) = DirectionToVector(DesiredDirection);

        var testRect = new Rect(
            X + dx * Speed,
            Y + dy * Speed,
            Size,
            Size
        );

        if (!map.IsWall(testRect))
        {
            CurrentDirection = DesiredDirection;
            UpdateDirection(dx, dy);
        }
    }
    
    private void UpdateDirection(double dx, double dy)
    {
        switch (dx, dy)
        {
            case (> 0, _): // dx maior que 0, e dy pode ser qualquer coisa
                CurrentDirection = Direction.Right;
                _rotateTransform.Angle = 0;
                break;

            case (< 0, _):
                CurrentDirection = Direction.Left;
                _rotateTransform.Angle = 180;
                break;

            case (_, > 0):
                CurrentDirection = Direction.Down;
                _rotateTransform.Angle = 90;
                break;

            case (_, < 0):
                CurrentDirection = Direction.Up;
                _rotateTransform.Angle = 270;
                break;
        }
    }

    public override void Update(GameMap map)
    {
        TryChangeDirection(map);

        // Move na direção atual
        var (dx, dy) = DirectionToVector(CurrentDirection);

        var nextRect = new Rect(
            X + dx * Speed,
            Y + dy * Speed,
            Size,
            Size
        );

        if (!map.IsWall(nextRect))
        {
            X += dx * Speed;
            Y += dy * Speed;
        }

        CheckCollisionWithPill(map);
    }
    
    public void CheckCollisionWithGhost(Ghost ghost)
    {
        var pacmanRect = new Rect(X, Y, Size, Size);
        var ghostRect = new Rect(ghost.X, ghost.Y, ghost.Size, ghost.Size);

        if (IsColliding(pacmanRect, ghostRect))
        {
            OnCollidedWithGhost?.Invoke(ghost);
        }
    }

    private void CheckCollisionWithPill(GameMap map)
    {
        var pacmanRect = new Rect(X, Y, Size, Size);

        for (var i = map.Pills.Count - 1; i >= 0; i--)
        {
            var pill = map.Pills[i];

            var pillRect = new Rect(
                pill.X,
                pill.Y,
                pill.Size,
                pill.Size
            );

            if (IsColliding(pacmanRect, pillRect))
            {
                OnEating?.Invoke(pill);
                break;
            }
        }
    }
    
    private static bool IsColliding(Rect a, Rect b)
    {
        return a.X < b.X + b.Width &&
               a.X + a.Width > b.X &&
               a.Y < b.Y + b.Height &&
               a.Y + a.Height > b.Y;
    }
}
