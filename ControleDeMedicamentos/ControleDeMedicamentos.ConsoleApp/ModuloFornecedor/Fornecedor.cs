using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class Fornecedor : EntidadeBase
    {
        public InformacoesPessoais informacoesPessoais { get; set; } 
        public string cnpj { get; set; }

        public Fornecedor()
        {
            
        }

        public Fornecedor(InformacoesPessoais informacoesPessoais, string cnpj) : base()
        {
            this.informacoesPessoais = informacoesPessoais;
            this.cnpj = cnpj;
        }

        public override void UpdateInfo(EntidadeBase updated)
        {
            Fornecedor toEdit = (Fornecedor)updated;

            informacoesPessoais = toEdit.informacoesPessoais;
            cnpj = toEdit.cnpj;
        }
    }
}
