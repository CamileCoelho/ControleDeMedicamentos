using ClubeDaLeituraDaCamile.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class RepositorioFornecedor
    {
        List<Fornecedor> listaFornecedor = new List<Fornecedor>();

        public string CadastrarFornecedor(Fornecedor toAdd)
        {
            string validacao = toAdd.Validar(toAdd.informacoesPessoais, toAdd.cnpj);
            if (validacao == "REGISTRO_REALIZADO")
            {
                listaFornecedor.Add(toAdd);
                return "\n   Fornecedor cadastrado com sucesso!";
            }

            return "\n   Fornecedor Não Cadastrado: " + validacao;
        }

        public string EditarFornecedor(Fornecedor toEdit, InformacoesPessoais informacoesPessoais, string cnpj)
        {
            toEdit.informacoesPessoais = informacoesPessoais;
            toEdit.cnpj = cnpj;
            return "\n   Fornecedor editado com sucesso!";
        }

        public string ExcluirFornecedor(int id, Validador validador)
        {
            Fornecedor toDelete = EncontrarFornecedorPorId(id);

            string validacaoExclusao = validador.PermitirExclusaoDoFornecedor(id);

            if (toDelete != null && validacaoExclusao == "SUCESSO!")
            {
                listaFornecedor.Remove(toDelete);
                return "\n   Fornecedor excluido com sucesso!";
            }
            return "\n   Fornecedor não excluido: " + validacaoExclusao;
        }

        public List<Fornecedor> ListarFornecedores()
        {
            return listaFornecedor;
        }

        public Fornecedor EncontrarFornecedorPorId(int id)
        {
            return listaFornecedor.Find(caixa => caixa.id == id);
        }
    }
}
