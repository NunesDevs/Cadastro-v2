using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Web
{
    
    public class CadastroModel
    {
        public int Id { get; set; }
        public Genero Genero { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public Area Area { get; set; }
        public string Funcao { get; set; }
        public bool Excluido { get; set; }

        public CadastroModel() { }
        public CadastroModel(CadastroProj cadastroProj)
        {
            Id = cadastroProj.retornarId();
            Genero = cadastroProj.retornaGenero();
            Nome = cadastroProj.retornoNome();
            Idade = cadastroProj.retornaIdade();
            Area = cadastroProj.retornaArea();
            Funcao = cadastroProj.retornoFuncao();
            Excluido = cadastroProj.retornaExcluido();

        }

        public CadastroProj ToCadastroProj()
        {
            return new CadastroProj(Id, Genero, Nome, Idade, Area, Funcao);
        }
    }

    
}
