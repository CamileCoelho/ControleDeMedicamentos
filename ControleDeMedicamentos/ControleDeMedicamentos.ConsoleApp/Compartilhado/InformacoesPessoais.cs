using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public class InformacoesPessoais 
    {
        public string nome { get; set; }
        public string telefone { get; set; }
        public string email { get; set; } 
        public string endereco { get; set; }

        public InformacoesPessoais()
        {
            
        }
        public InformacoesPessoais(string nome, string telefone, string email, string endereco)
        {
            this.nome = nome;
            this.telefone = telefone;
            this.email = email;
            this.endereco = endereco;
        }
    }
}
