using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CapstonePacMan.Services;

namespace CapstonePacMan.Presentation;

public partial class MainViewModel : ObservableObject
{
    public delegate void IniciarJogoHandler();
    public event IniciarJogoHandler? IniciarJogoEvent;
    public event Action? MostrarPontuacaoEvent;

    private readonly PontuacaoService _pontuacaoService;

    public ObservableCollection<PontuacaoModel> Pontuacoes { get; } = new();

    public ICommand IniciarCommand { get; }
    public ICommand PontuacaoCommand { get; }
    public ICommand MudoCommand { get; }
    public ICommand SairCommand { get; }

    public MainViewModel()
    {
        _pontuacaoService = new PontuacaoService();

        IniciarCommand = new RelayCommand(OnIniciarCommand);
        PontuacaoCommand = new RelayCommand(OnPontuacaoCommand);
        MudoCommand = new RelayCommand(OnMudoCommand);
        SairCommand = new RelayCommand(OnSairCommand);
    }

    private void OnIniciarCommand()
    {
        IniciarJogoEvent?.Invoke();
    }

    private async void OnPontuacaoCommand()
    {
        Pontuacoes.Clear();

        var lista = await _pontuacaoService.ObterPontuacoesAsync();

        foreach (var p in lista)
            Pontuacoes.Add(p);

        MostrarPontuacaoEvent?.Invoke();
    }

    private void OnMudoCommand()
    {
        AudioService.Muted = !AudioService.Muted;
    }

    private void OnSairCommand()
    {
        Application.Current.Exit();
    }
}
