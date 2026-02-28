namespace CapstonePacMan.Presentation;

public sealed partial class MainPage : Page
{
    private MainViewModel ViewModel => (MainViewModel)DataContext;

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        ViewModel.IniciarJogoEvent += OnIniciarJogo;
        ViewModel.MostrarPontuacaoEvent += OnMostrarPontuacao;

        Focus(FocusState.Programmatic);
    }
    
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);

        ViewModel.IniciarJogoEvent -= OnIniciarJogo;
        ViewModel.MostrarPontuacaoEvent -= OnMostrarPontuacao;
    }

    private void OnIniciarJogo()
    {
        Frame.Navigate(typeof(GamePage));
    }

    private void OnMostrarPontuacao()
    {
        Frame.Navigate(typeof(PointsPage), ViewModel.Pontuacoes);
    }
}
