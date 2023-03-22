using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.Aplicacao.ColetaMercadorias.Servicos.Interfaces;
using Tracking.DataTransfer.ColetaMercadorias.Request;
using Tracking.DataTransfer.ColetaMercadorias.Response;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Repositorios;
using Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces;
using Tracking.Dominio.Paginacao;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Servicos.Interfaces;
using Tracking.Dominio.Transportadoras.Servicos.Interfaces;
using ISession = NHibernate.ISession;
namespace Tracking.Aplicacao.ColetaMercadorias.Servicos
{
    public class ColetaMercadoriasAppServico : IColetaMercadoriasAppServico
    {
        private readonly IColetaMercadoriasServico coletaMercadoriasServico;
        private readonly IColetaMercadoriasRepositorio coletaMercadoriasRepositorio;
        private readonly IItemColetaMercadoriasServico itemColetaMercadoriasServico;
        private readonly IMapper mapper;
        private readonly ISession session;
        private readonly IClientesServico clientesServico;
        private readonly IProdutosServico produtosServico;
        public ColetaMercadoriasAppServico(IColetaMercadoriasServico coletaMercadoriasServico, IColetaMercadoriasRepositorio coletaMercadoriasRepositorio,
         IItemColetaMercadoriasServico itemColetaMercadoriasServico, IClientesServico clientesServico,
         IProdutosServico produtosServico, IMapper mapper, ISession session )
        {
            this.coletaMercadoriasRepositorio = coletaMercadoriasRepositorio;
            this.coletaMercadoriasServico = coletaMercadoriasServico;
            this.clientesServico = clientesServico;
            this.itemColetaMercadoriasServico = itemColetaMercadoriasServico;
            this.produtosServico = produtosServico;
            this.mapper = mapper;
            this.session = session;

        }
        public ColetaMercadoriaResponse Editar(int codigoColeta, ColetaMercadoriaEditarRequest coletaMercadoriaEditarRequest)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int codigo)
        {
           var transacao = session.BeginTransaction();
            try
            {
                var coletaMercadoria = coletaMercadoriasServico.Validar(codigo);
                coletaMercadoriasRepositorio.Excluir(coletaMercadoria);
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

        public ColetaMercadoriaResponse Inserir(ColetaMercadoriaInserirRequest request)
        {
           ColetaMercadoria coletaMercadoria = coletaMercadoriasServico.Instanciar(request.NotaFiscal, request.PedidoCompra, 
           request.IdCliente, request.IdTransportadora, request.NomeFantasia);

                var itensProdutos = new List<ItemColetaMercadoria>();

                request.ItensProdutos!.ToList().ForEach(item =>
                {
                    Produto? produto = produtosServico.ValidarProduto(item.IdProduto);
                    ColetaMercadoria? coleta = coletaMercadoriasServico.Validar(item.IdColetaMercadoria);
                    itensProdutos.Add(itemColetaMercadoriasServico.Instanciar(produto, item.Quantidade, coleta, 
                    item.ValorProduto, item.Descricao, item.Dimensoes, item.ValorFrete));
                });

                coletaMercadoriasServico.AdicionarItem(coletaMercadoria, itensProdutos);

                var transacao = session.BeginTransaction();
            try
            {
                coletaMercadoria = coletaMercadoriasRepositorio.Inserir(coletaMercadoria);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<ColetaMercadoriaResponse>(coletaMercadoria);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<ColetaMercadoriaResponse> Listar(int? pagina, int quantidade, ColetaMercadoriaListarRequest coletaMercadoriaListarRequest)
        {
            throw new NotImplementedException();
        }

        public ColetaMercadoriaResponse Recuperar(int codigoColeta)
        {
           var coleta = coletaMercadoriasServico.Validar(codigoColeta);
            var response = mapper.Map<ColetaMercadoriaResponse>(coleta);
            return response;
        }
    }
}