# Cadastro v2 — domínio, console e API

Evolução do cadastro de funcionários com biblioteca compartilhada, console e API ASP.NET Core.

## Comportamento

- Projetos migrados de .NET Core 3.1 para .NET 10 LTS.
- Repositório sincronizado por IDs lógicos, validação de domínio e exclusão lógica.
- API `/Cadastro` retorna 404 para ausentes, 400 para entradas inválidas e usa o ID da rota na atualização.
- Criação retorna a representação do cadastro e um cabeçalho Location por `CreatedAtAction`.
- Console usa o mesmo domínio validado e recupera erros de entrada.

## Executar e validar

SDK .NET 10; a primeira restauração baixa as dependências de teste do NuGet.

```powershell
./build.ps1 -Test -Package
dotnet run --project Cadastro.Console
dotnet run --project Cadastro.Web
```

A API escuta `http://127.0.0.1:5080` por padrão. `ASPNETCORE_URLS` permite um endereço explícito. `dotnet test Cadastro.sln -c Release` executa os testes de domínio e controlador.

JSON de criação: `{"genero":3,"nome":"Pessoa de teste","idade":25,"area":9,"funcao":"Desenvolvimento"}`. Listagem: `GET /Cadastro`; consulta, alteração e exclusão: `/Cadastro/{id}`.

## Limites

Dados permanecem em memória e somem ao reiniciar. A API é um exercício sem autenticação, destinada a uso local com dados fictícios. Não existe interface gráfica neste projeto. O pacote padrão é o console Windows; a API exige host e política de acesso próprios antes de uma publicação na rede.

## Manutenção e licença

Código autoral licenciado sob a [MIT](LICENSE). Dependências, ferramentas e materiais de terceiros mantêm suas próprias licenças e avisos.

Decisões e continuidade: [PROJECT_MEMORY.md](PROJECT_MEMORY.md).
