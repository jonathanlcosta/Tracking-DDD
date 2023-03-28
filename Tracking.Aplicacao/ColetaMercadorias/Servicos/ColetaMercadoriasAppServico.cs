using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.Aplicacao.ColetaMercadorias.Servicos.Interfaces;
using Tracking.DataTransfer.ColetaMercadorias.Request;
using Tracking.DataTransfer.ColetaMercadorias.Response;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Repositorios;
using Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Servicos.Interfaces;
using Tracking.Dominio.Paginacao;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Servicos.Interfaces;
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
        private readonly IOcorrenciasServico ocorrenciasServico;
        private readonly IOcorrenciaColetaMercadoriasServico ocorrenciaColetaMercadoriasServico;
        public ColetaMercadoriasAppServico(IColetaMercadoriasServico coletaMercadoriasServico, IColetaMercadoriasRepositorio coletaMercadoriasRepositorio,
         IItemColetaMercadoriasServico itemColetaMercadoriasServico, IClientesServico clientesServico,
         IProdutosServico produtosServico, IMapper mapper, ISession session, IOcorrenciasServico ocorrenciasServico,
         IOcorrenciaColetaMercadoriasServico ocorrenciaColetaMercadoriasServico )
        {
            this.coletaMercadoriasRepositorio = coletaMercadoriasRepositorio;
            this.coletaMercadoriasServico = coletaMercadoriasServico;
            this.clientesServico = clientesServico;
            this.itemColetaMercadoriasServico = itemColetaMercadoriasServico;
            this.produtosServico = produtosServico;
            this.mapper = mapper;
            this.session = session;
            this.ocorrenciasServico = ocorrenciasServico;
            this.ocorrenciaColetaMercadoriasServico = ocorrenciaColetaMercadoriasServico;

        }
        public ColetaMercadoriaResponse Editar(int codigoColeta, ColetaMercadoriaEditarRequest request)
        {

           ColetaMercadoria coleta = coletaMercadoriasServico.Validar(codigoColeta);
            coleta = coletaMercadoriasServico.Atualizar(codigoColeta, request.NotaFiscal,request.PedidoCompra, request.IdCliente,
            request.IdTransportadora, request.NomeFantasia);
            var transacao = session.BeginTransaction();
            try
            {
                coleta = coletaMercadoriasRepositorio.Editar(coleta);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<ColetaMercadoriaResponse>(coleta);;
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
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
                    Cliente? cliente = clientesServico.Validar(request.IdCliente);
                    itemColetaMercadoriasServico.CalcularFrete(produto.Altura, produto.Largura, cliente.CustoPorPeso,cliente.Seguro,item.ValorFrete, item.Quantidade);
                    itensProdutos.Add(itemColetaMercadoriasServico.Instanciar(produto, item.Quantidade, coletaMercadoria, 
                    item.ValorProduto, item.Descricao, item.Dimensoes, item.ValorFrete));
                });

                coletaMercadoriasServico.AdicionarItem(coletaMercadoria, itensProdutos);
                // coletaMercadoriasServico.AdicionarItem(coletaMercadoria, ocorrencias);

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

        public PaginacaoConsulta<ColetaMercadoriaResponse> Listar(int? pagina, int quantidade, ColetaMercadoriaListarRequest coletaListarRequest)
        {
             if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            IQueryable<ColetaMercadoria> query = coletaMercadoriasRepositorio.Query();
            if (coletaListarRequest is null)
                throw new Exception();

            if (!string.IsNullOrEmpty(coletaListarRequest.NotaFiscal))
                query = query.Where(p => p.NotaFiscal.Contains(coletaListarRequest.NotaFiscal));

            if (!string.IsNullOrEmpty(coletaListarRequest.PedidoCompra))
                query = query.Where(p => p.PedidoCompra.Contains(coletaListarRequest.PedidoCompra));

            if (!string.IsNullOrEmpty(coletaListarRequest.NomeFantasia))
                query = query.Where(p => p.NomeFantasia.Contains(coletaListarRequest.NomeFantasia));

          if (coletaListarRequest.IdCliente.HasValue && coletaListarRequest.IdCliente.Value != 0)
            {
                query = query.Where(x => x.Cliente!.Id == coletaListarRequest.IdCliente.Value);
            }

            if (coletaListarRequest.IdTransportadora.HasValue && coletaListarRequest.IdTransportadora.Value != 0)
            {
                query = query.Where(x => x.Transportadora!.CodigoTransportadora == coletaListarRequest.IdTransportadora.Value);
            }

            PaginacaoConsulta<ColetaMercadoria> coletas = coletaMercadoriasRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<ColetaMercadoriaResponse> response;
            response = mapper.Map<PaginacaoConsulta<ColetaMercadoriaResponse>>(coletas);
            return response;
        }

        public ColetaMercadoriaResponse Recuperar(int codigoColeta)
        {
           var coleta = coletaMercadoriasServico.Validar(codigoColeta);
            var response = mapper.Map<ColetaMercadoriaResponse>(coleta);
            return response;
        }
    }
}