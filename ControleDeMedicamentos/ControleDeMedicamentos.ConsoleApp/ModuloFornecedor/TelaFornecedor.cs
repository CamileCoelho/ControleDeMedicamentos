using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
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
                Console.WriteLine("   1  - Para cadastrar um novo fornecedor.                                        ");
                Console.WriteLine("   2  - Para visualizar seus fornecedores cadastrados.                            ");
                Console.WriteLine("   3  - Para editar o cadastro de um fornecedor.                                  ");
                Console.WriteLine("   4  - Para excluir o cadastro de um fornecedor.                                 ");
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
            Imput(out InformacoesPessoais informacoesPessoais, out string cnpj);

            string valido = validador.ValidarFornecedor(informacoesPessoais, cnpj);

            if (valido == "REGISTRO_REALIZADO")
            {
                Fornecedor toAdd = new Fornecedor(informacoesPessoais,cnpj);
                repositorioFornecedor.Create(toAdd);
                ExibirMensagem("\n   Fornecedor cadastrado com sucesso! ", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Fornecedor Não Cadastrado:" + valido, ConsoleColor.DarkRed);
            }
        }

        private void Visualizar()
        {
            if (repositorioFornecedor.GetAll().Count == 0)
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
            if (repositorioFornecedor.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum fornecedor cadastrado. " +
                    "\n   Você deve cadastrar um fornecedor para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }

            Fornecedor toEdit = repositorioFornecedor.GetById(ObterId(repositorioFornecedor));

            if (toEdit == null)
            {
                ExibirMensagem("\n   Fornecedor não encontrado!", ConsoleColor.DarkRed);
            }
            else
            {
                Imput(out InformacoesPessoais informacoesPessoais, out string cnpj);

                string valido = validador.ValidarFornecedor(informacoesPessoais, cnpj);

                if (valido == "REGISTRO_REALIZADO")
                {
                    repositorioFornecedor.Update(toEdit, informacoesPessoais, cnpj);
                    ExibirMensagem("\n   Fornecedor editado com sucesso!", ConsoleColor.DarkGreen);
                }
                else
                {
                    ExibirMensagem("\n   Fornecedor Não Editado:" + valido, ConsoleColor.DarkRed);
                }
            }
        }

        private void Excluir()
        {
            if (repositorioFornecedor.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum fornecedor cadastrado. " +
                    "\n   Você deve cadastrar um fornecedor para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }

            Fornecedor toDelete = repositorioFornecedor.GetById(ObterId(repositorioFornecedor));

            string validacaoExclusao = validador.PermitirExclusaoDoFornecedor(toDelete);

            if (validacaoExclusao == "SUCESSO!")
            {
                repositorioFornecedor.Delete(toDelete);
                ExibirMensagem("\n   Fornecedor excluido com sucesso! ", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Fornecedor não excluido:" +validacaoExclusao, ConsoleColor.DarkRed);
            }
        }

        public void Imput(out InformacoesPessoais informacoesPessoais, out string cnpj)
        {
            informacoesPessoais = new InformacoesPessoais();
            Console.Clear();
            Console.Write("\n   Digite o nome do Fornecedor: ");
            informacoesPessoais.nome = Console.ReadLine();
            Console.Write("\n   Digite o telefone desse fornecedor (XX)XXXXX-XXXX: ");
            informacoesPessoais.telefone = Console.ReadLine();
            Console.Write("\n   Digite o email desse fornecedor: ");
            informacoesPessoais.email = Console.ReadLine();
            Console.Write("\n   Digite o endereco desse fornecedor: ");
            informacoesPessoais.endereco = Console.ReadLine();
            Console.Write("\n   Digite o CNPJ desse fornecedor XX.XXX.XXX/XXXX-XX ou XXXXXXXXXXXXXX: ");
            cnpj = Console.ReadLine();
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
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                  Lista de Funcionarios                                      ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", "ID ", "  NOME ", "  TELEFONE ", "  CNPJ ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Fornecedor print in repositorioFornecedor.GetAll())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", print.id, print.informacoesPessoais.nome, print.informacoesPessoais.telefone, print.cnpj);
                }
            }
        }
    }
}
