using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloRemedios;
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

        public void ExibirAvisos(RepositorioRemedio repositorioRemedio)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            foreach (Remedio r in repositorioRemedio.GetAll())
            {
                if (r != null && r.quantidadeDisponivel == 0 && r.quantidadeDisponivel <= r.quantidadeMinima
                    && r.quantidadeDisponivel != 0)
                {
                    Console.Write("\n   Você possuí remédios com o estoque zerado ou em pouca quantidade. " +
                        "\n   Realize uma aquisição para repor seu estoque. ");
                    Console.ReadLine();
                    return;
                }
                else if (r != null && r.quantidadeDisponivel == 0)
                {
                    Console.Write("\n   Você possuí remédios com o estoque zerado. " +
                        "\n   Realize uma aquisição para repor seus remédios com o estoque zerado. ");
                    Console.ReadLine();
                    return;
                }
                else if (r != null && r.quantidadeDisponivel <= r.quantidadeMinima && r.quantidadeDisponivel != 0)
                {
                    Console.Write("\n   Você possuí remédios em pouca quantidade em estoque. " +
                        "\n   Realize uma aquisição para repor seus remédios em pouca quantidade. ");
                    Console.ReadLine();
                    return;
                }
            }            
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}
