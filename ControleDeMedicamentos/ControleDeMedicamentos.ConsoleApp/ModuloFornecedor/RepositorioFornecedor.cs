using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class RepositorioFornecedor
    {
        List<Fornecedor> listaFornecedor = new List<Fornecedor>();

        public void Create(Fornecedor toAdd)
        {
            listaFornecedor.Add(toAdd);
        }

        public void Update(Fornecedor toEdit, InformacoesPessoais informacoesPessoais, string cnpj)
        {
            toEdit.informacoesPessoais = informacoesPessoais;
            toEdit.cnpj = cnpj;
        }

        public void Delete(Fornecedor toDelete)
        {
            listaFornecedor.Remove(toDelete);               
        }

        public List<Fornecedor> GetAll()
        {
            return listaFornecedor;
        }

        public Fornecedor GetById(int id)
        {
            return listaFornecedor.Find(fornecedor => fornecedor.id == id);
        }
    }
}
