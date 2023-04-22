using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class TelaPaciente : TelaMae
    {
        RepositorioPaciente repositorioPaciente;

        public TelaPaciente(RepositorioPaciente repositorioPaciente, Validador validador)
        {
            this.repositorioPaciente = repositorioPaciente;
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
            Imput(out InformacoesPessoais informacoesPessoais, out string cpf);

            string valido = validador.ValidarPaciente(informacoesPessoais, cpf);

            if (valido == "REGISTRO_REALIZADO")
            {
                Paciente toAdd = new Paciente(informacoesPessoais, cpf);
                repositorioPaciente.Create(toAdd); 
                ExibirMensagem("\n   Paciente cadastrado com sucesso!", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem("\n   Paciente Não Cadastrado: " + valido, ConsoleColor.DarkRed);
            }
        }

        private void Visualizar()
        {
            if (repositorioPaciente.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum paciente cadastrado. " +
                    "\n   Você deve cadastrar um paciente para poder visualizar seus cadastros.", ConsoleColor.DarkRed);
                return;
            }
            MostarListaPacientes(repositorioPaciente);
            Console.ReadLine();
        }

        private void Editar()
        {
            if (repositorioPaciente.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum paciente cadastrado. " +
                    "\n   Você deve cadastrar um paciente para poder editar um cadastro.", ConsoleColor.DarkRed);
                return;
            }

            Paciente toEdit = repositorioPaciente.GetById(ObterId(repositorioPaciente));

            if (toEdit == null)
            {
                ExibirMensagem("\n   Paciente não encontrado!", ConsoleColor.DarkRed);
            }
            else
            {
                Imput(out InformacoesPessoais informacoesPessoais, out string cpf);

                string valido = validador.ValidarPaciente(informacoesPessoais, cpf);

                if (valido == "REGISTRO_REALIZADO")
                {
                    repositorioPaciente.Update(toEdit, informacoesPessoais, cpf);
                    ExibirMensagem("\n   Paciente editado com sucesso!", ConsoleColor.DarkGreen);
                }
                else
                {
                    ExibirMensagem("\n   Paciente Não Editado:" + valido, ConsoleColor.DarkRed);
                }
            }
        }

        private void Excluir()
        {
            if (repositorioPaciente.GetAll().Count == 0)
            {
                ExibirMensagem("\n   Nenhum paciente cadastrado. " +
                    "\n   Você deve cadastrar um paciente para poder excluir um cadastro.", ConsoleColor.DarkRed);
                return;
            }

            Paciente toDelete = repositorioPaciente.GetById(ObterId(repositorioPaciente));

            string valido = validador.PermitirExclusaoDoPaciente(toDelete);

            if (valido == "SUCESSO!")
            {
                repositorioPaciente.Delete(toDelete);
                ExibirMensagem("\n   Paciente excluido com sucesso! ", ConsoleColor.DarkGreen);
            }
            else
            {
                ExibirMensagem(valido, ConsoleColor.DarkRed);
            }
        }

        public void Imput(out InformacoesPessoais informacoesPessoais,out string cpf)
        {
            informacoesPessoais = new InformacoesPessoais();
            Console.Clear();
            Console.Write("\n   Digite o nome do paciente: ");
            informacoesPessoais.nome = Console.ReadLine();
            Console.Write("\n   Digite o telefone desse paciente (XX)XXXXX-XXXX: ");
            informacoesPessoais.telefone = Console.ReadLine();
            Console.Write("\n   Digite o email desse paciente: ");
            informacoesPessoais.email = Console.ReadLine();
            Console.Write("\n   Digite o endereco desse paciente: ");
            informacoesPessoais.endereco = Console.ReadLine();
            Console.Write("\n   Digite o CPF desse paciente XXX.XXX.XXX-XX ou XXXXXXXXXXX: ");
            cpf = Console.ReadLine();

        }

        public int ObterId(RepositorioPaciente repositorioPaciente)
        {
            Console.Clear();
            MostarListaPacientes(repositorioPaciente);

            Console.Write("\n   Digite o id do paciente: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                ExibirMensagem("\n   Entrada inválida! Digite um número inteiro. ", ConsoleColor.DarkRed);
                Console.Write("\n   Digite o id do paciente: ");
            }
            return id;
        }

        public void MostarListaPacientes(RepositorioPaciente repositorioPaciente)
        {
            Console.Clear();
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                                    Lista de Pacientes                                       ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", "ID ", "  NOME ", "  TELEFONE ", "  CPF ");
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine();

            foreach (Paciente print in repositorioPaciente.GetAll())
            {
                if (print != null)
                {
                    Console.WriteLine("{0,-5}|{1,-25}|{2,-25}|{3,-25}", print.id, print.informacoesPessoais.nome, print.informacoesPessoais.telefone, print.cpf);
                }
            }
        }

    }
}
