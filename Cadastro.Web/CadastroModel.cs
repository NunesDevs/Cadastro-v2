using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Web
{

    public class CadastroModel
    {
        public int Id { get; set; }
        [EnumDataType(typeof(Genero))]
        public Genero Genero { get; set; }
        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        [Range(0, 130)]
        public int Idade { get; set; }
        [EnumDataType(typeof(Area))]
        public Area Area { get; set; }
        [Required, StringLength(100)]
        public string Funcao { get; set; } = string.Empty;
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
