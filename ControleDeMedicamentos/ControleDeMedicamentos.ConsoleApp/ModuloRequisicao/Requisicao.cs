using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class Requisicao : EntidadeMae
    {
        private static int idCounter = 1;
        public Paciente paciente { get; set; }
        public InformacoesReposicao informacoesReposicao { get; set; }
        public int quantidadeRequisitada { get; set; }

        public Requisicao()
        {
            
        }

        public Requisicao(Paciente paciente, InformacoesReposicao informacoesReposicao, int quantidadeRequisitada)
        {
            id = idCounter++;
            this.paciente = paciente;
            this.informacoesReposicao = informacoesReposicao;
            this.informacoesReposicao.remedio.quantidadeDisponivel = informacoesReposicao.remedio.quantidadeDisponivel - quantidadeRequisitada;
        }
    }
}
