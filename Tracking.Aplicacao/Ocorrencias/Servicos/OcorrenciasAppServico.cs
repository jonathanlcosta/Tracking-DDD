using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tracking.Aplicacao.Ocorrencias.Servicos.Interfaces;
using Tracking.DataTransfer.Ocorrencias.Request;
using Tracking.DataTransfer.Ocorrencias.Response;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Repositorios;
using Tracking.Dominio.Ocorrencias.Servicos.Interfaces;
using Tracking.Dominio.Paginacao;
using ISession = NHibernate.ISession;

namespace Tracking.Aplicacao.Ocorrencias.Servicos
{
    public class OcorrenciasAppServico : IOcorrenciasAppServico
    {
        private readonly IOcorrenciasServico ocorrenciasServico;
        private readonly IOcorrenciasRepositorio ocorrenciasRepositorio;
        private readonly IOcorrenciaColetaMercadoriasServico ocorrenciaColetaMercadoriasServico;
        private readonly IMapper mapper;
        private readonly ISession session;
        private readonly IColetaMercadoriasServico coletaMercadoriasServico;
        public OcorrenciasAppServico(IOcorrenciasServico ocorrenciasServico, IOcorrenciasRepositorio ocorrenciasRepositorio,
        IOcorrenciaColetaMercadoriasServico ocorrenciaColetaMercadoriasServico, IMapper mapper, ISession session,
        IColetaMercadoriasServico coletaMercadoriasServico)
        {
            this.ocorrenciasServico = ocorrenciasServico;
            this.ocorrenciasRepositorio = ocorrenciasRepositorio;
            this.ocorrenciaColetaMercadoriasServico = ocorrenciaColetaMercadoriasServico;
            this.mapper = mapper; 
            this.session = session;
            this.coletaMercadoriasServico = coletaMercadoriasServico;
        }
        public OcorrenciaResponse Editar(int codigo, OcorrenciaEditarRequest request)
        {
           Ocorrencia ocorrencia = ocorrenciasServico.Validar(codigo);
            ocorrencia = ocorrenciasServico.Atualizar(codigo, request.NotaFiscal,request.IdCliente, request.IdTransportadora,
            request.Data, request.Observacao);
            var transacao = session.BeginTransaction();
            try
            {
                ocorrencia = ocorrenciasRepositorio.Editar(ocorrencia);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<OcorrenciaResponse>(ocorrencia);;
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
                var ocorrencia = ocorrenciasServico.Validar(codigo);
                ocorrenciasRepositorio.Excluir(ocorrencia);
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

        public OcorrenciaResponse Inserir(OcorrenciaInserirRequest request)
        {
           Ocorrencia ocorrencia = ocorrenciasServico.Instanciar(request.NotaFiscal, request.IdCliente, 
           request.IdTransportadora, request.Data, request.Observacao);

                var ocorrencias = request.Ocorrencias.Select(item => 
                { 
                ColetaMercadoria? coleta = coletaMercadoriasServico.Validar(item.IdColetaMercadoria);
                return ocorrenciaColetaMercadoriasServico.Instanciar(coleta, ocorrencia);
                 }).ToList();

                ocorrenciasServico.AdicionarOcorrencia(ocorrencia, ocorrencias);

                var transacao = session.BeginTransaction();
            try
            {
                ocorrencia = ocorrenciasRepositorio.Inserir(ocorrencia);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<OcorrenciaResponse>(ocorrencia);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<OcorrenciaResponse> Listar(int? pagina, int quantidade, OcorrenciaListarRequest ocorrenciaListarRequest)
        {
           if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            IQueryable<Ocorrencia> query = ocorrenciasRepositorio.Query();
            if (ocorrenciaListarRequest is null)
                throw new Exception();

            if (!string.IsNullOrEmpty(ocorrenciaListarRequest.NotaFiscal))
                query = query.Where(p => p.NotaFiscal.Contains(ocorrenciaListarRequest.NotaFiscal));

            if (!string.IsNullOrEmpty(ocorrenciaListarRequest.Observacao))
                query = query.Where(p => p.Observacao.Contains(ocorrenciaListarRequest.Observacao));

            if ((ocorrenciaListarRequest.Data != DateTime.MinValue))
                query = query.Where(p => p.Data.Date == ocorrenciaListarRequest.Data.Date);

          if (ocorrenciaListarRequest.IdCliente.HasValue && ocorrenciaListarRequest.IdCliente.Value != 0)
            {
                query = query.Where(x => x.Cliente!.Id == ocorrenciaListarRequest.IdCliente.Value);
            }

            if (ocorrenciaListarRequest.IdTransportadora.HasValue && ocorrenciaListarRequest.IdTransportadora.Value != 0)
            {
                query = query.Where(x => x.Transportadora!.CodigoTransportadora == ocorrenciaListarRequest.IdTransportadora.Value);
            }

            PaginacaoConsulta<Ocorrencia> ocorrencias = ocorrenciasRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<OcorrenciaResponse> response;
            response = mapper.Map<PaginacaoConsulta<OcorrenciaResponse>>(ocorrencias);
            return response;
        }

        public OcorrenciaResponse Recuperar(int codigo)
        {
            var ocorrencia = ocorrenciasServico.Validar(codigo);
            var response = mapper.Map<OcorrenciaResponse>(ocorrencia);
            return response;
        }
    }
}