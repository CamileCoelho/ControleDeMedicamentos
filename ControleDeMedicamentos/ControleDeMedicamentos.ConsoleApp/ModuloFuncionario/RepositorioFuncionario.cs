using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario
{
    public class RepositorioFuncionario
    {
        List<Funcionario> listaFuncionario = new List<Funcionario>();

        public void Create(Funcionario toAdd)
        {
            listaFuncionario.Add(toAdd);               
        }

        public void Update(Funcionario toEdit, InformacoesPessoais informacoesPessoais, string cpf)
        {
            toEdit.informacoesPessoais = informacoesPessoais; 
            toEdit.cpf = cpf;
        }

        public void Delete(Funcionario toDelete)
        {
            listaFuncionario.Remove(toDelete);
        }

        public List<Funcionario> GetAll()
        {
            return listaFuncionario;
        }

        public Funcionario GetById(int id)
        {
            return listaFuncionario.Find(funcionario => funcionario.id == id);
        }
    }
}
