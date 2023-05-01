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
    public class RepositorioAquisicao : RepositorioBase<Aquisicao>
    {
        public void Create(Aquisicao toAdd)
        {
            toAdd.id = idCounter++;
            listaObjeto.Add(toAdd);
        }

        public void Delete(Aquisicao toDelete)
        {
            toDelete.Estornar();
            listaObjeto.Remove(toDelete);
        }

        public List<Aquisicao> GetAll()
        {
            return listaObjeto.Cast<Aquisicao>().ToList();
        }

        public Aquisicao GetById(int id)
        {
            return (Aquisicao)listaObjeto.Find(aquisicao => aquisicao.id == id);
        }

    }
}
