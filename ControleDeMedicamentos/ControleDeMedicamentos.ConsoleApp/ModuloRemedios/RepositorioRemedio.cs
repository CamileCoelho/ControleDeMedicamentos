using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRemedios
{
    public class RepositorioRemedio
    {
        List<Remedio> listaRemedio = new List<Remedio>();

        public void Create(Remedio toAdd)
        {
            listaRemedio.Add(toAdd);
        }

        public void Update(Remedio toEdit, string nome, string descricao, int quantidadeDisponivel, int quantidadeMinima, Fornecedor fornecedor)
        {
            toEdit.nome = nome;
            toEdit.descricao = descricao;
            toEdit.quantidadeDisponivel = quantidadeDisponivel;
            toEdit.quantidadeMinima = quantidadeMinima;
            toEdit.fornecedor = fornecedor;                
        }

        public void Delete(Remedio toDelete)
        {
            listaRemedio.Remove(toDelete);
        }

        public List<Remedio> GetAll()
        {
            return listaRemedio;
        }

        public Remedio GetById(int id)
        {
            return listaRemedio.Find(remedio => remedio.id == id);
        }
    }
}
