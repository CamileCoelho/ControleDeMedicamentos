using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
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
        public InformacoesReposicao informacoesReposicao { get; set; }
        public int quantidadeAdquirida { get; set; }

        public Aquisicao()
        {
            
        }

        public Aquisicao(InformacoesReposicao informacoesReposicao, int quantidadeAdquirida)
        {
            id = idCounter++;            
            this.informacoesReposicao = informacoesReposicao;
            this.informacoesReposicao.remedio.quantidadeDisponivel = informacoesReposicao.remedio.quantidadeDisponivel + quantidadeAdquirida;
        }
    }
}
