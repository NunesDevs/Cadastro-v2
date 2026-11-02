# Memória do projeto

Revisão: 2026-11-01. Ler este arquivo e `docs/ai-context/QUALITY_REVIEW_2026-11-01.md` antes de retomar alterações.

## Objetivo e estado

Evolução do cadastro de funcionários com biblioteca compartilhada, console e API ASP.NET Core.

Mudanças funcionais desta revisão:
- Projetos migrados de .NET Core 3.1 para .NET 10 LTS.
- Repositório sincronizado por IDs lógicos, validação de domínio e exclusão lógica.
- API `/Cadastro` retorna 404 para ausentes, 400 para entradas inválidas e usa o ID da rota na atualização.
- Criação retorna a representação do cadastro e um cabeçalho Location por `CreatedAtAction`.
- Console usa o mesmo domínio validado e recupera erros de entrada.

## Decisões

- Preservar o propósito didático e o histórico anterior do Git.
- Manter esta revisão na branch `codex/quality-refresh` até integração coordenada.
- Preservar arquivos locais e avisos de terceiros; não versionar resultados de build, credenciais ou dados reais.
- Artefatos de console são distribuíveis locais. Um build ou pacote não comprova publicação em produção.
- .NET 10 LTS foi escolhido porque .NET 3.1 e 6 estavam sem suporte e .NET 8 encerra suporte em 2026-11-10. Referência: https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core .

## Validação reproduzível

SDK .NET 10; a primeira restauração baixa as dependências de teste do NuGet.

```powershell
./build.ps1 -Test -Package
dotnet run --project Cadastro.Console
dotnet run --project Cadastro.Web
```

A API escuta `http://127.0.0.1:5080` por padrão. `ASPNETCORE_URLS` permite um endereço explícito. `dotnet test Cadastro.sln -c Release` executa os testes de domínio e controlador.

JSON de criação: `{"genero":3,"nome":"Pessoa de teste","idade":25,"area":9,"funcao":"Desenvolvimento"}`. Listagem: `GET /Cadastro`; consulta, alteração e exclusão: `/Cadastro/{id}`.

## Limitações para continuidade

Dados permanecem em memória e somem ao reiniciar. A API é um exercício sem autenticação, destinada a uso local com dados fictícios. Não existe interface gráfica neste projeto. O pacote padrão é o console Windows; a API exige host e política de acesso próprios antes de uma publicação na rede.

Os documentos automáticos de maio em `docs/ai-context` são um registro histórico, incluindo falsos positivos por palavras como “secret”. Esta memória e a revisão de qualidade descrevem o estado posterior às correções.

IDs são reservados sob lock; podem existir lacunas após cancelar um cadastro. Não reutilizar IDs excluídos ou presumir que ID seja a posição em uma lista.

Pacotes Release omitem símbolos de depuração e mapeiam caminhos de origem em `Directory.Build.props`, para não distribuir caminhos locais. Para depurar, use a configuração Debug.

## Evidência da revisão

Build .NET 10, nove testes xUnit e publish de console/API aprovados. Oito verificações HTTP reais em loopback aprovaram CRUD, Location, identidade da URL, validação de enum e 404 após exclusão. Console empacotado aprovado com dados fictícios e EOF.
