using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class TelaFuncionario : TelaMae
    {
        bool continuar = true;

        RepositorioFuncionario repositorioFuncionario;

        public TelaFuncionario(RepositorioFuncionario repositorioFuncionario, Validador validador)
        {
            this.repositorioFuncionario = repositorioFuncionario;
            this.validador = validador;
        }

        public void VisualizarTela()
        {
            do
            {
                string opcao = MostrarMenu();

                switch (opcao)
                {
                    case "5":
                        continuar = false;
                        Console.ResetColor();
                        break;
                    case "1":
                        Cadastrar();
                        continue;
                    case "2":
                        Visualizar();
                        continue;
                    case "3":
                        Editar();
                        continue;
                    case "4":
                        Excluir();
                        continue;
                }
            } while (continuar);

            string MostrarMenu()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("                              Gestão de Pacientes!                                ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("   Digite:                                                                        ");
                Console.WriteLine();
                Console.WriteLine("   1  - Para cadastrar um novo paciente.                                          ");
                Console.WriteLine("   2  - Para visualizar seus pacientes cadastrados.                               ");
                Console.WriteLine("   3  - Para editar o cadastro de um paciente.                                    ");
                Console.WriteLine("   4  - Para excluir o cadastro de um paciente.                                   ");
                Console.WriteLine();
                Console.WriteLine("   5  - Para voltar ao menu principal.                                            ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.Write("   Opção escolhida: ");
                string opcao = Console.ReadLine().ToUpper();
                bool opcaoValida = opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5";
                while (opcaoValida)
                {
                    if (opcaoValida)
                    {
                        ExibirMensagem("\n   Opção inválida, tente novamente. ", ConsoleColor.DarkRed);
                        break;
                    }
                }
                return opcao;
            }
        }

        private void Cadastrar()
        {
            InformacoesPessoais informacoesPessoais;
            string cpf;
            Imput(out informacoesPessoais, out cpf);

            Funcionario funcionarioToAdd = new Funcionario(informacoesPessoais, cpf);

            string validacao = repositorioFuncionario.CadastrarFuncionario(funcionarioToAdd);

            if (validacao == "\n   Funcionário cadastrado com sucesso!")
            {
                ExibirMensagem(validacao, ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem(validacao, ConsoleColor.DarkRed);
            }
        }

        private void Visualizar()
        {
            if (repositorioFuncionario.ListarFuncionarios().Count == 0)
            {
                ExibirMensagem("\n   Nenhum funcionário cadastrado. " +
                    "\n   Você deve cadastrar um funcionário para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }
            MostarListaFuncionarios(repositorioFuncionario);
            Console.ReadLine();
        }

        private void Editar()
        {
            if (repositorioFuncionario.ListarFuncionarios().Count == 0)
            {
                ExibirMensagem("\n   Nenhum funcionário cadastrado. " +
                    "\n   Você deve cadastrar um funcionário para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }

            Funcionario toEdit = repositorioFuncionario.EncontrarFuncionarioPorId(ObterId(repositorioFuncionario));

            if (toEdit == null)
            {
                ExibirMensagem("\n   Funcionário não encontrado!", ConsoleColor.DarkRed);
            }
            else
            {
                InformacoesPessoais informacoesPessoais;
                string cpf;
                Imput(out informacoesPessoais, out cpf);

                string validacaoEdit = toEdit.Validar(informacoesPessoais, cpf);
                repositorioFuncionario.EditarFuncionario(toEdit, informacoesPessoais, cpf);

                if (validacaoEdit == "REGISTRO_REALIZADO")
                {
                    ExibirMensagem(validacaoEdit, ConsoleColor.DarkGreen);
                }
                else
                {
                    ExibirMensagem(validacaoEdit, ConsoleColor.DarkRed);
                }

            }
        }

        private void Excluir()
        {
            if (repositorioFuncionario.ListarFuncionarios().Count == 0)
            {
                ExibirMensagem("\n   Nenhum funcionário cadastrado. " +
                    "\n   Você deve cadastrar um funcionário para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }
            string validacaoExclusao = repositorioFuncionario.ExcluirFuncionario(ObterId(repositorioFuncionario), validador);

            if (validacaoExclusao == "\n   Funcionario excluido com sucesso!")
            {
                ExibirMensagem(validacaoExclusao, ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem(validacaoExclusao, ConsoleColor.DarkRed);
            }
        }

        public void Imput(out InformacoesPessoais informacoesPessoais, out string cpf)
        {
            Console.Clear();
            Console.Write("\n   Digite o nome do paciente: ");
            string nome = Console.ReadLine();
            Console.Write("\n   Digite o telefone desse paciente (XX)XXXXX-XXXX: ");
            string telefone = Console.ReadLine();
            Console.Write("\n   Digite o email desse paciente: ");
            string email = Console.ReadLine();
            Console.Write("\n   Digite o endereco desse paciente: ");
            string endereco = Console.ReadLine();
            Console.Write("\n   Digite o CPF desse paciente XXX.XXX.XXX-XX ou XXXXXXXXXXX: ");
            cpf = Console.ReadLine();
            informacoesPessoais = new InformacoesPessoais(nome, telefone, email, endereco);
        }

        public int ObterId(RepositorioFuncionario repositorioFuncionario)
        {
            Console.Clear();
            MostarListaFuncionarios(repositorioFuncionario);

            Console.Write("\n   Digite o id do funcionario: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite o id do paciente: ");
            }
            return id;
        }

        public void MostarListaFuncionarios(RepositorioFuncionario repositorioFuncionario)
        {
            Console.Clear();
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                 Lista de Funcionarios                       ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", "ID ", "  NOME ", "  TELEFONE ", "  CPF ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            foreach (Funcionario print in repositorioFuncionario.ListarFuncionarios())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", print.informacoesPessoais.nome, print.informacoesPessoais.telefone, print.cpf);
                }
            }
        }
    }
}
