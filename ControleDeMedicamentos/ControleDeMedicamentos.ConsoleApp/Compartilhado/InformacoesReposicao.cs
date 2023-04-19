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
        Remedio remedio { get; set; }
        Funcionario funcionario { get; set; }
        public string data { get; set; }
        public int quantidadeNecessaria { get; set; }

        public InformacoesReposicao()
        {
            
        }
    }
}
