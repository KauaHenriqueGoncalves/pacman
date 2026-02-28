using CapstonePacMan.Game;
using Microsoft.UI.Xaml.Input;

namespace CapstonePacMan.Presentation;

public sealed partial class GamePage : Page
{
    private GameEngine engine;
    private GamePageViewModel ViewModel => (GamePageViewModel)DataContext;
    
    public GamePage()
    {
        InitializeComponent();

        engine = new GameEngine(GameCanvas);
        DataContext = new GamePageViewModel();
        ViewModel.SetEngine(engine);
        engine.Start();
        
        // Trocar para tela de game over
        engine.GameOver += () =>
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                Frame.Navigate(typeof(GameOverPage), engine.Score);
            });
        };

        engine.Victory += () =>
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                Frame.Navigate(typeof(VictoryPage), engine.Score);
            });
        };
    }
    
    private void GameCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        GameCanvas.Focus(FocusState.Pointer);
        e.Handled = true;
    }
    
    private void OnCanvasKeyDown(object sender, KeyRoutedEventArgs e)
    {
        engine.OnKeyDown(e.Key);
    }
    
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        engine?.Dispose();
        engine = null;

        base.OnNavigatedFrom(e);
    }
}
