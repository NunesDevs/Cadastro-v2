using Bogus;
using Cadastro;
using Cadastro.Interfaces;
using Cadastro.Web;
using Cadastro.Web.Controllers;
using Moq;
using System;
using Xunit;

namespace CadastroControllerTeste
{
    public class CadastroControllerTeste
    {

        private Faker<CadastroModel> CriandoFaker()
        {
            return new Faker<CadastroModel>()
                .RuleFor(_ => _.Nome, _ => _.Name.FirstName());
        }
        [Fact(DisplayName = "DADO uma ficha valida QUANDO inserimos ENTAO chamar repositorio para inserir")]
        public void Insere_Sucesso()
        {
            CadastroModel model = CriandoFaker().Generate();

            var cadastro = new CadastroModel() { Nome = "Seu Nome" };

            var repositorio = new Mock<IRepositorio<CadastroProj>>();

            repositorio.Setup(_ => _.ProximoId()).Returns(1);

            var controller = new CadastroController(repositorio.Object);
            
            controller.Insere(cadastro);

            repositorio.Verify(_ => _.Insere(It.Is<CadastroProj>(_ => _.Id == 1 && _.retornoNome() == "Seu Nome")), Times.Once);
        }
    }
}
