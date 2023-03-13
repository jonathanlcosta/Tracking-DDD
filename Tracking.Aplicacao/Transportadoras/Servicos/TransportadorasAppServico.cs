using AutoMapper;
using Microsoft.AspNetCore.Http;
using Tracking.Aplicacao.Transportadoras.Servicos.Interfaces;
using Tracking.DataTransfer.Transportadoras.Request;
using Tracking.DataTransfer.Transportadoras.Response;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Paginacao;
using Tracking.Dominio.Telefones.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;
using Tracking.Dominio.Transportadoras.Repositorios;
using Tracking.Dominio.Transportadoras.Servicos.Interfaces;
using ISession = NHibernate.ISession;

namespace Tracking.Aplicacao.Transportadoras.Servicos
{
    public class TransportadorasAppServico : ITransportadorasAppServico
    {
        private readonly ITransportadorasServico transportadorasServico;
        private readonly IMapper mapper;
        private readonly ISession session;
        private readonly ITransportadorasRepositorio transportadorasRepositorio;

        public TransportadorasAppServico(ITransportadorasServico transportadorasServico, IMapper mapper, ISession session, ITransportadorasRepositorio transportadorasRepositorio)
        {
            this.transportadorasServico = transportadorasServico;
            this.mapper = mapper;
            this.session = session;
            this.transportadorasRepositorio = transportadorasRepositorio;
        }
        public TransportadoraResponse Editar(int codigoTransportadora, TransportadoraEditarRequest transportadoraEditarRequest)
        {
            transportadoraEditarRequest = transportadoraEditarRequest ?? new TransportadoraEditarRequest();
            var transportadora = mapper.Map<Transportadora>(transportadoraEditarRequest);

                transportadora.Telefones[0].SetNumeroTelefone(transportadoraEditarRequest.NumeroTelefone1);
                transportadora.Telefones[1].SetNumeroTelefone(transportadoraEditarRequest.NumeroTelefone2);
                var telefones = transportadora.Telefones;

                transportadora.Emails[0].SetEnderecoEmail(transportadoraEditarRequest.Email1);
                transportadora.Emails[1].SetEnderecoEmail(transportadoraEditarRequest.Email2);
                var emails = transportadora.Emails;

            transportadora = transportadorasServico.EditarTransportadora(codigoTransportadora, 
                                    transportadora.RazaoSocial, 
                                    transportadora.NomeFantasia,
                                    transportadora.Cnpj,
                                    transportadora.InscricaoEstadual,
                                    emails, 
                                    telefones, 
                                    transportadora.Endereco,
                                    transportadora.Cidade,
                                    transportadora.Cep,
                                    transportadora.Uf,
                                    transportadora.Site);
            var transacao = session.BeginTransaction();
            try
            {
                transportadora = transportadorasRepositorio.Editar(transportadora);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<TransportadoraResponse>(transportadora);;
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
                var transportadora = transportadorasServico.ValidarTransportadora(codigo);
                transportadorasRepositorio.Excluir(transportadora);
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

        public TransportadoraResponse Inserir(TransportadoraInserirRequest transportadoraInserirRequest)
        {
             transportadoraInserirRequest =  transportadoraInserirRequest ?? new TransportadoraInserirRequest();
            var telefones = new List<Telefone>();
                telefones.Add(new Telefone(transportadoraInserirRequest.NumeroTelefone1, null));
                telefones.Add(new Telefone(transportadoraInserirRequest.NumeroTelefone2, null));
                
                var emails = new List<Email>();
                emails.Add(new Email(transportadoraInserirRequest.Email1, null));
                emails.Add(new Email(transportadoraInserirRequest.Email2, null));
            var transportadora = transportadorasServico.InstanciarTransportadora(
                transportadoraInserirRequest.RazaoSocial, 
                                    transportadoraInserirRequest.NomeFantasia,
                                    transportadoraInserirRequest.Cnpj,
                                    transportadoraInserirRequest.InscricaoEstadual,
                                    emails, 
                                    telefones, 
                                    transportadoraInserirRequest.Endereco,
                                    transportadoraInserirRequest.Cidade,
                                    transportadoraInserirRequest.Cep,
                                    transportadoraInserirRequest.Uf,
                                    transportadoraInserirRequest.Site
                );
                
                 foreach (var item in transportadora.Telefones)
                {
                    item.SetTransportadora(transportadora);
                }

                foreach (var item in transportadora.Emails)
                {
                    item.SetTransportadora(transportadora);
                }

            var transacao = session.BeginTransaction();
            try
            {
                
                transportadora = transportadorasRepositorio.Inserir(transportadora);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<TransportadoraResponse>(transportadora);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<TransportadoraResponse> Listar(int? pagina, int quantidade, TransportadoraListarRequest transportadoraListarRequest)
        {
             var query = transportadorasRepositorio.Query();
            if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            if (transportadoraListarRequest is null)
                throw new Exception();

            if (!String.IsNullOrEmpty(transportadoraListarRequest.InscricaoEstadual))
                query = query.Where(p => p.InscricaoEstadual.Contains(transportadoraListarRequest.InscricaoEstadual));
            
             if (!String.IsNullOrEmpty(transportadoraListarRequest.NomeFantasia))
                query = query.Where(p => p.NomeFantasia.Contains(transportadoraListarRequest.NomeFantasia));

                if (!String.IsNullOrEmpty(transportadoraListarRequest.RazaoSocial))
                query = query.Where(p => p.RazaoSocial.Contains(transportadoraListarRequest.RazaoSocial));

            if (!String.IsNullOrEmpty(transportadoraListarRequest.Cidade))
                query = query.Where(p => p.Cidade.Contains(transportadoraListarRequest.Cidade));

             if (!String.IsNullOrEmpty(transportadoraListarRequest.Endereco))
                query = query.Where(p => p.Endereco.Contains(transportadoraListarRequest.Endereco));
            if (!String.IsNullOrEmpty(transportadoraListarRequest.Cnpj))
                query = query.Where(p => p.Cnpj.Contains(transportadoraListarRequest.Cnpj));
            if (!String.IsNullOrEmpty(transportadoraListarRequest.Email1))
                query = query.Where(x => x.Emails.Where(t => t.EnderecoEmail == transportadoraListarRequest.Email1).Any());
            if (!String.IsNullOrEmpty(transportadoraListarRequest.Email2))
                query = query.Where(x => x.Emails.Where(t => t.EnderecoEmail == transportadoraListarRequest.Email2).Any());
           if (!String.IsNullOrEmpty(transportadoraListarRequest.NumeroTelefone1))
                query = query.Where(x => x.Telefones.Where(t => t.NumeroTelefone == transportadoraListarRequest.NumeroTelefone1).Any());
            if (!String.IsNullOrEmpty(transportadoraListarRequest.NumeroTelefone2))
                query = query.Where(x => x.Telefones.Where(t => t.NumeroTelefone == transportadoraListarRequest.NumeroTelefone2).Any());
           
            if (!String.IsNullOrEmpty(transportadoraListarRequest.Site))
                query = query.Where(p => p.Site.Contains(transportadoraListarRequest.Site));
            if (!String.IsNullOrEmpty(transportadoraListarRequest.Uf))
                query = query.Where(p => p.Uf.Contains(transportadoraListarRequest.Uf));
            PaginacaoConsulta<Transportadora> transportadoras = transportadorasRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<TransportadoraResponse> response;
            response = mapper.Map<PaginacaoConsulta<TransportadoraResponse>>(transportadoras);
            return response;
        }

        public TransportadoraResponse Recuperar(int codigoTransportadora)
        {
            var transportadora = transportadorasServico.ValidarTransportadora(codigoTransportadora);
            var response = mapper.Map<TransportadoraResponse>(transportadora);
            return response;
        }
    }
}