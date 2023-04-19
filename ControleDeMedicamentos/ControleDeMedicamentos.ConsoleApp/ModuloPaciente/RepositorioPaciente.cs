using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class RepositorioPaciente
    {
        List<Paciente> listaPaciente = new List<Paciente>();

        public string CadastrarPaciente(Paciente toAdd)
        {
            string validacao = toAdd.Validar(toAdd.informacoesPessoais, toAdd.cpf);
            if (validacao == "REGISTRO_REALIZADO")
            {
                listaPaciente.Add(toAdd);
                return "\n   Paciente cadastrado com sucesso!";
            }

            return "\n   Paciente Não Cadastrado: " + validacao;
        }

        public string EditarPaciente(Paciente toEdit, InformacoesPessoais informacoesPessoais, string cpf)
        {
            toEdit.informacoesPessoais = informacoesPessoais;
            toEdit.cpf = cpf;
            return "\n   Paciente editado com sucesso!";
        }

        public string ExcluirPaciente(int id, Validador validador)
        {
            Paciente toDelete = EncontrarPacientePorId(id);

            string validacaoExclusao = validador.PermitirExclusaoDoPaciente(id);

            if (toDelete != null && validacaoExclusao == "SUCESSO!")
            {
                listaPaciente.Remove(toDelete);
                return "\n   Paciente excluido com sucesso!";
            }
            return "\n   Paciente não excluido: " + validacaoExclusao;
        }

        public List<Paciente> ListarPacientes()
        {
            return listaPaciente;
        }

        public Paciente EncontrarPacientePorId(int id)
        {
            return listaPaciente.Find(caixa => caixa.id == id);
        }
    }
}
