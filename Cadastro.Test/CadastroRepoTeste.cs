using System;
using Xunit;

namespace Cadastro.Test
{
    public class CadastroRepoTeste
    {
        [Fact(DisplayName = "DADO uma ficha valida QUANDO inserimos ENTAO persistir cadastro")]
        public void Insere_Sucesso()
        {
            var repositorio = new cadastroRepo();
            repositorio.Insere(new CadastroProj(1, Genero.Masculino, "Nome", 20, Area.Administracao, "Administrador de serviços"));
            var cadastroPersistido = repositorio.RetornarPorId(1);
            Assert.NotNull(cadastroPersistido);
            Assert.Equal("Nome", cadastroPersistido.retornoNome());
        }
    }
}
