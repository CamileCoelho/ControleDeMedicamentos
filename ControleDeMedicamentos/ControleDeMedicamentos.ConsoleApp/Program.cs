using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloAquisicao;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;

namespace ControleDeMedicamentos.ConsoleApp
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            RepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();
            RepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();
            RepositorioPaciente repositorioPaciente = new RepositorioPaciente();
            RepositorioRemedio repositorioRemedio = new RepositorioRemedio();
            RepositorioRequisicao repositorioRequisicao = new RepositorioRequisicao();
            RepositorioAquisicao repositorioAquisicao = new RepositorioAquisicao();

            Validador validador = new (repositorioFornecedor, repositorioFuncionario, repositorioPaciente, repositorioRemedio, repositorioRequisicao, repositorioAquisicao);

            TelaMae telaMae = new();
            TelaFuncionario telaFuncionario = new (repositorioFuncionario, validador);
            TelaPaciente telaPaciente = new (repositorioPaciente, validador);
            TelaFornecedor telaFornecedor = new (repositorioFornecedor, validador);
            TelaRemedio telaRemedio = new (repositorioRemedio, repositorioFornecedor, telaFornecedor, validador);
            TelaRequisicao telaRequisicao= new (repositorioRequisicao, repositorioPaciente, repositorioRemedio, repositorioFuncionario, telaPaciente, telaRemedio, telaFuncionario, validador);
            TelaAquisicao telaAquisicao = new(repositorioAquisicao, repositorioRemedio, repositorioFuncionario, telaRemedio, telaFuncionario, validador);

            bool continuar = true;

            PopularCamposParaTeste();

            do
            {
                string opcao = MostrarMenuPrincipal();

                switch (opcao)
                {
                    case "S":
                        continuar = false;
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "1":
                        telaFuncionario.VisualizarTela();
                        break;
                    case "2":
                        telaPaciente.VisualizarTela();
                        break;
                    case "3":
                        telaFornecedor.VisualizarTela();
                        break;
                    case "4":
                        telaRemedio.VisualizarTela();
                        break;
                    case "5":
                        telaRequisicao.VisualizarTela();
                        break;
                    case "6":
                        telaAquisicao.VisualizarTela();
                        break;
                }

            } while (continuar);

            string MostrarMenuPrincipal()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("                             Controle De Medicamentos!                            ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("   Digite:                                                                        ");
                Console.WriteLine();
                Console.WriteLine("   1  - Para gestão de Funcionários.                                              ");
                Console.WriteLine();
                Console.WriteLine("   2  - Para gestão de Pacientes.                                                 ");
                Console.WriteLine();
                Console.WriteLine("   3  - Para gestão de Fornecedores.                                              ");
                Console.WriteLine();
                Console.WriteLine("   4  - Para gestão de Remedios.                                                  ");
                Console.WriteLine();
                Console.WriteLine("   5  - Para controle de Requisições.                                             ");
                Console.WriteLine();
                Console.WriteLine("   6  - Para controle de Aquisições.                                              ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("   S  - Para sair.                                                                ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.Write("   Opção escolhida: ");
                string opcao = Console.ReadLine().ToUpper();
                bool opcaoInvalida = opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5"
                    && opcao != "6" && opcao != "S";
                while (opcaoInvalida)
                {
                    if (opcaoInvalida)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\n   Opção inválida, tente novamente. ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    }
                }
                telaMae.ExibirAvisos(repositorioRemedio);
                return opcao;
            }

            void PopularCamposParaTeste()
            {
                InformacoesPessoais dadosTiago = new("Tiago", "(49)99999-9999", "tiago@academia.com", "sua casa");
                Funcionario Tiago = new (dadosTiago, "111.111.111-11", "1111");

                InformacoesPessoais dadosRech = new("Rech", "(49)99999-9999", "rech@academia.com", "seu apartamento");
                Paciente Rech = new(dadosRech, "111.111.111-11");

                InformacoesPessoais dadosCamile = new("Camile", "(49)99999-9999", "camile@academia.com", "seu apartamento");
                Fornecedor Camile = new(dadosCamile, "11.111.111/1111-11");

                Remedio Rivotrilzinho = new("Rivotril", "Remedio De Aluno", 100, 50, Camile);
                Remedio Ciclobenzaprininha = new("Ciclobenzaprina", "Dor De Cabeça", 100, 30, Camile);

                repositorioFuncionario.Create(Tiago);
                repositorioPaciente.Create(Rech);
                repositorioFornecedor.Create(Camile);
                repositorioRemedio.Create(Rivotrilzinho);
                repositorioRemedio.Create(Ciclobenzaprininha);
            }
        }
    }
}