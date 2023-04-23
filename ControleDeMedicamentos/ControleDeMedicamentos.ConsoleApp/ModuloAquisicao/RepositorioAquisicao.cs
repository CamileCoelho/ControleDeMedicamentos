using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloAquisicao
{
    public class RepositorioAquisicao
    {
        List<Aquisicao> listaAquisicaor = new List<Aquisicao>();

        public void Create(Aquisicao toAdd)
        {
            listaAquisicaor.Add(toAdd);
        }

        public void Delete(Aquisicao toDelete)
        {
            toDelete.informacoesReposicao.remedio.quantidadeDisponivel = (toDelete.informacoesReposicao.remedio.quantidadeDisponivel - toDelete.quantidadeAdquirida);
            listaAquisicaor.Remove(toDelete);
        }

        public List<Aquisicao> GetAll()
        {
            return listaAquisicaor;
        }

        public Aquisicao GetById(int id)
        {
            return listaAquisicaor.Find(aquisicao => aquisicao.id == id);
        }

    }
}
