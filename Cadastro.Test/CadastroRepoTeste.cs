using System;
using System.Collections.Generic;
using Xunit;
namespace Cadastro.Test {
public class CadastroRepoTeste {
    private CadastroProj Ficha(int id,string nome="Pessoa de teste") => new CadastroProj(id,Genero.Outros,nome,20,Area.TI,"Desenvolvimento");
    [Fact] public void IdLogicoNaoDependeDaPosicao() { var repo=new cadastroRepo();repo.Insere(Ficha(7));repo.Atualiza(7,Ficha(7,"Atualizado"));Assert.Equal("Atualizado",repo.RetornarPorId(7).Nome);repo.Exclui(7);Assert.Null(repo.RetornarPorId(7));Assert.Equal(8,repo.ProximoId()); }
    [Fact] public void ListaNaoExponheArmazenamento() {var repo=new cadastroRepo();repo.Insere(Ficha(1));repo.Lista().Clear();Assert.Single(repo.Lista());}
    [Fact] public void RejeitaDuplicadoEMudancaDeId() {var repo=new cadastroRepo();repo.Insere(Ficha(1));Assert.Throws<ArgumentException>(()=>repo.Insere(Ficha(1)));Assert.Throws<ArgumentException>(()=>repo.Atualiza(1,Ficha(2)));}
    [Fact] public void ExclusaoNaoRessuscitaEmAtualizacao() {var repo=new cadastroRepo();repo.Insere(Ficha(1));repo.Exclui(1);Assert.Throws<KeyNotFoundException>(()=>repo.Atualiza(1,Ficha(1)));}
    [Fact] public void RejeitaDadosInvalidos() {Assert.Throws<ArgumentException>(()=>Ficha(1," "));Assert.Throws<ArgumentException>(()=>new CadastroProj(1,(Genero)99,"Nome",20,Area.TI,"Função"));Assert.Throws<ArgumentOutOfRangeException>(()=>new CadastroProj(1,Genero.Outros,"Nome",-1,Area.TI,"Função"));}
}}
