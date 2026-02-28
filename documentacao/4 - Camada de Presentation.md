# 4 - Camada de Presentation (MVVM)

A camada de **Presentation** é responsável pela interface do usuário e pela lógica de interação. No Uno Platform, ela é dividida entre **Views** (definidas em XAML e Code-behind) e **ViewModels** (que gerenciam o estado e os comandos).

Esta estrutura segue os princípios:
- **Separação de Preocupações:** O XAML cuida apenas do layout; a ViewModel cuida da lógica.
- **Comunicação por Eventos:** Navegação e ações complexas são disparadas por eventos, mantendo as classes testáveis.
- **Reatividade:** Uso de `ObservableProperty` para atualização instantânea da interface.s

<br>

## Main Screen (`MainPage` & `MainViewModel`)

A porta de entrada do jogo. Gerencia o menu principal e o acesso ao ranking.

### Funcionalidades
- **Navegação Centralizada:** Utiliza comandos (`IniciarCommand`, `PontuacaoCommand`) que disparam eventos capturados pela View para navegar entre as páginas.
- **Toggle de Áudio:** Gerencia o estado global de `Muted` do `AudioService` de forma estática.
- **Integração de Dados:** Recupera a lista de recordes através do `PontuacaoService` antes de navegar para a tela de pontos.

<br>

## Game Screen (`GamePage` & `GamePageViewModel`)

A tela onde a ação acontece. É aqui que o `GameEngine` é instanciado e vinculado à UI.

### Funcionalidades
- **Hospedagem de Engine:** A `GamePage` cria e gerencia o ciclo de vida do `GameEngine`, garantindo que o `Dispose()` seja chamado ao sair da tela.
- **Data Binding de Status:** O `GamePageViewModel` observa os eventos de mudança de vida e pontos da Engine e os expõe via `[ObservableProperty]` para os `TextBlocks` de cabeçalho.
- **Captura de Input:** O Canvas do jogo possui o foco para capturar eventos de `KeyDown`, repassando-os diretamente para a lógica de direção do Pac-Man na Engine.

<br>

## Finish Screens (`GameOverPage`, `VictoryPage` & `FinishGameViewModel`)

Telas de encerramento que lidam com o resultado final da partida.

### Funcionalidades
- **Persistência de Recordes:** O `FinishGameViewModel` captura o nome do jogador via *Two-Way Binding* e utiliza o `PontuacaoService` para salvar o resultado de forma assíncrona.
- **Reutilização de Lógica:** Ambas as páginas (Vitória e Derrota) compartilham o `FinishGameViewModel`, respeitando o princípio **DRY**.
- **Navegação de Retorno:** Após salvar o nome, o sistema limpa a pilha de navegação e retorna o usuário ao menu principal.

<br>

## Ranking Screen (`PointsPage` & `PointsViewModel`)

Exibição simplificada do ranking local.

### Funcionalidades
- **Passagem de Parâmetros:** Recebe a lista de pontuações via parâmetro de navegação (`OnNavigatedTo`), evitando chamadas redundantes ao disco.
- **Apresentação em Lista:** Utiliza uma `ListView` com `DataTemplate` personalizado para alinhar nomes e pontuações de forma tabular.

<br>

## Tecnologias de Presentation

| Componente | Tecnologia | Finalidade |
| :--- | :--- | :--- |
| **Binding** | `x:Bind` / `Binding` | Sincronização entre UI e Modelos de View. |
| **Comandos** | `RelayCommand` | Execução de métodos a partir de cliques em botões. |
| **Notificação** | `ObservableObject` | Notificar a UI sobre mudanças de propriedades. |
| **Navegação** | `Frame.Navigate` | Troca de contextos entre menus, jogo e recordes. |