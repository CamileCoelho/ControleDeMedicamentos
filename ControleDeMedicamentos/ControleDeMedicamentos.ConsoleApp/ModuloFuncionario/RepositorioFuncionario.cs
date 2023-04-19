using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class RepositorioFuncionario
    {
        List<Funcionario> listaFuncionario = new List<Funcionario>();

        public string CadastrarFuncionario(Funcionario toAdd)
        {
            string validacao = toAdd.Validar(toAdd.informacoesPessoais, toAdd.cpf);
            if (validacao == "REGISTRO_REALIZADO")
            {
                listaFuncionario.Add(toAdd);
                return "\n   Funcionário cadastrado com sucesso!";
            }

            return "\n   Funcionário Não Cadastrado: " + validacao;
        }

        public string EditarFuncionario(Funcionario toEdit, InformacoesPessoais informacoesPessoais, string cpf)
        {
            toEdit.informacoesPessoais = informacoesPessoais;
            toEdit.cpf = cpf;
            return "\n   Funcionario editado com sucesso!";
        }

        public string ExcluirFuncionario(int id, Validador validador)
        {
            Funcionario toDelete = EncontrarFuncionarioPorId(id);

            string validacaoExclusao = validador.PermitirExclusaoDoFuncionario(id);

            if (toDelete != null && validacaoExclusao == "SUCESSO!")
            {
                listaFuncionario.Remove(toDelete);
                return "\n   Funcionario excluido com sucesso!";
            }
            return "\n   Funcionario não excluido: " + validacaoExclusao;
        }

        public List<Funcionario> ListarFuncionarios()
        {
            return listaFuncionario;
        }

        public Funcionario EncontrarFuncionarioPorId(int id)
        {
            return listaFuncionario.Find(caixa => caixa.id == id);
        }
    }
}
