using Cadastro;
using Cadastro.Web;
using Cadastro.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace CadastroControllerTeste {
public class CadastroControllerTeste {
    private CadastroModel Ficha() => new CadastroModel {Nome="Pessoa de teste",Genero=Genero.Outros,Idade=25,Area=Area.TI,Funcao="Desenvolvimento"};
    [Fact] public void InsereERetornaRepresentacaoPublica() {var repo=new cadastroRepo();var c=new CadastroController(repo);var response=Assert.IsType<CreatedAtActionResult>(c.Insere(Ficha()));Assert.IsType<CadastroModel>(response.Value);Assert.Single(repo.Lista());}
    [Fact] public void AusentesRetornam404() {var c=new CadastroController(new cadastroRepo());Assert.IsType<NotFoundResult>(c.Consulta(99));Assert.IsType<NotFoundResult>(c.Atualiza(99,Ficha()));Assert.IsType<NotFoundResult>(c.Exclui(99));}
    [Fact] public void AtualizacaoUsaIdDaRota() {var repo=new cadastroRepo();var c=new CadastroController(repo);c.Insere(Ficha());var f=Ficha();f.Id=999;f.Nome="Atualizado";Assert.IsType<NoContentResult>(c.Atualiza(0,f));Assert.Equal("Atualizado",repo.RetornarPorId(0).Nome);Assert.Null(repo.RetornarPorId(999));}
    [Fact] public void DadosInvalidosRetornam400() {var c=new CadastroController(new cadastroRepo());var f=Ficha();f.Nome=" ";Assert.IsType<BadRequestObjectResult>(c.Insere(f));}
}}
