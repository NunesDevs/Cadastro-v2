# Cadastro-v2

Cadastro de funcionários com interface de console e API REST, repositório em memória e testes xUnit.

## Funcionalidades verificadas

- Listar, inserir, atualizar e excluir cadastros.
- Interface de console.
- API HTTP de cadastro.
- Testes de repositório e controlador.

## Operação e arquitetura verificadas

Biblioteca de domínio Cadastro-v2, Cadastro.Console, Cadastro.Web e projetos de testes. A API usa um repositório singleton em memória.

Arquivos principais: `Cadastro.Web/Controllers/CadastroController.cs`, `Cadastro.Web/Startup.cs`, `Cadastro-v2/Classes/cadastroRepo.cs`, `Cadastro.Test/CadastroRepoTeste.cs`.

### Requisitos e execução

SDK .NET compatível com os TargetFramework dos projetos.

```text
dotnet build Cadastro.sln
dotnet test Cadastro.sln
dotnet run --project Cadastro.Console/Cadastro.Console.csproj
dotnet run --project Cadastro.Web/Cadastro.Web.csproj
```



### Testes e estado

Há projetos xUnit de repositório e controlador. Não foram executados nesta máquina, que não possui SDK .NET.

Estado: projeto de estudo/experimental. Deploy ativo não foi confirmado nesta revisão.

### Limitações conhecidas

- Consultas e atualizações precisam tratar IDs ausentes.
- Não há persistência de dados nem autenticação nas rotas de cadastro.

### Licença e manutenção

Nenhum arquivo de licença foi identificado nesta revisão; não foi adicionada uma nova licença. Atualize comandos e limitações quando houver mudança de comportamento.
