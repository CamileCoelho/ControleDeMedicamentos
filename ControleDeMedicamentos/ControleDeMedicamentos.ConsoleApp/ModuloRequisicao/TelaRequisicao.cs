
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaRequisicao : TelaMae
    {
        RepositorioRequisicao repositorioRequisicao;
        RepositorioPaciente repositorioPaciente;
        RepositorioRemedio repositorioRemedio;
        RepositorioFuncionario repositorioFuncionario;
        TelaPaciente telaPaciente;
        TelaRemedio telaRemedio;
        TelaFuncionario telaFuncionario;
        Validador validador;

        public TelaRequisicao(RepositorioRequisicao repositorioRequisicao, RepositorioPaciente repositorioPaciente, RepositorioRemedio repositorioRemedio, RepositorioFuncionario repositorioFuncionario, TelaPaciente telaPaciente, TelaRemedio telaRemedio, TelaFuncionario telaFuncionario, Validador validador) 
        {
            this.repositorioRequisicao = repositorioRequisicao;
            this.repositorioPaciente = repositorioPaciente;
            this.repositorioRemedio = repositorioRemedio;
            this.repositorioFuncionario = repositorioFuncionario;
            this.telaPaciente = telaPaciente;
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
                        Request();
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
                Console.WriteLine("   1  - Para realizar uma requisição.                                             ");
                Console.WriteLine();
                Console.WriteLine("   2  - Para visualizar suas requisições.                                         ");
                Console.WriteLine();
                Console.WriteLine("   3  - Para editar a quantidade de remédios de uma requisição.                   ");
                Console.WriteLine();
                Console.WriteLine(); // não pode devolver o remedio, logo não pode excluir uma requisicao!
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

        private void Request()
        {
            if (repositorioRemedio.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum remédio cadastrado. " +
                    "\n   Você deve cadastrar um remédio para poder realizar uma requisição.", ConsoleColor.DarkRed);
                return;
            }
            if (repositorioPaciente.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum paciente cadastrado. " +
                    "\n   Você deve cadastrar um paciente para poder realizar uma requisição.", ConsoleColor.DarkRed);
                return;
            }
            if (repositorioFuncionario.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum funcionário cadastrado. " +
                    "\n   Você deve cadastrar um funcionário para poder realizar uma requisição.", ConsoleColor.DarkRed);
                return;
            }

            Imput(out Paciente paciente, out InformacoesReposicao informacoesReposicao, out int quantidadeRequisitada, out string senhaImputada);

            string valido = validador.ValidarRequisicao(informacoesReposicao, quantidadeRequisitada, senhaImputada);

            if (valido == "REGISTRO_REALIZADO")
            {
                Requisicao toAdd = new Requisicao(paciente, informacoesReposicao, quantidadeRequisitada);
                repositorioRequisicao.Create(toAdd);
                ExibirMensagem("\n   Requisição realizada com sucesso!", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Requisição Não Realizada: " + valido, ConsoleColor.DarkRed);
            }
        }

        private void Visualizar()
        {
            if (repositorioRequisicao.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhuma requisição encontrada. " +
                    "\n   Você deve realizar uma requisição para poder visualizar suas requisições.", ConsoleColor.DarkRed);
                return;
            }
            MostarListaRequisicoes(repositorioRequisicao);
            Console.ReadLine();
        }

        private void Editar()
        {
            if (repositorioRequisicao.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhuma requisição encontrada. " +
                    "\n   Você deve realizar uma requisição para poder visualizar suas requisições.", ConsoleColor.DarkRed);
                return;
            }

            Requisicao toEdit = repositorioRequisicao.GetById(ObterId(repositorioRequisicao));

            if (toEdit == null)
            {
                ExibirMensagem("\n   Requisição não encontrada!", ConsoleColor.DarkRed);
            }
            else
            {
                ImputEdit(toEdit, out int quantidadeRequisitada, out string senhaImputada);

                string valido = validador.ValidarRequisicao(toEdit.informacoesReposicao, quantidadeRequisitada, senhaImputada);

                if (valido == "REGISTRO_REALIZADO")
                {
                    repositorioRequisicao.Update(toEdit, quantidadeRequisitada);
                    ExibirMensagem("\n   Requisição editada com sucesso!", ConsoleColor.DarkGreen);
                }
                else
                {
                    ExibirMensagem("\n   Requisição Não Editada:" + valido, ConsoleColor.DarkRed);
                }
            }
        }

        private void Imput(out Paciente paciente, out InformacoesReposicao informacoesReposicao, out int quantidadeRequisitada, out string senhaImputada)
        {
            informacoesReposicao = new InformacoesReposicao();
            Console.Clear();

            paciente = repositorioPaciente.GetById(telaPaciente.ObterId(repositorioPaciente));

            informacoesReposicao.remedio = repositorioRemedio.GetById(telaRemedio.ObterId(repositorioRemedio));

            Console.Write("\n   Digite a quatidade de unidades que o paciente deseja desse remédio: ");
            while (!int.TryParse(Console.ReadLine(), out quantidadeRequisitada))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite a quatidade de unidades que o paciente deseja desse remédio: ");
            }
            informacoesReposicao.funcionario = repositorioFuncionario.GetById(telaFuncionario.ObterId(repositorioFuncionario));
            Console.Write("\n   Digite a senha: ");
            senhaImputada = Console.ReadLine();
        }

        private void ImputEdit(Requisicao toEdit, out int quantidadeRequisitada, out string senhaImputada)
        {
            Console.Clear();

            Console.Write("\n   Digite a nova quatidade de unidades que o paciente deseja de remédio: ");
            while (!int.TryParse(Console.ReadLine(), out quantidadeRequisitada))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite a nova quatidade de unidades que o paciente deseja de remédio: ");
            }
            toEdit.informacoesReposicao.funcionario = repositorioFuncionario.GetById(telaFuncionario.ObterId(repositorioFuncionario));
            Console.Write("\n   Digite a senha: ");
            senhaImputada = Console.ReadLine();
        }

        public int ObterId(RepositorioRequisicao repositorioRequisiscao)
        {
            Console.Clear();
            MostarListaRequisicoes(repositorioRequisiscao);

            Console.Write("\n   Digite o id da requisição: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite o id da requisição: ");
            }
            return id;
        }

        public void MostarListaRequisicoes(RepositorioRequisicao repositorioRequisicao)
        {
            Console.Clear();
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                   Lista de Requisições!                                     ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|{5,-20}", "ID ", "  PACIENTE ", "  FUNCIONÁRIO ", "  REMÉDIO " , "  FORNECEDOR ", "  DATA ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Requisicao print in repositorioRequisicao.GetAll())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|{5,-20}", print.id, print.paciente.informacoesPessoais.nome, print.informacoesReposicao.funcionario.informacoesPessoais.nome, print.informacoesReposicao.remedio.nome, print.informacoesReposicao.remedio.fornecedor.informacoesPessoais.nome, print.informacoesReposicao.data);
                }
            }
        }

    }
}
