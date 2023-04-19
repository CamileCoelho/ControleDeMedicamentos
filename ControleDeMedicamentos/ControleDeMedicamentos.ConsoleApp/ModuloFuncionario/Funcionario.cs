using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
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

        // public string login { get; set; }
        // public string senha { get; set; }

        public Funcionario()
        {
            
        }

        public Funcionario(InformacoesPessoais informacoesPessoais, string cpf)
        {
            id = idCounter++;
            this.informacoesPessoais = informacoesPessoais;
            this.cpf = cpf;
        }

        public string Validar(InformacoesPessoais informacoesPessoais, string cpf)
        {
            Validador valida = new Validador();
            string mensagem = "";

            if (valida.ValidaTelefone(informacoesPessoais.telefone))
                mensagem += "TELEFONE_INVALIDO";

            if (valida.ValidarString(informacoesPessoais.endereco))
                mensagem += "ENDERECO_INVALIDO";

            if (valida.ValidarString(informacoesPessoais.nome))
                mensagem += "ENDERECO_INVALIDO";

            if (valida.ValidaEmail(informacoesPessoais.email))
                mensagem += "E-MAIL_INVALIDO";

            if (valida.ValidaCPF(cpf))
                mensagem += "CPF_INVALIDO ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }
    }
}
