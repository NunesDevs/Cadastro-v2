using System;

namespace Cadastro
{
    public class CadastroProj : EntidadeBase
    {
        private Genero Genero { get; set;}
        public string Nome { get; set;}
        private int Idade { get; set; }
        private string Funcao { get; set; }
        private Area Area { get; set;}
        private bool Excluido {get; set;}

        public CadastroProj(int id, Genero genero, string nome, int idade, Area area, string funcao)
        {
            this.Id = id;
            this.Genero = genero;
            this.Nome = nome;
            this.Idade = idade;
            this.Area = area;
            this.Funcao = funcao;
            this.Excluido = false;
        }

        public override string ToString()
        {
            string retorno = " ";
            retorno += "Genero: " + this.Genero + Environment.NewLine;
            retorno += "Nome: " + this.Nome + Environment.NewLine;
            retorno += "Idade: " + this.Idade + Environment.NewLine;
            retorno += "Area: " + this.Area + Environment.NewLine;
            retorno += "Função: " + this.Funcao + Environment.NewLine;
            retorno += "Excluido: " + this.Excluido;
            return retorno;
        }

        public string retornoNome()
        {
            return this.Nome;
        }

        public int retornarId()
        {
            return this.Id;
        }

        public void Excluir()
        {
            this.Excluido = true;
        }
        public Area retornaArea()
        {
            return this.Area;
        }
        public Genero retornaGenero()
        {
            return this.Genero;
        }

        public int retornaIdade()
        {
            return this.Idade;
        }

        public string retornoFuncao()
        {
            return this.Funcao;
        }

        public bool retornaExcluido()
		{
			return this.Excluido;
		}


    }
}