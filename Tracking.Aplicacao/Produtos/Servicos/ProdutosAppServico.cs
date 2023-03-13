using AutoMapper;
using NHibernate;
using Tracking.Aplicacao.Produtos.Servicos.Interfaces;
using Tracking.DataTransfer.Produtos.Request;
using Tracking.DataTransfer.Produtos.Response;
using Tracking.Dominio.Paginacao;
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
                                    produto.Largura);
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

         public PaginacaoConsulta<ProdutoResponse> Listar(int? pagina, int quantidade, ProdutoListarRequest produtoListarRequest)
        {
             if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            IQueryable<Produto> query = produtosRepositorio.Query().Where(p => p.Situacao != SituacaoProdutoEnum.Inativo);
            if (produtoListarRequest is null)
                throw new Exception();

            if (!string.IsNullOrEmpty(produtoListarRequest.Descricao))
                query = query.Where(p => p.Descricao.Contains(produtoListarRequest.Descricao));

           if (produtoListarRequest.Preco != 0)
            {
                query = query.Where(p => p.Preco == produtoListarRequest.Preco);}

            if (produtoListarRequest.Altura != 0)
            {
                query = query.Where(p => p.Altura == produtoListarRequest.Altura);}

            if (produtoListarRequest.Comprimento != 0)
            {
                query = query.Where(p => p.Comprimento == produtoListarRequest.Comprimento);}

            if (produtoListarRequest.Largura != 0)
            {
                query = query.Where(p => p.Largura == produtoListarRequest.Largura);}

            if (produtoListarRequest.Peso != 0)
            {
                query = query.Where(p => p.Peso == produtoListarRequest.Peso);}

            PaginacaoConsulta<Produto> produtos = produtosRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<ProdutoResponse> response;
            response = mapper.Map<PaginacaoConsulta<ProdutoResponse>>(produtos);
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