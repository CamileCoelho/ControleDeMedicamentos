using ControleDeMedicamentos.ConsoleApp.Compartilhado;
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
    }
}
