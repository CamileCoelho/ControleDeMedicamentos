﻿using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected List<T> listaObjeto = new List<T>();

        protected int idCounter = 1;

        public virtual void Insert(T toAdd)
        {
            toAdd.id = idCounter++;
            listaObjeto.Add(toAdd);
        }

        public virtual EntidadeBase GetById(int id)
        {
            return listaObjeto.Find(entidade => entidade.id == id);
        }

        public virtual List<T> GetAll()
        {
            return listaObjeto;
        }

        public virtual void Update(T updated, T imput)
        {
            updated.UpdateInfo(imput); 
        }

        public virtual void Delete(T toDelete)
        {
            listaObjeto.Remove(toDelete);
        }
    }
}
