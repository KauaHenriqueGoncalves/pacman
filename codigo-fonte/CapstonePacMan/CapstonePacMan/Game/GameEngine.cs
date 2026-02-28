using Windows.System;
using CapstonePacMan.Models.Enum;

namespace CapstonePacMan.Game;

public class GameEngine : IDisposable
{ 
    private readonly Canvas _canvas;
    private readonly DispatcherTimer _timer;
    private readonly DispatcherTimer _frightTimer;
    private readonly AudioService _audioService;

    private GameMap Map { get; }
    private PacMan PacMan { get; }

    public int Vidas { get; private set; } = 3;
    public int Score { get; private set; }

    private List<Ghost> Ghosts { get; } = [];

    public event Action<int>? LivesChanged;
    public event Action<int>? ScoreChanged;
    public event Action? GameOver;
    public event Action? Victory;

    private bool _isGameOver;

    public GameEngine(Canvas canvas)
    {
        _canvas = canvas;

        Map = new GameMap();
        PacMan = new PacMan(38, 38);

        Ghosts.AddRange(
        [
            new Ghost(200, 120, GhostType.Blinky) { Speed = 5.0 },
            new Ghost(200, 120, GhostType.Inky)   { Speed = 5.0 },
            new Ghost(200, 120, GhostType.Pinky)  { Speed = 4.5 },
            new Ghost(200, 120, GhostType.Clyde)  { Speed = 7.0 }
        ]);

        _audioService = new AudioService();

        PacMan.OnEating += OnPacManAtePill;
        PacMan.OnCollidedWithGhost += HandleGhostCollision;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(33) // ~30 FPS
        };
        _timer.Tick += GameLoop;

        _frightTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(6)
        };
        _frightTimer.Tick += OnFrightTimerTick;
    }

    public void Start()
    {
        Map.Draw(_canvas);
        Map.CreatePills(_canvas);
        _timer.Start();
        _audioService.PlaySoundGhostSiren();
    }

    private void GameLoop(object? sender, object e)
    {
        PacMan.Update(Map);
        PacMan.Draw(_canvas);

        foreach (var ghost in Ghosts)
        {
            ghost.Update(Map);
            ghost.Draw(_canvas);
            PacMan.CheckCollisionWithGhost(ghost);
        }
    }

    private void HandleGhostCollision(Ghost ghost)
    {
        if (ghost.IsFrightened)
        {
            _audioService.PlaySoundEatGhost();
            ghost.Respawn();
            Score += 200;
            ScoreChanged?.Invoke(Score);
        }
        else
        {
            OnPacManKilled();
        }
    }

    private void OnPacManAtePill(Pill pill)
    {
        _canvas.Children.Remove(pill.Sprite);
        Map.Pills.Remove(pill);

        _audioService.PlaySoundEatingPacMan();

        if (pill.IsPower)
        {
            ActivateFrightMode();
        }
        
        Score += pill.IsPower ? 50 : 10;
        ScoreChanged?.Invoke(Score);
        
        if (Map.Pills.Count > 0) return;

        _timer.Stop();
        _frightTimer.Stop();
        _audioService.StopGhostSound();
        Victory?.Invoke();
        _audioService.PlaySoundVictory();
    }

    private void OnPacManKilled()
    {
        if (_isGameOver) return;

        _audioService.PlaySoundDeathingPacMan();
        Vidas--;
        LivesChanged?.Invoke(Vidas);

        if (Vidas > 0)
        {
            ResetPacManPosition();
        }
        else
        {
            _isGameOver = true;
            _timer.Stop();
            _audioService.StopGhostSound();
            GameOver?.Invoke();
        }
    }

    private void ActivateFrightMode()
    {
        _audioService.PlaySoundGhostFright();
        foreach (var ghost in Ghosts)
        {
            ghost.EnterFrightMode();
        }
        _frightTimer.Stop(); // cancela um timer anterior caso o PacMan coma outra power pill antes de acabar
        _frightTimer.Start(); 
    }

    private void ResetPacManPosition()
    {
        PacMan.X = 38;
        PacMan.Y = 38;
        PacMan.DesiredDirection = Direction.Right;
    }

    private void OnFrightTimerTick(object? sender, object e)
    {
        foreach (var ghost in Ghosts)
        {
            ghost.ExitFrightMode();
            _audioService.PlaySoundGhostSiren();
            _frightTimer.Stop();  
        }
    }

    public void OnKeyDown(VirtualKey key)
    {
        PacMan.DesiredDirection = key switch
        {
            VirtualKey.Left  => Direction.Left,
            VirtualKey.Right => Direction.Right,
            VirtualKey.Up    => Direction.Up,
            VirtualKey.Down  => Direction.Down,
            _ => PacMan.DesiredDirection
        };
    }

    public void Dispose()
    {
        _timer.Stop();
        _frightTimer.Stop();

        _timer.Tick -= GameLoop;
        _frightTimer.Tick -= OnFrightTimerTick;

        PacMan.OnEating -= OnPacManAtePill;
        PacMan.OnCollidedWithGhost -= HandleGhostCollision;

        _canvas.Children.Clear();
        
        GC.SuppressFinalize(this); // Cumpre o padrão IDisposable
    }
}
