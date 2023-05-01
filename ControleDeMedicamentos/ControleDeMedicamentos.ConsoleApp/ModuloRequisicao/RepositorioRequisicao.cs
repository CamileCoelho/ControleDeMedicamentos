using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class RepositorioRequisicao : RepositorioBase<Requisicao>
    {
        List<Requisicao> listaRequisicao = new List<Requisicao>();

        public void Create(Requisicao toAdd)
        {
            listaRequisicao.Add(toAdd);
        }

        public void Update(Requisicao toEdit, int quantidadeRequisitada)
        {
            if(toEdit.quantidadeRequisitada < quantidadeRequisitada)
                toEdit.informacoesReposicao.remedio.quantidadeDisponivel = toEdit.informacoesReposicao.remedio.quantidadeDisponivel + (quantidadeRequisitada - toEdit.quantidadeRequisitada);
            else
                toEdit.quantidadeRequisitada = quantidadeRequisitada;
        }

        public List<Requisicao> GetAll()
        {
            return listaRequisicao;
        }

        public Requisicao GetById(int id)
        {
            return listaRequisicao.Find(requisicao => requisicao.id == id);
        }
    }
}
