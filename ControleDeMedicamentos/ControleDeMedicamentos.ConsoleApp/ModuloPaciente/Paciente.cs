using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeMae
    {
        private static int idCounter = 1;
        public InformacoesPessoais informacoesPessoais { get; set; }
        public string cpf { get; set; }

        public Paciente()
        {
            
        }

        public Paciente(InformacoesPessoais informacoesPessoais, string cpf)
        {
            id = idCounter++;
            this.informacoesPessoais = informacoesPessoais;
            this.cpf = cpf;
        }
    }
}
