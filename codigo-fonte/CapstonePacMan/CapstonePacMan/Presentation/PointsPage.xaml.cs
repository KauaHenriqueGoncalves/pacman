namespace CapstonePacMan.Presentation;

public sealed partial class PointsPage : Page
{
    private PointsViewModel ViewModel => (PointsViewModel)DataContext;

    public PointsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is IEnumerable<PontuacaoModel> lista)
        {
            ViewModel.CarregarPontuacoes(lista);
        }
    }

    private void Voltar_Click(object sender, RoutedEventArgs e)
    {
        if (Frame.CanGoBack)
            Frame.GoBack();
    }
}
