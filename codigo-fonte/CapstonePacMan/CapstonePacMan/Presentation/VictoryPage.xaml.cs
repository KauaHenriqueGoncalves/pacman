namespace CapstonePacMan.Presentation;

public sealed partial class VictoryPage : Page
{
    private FinishGameViewModel ViewModel => (FinishGameViewModel)DataContext;
    
    public VictoryPage()
    {
        InitializeComponent();
    }
    
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        
        if (e.Parameter is int score)
        {
            ViewModel.SetPontos(score);
        }
        
        ViewModel.ReiniciarJogoEvent += OnReiniciarJogo;

        Focus(FocusState.Programmatic);
    }
    
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);

        ViewModel.ReiniciarJogoEvent -= OnReiniciarJogo;
    }

    private void OnReiniciarJogo()
    {
        Frame.Navigate(typeof(MainPage));
    }
}

