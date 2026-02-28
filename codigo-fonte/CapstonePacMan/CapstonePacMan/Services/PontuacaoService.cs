using System.Text.Json;

namespace CapstonePacMan.Services;

public class PontuacaoService
{
    private readonly string _caminhoArquivo;
    
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public PontuacaoService()
    {
        var pastaData = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CapstonePacMan"
        );

        Directory.CreateDirectory(pastaData);

        _caminhoArquivo = Path.Combine(pastaData, "pontuacoes.json");

        // Copia o arquivo inicial dos Assets se ainda não existir na pasta gravável
        if (!File.Exists(_caminhoArquivo))
        {
            var arquivoInicial = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                "Data",
                "pontuacoes.json"
            );

            if (File.Exists(arquivoInicial))
            {
                File.Copy(arquivoInicial, _caminhoArquivo);
            }
        }
    }
    
    public async Task<List<PontuacaoModel>> ObterPontuacoesAsync()
    {
        if (!File.Exists(_caminhoArquivo))
            return [];

        await using var stream = File.OpenRead(_caminhoArquivo);

        return await JsonSerializer.DeserializeAsync<List<PontuacaoModel>>(stream)
               ?? [];
    }
    
    public async Task SalvarPontuacaoAsync(PontuacaoModel novaPontuacao)
    {
        var lista = await ObterPontuacoesAsync();

        lista.Add(novaPontuacao);

        // Ordena do maior para o menor
        lista = lista
            .OrderByDescending(p => p.Pontos)
            .Take(10) // Top 10
            .ToList();

        await using var stream = File.Create(_caminhoArquivo);

        await JsonSerializer.SerializeAsync(stream, lista, JsonOptions);
    }
}
