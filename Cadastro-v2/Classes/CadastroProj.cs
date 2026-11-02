using System;

namespace Cadastro
{
    public class CadastroProj : EntidadeBase
    {
        private Genero Genero { get; set;}
        public string Nome { get; private set;}
        private int Idade { get; set; }
        private string Funcao { get; set; }
        private Area Area { get; set;}
        private bool Excluido {get; set;}

        public CadastroProj(int id, Genero genero, string nome, int idade, Area area, string funcao)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (!Enum.IsDefined(typeof(Genero), genero)) throw new ArgumentException("Gênero inválido.");
            if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length > 100) throw new ArgumentException("Nome obrigatório, com até 100 caracteres.");
            if (idade < 0 || idade > 130) throw new ArgumentOutOfRangeException(nameof(idade), "Idade deve estar entre 0 e 130.");
            if (!Enum.IsDefined(typeof(Area), area)) throw new ArgumentException("Área inválida.");
            if (string.IsNullOrWhiteSpace(funcao) || funcao.Trim().Length > 100) throw new ArgumentException("Função obrigatória, com até 100 caracteres.");
            this.Id = id;
            this.Genero = genero;
            this.Nome = nome.Trim();
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
