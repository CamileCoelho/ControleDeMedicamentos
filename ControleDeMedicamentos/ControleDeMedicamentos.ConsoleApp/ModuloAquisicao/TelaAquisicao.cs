using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloAquisicao
{
    public class TelaAquisicao : TelaMae
    {
        RepositorioAquisicao repositorioAquisicao;
        RepositorioRemedio repositorioRemedio;
        RepositorioFuncionario repositorioFuncionario;
        TelaRemedio telaRemedio;
        TelaFuncionario telaFuncionario;
        Validador validador;

        public TelaAquisicao(RepositorioAquisicao repositorioAquisicao, RepositorioRemedio repositorioRemedio, RepositorioFuncionario repositorioFuncionario, TelaRemedio telaRemedio, TelaFuncionario telaFuncionario, Validador validador)
        {
            this.repositorioAquisicao = repositorioAquisicao;
            this.repositorioRemedio = repositorioRemedio;
            this.repositorioFuncionario = repositorioFuncionario;
            this.telaRemedio = telaRemedio;
            this.telaFuncionario = telaFuncionario;
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
                    case "4":
                        continuar = false;
                        Console.ResetColor();
                        break;
                    case "1":
                        Acquire();
                        continue;
                    case "2":
                        Visualizar();
                        continue;
                    case "3":
                        Editar();
                        continue;
                }
            } while (continuar);

            string MostrarMenu()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("                             Gestão de Requisições!                               ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.WriteLine("   Digite:                                                                        ");
                Console.WriteLine();
                Console.WriteLine("   1  - Para realizar uma aquisição.                                              ");
                Console.WriteLine(); 
                Console.WriteLine("   2  - Para visualizar suas aquisições.                                          ");
                Console.WriteLine();
                Console.WriteLine("   3  - Para editar uma de suas aquisições.                                       ");
                Console.WriteLine();
                Console.WriteLine(); // não pode cancelar uma compra, logo não pode excluir uma aquisição!
                Console.WriteLine("   4  - Para voltar ao menu principal.                                            ");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.Write("   Opção escolhida: ");
                string opcao = Console.ReadLine().ToUpper();
                bool opcaoInvalida = opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4";
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

        private void Acquire()
        {
            Imput( out InformacoesReposicao informacoesReposicao, out int quantidadeAdquirida, out string senhaImputada);

            string valido = validador.ValidarAquisicao(informacoesReposicao, senhaImputada);

            if (valido == "REGISTRO_REALIZADO")
            {
                Aquisicao toAdd = new Aquisicao(informacoesReposicao, quantidadeAdquirida);
                repositorioAquisicao.Create(toAdd);
                ExibirMensagem("\n   Aquisição realizada com sucesso!", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Aquisição Não Realizada: " + valido, ConsoleColor.DarkRed);
            }
        }

        private void Visualizar()
        {
            if (repositorioAquisicao.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhuma aquisição encontrada. " +
                    "\n   Você deve realizar uma aquisição para poder visualizar suas aquisições.", ConsoleColor.DarkRed);
                return;
            }
            MostarListaAquisicoes(repositorioAquisicao);
            Console.ReadLine();
        }

        private void Editar()
        {
            if (repositorioAquisicao.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhuma aquisição encontrada. " +
                    "\n   Você deve realizar uma aquisição para poder visualizar suas aquisições.", ConsoleColor.DarkRed);
                return;
            }

            Aquisicao toEdit = repositorioAquisicao.GetById(ObterId(repositorioAquisicao));

            if (toEdit == null)
            {
                ExibirMensagem("\n   Aquisição não encontrada!", ConsoleColor.DarkRed);
            }
            else
            {
                Imput(out InformacoesReposicao informacoesReposicao, out int quantidadeAdquirida, out string senhaImputada);

                string valido = validador.ValidarAquisicao(informacoesReposicao, senhaImputada);

                if (valido == "REGISTRO_REALIZADO")
                {
                    repositorioAquisicao.Update(toEdit, informacoesReposicao, quantidadeAdquirida);
                    ExibirMensagem("\n   Aquisição editada com sucesso!", ConsoleColor.DarkGreen);
                }
                else
                {
                    ExibirMensagem("\n   Aquisição Não Editada:" + valido, ConsoleColor.DarkRed);
                }
            }
        }

        public void Imput( out InformacoesReposicao informacoesReposicao, out int quantidadeAdquirida, out string senhaImputada)
        {
            informacoesReposicao = new InformacoesReposicao();
            Console.Clear();

            informacoesReposicao.remedio = repositorioRemedio.GetById(telaRemedio.ObterId(repositorioRemedio));

            Console.Write("\n   Digite a quatidade de unidades que deseja comprar desse remédio: ");
            while (!int.TryParse(Console.ReadLine(), out quantidadeAdquirida))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite a quatidade de unidades que deseja comprar desse remédio: ");
            }
            informacoesReposicao.funcionario = repositorioFuncionario.GetById(telaFuncionario.ObterId(repositorioFuncionario));
            Console.Write("\n   Digite a senha: ");
            senhaImputada = Console.ReadLine();
        }

        public int ObterId(RepositorioAquisicao repositorioAquisicao)
        {
            Console.Clear();
            MostarListaAquisicoes(repositorioAquisicao);

            Console.Write("\n   Digite o id da aquisição: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed); 
                Console.Write("\n   Digite o id da aquisição: ");
            }
            return id;
        }

        public void MostarListaAquisicoes(RepositorioAquisicao repositorioAquisicao)
        {
            Console.Clear();
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                   Lista de Aquisições!                                      ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", "ID ", "  FUNCIONÁRIO ", "  REMÉDIO ", "  FORNECEDOR ", "  DATA ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Aquisicao print in repositorioAquisicao.GetAll())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}|{4,-25}", print.id, print.informacoesReposicao.funcionario.informacoesPessoais.nome, print.informacoesReposicao.remedio.nome, print.informacoesReposicao.remedio.fornecedor, print.informacoesReposicao.data);
                }
            }
        }
    }
}
