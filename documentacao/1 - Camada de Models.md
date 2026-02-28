# 1 - Camada de Models

A camada de **Models** representa o núcleo do jogo. Ela contém as definições de estado, as regras de movimento e a lógica de colisão. Estas classes são projetadas para serem focadas estritamente no comportamento das entidades.

Essa estrutura promove:
- **DRY (Don't Repeat Yourself):** Reutilização de lógica comum através de herança (`GameEntity`).
- **CUPID:** Código focado no domínio e com responsabilidades únicas.
- **Previsibilidade:** O comportamento físico e as regras de colisão são isolados da interface.

<br>

## Subpasta: `Enum`

Os enumeradores definem os tipos fixos e direções permitidas no sistema, garantindo tipagem forte e evitando o uso de strings ou números mágicos.

### `Direction`
Define os eixos de movimento permitidos para qualquer entidade móvel.
- **Valores:** `Right`, `Left`, `Up`, `Down`.

### `GhostType`
Identifica as quatro personalidades clássicas dos fantasmas.
- **Valores:** `Blinky`, `Inky`, `Pinky`, `Clyde`.

<br>

## `GameEntity`

Classe base abstrata que define o contrato fundamental para qualquer objeto renderizável e atualizável no jogo.

### Funcionalidades
- **Gerenciamento de Transformação:** Centraliza propriedades de posição (`X`, `Y`) e tamanho (`Size`).
- **Renderização em Canvas:** O método `Draw` manipula diretamente o posicionamento do `FrameworkElement` dentro de um `Canvas` do *Uno Platform*.
- **Contrato de Atualização:** Define o método abstrato `Update`, forçando cada entidade a implementar sua própria lógica de comportamento a cada tick do jogo.

<br>

## `PacMan`

O protagonista do jogo. Esta classe gerencia o movimento reativo e o sistema de eventos de interação.

### Funcionalidades
- **Sistema de Direção Dupla:** Diferencia a `CurrentDirection` da `DesiredDirection`, permitindo que o jogador "pré-selecione" uma curva antes de chegar a um cruzamento.
- **Feedback Visual Dinâmico:** Utiliza `RotateTransform` para girar o sprite em tempo real com base no vetor de movimento.
- **Arquitetura Baseada em Eventos:** Expõe os eventos estáticos `OnEating` e `OnCollidedWithGhost`, permitindo que outras camadas reajam a colisões sem acoplamento direto.

<br>

## `Ghost`

Representa os inimigos com lógica de navegação autônoma.

### Funcionalidades
- **IA de Movimentação:** Implementa lógica de decisão em cruzamentos (através do método `GetPossibleDirections`), evitando que o fantasma inverta o sentido bruscamente e garantindo que ele sempre tente se mover.
- **Máquina de Estados Visual:** Gerencia a troca dinâmica de sprites entre o estado normal e o estado `IsFrightened`.
- **Sistema de Respawn:** Capacidade de resetar para as coordenadas de origem (`_spawnX`, `_spawnY`) ao ser derrotado ou ao reiniciar a partida.

<br>

## `Pill`

Representa os itens consumíveis espalhados pelo labirinto.

### Funcionalidades
- **Tipagem de Item:** A propriedade `IsPower` define se a pílula é comum ou uma especial, o que altera o tamanho do sprite e as regras de pontuação.
- **Posicionamento Relativo:** Calcula automaticamente sua posição centralizada com base no tamanho do *tile* do mapa.

<br>

## `PontuacaoModel`

Um Objeto de Transferência de Dados (DTO) simples para o sistema de ranking.

### Funcionalidades
- **Estrutura de Dados:** Contém `Nome` e `Pontos`, utilizada pelo `PontuacaoService` para serialização e persistência em arquivos JSON.