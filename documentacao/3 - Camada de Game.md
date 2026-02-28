# 3 - Camada de Game

A pasta **Game** contém a lógica central de execução, o controle de tempo e a definição estrutural do mundo. Enquanto os **Models** guardam os dados, as classes aqui presentes ditam como esses dados interagem entre si a cada milissegundo.

Essa camada é responsável por:
- **Game Loop:** Controle de frames por segundo (FPS).
- **Orquestração:** Comunicação entre Entidades, Áudio e Interface.
- **Regras:** Tradução da matriz lógica em colisões físicas.

<br>

## `GameEngine`

O `GameEngine` é o cérebro operacional do projeto. Ele implementa a interface `IDisposable` para garantir que recursos de memória e timers sejam liberados corretamente ao fechar o jogo.

### Funcionalidades

- **Controle de Timers (Loops):**
 - Utiliza um `DispatcherTimer` principal configurado para **~30 FPS** para processar movimentação e renderização.
  - Gerencia um `_frightTimer` exclusivo para controlar a duração do estado de vulnerabilidade dos fantasmas.

- **Gerenciamento de Estado e Pontuação:** 
  - Controla o sistema de vidas, incrementa o score e dispara eventos críticos como `GameOver` e `Victory`.
  - Atua como mediador de áudio, acionando o `AudioService` nos momentos exatos  de morte, consumo de pílulas e entre outros.

- **Processamento de Input:** 
  - Captura as teclas direcionais via `OnKeyDown` e as traduz em intenções de movimento para o modelo do Pac-Man.

- **Detecção de Colisões Dinâmicas:** 
  - A cada frame, verifica a proximidade entre o Pac-Man e cada um dos fantasmas, decidindo se o jogador deve perder uma vida ou se o fantasma deve ser consumido.

<br>

## `GameMap`

Responsável pela definição física do labirinto e pela lógica de ocupação de espaço.

### Funcionalidades

- **Matriz de Definição (Grid):** 
  - Utiliza uma matriz de inteiros (`int[,]`) para mapear o cenário:
    - `1`: Paredes (Walls)
    - `0`: Pílulas comuns
    - `2`: Super Pílulas (Power Pills)

- **Lógica de Colisão por Tiles:** 
  - Implementa o método `IsWall`, que converte as coordenadas de pixels da tela em índices da matriz. Isso permite uma detecção de colisão extremamente eficiente e performática, evitando checagens desnecessárias entre objetos.

- **Geração de Consumíveis:** 
  - Popula o cenário dinamicamente através do método `CreatePills`, instanciando os modelos de pílulas nas posições livres do grid.

- **Renderização de Cenário:** 
  - Desenha as paredes do labirinto diretamente no `Canvas`, criando a base visual sobre a qual as entidades se movem.

<br>

## Detalhes de Implementação

| Recurso | Técnica Utilizada | Objetivo |
| :--- | :--- | :--- |
| **Sincronização** | `DispatcherTimer` | Garantir que a lógica de jogo rode na Thread de UI do Uno Platform. |
| **Limpeza de Recursos** | `IDisposable` | Evitar vazamentos de memória (Memory Leaks) e eventos órfãos. |
| **Performance** | Tradução de Coordenadas | Detectar paredes via índices de matriz em vez de checar cada retângulo individualmente. |
