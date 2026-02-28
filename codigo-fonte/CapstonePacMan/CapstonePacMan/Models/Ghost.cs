using Windows.Foundation;
using CapstonePacMan.Models.Enum;
using CapstonePacMan.Game;
using Microsoft.UI.Xaml.Media.Imaging;

namespace CapstonePacMan.Models;

public class Ghost : GameEntity
{
    private readonly Random _random = new();
    public GhostType Type { get; }
    public double Speed { get; set; } = 3;
    private Direction _currentDirection = Direction.Left;
    
    public bool IsFrightened { get; private set; }
    private readonly double _spawnX;
    private readonly double _spawnY;
    private static readonly BitmapImage BlinkySprite;
    private static readonly BitmapImage InkySprite;
    private static readonly BitmapImage PinkySprite;
    private static readonly BitmapImage ClydeSprite;
    private static readonly BitmapImage FrightenedSprite;
    private readonly BitmapImage _normalSprite;
    private readonly BitmapImage _frightenedSprite;

    static Ghost()
    {
        try
        {
            BlinkySprite = new BitmapImage(new Uri("ms-appx:///Assets/Sprites/GhostBlink.png"));
            InkySprite = new BitmapImage(new Uri("ms-appx:///Assets/Sprites/GhostInky.png"));
            PinkySprite = new BitmapImage(new Uri("ms-appx:///Assets/Sprites/GhostPinky.png"));
            ClydeSprite = new BitmapImage(new Uri("ms-appx:///Assets/Sprites/GhostClyde.png"));

            FrightenedSprite = new BitmapImage(
                new Uri("ms-appx:///Assets/Sprites/GhostScared.png")
            );
        }
        catch (UriFormatException ex)
        {
            Console.WriteLine("Caminho do sprite do Ghost é inválido.");
            Console.WriteLine(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao carregar sprites do Ghost.");
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public Ghost(double x, double y, GhostType type)
    {
        X = x;
        Y = y;
        _spawnX = x;
        _spawnY = y;

        _normalSprite = type switch
        {
            GhostType.Blinky => BlinkySprite,
            GhostType.Inky   => InkySprite,
            GhostType.Pinky  => PinkySprite,
            GhostType.Clyde  => ClydeSprite,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Tipo de fantasma inválido")
        };

        _frightenedSprite = FrightenedSprite;

        Sprite = new Image
        {
            Width = Size,
            Height = Size,
            Source = _normalSprite,
            Stretch = Stretch.Uniform
        };
    }
    
    public override void Update(GameMap map)
    {
        if (TryMove(_currentDirection, map)) return; // Tenta andar na direção atual
        List<Direction> dirs = GetPossibleDirections(map); // Caso bloqueie, pega as possíveis direções
        if (dirs.Count == 0) return;
        _currentDirection = dirs[_random.Next(dirs.Count)];
        TryMove(_currentDirection, map);
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
    
    private static bool IsOpposite(Direction a, Direction b)
    {
        return (a == Direction.Left && b == Direction.Right) ||
               (a == Direction.Right && b == Direction.Left) ||
               (a == Direction.Up && b == Direction.Down) ||
               (a == Direction.Down && b == Direction.Up);
    }
    
    public void EnterFrightMode()
    {
        IsFrightened = true;
        ((Image)Sprite).Source = _frightenedSprite;
    }

    public void ExitFrightMode()
    {
        IsFrightened = false;
        ((Image)Sprite).Source = _normalSprite;
    }

    public void Respawn()
    {
        X = _spawnX;
        Y = _spawnY;
        ExitFrightMode();
    }
    
    private bool TryMove(Direction dir, GameMap map)
    {
        var (dx, dy) = DirectionToVector(dir);

        var nextRect = new Rect(
            X + dx * Speed,
            Y + dy * Speed,
            Size,
            Size
        );

        if (map.IsWall(nextRect)) return false;

        X += dx * Speed;
        Y += dy * Speed;
        
        return true;
    }
    
    private List<Direction> GetPossibleDirections(GameMap map)
    {
        var result = new List<Direction>();

        Direction[] allDirections =
        {
            Direction.Up,
            Direction.Left,
            Direction.Down,
            Direction.Right
        };
        
        foreach (var dir in allDirections)
        {
            if (IsOpposite(dir, _currentDirection))
                continue;

            var (dx, dy) = DirectionToVector(dir);

            var testRect = new Rect(
                X + dx * Speed,
                Y + dy * Speed,
                Size,
                Size
            );

            if (!map.IsWall(testRect)) result.Add(dir);
        }
        
        return result;
    }
}
