# 5 - Organização dos Assets

O projeto organiza seus recursos externos em uma estrutura de subpastas dentro de `Assets`. O acesso a esses arquivos é feito de forma relativa, garantindo portabilidade entre diferentes ambientes de execução Desktop.

<br>

## Subpasta: `Data`
Contém arquivos de dados estáticos e modelos iniciais para persistência.
* **`pontuacoes.json`**: Arquivo base que define a estrutura do ranking. Ele é utilizado pelo `PontuacaoService` como semente para criar o arquivo de recordes na pasta de dados local do usuário (`LocalApplicationData`).

<br>

## Subpasta: `Sprites`
Armazena toda a identidade visual do jogo em formato de imagem.
* **Entidades:** Contém os arquivos `.png` para o Pac-Man e para os quatro fantasmas.
* **Vulnerabilidade:** Inclui o sprite `GhostScared.png`, utilizado quando os fantasmas entram em modo *Frightened*.
* **Itens:** Sprites para as pílulas comuns e pílulas de poder.

<br>

## Subpasta: `Sounds`
Centraliza os efeitos sonoros e trilhas, gerenciados pelo `AudioService` via **LibVLCSharp**.
* **Interação:** Efeitos de gatilho rápido como `PacManEating.mp3`, `PacManEatGhost.mp3` e `PacManDeath.wav`.
* **Feedback:** Som de vitória (`PacManVictory.mp3`) disparado ao limpar o mapa.

<br>

## Como os Assets são carregados
1.  **Imagens:** Carregadas via URI usando o esquema `ms-appx:///Assets/Sprites/...`, que permite ao Uno Platform localizar os arquivos dentro do pacote da aplicação.
2.  **Áudio:** O `AudioService` localiza os arquivos combinando o `AppContext.BaseDirectory` com o caminho relativo da pasta `Sounds`.
3.  **Dados:** O sistema verifica a existência do JSON em tempo de execução e realiza o deploy automático para a pasta de escrita do sistema operacional.