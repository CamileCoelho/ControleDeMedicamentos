using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class RepositorioPaciente : RepositorioBase <Paciente>
    {
        List<Paciente> listaPaciente = new List<Paciente>();

        public void Create(Paciente toAdd)
        {
             listaPaciente.Add(toAdd);
        }

        public void Update(Paciente toEdit, InformacoesPessoais informacoesPessoais, string cpf)
        {
            toEdit.informacoesPessoais = informacoesPessoais;
            toEdit.cpf = cpf;
        }

        public void Delete(Paciente toDelete)
        {
            listaPaciente.Remove(toDelete);
        }

        public List<Paciente> GetAll()
        {
            return listaPaciente;
        }

        public Paciente GetById(int id)
        {
            return listaPaciente.Find(paciente => paciente.id == id);
        }
    }
}
