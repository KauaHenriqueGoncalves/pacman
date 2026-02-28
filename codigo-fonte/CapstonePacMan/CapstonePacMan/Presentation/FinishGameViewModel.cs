using CapstonePacMan.Services;

namespace CapstonePacMan.Presentation;

public partial class FinishGameViewModel : ObservableObject
{
    public delegate void ReiniciarJogoHandler();
    public event ReiniciarJogoHandler? ReiniciarJogoEvent;
    private readonly PontuacaoService _pontuacaoService = new();

    [ObservableProperty]
    private string nomeJogador = "";

    [ObservableProperty]
    private int pontos;

    public ICommand SalvarCommand { get; }
    
    public ICommand ReiniciarCommand { get; }

    public FinishGameViewModel()
    {
        SalvarCommand = new AsyncRelayCommand(SalvarPontuacaoAsync);
        ReiniciarCommand = new RelayCommand(OnReiniciarCommand);
    }
    
    public void SetPontos(int score)
    {
        Pontos = score;
    }

    private async Task SalvarPontuacaoAsync()
    {
        if (string.IsNullOrWhiteSpace(NomeJogador))
            return;

        var pontuacao = new PontuacaoModel
        {
            Nome = NomeJogador,
            Pontos = Pontos
        };

        await _pontuacaoService.SalvarPontuacaoAsync(pontuacao);

        ReiniciarJogoEvent?.Invoke();
    }

    private void OnReiniciarCommand()
    {
        ReiniciarJogoEvent?.Invoke();
    }
}
