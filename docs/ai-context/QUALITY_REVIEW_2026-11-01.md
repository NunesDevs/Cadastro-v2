# Revisão de qualidade — 2026-11-01

Evolução do cadastro de funcionários com biblioteca compartilhada, console e API ASP.NET Core.

## Escopo

Leitura de fontes, documentação, configurações e scripts do projeto, excluindo dependências, metadados Git e saídas geradas. Sem alteração visual; mudanças de interface gráfica exigem Figma.

## Correções e decisões

- Projetos migrados de .NET Core 3.1 para .NET 10 LTS.
- Repositório sincronizado por IDs lógicos, validação de domínio e exclusão lógica.
- API `/Cadastro` retorna 404 para ausentes, 400 para entradas inválidas e usa o ID da rota na atualização.
- Criação retorna a representação do cadastro e um cabeçalho Location por `CreatedAtAction`.
- Console usa o mesmo domínio validado e recupera erros de entrada.

## Como validar

SDK .NET 10; a primeira restauração baixa as dependências de teste do NuGet.

```powershell
./build.ps1 -Test -Package
dotnet run --project Cadastro.Console
dotnet run --project Cadastro.Web
```

A API escuta `http://127.0.0.1:5080` por padrão. `ASPNETCORE_URLS` permite um endereço explícito. `dotnet test Cadastro.sln -c Release` executa os testes de domínio e controlador.

JSON de criação: `{"genero":3,"nome":"Pessoa de teste","idade":25,"area":9,"funcao":"Desenvolvimento"}`. Listagem: `GET /Cadastro`; consulta, alteração e exclusão: `/Cadastro/{id}`.

## Limites reais

Dados permanecem em memória e somem ao reiniciar. A API é um exercício sem autenticação, destinada a uso local com dados fictícios. Não existe interface gráfica neste projeto. O pacote padrão é o console Windows; a API exige host e política de acesso próprios antes de uma publicação na rede.

Confira `PROJECT_MEMORY.md` para continuidade. Os resultados executados estão registrados abaixo.

## Resultado executado

Build .NET 10, nove testes xUnit e publish de console/API aprovados. Oito verificações HTTP reais em loopback aprovaram CRUD, Location, identidade da URL, validação de enum e 404 após exclusão. Console empacotado aprovado com dados fictícios e EOF.

Publicação em produção não foi realizada. Pacotes foram preparados e a publicação da branch será registrada na auditoria local; integração e distribuição são coordenadas separadamente.
