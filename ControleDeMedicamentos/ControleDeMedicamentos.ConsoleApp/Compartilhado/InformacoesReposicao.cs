using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public class InformacoesReposicao
    {
        public Remedio remedio { get; set; }
        public Funcionario funcionario { get; set; }
        public DateTime data { get; set; }

        public InformacoesReposicao()
        {
            
        }

        public InformacoesReposicao(Remedio remedio, Funcionario funcionario, int quantidadeRequisitada, DateTime data)
        {
            this.remedio = remedio;
            this.funcionario = funcionario;
            this.data = DateTime.Today;
        }
    }
}
