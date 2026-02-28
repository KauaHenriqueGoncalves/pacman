using CapstonePacMan.Game;
using System.Collections.ObjectModel;

namespace CapstonePacMan.Presentation;

public partial class GamePageViewModel : ObservableObject
{
    private GameEngine _engine;
    
    [ObservableProperty]
    private int vidas;

    [ObservableProperty] 
    private int score;
    
    public void SetEngine(GameEngine engine)
    {
        _engine = engine;
        Vidas = engine.Vidas;
        Score = engine.Score;

        _engine.LivesChanged += OnLivesChanged;
        _engine.ScoreChanged += OnPontosAtualizados;
    }

    private void OnLivesChanged(int lives)
    {
        Vidas = lives;
    }

    private void OnPontosAtualizados(int score)
    {
        Score = score;
    }
}
