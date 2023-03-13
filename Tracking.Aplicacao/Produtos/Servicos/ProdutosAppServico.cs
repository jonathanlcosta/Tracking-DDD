using AutoMapper;
using NHibernate;
using Tracking.Aplicacao.Produtos.Servicos.Interfaces;
using Tracking.DataTransfer.Produtos.Request;
using Tracking.DataTransfer.Produtos.Response;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Enumeradores;
using Tracking.Dominio.Produtos.Repositorios;
using Tracking.Dominio.Produtos.Servicos.Interfaces;

namespace Produtos.Aplicacao.Produtos.Servicos
{
    public class ProdutosAppServico : IProdutosAppServico
    {
        private readonly IProdutosServico produtosServico;
        private readonly IMapper mapper;
        private readonly ISession session;
        private readonly IProdutosRepositorio produtosRepositorio;

        public ProdutosAppServico(IProdutosServico produtosServico, IMapper mapper, ISession session, IProdutosRepositorio produtosRepositorio)
        {
            this.produtosServico = produtosServico;
            this.mapper = mapper;
            this.session = session;
            this.produtosRepositorio = produtosRepositorio;
        }

        public ProdutoResponse Editar(int codigo, ProdutoEditarRequest produtoEditarRequest)
        {
            produtoEditarRequest = produtoEditarRequest ?? new ProdutoEditarRequest();
            var produto = mapper.Map<Produto>(produtoEditarRequest);
            produto = produtosServico.EditarProduto(codigo, 
                                    produto.Descricao, 
                                    produto.Preco, 
                                    produto.Peso, 
                                    produto.Altura,
                                    produto.Largura,
                                    produto.Comprimento);
            var transacao = session.BeginTransaction();
            try
            {
                produto = produtosRepositorio.Editar(produto);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<ProdutoResponse>(produto);;
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }

        }

        public ProdutoResponse Inserir(ProdutoInserirRequest produtoInserirRequest)
        {
            produtoInserirRequest = produtoInserirRequest ?? new ProdutoInserirRequest();
            var produto = produtosServico.InstanciarProduto(
                produtoInserirRequest.Descricao, 
                                    produtoInserirRequest.Preco, 
                                    produtoInserirRequest.Peso, 
                                    produtoInserirRequest.Altura,
                                    produtoInserirRequest.Largura,
                                    produtoInserirRequest.Comprimento);
            var transacao = session.BeginTransaction();
            try
            {
                produto = produtosRepositorio.Inserir(produto);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<ProdutoResponse>(produto);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public ProdutoResponse Recuperar(int codigoProduto)
        {
            var produto = produtosServico.ValidarProduto(codigoProduto);
            var response = mapper.Map<ProdutoResponse>(produto);
            return response;
        }

        public IList<ProdutoResponse> ListarPaginado(int pagina, int tamanho, int? order = 0, string? search = "")
        {
            IQueryable<Produto> produtos = produtosRepositorio.Query()
            .Where(x => x.Situacao != SituacaoProdutoEnum.Inativo);
            
            if (!string.IsNullOrEmpty(search))
            {
                produtos = produtos.Where(x=>x.Descricao.Contains(search));
            }
            
            switch (order)
            {
                case 1:
                    produtos = produtos.OrderBy(x=>x.CodigoProduto);
                    break;
                case 2:
                    produtos = produtos.OrderBy(x=>x.Descricao);
                    break;
                case 3:
                    produtos = produtos.OrderBy(x=>x.Altura);
                    break;
                case 4:
                    produtos = produtos.OrderBy(x=>x.Comprimento);
                    break;
                case 5:
                    produtos = produtos.OrderBy(x=>x.Largura);
                    break;
                case 6:
                    produtos = produtos.OrderBy(x=>x.Peso);
                    break;
                case 7:
                    produtos = produtos.OrderBy(x=>x.Preco);
                    break;
            } 
            produtos = produtos.Skip((pagina-1)*tamanho).Take(tamanho);
            var response = mapper.Map<IList<ProdutoResponse>>(produtos.ToList());
            return response;
        }

        public void Excluir(int codigo)
        {
            var transacao = session.BeginTransaction();
            try
            {
                var produto = produtosServico.ValidarProduto(codigo);
                produtosRepositorio.Excluir(produto);
                if(transacao.IsActive)
                    transacao.Commit();
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }
    }
}