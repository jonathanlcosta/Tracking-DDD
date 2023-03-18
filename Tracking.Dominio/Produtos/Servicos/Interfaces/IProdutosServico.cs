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
        Produto InserirProduto(Produto produto);
        void ExcluirProduto(int id);
        Produto InstanciarProduto(string descricao, decimal preco, decimal peso, decimal altura, decimal largura, decimal comprimento);
        Produto EditarProduto(int codigoProduto, string descricao, decimal preco, decimal peso, decimal altura, decimal largura, decimal comprimento);
    }
}