using System.Collections.ObjectModel;

namespace CapstonePacMan.Presentation;

public class PointsViewModel: ObservableObject
{
    public ObservableCollection<PontuacaoModel> Pontuacoes { get; } = new();

    public void CarregarPontuacoes(IEnumerable<PontuacaoModel> lista)
    {
        Pontuacoes.Clear();

        foreach (var p in lista)
            Pontuacoes.Add(p);
    }
}
