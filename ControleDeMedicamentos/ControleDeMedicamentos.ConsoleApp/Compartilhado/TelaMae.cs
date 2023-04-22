using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public class TelaMae
    {
        public Validador validador;

        public void ExibirMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}
