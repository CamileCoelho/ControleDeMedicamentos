using ControleDeMedicamentos.ConsoleApp.Compartilhado;
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
        RepositorioFuncionario repositorioFuncionario;

        public TelaFuncionario(RepositorioFuncionario repositorioFuncionario, Validador validador)
        {
            this.repositorioFuncionario = repositorioFuncionario;
            this.validador = validador;
        }

        public void VisualizarTela()
        {
            bool continuar = true;

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
                Console.WriteLine("                              Gestão de Funcionários!                             ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("   Digite:                                                                        ");
                Console.WriteLine();
                Console.WriteLine("   1  - Para cadastrar um novo funcionário.                                       ");
                Console.WriteLine("   2  - Para visualizar seus funcionários cadastrados.                            ");
                Console.WriteLine("   3  - Para editar o cadastro de um funcionário.                                 ");
                Console.WriteLine("   4  - Para excluir o cadastro de um funcionário.                                ");
                Console.WriteLine();
                Console.WriteLine("   5  - Para voltar ao menu principal.                                            ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.Write("   Opção escolhida: ");
                string opcao = Console.ReadLine().ToUpper();
                bool opcaoInvalida = opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5";
                while (opcaoInvalida)
                {
                    if (opcaoInvalida)
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
            Imput(out InformacoesPessoais informacoesPessoais, out string cpf, out string senha);

            string valido = validador.ValidarFunicionario(informacoesPessoais, cpf, senha);

            if (valido == "REGISTRO_REALIZADO" )
            {
                Funcionario toAdd = new Funcionario(informacoesPessoais, cpf, senha);
                repositorioFuncionario.Create(toAdd);
                ExibirMensagem("\n   Funcionário cadastrado com sucesso!" , ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Funcionário Não Cadastrado: " + valido, ConsoleColor.DarkRed);
            }
        }

        private void Visualizar()
        {
            if (repositorioFuncionario.GetAll().Count == 0)
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
            if (repositorioFuncionario.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum funcionário cadastrado. " +
                    "\n   Você deve cadastrar um funcionário para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }

            Funcionario toEdit = repositorioFuncionario.GetById(ObterId(repositorioFuncionario));
            if (toEdit == null)
            {
                ExibirMensagem("\n   Funcionário não encontrado!", ConsoleColor.DarkRed);
            }
            else
            {
                Imput(out InformacoesPessoais informacoesPessoais, out string cpf, out string senha);

                string valido = validador.ValidarFunicionario(informacoesPessoais, cpf, senha);

                if (valido == "REGISTRO_REALIZADO")
                {
                    repositorioFuncionario.Update(toEdit, informacoesPessoais, cpf);
                    ExibirMensagem("\n   Funcionario editado com sucesso!", ConsoleColor.DarkGreen);
                }
                else
                {
                    ExibirMensagem("\n   Funcionário Não Editado:" + valido, ConsoleColor.DarkRed);
                }
            }
        }

        private void Excluir()
        {
            if (repositorioFuncionario.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum funcionário cadastrado. " +
                    "\n   Você deve cadastrar um funcionário para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }

            Funcionario toDelete = repositorioFuncionario.GetById(ObterId(repositorioFuncionario));

            string validacaoExclusao = validador.PermitirExclusaoDoFuncionario(toDelete);

            if (toDelete != null && validacaoExclusao == "SUCESSO!")
            {
                repositorioFuncionario.Delete(toDelete);
                ExibirMensagem("\n   Funcionário excluido com sucesso! ", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Funcionario não excluido:" + validacaoExclusao, ConsoleColor.DarkRed);
            }
        }

        public void Imput(out InformacoesPessoais informacoesPessoais, out string cpf, out string senha)
        {
            informacoesPessoais = new InformacoesPessoais();
            Console.Clear();
            Console.Write("\n   Digite seu nome : ");
            informacoesPessoais.nome = Console.ReadLine();
            Console.Write("\n   Digite seu telefone (XX)XXXXX-XXXX: ");
            informacoesPessoais.telefone = Console.ReadLine();
            Console.Write("\n   Digite seu email: ");
            informacoesPessoais.email = Console.ReadLine();
            Console.Write("\n   Digite seu endereco: ");
            informacoesPessoais.endereco = Console.ReadLine();
            Console.Write("\n   Digite seu CPF XXX.XXX.XXX-XX ou XXXXXXXXXXX: ");
            cpf = Console.ReadLine();
            Console.Write("\n   Digite sua senha XXXX : ");
            senha = Console.ReadLine();
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
                Console.Write("\n   Digite o id do funcionario: ");
            }
            return id;
        }
        
        public void MostarListaFuncionarios(RepositorioFuncionario repositorioFuncionario)
        {
            Console.Clear();
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                   Lista de Funcionarios                                     ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", "ID ", "  NOME ", "  TELEFONE ", "  CPF "); 
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Funcionario print in repositorioFuncionario.GetAll())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}" , print.id, print.informacoesPessoais.nome, print.informacoesPessoais.telefone, print.cpf);
                }
            }
        }
    }
}
