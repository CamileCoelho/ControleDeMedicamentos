using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class Funcionario : EntidadeBase
    {
        private static int idCounter = 1;
        public InformacoesPessoais informacoesPessoais { get; set; }
        public string cpf { get; set; }
        public string senha { get; set; }

        public Funcionario()
        {

        }

        public Funcionario(InformacoesPessoais informacoesPessoais, string cpf, string senha)
        {
            this.informacoesPessoais = informacoesPessoais;
            this.cpf = cpf;
            this.senha = senha;
        }

        public override void UpdateInfo(EntidadeBase imput)
        {
            Funcionario valid = (Funcionario)imput;

            informacoesPessoais = valid.informacoesPessoais;
            cpf = valid.cpf;
            senha = valid.senha;
        }
    }
}
