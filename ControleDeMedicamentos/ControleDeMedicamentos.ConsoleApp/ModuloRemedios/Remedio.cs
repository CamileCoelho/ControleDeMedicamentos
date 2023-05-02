using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRemedios
{
    public class Remedio : EntidadeBase
    {
        private static int idCounter = 1;
        public string nome { get; set; }
        public string descricao { get; set; }
        public int quantidadeDisponivel { get; set; }
        public int quantidadeMinima { get; set; } 
        public Fornecedor fornecedor { get; set; }

        public Remedio()
        {
            
        }
        public Remedio(string nome, string descricao, int quantidadeDisponivel, int quantidadeMinima, Fornecedor fornecedor)
        {
            this.nome = nome;
            this.descricao = descricao;
            this.quantidadeDisponivel = quantidadeDisponivel; 
            this.quantidadeMinima = quantidadeMinima;   
            this.fornecedor = fornecedor;
        }

        public override void UpdateInfo(EntidadeBase updated)
        {
            Remedio toEdit = (Remedio)updated;

            nome = toEdit.nome;
            descricao = toEdit.descricao;
            quantidadeDisponivel = toEdit.quantidadeDisponivel;
            quantidadeMinima = toEdit.quantidadeMinima;
            fornecedor = toEdit.fornecedor;
        }
    }
}
