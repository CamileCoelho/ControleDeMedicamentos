using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
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

            Validador validador = new Validador(repositorioFornecedor, repositorioFuncionario, repositorioPaciente, repositorioRemedio, repositorioRequisicao, repositorioAquisicao);

            TelaFornecedor telaFornecedor = new TelaFornecedor(validador);
        }
    }
}
