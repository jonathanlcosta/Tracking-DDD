using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Produtos.Entidades;

namespace Tracking.Dominio.Produtos.Servicos.Interfaces
{
    public interface IProdutosServico
    {
        Produto ValidarProduto(int codigoProduto);
        Produto InserirProduto(Produto codigoProduto);
        void ExcluirProduto(int id);
        Produto InstanciarProduto(string descricao, decimal preco, double peso, double altura, double largura, double comprimento);
        Produto EditarProduto(int codigoProduto, string descricao, decimal preco, double peso, double altura, double largura, double comprimento);
    }
}