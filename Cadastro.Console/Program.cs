using Cadastro;
using System;
using Console = System.Console;

namespace Cadastro
{
    class Program
    {
        static cadastroRepo repositorio = new cadastroRepo();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario != "X")
            {
                try
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
                        if (!Console.IsOutputRedirected) Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                }
                catch (Exception error) when (error is ArgumentException || error is System.Collections.Generic.KeyNotFoundException)
                { Console.WriteLine(error.Message); }
                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void ExcluirFuncionarios()
        {
            int? indiceFuncionarios = LerIdFuncionario("Digite o id do Funcionário: ");
            if (indiceFuncionarios is null)
            {
                return;
            }

            repositorio.Exclui(indiceFuncionarios.Value);
        }

        private static void VisualizarFuncionarios()
        {
            int? indiceFuncionarios = LerIdFuncionario("Digite o id do funcionário: ");
            if (indiceFuncionarios is null)
            {
                return;
            }

            var funcionario = repositorio.RetornarPorId(indiceFuncionarios.Value);

            Console.WriteLine(funcionario);
        }

        private static void AtualizarFuncionarios()
        {
            int? indiceFuncionarios = LerIdFuncionario("Digite o id do funcionário: ");
            if (indiceFuncionarios is null)
            {
                return;
            }

            ExibirOpcoesEnum<Genero>();
            Genero? entradaGenero = LerEnum<Genero>("Digite o gênero entre as opções acima: ");
            if (entradaGenero is null)
            {
                return;
            }

            string? entradaNome = LerTextoObrigatorio("Digite o seu nome: ");
            if (entradaNome is null)
            {
                return;
            }

            int? entradaIdade = LerInteiro("Digite a sua idade: ", minimo: 0);
            if (entradaIdade is null)
            {
                return;
            }

            ExibirOpcoesEnum<Area>();
            Area? entradaArea = LerEnum<Area>("Digite o seu cargo entre as opções acima: ");
            if (entradaArea is null)
            {
                return;
            }

            string? entradaFuncao = LerTextoObrigatorio("Digite a sua função: ");
            if (entradaFuncao is null) return;

            CadastroProj atualizaFuncionarios = new CadastroProj(id: indiceFuncionarios.Value,
                                        genero: entradaGenero.Value,
                                        nome: entradaNome,
                                        idade: entradaIdade.Value,
                                        area: entradaArea.Value, funcao: entradaFuncao);

            repositorio.Atualiza(indiceFuncionarios.Value, atualizaFuncionarios);
        }

        private static void ListarFuncionarios()
        {
            Console.WriteLine("Listar funcionários");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum funcionário cadastrado.");
                return;
            }

            foreach (var funcionario in lista)
            {
                var excluido = funcionario.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", funcionario.retornarId(), funcionario.retornoNome(), (excluido ? "*Excluído*" : ""));
            }
        }

        private static void InserirFuncionarios()
        {
            Console.WriteLine("Inserir novo funcionário");

            ExibirOpcoesEnum<Genero>();
            Genero? entradaGenero = LerEnum<Genero>("Digite o gênero entre as opções acima: ");
            if (entradaGenero is null)
            {
                return;
            }

            string? entradaTitulo = LerTextoObrigatorio("Digite o seu nome: ");
            if (entradaTitulo is null)
            {
                return;
            }

            int? entradaAno = LerInteiro("Digite a sua idade: ", minimo: 0);
            if (entradaAno is null)
            {
                return;
            }

            ExibirOpcoesEnum<Area>();
            Area? entradaArea = LerEnum<Area>("Digite o seu cargo entre as opções acima: ");
            if (entradaArea is null)
            {
                return;
            }

            string? entradaFuncao = LerTextoObrigatorio("Digite a sua função: ");
            if (entradaFuncao is null) return;

            CadastroProj novoFuncionario = new CadastroProj(id: repositorio.ProximoId(),
                                        genero: entradaGenero.Value,
                                        nome: entradaTitulo,
                                        idade: entradaAno.Value,
                                        area: entradaArea.Value, funcao: entradaFuncao);

            repositorio.Insere(novoFuncionario);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("################################");
            Console.WriteLine("# Painel de funcionários ativo! #");
            Console.WriteLine("################################");

            Console.WriteLine(" - Informe a opção desejada -");

            Console.WriteLine("1 - Listar funcionários");
            Console.WriteLine("2 - Inserir novo funcionário");
            Console.WriteLine("3 - Atualizar dados");
            Console.WriteLine("4 - Excluir funcionário");
            Console.WriteLine("5 - Visualizar funcionários");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = (Console.ReadLine() ?? "X").Trim().ToUpperInvariant();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static int? LerIdFuncionario(string mensagem)
        {
            int? id = LerInteiro(mensagem, minimo: 0);
            if (id is null)
            {
                return null;
            }

            if (repositorio.RetornarPorId(id.Value) == null)
            {
                Console.WriteLine("Funcionário não encontrado.");
                return null;
            }

            return id.Value;
        }

        private static int? LerInteiro(string mensagem, int? minimo = null)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out int valor))
            {
                Console.WriteLine("Valor inválido. Informe um número inteiro.");
                return null;
            }

            if (minimo.HasValue && valor < minimo.Value)
            {
                Console.WriteLine($"Valor inválido. Informe um número maior ou igual a {minimo.Value}.");
                return null;
            }

            return valor;
        }

        private static TEnum? LerEnum<TEnum>(string mensagem) where TEnum : struct, Enum
        {
            int? valor = LerInteiro(mensagem);
            if (valor is null)
            {
                return null;
            }

            if (!Enum.IsDefined(typeof(TEnum), valor.Value))
            {
                Console.WriteLine("Opção inválida.");
                return null;
            }

            return (TEnum)Enum.ToObject(typeof(TEnum), valor.Value);
        }

        private static string? LerTextoObrigatorio(string mensagem)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("Valor obrigatório.");
                return null;
            }

            return entrada.Trim();
        }

        private static void ExibirOpcoesEnum<TEnum>() where TEnum : struct, Enum
        {
            foreach (int i in Enum.GetValues(typeof(TEnum)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(TEnum), i));
            }
        }
    }
}
