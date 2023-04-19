using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloAquisicao
{
    public class Aquisicao : EntidadeMae
    {
        private static int idCounter = 1;
        public Fornecedor fornecedor { get; set; }
        public InformacoesReposicao informacoesReposicao { get; set; }

        public Aquisicao()
        {
            
        }
    }
}
