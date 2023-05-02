using ControleDeMedicamentos.ConsoleApp.ModuloAquisicao;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado
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
            this.repositorioAquisicao = repositorioAquisicao;
        }
        
        public bool ValidarString(string str)
        {
            if (string.IsNullOrEmpty(str) && string.IsNullOrWhiteSpace(str))
                return true;
            else
                return false;
        }

        public bool ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha) && string.IsNullOrWhiteSpace(senha) && senha.ToCharArray().Length >= 4)
                return false;
            else
                return true;
        }

        public bool ValidaTelefone(string telefone)
        {
            // formato (XX)XXXXX-XXXX
            Regex Rgx = new(@"^\(\d{2}\)\d{5}-\d{4}$");

            if (Rgx.IsMatch(telefone))
                return false;
            else
                return true;
        }

        public bool ValidaCPF(string cpf)
        {
            // formato XXX.XXX.XXX-XX ou XXXXXXXXXXX
            Regex Rgx = new(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$");

            if (Rgx.IsMatch(cpf))
                return false;
            else
                return true;
        }

        public bool ValidaCNPJ(string cnpj)
        {
            // formato XX.XXX.XXX/XXXX-XX ou XXXXXXXXXXXXXX
            Regex Rgx = new(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$|^\d{14}$");

            if (Rgx.IsMatch(cnpj))
                return false;
            else
                return true;
        }

        public bool ValidaFormatoEmail(string email)
        {
            // formato permitido: qualquer coisa antes do "@" seguido por pelo menos uma letra depois
            Regex Rgx = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

            if (Rgx.IsMatch(email))
                return false;
            else
                return true;
        }

        public bool ValidaEmail(string email)
        {
            // formato permitido: qualquer coisa antes do "@" seguido por pelo menos um caractere depois
            Regex Rgx = new(@"^[^\s@]+@[^\s@]+$");

            if (Rgx.IsMatch(email))
                return false;
            else
                return true;
        }

        public string ValidarInfoPessoal(InformacoesPessoais informacoesPessoais)
        {
            Validador valida = new();
            string mensagem = "";

            if (valida.ValidarString(informacoesPessoais.nome))
                mensagem += " NOME_INVALIDO ";

            if (valida.ValidaTelefone(informacoesPessoais.telefone))
                mensagem += " TELEFONE_INVALIDO ";

            if (valida.ValidarString(informacoesPessoais.nome) || valida.ValidaFormatoEmail(informacoesPessoais.email))
                mensagem += " E-MAIL_INVALIDO ";

            if (valida.ValidarString(informacoesPessoais.endereco))
                mensagem += " ENDERECO_INVALIDO ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

        public string ValidarPaciente(Paciente imput)
        {
            Validador valida = new();
            string mensagem = "";
            string validarInfoPessoalCB = ValidarInfoPessoal(imput.informacoesPessoais);

            if (validarInfoPessoalCB != "REGISTRO_REALIZADO")
                mensagem += validarInfoPessoalCB;

            if (valida.ValidaCPF(imput.cpf))
                mensagem += " CPF_INVALIDO ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

        public string ValidarFunicionario(Funcionario imput)
        {
            Validador valida = new();
            string mensagem = "";
            string validarInfoPessoalCB = ValidarInfoPessoal(imput.informacoesPessoais);

            if (validarInfoPessoalCB != "REGISTRO_REALIZADO")
                mensagem += validarInfoPessoalCB;

            if (valida.ValidaCPF(imput.cpf))
                mensagem += " CPF_INVALIDO ";

            if (valida.ValidarSenha(imput.senha))
                mensagem += " SENHA_INVALIDA ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

        public string ValidarFornecedor(Fornecedor imput)
        {
            Validador valida = new();
            string mensagem = "";
            string validarInfoPessoalCB = ValidarInfoPessoal(imput.informacoesPessoais);

            if (validarInfoPessoalCB != "REGISTRO_REALIZADO")
                mensagem += validarInfoPessoalCB;

            if (valida.ValidaCNPJ(imput.cnpj))
                mensagem += " CNPJ_INVALIDO ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

        public string ValidarRequisicao(Requisicao toAdd, string senhaImputada)
        {
            Validador valida = new();
            string mensagem = "";

            if (toAdd.informacoesReposicao.remedio == null)
                mensagem += " REMEDIO_INVALIDO ";
            else if (toAdd.informacoesReposicao.remedio.quantidadeDisponivel < toAdd.quantidadeRequisitada)
                mensagem += " QUANTIDADE_INDISPONÍVEL ";

            if (toAdd.informacoesReposicao.funcionario == null)
                mensagem += " FUNCIONARIO_INVALIDO ";
            else if (valida.ValidarString(senhaImputada) || toAdd.informacoesReposicao.funcionario.senha != senhaImputada)
                mensagem += " SENHA_ERRADA ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

        public string ValidarAquisicao(Aquisicao imput, string senhaImputada)
        {
            Validador valida = new();
            string mensagem = "";

            if (imput.informacoesReposicao.funcionario == null)
                mensagem += " FUNCIONARIO_INVALIDO ";
            else if (valida.ValidarString(senhaImputada) || imput.informacoesReposicao.funcionario.senha != senhaImputada)
                mensagem += " SENHA_ERRADA ";

            if (imput.informacoesReposicao.remedio == null)
                mensagem += " REMEDIO_INVALIDO ";                      

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

        public string ValidarRemedio(Remedio imput)
        {
            Validador valida = new();
            string mensagem = "";

            if (valida.ValidarString(imput.nome))
                mensagem += " NOME_INVALIDO ";

            if (valida.ValidarString(imput.descricao))
                mensagem += " DESCRICAO_INVALIDA ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

        public string PermitirExclusaoDoPaciente(Paciente toDelete)
        {
            if (toDelete == null)
                return " Paciente não encontrado!";
            if (repositorioRequisicao.GetAll().Any(x => x.paciente.id == toDelete.id))
                return " Este paciente possuí processo em andamento. ";
            else
                return "SUCESSO!";
        }

        public string PermitirExclusaoDoFuncionario(Funcionario toDelete)
        {
            if (toDelete == null)
                return " Funcionário não encontrado!";
            if (repositorioRequisicao.GetAll().Any(x => x.informacoesReposicao.funcionario.id == toDelete.id) || repositorioAquisicao.GetAll().Any(x => x.informacoesReposicao.funcionario.id == toDelete.id))
                return " Este funcionário possuí processo em andamento. ";
            else
                return "SUCESSO!";
        }

        public string PermitirExclusaoDoFornecedor(Fornecedor toDelete)
        {
            if (toDelete == null)
                return " Fornecedor não encontrado!";
            if (repositorioAquisicao.GetAll().Any(x => x.informacoesReposicao.remedio.fornecedor.id == toDelete.id))
                return " Este fornecedor possuí processo em andamento. ";
            else
                return "SUCESSO!";
        }

        public string PermitirExclusaoDoRemedio(Remedio toDelete)
        {
            if (toDelete == null)
                return " Remédio não encontrado!";
            if (repositorioRequisicao.GetAll().Any(x => x.informacoesReposicao.remedio.id == toDelete.id)|| repositorioAquisicao.GetAll().Any(x => x.informacoesReposicao.remedio.id == toDelete.id))
                return " Este remédio possuí processo em andamento. ";
            else
                return "SUCESSO!";
        }

        public string PermitirExclusaoRequisicao(Requisicao toDelete)
        {
            if (toDelete == null)
                return " Requisição não encontrada!";            
            else
                return "SUCESSO!";
        }

        public string PermitirExclusaoAquisicao(Aquisicao toDelete, string senhaImputada)
        {
            Validador valida = new();
            string mensagem = "";

            if (toDelete.informacoesReposicao.funcionario == null)
                mensagem += " FUNCIONARIO_INVALIDO ";
            else if (valida.ValidarString(senhaImputada) || toDelete.informacoesReposicao.funcionario.senha != senhaImputada)
                mensagem += " SENHA_ERRADA ";

            if (toDelete == null)
                return " AQUISICAO_INEXISTENTE ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }

    }
}
