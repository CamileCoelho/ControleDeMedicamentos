using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRemedios
{
    public class Remedio
    {
        private static int idCounter = 1;
        public string nome { get; set; }
        public string descricao { get; set; }
        public List<Remedio> historicoRequisicaoes { get; set; }
        public int quantidadeDisponivel { get; set; }
        public int quantidadeMinima { get; set; } //10 remedios
        public Fornecedor fornecedor { get; set; }

        public Remedio()
        {
            
        }

    }
}
