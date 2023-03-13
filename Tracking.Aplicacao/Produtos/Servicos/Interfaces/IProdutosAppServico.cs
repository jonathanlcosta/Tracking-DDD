using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.DataTransfer.Produtos.Request;
using Tracking.DataTransfer.Produtos.Response;
using Tracking.Dominio.Generico.Repositorios;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.Produtos.Servicos.Interfaces
{
    public interface IProdutosAppServico
    {
        PaginacaoConsulta<ProdutoResponse> Listar(int? pagina, int quantidade, ProdutoListarRequest produtoListarRequest);
        ProdutoResponse Recuperar(int codigoProduto);
        ProdutoResponse Inserir(ProdutoInserirRequest produtoInserirRequest);
        ProdutoResponse Editar(int codigoProduto, ProdutoEditarRequest produtoEditarRequest);
        void Excluir(int codigo);
    }
}