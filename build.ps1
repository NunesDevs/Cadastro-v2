param([switch]$Test, [switch]$Package)
$ErrorActionPreference = 'Stop'
$dotnet = if ($env:DOTNET_ROOT) { Join-Path $env:DOTNET_ROOT 'dotnet.exe' } else { 'dotnet' }
Push-Location $PSScriptRoot
try {
    & $dotnet build 'Cadastro.sln' -c Release --nologo
    if ($LASTEXITCODE) { throw 'Falha no build.' }
    if ($Test) {
        & $dotnet test 'Cadastro.sln' -c Release --no-build
        if ($LASTEXITCODE) { throw 'Falha nos testes.' }
    }
    if ($Package) {
        & $dotnet publish 'Cadastro.Console/Cadastro.Console.csproj' -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o artifacts/publish --nologo
        if ($LASTEXITCODE) { throw 'Falha no empacotamento.' }
    }
} finally { Pop-Location }
