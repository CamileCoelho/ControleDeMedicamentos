using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRemedios
{
    public class TelaRemedio : TelaBase<Remedio>
    {
        RepositorioRemedio repositorioRemedio;
        RepositorioFornecedor repositorioFornecedor;
        TelaFornecedor telaFornecedor;

        public TelaRemedio(RepositorioRemedio repositorioRemedio, RepositorioFornecedor repositorioFornecedor, TelaFornecedor telaFornecedor, Validador validador)
        {
            this.repositorioRemedio = repositorioRemedio;
            this.repositorioFornecedor = repositorioFornecedor;
            this.telaFornecedor = telaFornecedor;
            this.validador = validador;
        }

        public void VisualizarTela()
        {
            bool continuar = true;

            do
            {
                string opcao = MostrarMenuRemedios();

                switch (opcao)
                {
                    case "7":
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
                    case "5":
                        VizualizarPoucaQuantidade();
                        continue;
                    case "6":
                        VizualizarEmFalta();
                        continue;
                }
            } while (continuar);

            string MostrarMenuRemedios()
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
                Console.WriteLine("   1  - Para cadastrar um novo remédio.                                           ");
                Console.WriteLine();
                Console.WriteLine("   2  - Para visualizar seus remédios cadastrados.                                ");
                Console.WriteLine();
                Console.WriteLine("   3  - Para editar o cadastro de um remédio.                                     ");
                Console.WriteLine();
                Console.WriteLine("   4  - Para excluir o cadastro de um remédio.                                    ");
                Console.WriteLine();
                Console.WriteLine("   5  - Para visualizar seus remédios com pouca quantidade no estoque.            ");
                Console.WriteLine();
                Console.WriteLine("   6  - Para visualizar seus remédios em falta.                                   ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("   7  - Para voltar ao menu principal.                                            ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.Write("   Opção escolhida: ");
                string opcao = Console.ReadLine().ToUpper();
                bool opcaoInvalida = opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5" && opcao != "6" && opcao != "7";
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
            Imput(out string nome, out string descricao, out int quantidadeDisponivel, out int quantidadeMinima, out Fornecedor fornecedor);
           
            string valido = validador.ValidarRemedio(nome, descricao);

            if (valido == "REGISTRO_REALIZADO")
            {
                Remedio toAdd = new Remedio(nome, descricao, quantidadeDisponivel, quantidadeMinima, fornecedor);
                repositorioRemedio.Insert(toAdd);
                ExibirMensagem("\n   Remédio cadastrado com sucesso! ", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Remédio Não Cadastrado:" + valido, ConsoleColor.DarkRed);
            }
        }

        private void Visualizar()
        {
            if (repositorioRemedio.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum remédio cadastrado. " +
                    "\n   Você deve cadastrar um remédio para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }
            MostarListaRemedios(repositorioRemedio);
            Console.ReadLine();
        }

        private void Editar()
        {
            if (repositorioRemedio.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum remédio cadastrado. " +
                    "\n   Você deve cadastrar um remédio para poder editar um cadastro.", ConsoleColor.DarkRed);
                return;
            }

            Remedio toEdit = repositorioRemedio.GetById(ObterId(repositorioRemedio));

            if (toEdit == null)
            {
                ExibirMensagem("\n   Paciente não encontrado!", ConsoleColor.DarkRed);
            }
            else
            {
                Imput(out string nome, out string descricao, out int quantidadeDisponivel, out int quantidadeMinima, out Fornecedor fornecedor);

                string valido = validador.ValidarRemedio(nome, descricao);

                if (valido == "REGISTRO_REALIZADO")
                {
                    repositorioRemedio.Update(toEdit, nome, descricao, quantidadeDisponivel, quantidadeMinima, fornecedor);
                    ExibirMensagem("\n   Remédio editado com sucesso!", ConsoleColor.DarkGreen);
                }
                else
                {
                    ExibirMensagem("\n   Remédio Não Editado:" + valido, ConsoleColor.DarkRed);
                }
            }
        }

        private void Excluir()
        {
            if (repositorioRemedio.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum remédio cadastrado. " +
                    "\n   Você deve cadastrar um remédio para poder excluir um cadastro.", ConsoleColor.DarkRed);
                return;
            }

            Remedio toDelete = repositorioRemedio.GetById(ObterId(repositorioRemedio));

            string validacaoExclusao = validador.PermitirExclusaoDoRemedio(toDelete);

            if (validacaoExclusao == "SUCESSO!")
            {
                repositorioRemedio.Delete(toDelete);
                ExibirMensagem("\n   Remédio excluido com sucesso!", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Remédio não excluido: " + validacaoExclusao, ConsoleColor.DarkRed);
            }
        }

        public void VizualizarEmFalta()
        {
            if (repositorioRemedio.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum remédio cadastrado. " +
                "\n   Você deve cadastrar um remédio para poder visualizar os que estão em falta.", ConsoleColor.DarkRed);
                return;
            }
            else if (repositorioRemedio.GetAll().Any(x => x.quantidadeDisponivel == 0))
            {
                MostarListaRemediosEmFalta(repositorioRemedio);
                Console.ReadLine();
                return;
            }
            else
                ExibirMensagem("\n   Nenhum remédio em falta. ", ConsoleColor.DarkRed);            
        }

        public void VizualizarPoucaQuantidade()
        {
            if (repositorioRemedio.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum remédio cadastrado. " +
                "\n   Você deve cadastrar um remédio para poder visualizar os que estão com pouca quantidade.", ConsoleColor.DarkRed);
                return;
            }
            else if (repositorioRemedio.GetAll().Any(x => x.quantidadeDisponivel <= x.quantidadeMinima && x.quantidadeDisponivel !=0))
            {
                MostarListaRemediosPoucaQtd(repositorioRemedio);
                Console.ReadLine();
                return;
            }
            else
                ExibirMensagem("\n   Nenhum remédio com pouca quantidade. ", ConsoleColor.DarkRed);            
        }

        public void Imput(out string nome, out string descricao, out int quantidadeDisponivel, out int quantidadeMinima, out Fornecedor fornecedor)
        {
            Console.Clear();
            Console.Write("\n   Digite o nome do remédio: ");
            nome = Console.ReadLine();
            Console.Write("\n   Digite a descição do remédio: ");
            descricao = Console.ReadLine();
            Console.Write("\n   Digite a quatidade disponível desse remédio: ");
            while (!int.TryParse(Console.ReadLine(), out quantidadeDisponivel))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite a quatidade disponível desse remédio: ");
            }
            Console.Write("\n   Digite a quatidade minima que deve ter em estoque: ");
            while (!int.TryParse(Console.ReadLine(), out quantidadeMinima))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite a quatidade limite para esse remédio: ");
            }
            fornecedor = (Fornecedor)repositorioBase.GetById(telaFornecedor.ObterId(repositorioFornecedor));
        }

        public override int ObterId(RepositorioBase<Remedio> repositorioBase)
        {
            Console.Clear();
            MostarListaRemediosDisponiveis(repositorioRemedio);

            Console.Write("\n   Digite o id do remédio que deseja (cada remédio possuí seu fornecedor): ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite o id do remédio (cada remédio possuí seu fornecedor): ");
            }
            return id;
        }
        
        public void MostarListaRemedios(RepositorioRemedio repositorioRemedio)
        {
            Console.Clear();
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                         Lista de Remédios                                                ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", "ID ", "  NOME ", "  FORNECEDOR ", "  DISPONÍVEL ", "  MÍNIMO DESEJADO ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Remedio print in repositorioRemedio.GetAll())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", print.id, print.nome, print.fornecedor.informacoesPessoais.nome, print.quantidadeDisponivel, print.quantidadeMinima);
                }
            }
        }

        public void MostarListaRemediosDisponiveis(RepositorioRemedio repositorioRemedio)
        {
            Console.Clear();
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                    Lista de Remédios Disponíveis                                         ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", "ID ", "  NOME ", "  FORNECEDOR ", "  DISPONÍVEL ", "  MÍNIMO DESEJADO ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Remedio print in repositorioRemedio.GetAll())
            {
                if (print != null && print.quantidadeDisponivel != 0)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", print.id, print.nome, print.fornecedor.informacoesPessoais.nome, print.quantidadeDisponivel, print.quantidadeMinima);
                }
            }
        }

        public void MostarListaRemediosEmFalta(RepositorioRemedio repositorioRemedio)
        {
            Console.Clear();
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                      Lista de Remédios Em Falta                                          ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", "ID ", "  NOME ", "  FORNECEDOR ", "  DISPONÍVEL ", "  MÍNIMO DESEJADO ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Remedio print in repositorioRemedio.GetAll())
            {
                if (print != null && print.quantidadeDisponivel == 0)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", print.id, print.nome, print.fornecedor.informacoesPessoais.nome, print.quantidadeDisponivel, print.quantidadeMinima);
                }
            }
        }

        public void MostarListaRemediosPoucaQtd(RepositorioRemedio repositorioRemedio)
        {
            Console.Clear();
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                 Lista de Remédios Com Pouca Quantidade                                   ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", "ID ", "  NOME ", "  FORNECEDOR ", "  DISPONÍVEL ", "  MÍNIMO DESEJADO ");
            Console.WriteLine("__________________________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Remedio print in repositorioRemedio.GetAll())
            {
                if (print != null && print.quantidadeDisponivel <= print.quantidadeMinima && print.quantidadeDisponivel != 0)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", print.id, print.nome, print.fornecedor.informacoesPessoais.nome, print.quantidadeDisponivel, print.quantidadeMinima);
                }
            }
        }

    }
}