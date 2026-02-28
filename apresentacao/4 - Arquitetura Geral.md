# 4 - Arquitetura do Projeto

A estrutura do projeto foi organizada com foco na separação de responsabilidades.. A divisão em camadas permite que regras de negócio, interface e serviços funcionem de forma desacoplada.

Abaixo está a visão geral da arquitetura.


## 1. Game

A pasta `Game/` concentra o núcleo da lógica do jogo.

**Responsabilidades:**

- Controle do loop
- Atualização das entidades
- Gerenciamento do mapa
- Coordenação das regras gerais da partida

**Principais arquivos:**

- `GameEngine.cs`: Responsável por orquestrar o funcionamento do jogo (atualizações e estados).

- `GameMap.cs`: Representa a estrutura do mapa, incluindo paredes, organização das pílulas e verificação de colisão.

<br>

## 2. Models

A pasta `Models/` contém as entidades do domínio do jogo.

### Entidades principais

- `GameEntity.cs`: Classe base para todas as entidades do jogo (posição, tamanho, sprite, atualização).

- `Pacman.cs`: Implementa movimentação, colisões e eventos relacionados ao jogador.

- `Ghost.cs`: Define comportamento, estados e movimentação dos fantasmas.

- `Pill.cs`: Representa as pílulas normais e especiais (Power Pills).

### Estruturas auxiliares

- `Direction.cs`: Enumeração para controle de direção.

- `GhostType.cs`: Enumeração para diferenciar tipos de fantasmas.

- `PontuacaoModel.cs`: Modelo responsável por representar os dados de pontuação.

<br>

## 3. Presentation

A pasta `Presentation/` concentra a camada de interface e interação com o usuário.

A estrutura segue o padrão MVVM (Model-View-ViewModel).

**Views (.xaml):**
- `MainPage.xaml`
- `GamePage.xaml`
- `GameOverPage.xaml`
- `VictoryPage.xaml`
- `PointsPage.xaml`

**ViewModels:**
- `MainViewModel.cs`
- `GameOverViewModel.cs`
- `PointsViewModel.cs`
- etc.

Essa separação permite que a lógica da interface fique desacoplada das regras do jogo.

<br>

## 4. Services

A pasta `Services/` contém serviços reutilizáveis e auxiliares.

- `AudioService.cs`: Responsável pelo controle de sons e efeitos sonoros.

- `PontuacaoService.cs`: Gerencia persistência ou manipulação da pontuação.

<br>

## 5. Organização Geral

Simplificando:

- **Game**: Coordena o funcionamento do jogo  
- **Models**: Representam as entidades e regras de domínio  
- **Presentation**: Interface e interação com o usuário (MVVM)  
- **Services**: Funcionalidades auxiliares desacopladas  

A arquitetura foi implementada para manter a logica desaclopada, apesar de ainda possuir um espirito de layer architecture, porém ainda manteve o foco para o desenvolvimento de game.