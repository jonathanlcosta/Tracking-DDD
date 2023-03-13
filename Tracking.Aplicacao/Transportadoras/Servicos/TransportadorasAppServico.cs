using AutoMapper;
using Microsoft.AspNetCore.Http;
using Tracking.Aplicacao.Transportadoras.Servicos.Interfaces;
using Tracking.DataTransfer.Transportadoras.Request;
using Tracking.DataTransfer.Transportadoras.Response;
using Tracking.Dominio.Emails.Entidades;
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

        public IList<TransportadoraResponse> ListarPaginado(int pagina, int tamanho, int? order = 0, string? search = null)
        {
            IQueryable<Transportadora> transportadora = transportadorasRepositorio.Query();
            
            if (!string.IsNullOrEmpty(search))
            {
                transportadora = transportadora.Where(x=>x.NomeFantasia.Contains(search));
            }
            
            switch (order)
            {
                case 1:
                    transportadora = transportadora.OrderBy(x=>x.RazaoSocial);
                    break;
                case 2:
                    transportadora = transportadora.OrderBy(x=>x.Cidade);
                    break;
                case 3:
                    transportadora = transportadora.OrderBy(x=>x.Cnpj);
                    break;
                case 4:
                    transportadora = transportadora.OrderBy(x=>x.InscricaoEstadual);
                    break;
                case 5:
                    transportadora = transportadora.OrderBy(x=>x.Telefones);
                    break;
                case 6:
                    transportadora = transportadora.OrderBy(x=>x.Emails);
                    break;
                case 7:
                    transportadora = transportadora.OrderBy(x=>x.Cidade);
                    break;
                case 8:
                    transportadora = transportadora.OrderBy(x=>x.Cep);
                    break;
                case 9:
                    transportadora = transportadora.OrderBy(x=>x.Endereco);
                    break;
            } 
           transportadora = transportadora.Skip((pagina-1)*tamanho).Take(tamanho);
            var response = mapper.Map<IList<TransportadoraResponse>>(transportadora.ToList());
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