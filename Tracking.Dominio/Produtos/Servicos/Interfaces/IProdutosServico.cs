using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Produtos.Entidades;

namespace Tracking.Dominio.Produtos.Servicos.Interfaces
{
    public interface IProdutosServico
    {
        Produto Validar(int codigoProduto);
        Produto Inserir(Produto produto);
        void Excluir(int id);
        Produto Instanciar(string descricao, decimal preco, decimal peso, decimal altura, decimal largura, decimal comprimento);
        Produto Editar(int codigoProduto, string descricao, decimal preco, decimal peso, decimal altura, decimal largura, decimal comprimento);
    }
}