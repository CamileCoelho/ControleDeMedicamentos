using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class Fornecedor : EntidadeMae
    {
        private static int idCounter = 1;
        public InformacoesPessoais informacoesPessoais { get; set; } //prop
        public string cnpj { get; set; }

        public Fornecedor()
        {
            
        }

        public Fornecedor(InformacoesPessoais informacoesPessoais, string cnpj)
        {
            id = idCounter++;
            this.informacoesPessoais = informacoesPessoais;
            this.cnpj = cnpj;
        }

        public string Validar(InformacoesPessoais informacoesPessoais, string cnpj)

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

            if (valida.ValidaCNPJ(cnpj))
                mensagem += "CNPJ_INVALIDO ";

            if (mensagem != "")
                return mensagem;

            return "REGISTRO_REALIZADO";
        }
    }
}
