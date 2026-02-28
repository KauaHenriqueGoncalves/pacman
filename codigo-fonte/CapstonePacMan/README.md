# Como Executar o Projeto

Este documento descreve como configurar o ambiente e executar o projeto **CapstonePacMan**, desenvolvido em **C#**, **.NET 10** e **Uno Platform**.

<br>

## 1. Pré-requisitos

Antes de iniciar, verifique se os seguintes itens estão instalados:

### .NET 10 SDK
Instale o SDK do .NET 10:

```bash
dotnet --version
```
A versão exibida deve ser compatível com .NET 10.

<br>

## 2. Restaurar Dependências

No diretório raiz do projeto (onde está o .sln), execute:

```bash
dotnet restore
```

<br>

## 3. Compilar o Projeto

Realize a compilação do projeto, execute:

```bash
dotnet build
```

<br>

## 4. Executar a Aplicação

Para rodar o projeto principal:

```bash
dotnet run --project CapstonePacMan --framework net10.0-desktop"
```

<br>

## 5 . Limpeza (caso necessário)

Se ocorrer algum erro de build:

```bash
dotnet clean
dotnet restore
dotnet build
```

## Observação

O projeto está configurado para execução como aplicação desktop (WinUI) através da Uno Platform.
