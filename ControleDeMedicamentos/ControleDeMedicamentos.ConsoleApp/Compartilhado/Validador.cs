using ControleDeMedicamentos.ConsoleApp.ModuloAquisicao;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;
using System.Text.RegularExpressions;

namespace ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado
{
    public class Validador
    {
        public RepositorioFornecedor? repositorioFornecedor;
        public RepositorioFuncionario? repositorioFuncionario;
        public RepositorioPaciente? repositorioPaciente;
        public RepositorioRemedio? repositorioRemedio;
        public RepositorioRequisicao? repositorioRequisicao;
        public RepositorioAquisicao? repositorioAquisicao;

        public Validador()
        {
           
        }

        public Validador(RepositorioFornecedor? repositorioFornecedor, RepositorioFuncionario? repositorioFuncionario, RepositorioPaciente? repositorioPaciente, RepositorioRemedio? repositorioRemedio, RepositorioRequisicao? repositorioRequisicao, RepositorioAquisicao? repositorioAquisicao)
        {
            this.repositorioFornecedor = repositorioFornecedor;
            this.repositorioFuncionario = repositorioFuncionario;
            this.repositorioPaciente = repositorioPaciente;
            this.repositorioRemedio = repositorioRemedio;
            this.repositorioRequisicao = repositorioRequisicao;
        }
        
        public bool ValidarString(string str)
        {
            if (string.IsNullOrEmpty(str) && string.IsNullOrWhiteSpace(str))
                return true;
            else
                return false;
        }

        public bool ValidaTelefone(string telefone)
        {
            // formato (XX)XXXXX-XXXX
            Regex Rgx = new Regex(@"^\(\d{2}\)\d{5}-\d{4}$");

            if (Rgx.IsMatch(telefone))
                return false;
            else
                return true;
        }

        public bool ValidaCPF(string cpf)
        {
            // formato XXX.XXX.XXX-XX ou XXXXXXXXXXX
            Regex Rgx = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$");

            if (Rgx.IsMatch(cpf))
                return true;
            else
                return false;
        }

        public bool ValidaCNPJ(string cnpj)
        {
            // formato XX.XXX.XXX/XXXX-XX ou XXXXXXXXXXXXXX
            Regex Rgx = new Regex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$|^\d{14}$");

            if (Rgx.IsMatch(cnpj))
                return true;
            else
                return false;
        }

        public bool ValidaFormatoEmail(string email)
        {
            // formato permitido: qualquer coisa antes do "@" seguido por pelo menos uma letra depois
            Regex Rgx = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

            if (Rgx.IsMatch(email))
                return true;
            else
                return false;
        }

        public bool ValidaEmail(string email)
        {
            // formato permitido: qualquer coisa antes do "@" seguido por pelo menos um caractere depois
            Regex Rgx = new Regex(@"^[^\s@]+@[^\s@]+$");

            if (Rgx.IsMatch(email))
                return true;
            else
                return false;
        }

        public string PermitirExclusaoDoPaciente(int id)
        {
            throw new NotImplementedException();
            //if (repositorioPaciente.ListarPacientes().Any(x => x.paciente.id == id))
            //    return " Este paciente possuí um processo em andamento. ";
            //else
            //    return "SUCESSO!";
        }

        public string PermitirExclusaoDoFuncionario(int id)
        {
            throw new NotImplementedException();
        }

        public string PermitirExclusaoDoFornecedor(int id)
        {
            throw new NotImplementedException();
        }
                
    }
}
