using System;
using Cadastro;

namespace Cadastro.Console
{
	class Program
	{
		static cadastroRepo repositorio = new cadastroRepo();
		static void Main(string[] args)
		{
			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarFuncionarios();
						break;
					case "2":
						InserirFuncionarios();
						break;
					case "3":
						AtualizarFuncionarios();
						break;
					case "4":
						ExcluirFuncionarios();
						break;
					case "5":
						VisualizarFuncionarios();
						break;
					case "C":
						System.Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			System.Console.WriteLine("Obrigado por utilizar nossos serviços.");
			System.Console.ReadLine();
		}

		private static void ExcluirFuncionarios()
		{
			System.Console.Write("Digite o id do Funcionário: ");
			int indiceFuncionarios = int.Parse(System.Console.ReadLine());

			repositorio.Exclui(indiceFuncionarios);
		}

		private static void VisualizarFuncionarios()
		{
			System.Console.Write("Digite o id do funcionário: ");
			int indiceFuncionarios = int.Parse(System.Console.ReadLine());

			var serie = repositorio.RetornarPorId(indiceFuncionarios);

			System.Console.WriteLine(serie);
		}

		private static void AtualizarFuncionarios()
		{
			System.Console.Write("Digite o id do funcionário: ");
			int indiceFuncionarios = int.Parse(System.Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				System.Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			System.Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(System.Console.ReadLine());

			System.Console.Write("Digite o seu nome: ");
			string entradaNome = System.Console.ReadLine();

			System.Console.Write("Digite a sua idade: ");
			int entradaIdade = int.Parse(System.Console.ReadLine());
			foreach (int i in Enum.GetValues(typeof(Area)))
			{
				System.Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Area), i));
			}
			System.Console.Write("Digite a sua area entre as opções acima: ");
			int entradaCargo = int.Parse(System.Console.ReadLine());

			System.Console.Write("Digite a sua função: ");
			string entradaFuncao = System.Console.ReadLine();

			CadastroProj atualizaFuncionarios = new CadastroProj(id: indiceFuncionarios,
										genero: (Genero)entradaGenero,
										nome: entradaNome,
										idade: entradaIdade,
										area: (Area)entradaCargo,
										funcao: entradaFuncao);

			repositorio.Atualiza(indiceFuncionarios, atualizaFuncionarios);
		}
		private static void ListarFuncionarios()
		{
			System.Console.WriteLine("Listar funcionários");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				System.Console.WriteLine("Nenhum funcionário cadastrado.");
				return;
			}

			foreach (var funcionario in lista)
			{
				var excluido = funcionario.retornaExcluido();

				System.Console.WriteLine("#ID {0}: - {1} {2}", funcionario.retornarId(), funcionario.retornoNome(), (excluido ? "*Excluído*" : ""));
			}
		}

		private static void InserirFuncionarios()
		{
			System.Console.WriteLine("Inserir novo funcionário");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				System.Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			System.Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(System.Console.ReadLine());

			System.Console.Write("Digite o seu nome: ");
			string entradaNome = System.Console.ReadLine();

			System.Console.Write("Digite a sua idade: ");
			int entradaIdade = int.Parse(System.Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Area)))
			{
				System.Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Area), i));
			}
			System.Console.Write("Digite a sua area entre as opções acima: ");

			System.Console.Write("Digite a sua função: ");
			string entradaFuncao = System.Console.ReadLine();

			int entradaCargo = int.Parse(System.Console.ReadLine());
			CadastroProj novoFuncionario = new CadastroProj(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										nome: entradaNome,
										idade: entradaIdade,
										area: (Area)entradaCargo,
										funcao: entradaFuncao);

			repositorio.Insere(novoFuncionario);
		}

		private static string ObterOpcaoUsuario()
		{
			System.Console.WriteLine();
			System.Console.WriteLine("#################################");
			System.Console.WriteLine("# Painel de funcionários ativo! #");
			System.Console.WriteLine("#################################");

			System.Console.WriteLine(" - Informe a opção desejada -");

			System.Console.WriteLine("1 - Listar funcionários");
			System.Console.WriteLine("2 - Inserir novo funcionário");
			System.Console.WriteLine("3 - Atualizar dados");
			System.Console.WriteLine("4 - Excluir funcionário");
			System.Console.WriteLine("5 - Visualizar funcionários");
			System.Console.WriteLine("C - Limpar Tela");
			System.Console.WriteLine("X - Sair");
			System.Console.WriteLine();
			System.Console.WriteLine("#################################");


			string opcaoUsuario = System.Console.ReadLine().ToUpper();
			System.Console.WriteLine();
			return opcaoUsuario;
		}
	}
}

