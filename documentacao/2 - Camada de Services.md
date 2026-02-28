# 2 - Camada de Services


A camada de **Services** encapsula toda a lógica de infraestrutura e integração com recursos externos (APIs, arquivos e mídia), garantindo que as **ViewModels** permaneçam focadas exclusivamente na lógica de apresentação.

Essa separação melhora:
- Organização do código  
- Testabilidade  
- Manutenção e reutilização  

<br>

## `AudioService`

Responsável por toda a experiência sonora do jogo, utilizando a biblioteca **LibVLCSharp**.

### Funcionalidades

- **Gerenciamento de Canais de Áudio**  
  Utiliza instâncias independentes de `MediaPlayer`, evitando que efeitos sonoros curtos (ex: comer pílula) interrompam sons contínuos (ex: sirene dos fantasmas).

- **Loop de Sons Ambiente**  
  Implementa repetição automática por meio de argumentos nativos da LibVLC  
  (`--input-repeat`), garantindo continuidade sonora.

- **Controle Global de Áudio**  
  Possui um estado estático `Muted`, permitindo ativar ou desativar o som de toda a aplicação instantaneamente.

- **Gerenciamento de Recursos**  
  Centraliza o carregamento dos arquivos localizados em `Assets/Sounds`.

<br>

## `PontuacaoService`

Responsável pela persistência das pontuações e pelo ranking local de jogadores.

### Funcionalidades

- **Persistência em JSON**  
  Utiliza `System.Text.Json` para salvar e carregar os dados de forma assíncrona.

- **Gestão de Arquivos Locais**  
  Realiza a cópia inicial do arquivo de pontuações dos `Assets` para o diretório  
  `LocalApplicationData`, garantindo compatibilidade com diferentes plataformas Desktop.

- **Lógica de Ranking**  
  Mantém automaticamente apenas as **10 maiores pontuações**, ordenadas de forma decrescente a cada nova inserção.