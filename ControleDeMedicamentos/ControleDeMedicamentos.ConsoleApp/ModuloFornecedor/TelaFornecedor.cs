using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class TelaFornecedor : TelaMae
    {
        RepositorioFornecedor repositorioFornecedor;
        Validador validador;

        public TelaFornecedor(RepositorioFornecedor repositorioFornecedor, Validador validador)
        {
            this.repositorioFornecedor = repositorioFornecedor;
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
                Console.WriteLine("                           Gestão de Fornecedores!                                ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("   Digite:                                                                        ");
                Console.WriteLine();
                Console.WriteLine("   1  - Para cadastrar um novo fornecedor.                                          ");
                Console.WriteLine("   2  - Para visualizar seus fornecedores cadastrados.                               ");
                Console.WriteLine("   3  - Para editar o cadastro de um fornecedor.                                    ");
                Console.WriteLine("   4  - Para excluir o cadastro de um fornecedor.                                   ");
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
            string cnpj;
            Imput(out informacoesPessoais, out cnpj);

            Fornecedor toAdd = new Fornecedor(informacoesPessoais, cnpj);

            string validacao = repositorioFornecedor.CadastrarFornecedor(toAdd);

            if (validacao == "\n   Fornecedor cadastrado com sucesso!")
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
            if (repositorioFornecedor.ListarFornecedores().Count == 0)
            {
                ExibirMensagem("\n   Nenhum fornecedor cadastrado. " +
                    "\n   Você deve cadastrar um fornecedor para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }
            MostarListaFornecedores(repositorioFornecedor);
            Console.ReadLine();
        }

        private void Editar()
        {
            if (repositorioFornecedor.ListarFornecedores().Count == 0)
            {
                ExibirMensagem("\n   Nenhum fornecedor cadastrado. " +
                    "\n   Você deve cadastrar um fornecedor para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }

            Fornecedor toEdit = repositorioFornecedor.EncontrarFornecedorPorId(ObterId(repositorioFornecedor));

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
                repositorioFornecedor.EditarFornecedor(toEdit, informacoesPessoais, cpf);

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
            if (repositorioFornecedor.ListarFornecedores().Count == 0)
            {
                ExibirMensagem("\n   Nenhum fornecedor cadastrado. " +
                    "\n   Você deve cadastrar um fornecedor para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }
            string validacaoExclusao = repositorioFornecedor.ExcluirFornecedor(ObterId(repositorioFornecedor), validador);

            if (validacaoExclusao == "\n   Fornecedor excluido com sucesso!")
            {
                ExibirMensagem(validacaoExclusao, ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem(validacaoExclusao, ConsoleColor.DarkRed);
            }
        }

        public void Imput(out InformacoesPessoais informacoesPessoais, out string cnpj)
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
            Console.Write("\n   Digite o CNPJ desse paciente XX.XXX.XXX/XXXX-XX ou XXXXXXXXXXXXXX: ");
            cnpj = Console.ReadLine();
            informacoesPessoais = new InformacoesPessoais(nome, telefone, email, endereco);
        }

        public int ObterId(RepositorioFornecedor repositorioFornecedor)
        {
            Console.Clear();
            MostarListaFornecedores(repositorioFornecedor);

            Console.Write("\n   Digite o id do fornecedor: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite o id do paciente: ");
            }
            return id;
        }

        public void MostarListaFornecedores(RepositorioFornecedor repositorioFornecedor)
        {
            Console.Clear();
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                 Lista de Funcionarios                       ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", "ID ", "  NOME ", "  TELEFONE ", "  CNPJ ");
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine();

            foreach (Fornecedor print in repositorioFornecedor.ListarFornecedores())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", print.informacoesPessoais.nome, print.informacoesPessoais.telefone, print.cnpj);
                }
            }
        }
    }
}
