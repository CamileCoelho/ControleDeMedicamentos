using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class Funcionario : EntidadeMae
    {
        private static int idCounter = 1;
        public InformacoesPessoais informacoesPessoais { get; set; }
        public string cpf { get; set; }

        // public string login { get; set; }  =>   é  o  id.
        public string senha { get; set; }

        public Funcionario()
        {

        }

        public Funcionario(InformacoesPessoais informacoesPessoais, string cpf, string senha)
        {
            id = idCounter++;
            this.informacoesPessoais = informacoesPessoais;
            this.cpf = cpf;
            this.senha = senha;
        }
    }
}
