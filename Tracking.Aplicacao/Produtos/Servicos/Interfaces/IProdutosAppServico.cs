using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Produtos.Request;
using Tracking.DataTransfer.Produtos.Response;
using Tracking.Dominio.Generico.Repositorios;

namespace Tracking.Aplicacao.Produtos.Servicos.Interfaces
{
    public interface IProdutosAppServico
    {
        IList<ProdutoResponse> ListarPaginado(int pagina, int tamanho, int? order = 0, string? search = null);
        ProdutoResponse Recuperar(int codigoProduto);
        ProdutoResponse Inserir(ProdutoInserirRequest produtoInserirRequest);
        ProdutoResponse Editar(int codigoProduto, ProdutoEditarRequest produtoEditarRequest);
        void Excluir(int codigo);
    }
}