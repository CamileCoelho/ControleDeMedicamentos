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
    public class Aquisicao : EntidadeBase
    {
        public InformacoesReposicao informacoesReposicao { get; set; }
        public int quantidadeAdquirida { get; set; }

        public Aquisicao()
        {
            
        }

        public Aquisicao(InformacoesReposicao informacoesReposicao, int quantidadeAdquirida)
        {     
            this.informacoesReposicao = informacoesReposicao;
            this.quantidadeAdquirida = quantidadeAdquirida;
            this.informacoesReposicao.remedio.quantidadeDisponivel = informacoesReposicao.remedio.quantidadeDisponivel + quantidadeAdquirida;
        }

        public override void UpdateInfo(EntidadeBase updated)
        {
            Aquisicao toEdit = (Aquisicao)updated;

            this.informacoesReposicao = toEdit.informacoesReposicao;
            this.quantidadeAdquirida = toEdit.quantidadeAdquirida;
        }

        public void Estornar()
        {
            this.informacoesReposicao.remedio.quantidadeDisponivel = (this.informacoesReposicao.remedio.quantidadeDisponivel - this.quantidadeAdquirida);
        }
    }
}
