using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Telefones.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;
using Tracking.Dominio.Transportadoras.Repositorios;
using Tracking.Dominio.Transportadoras.Servicos.Interfaces;

namespace Tracking.Dominio.Transportadoras.Servicos
{
    public class TransportadorasServico : ITransportadorasServico
    {
        private readonly ITransportadorasRepositorio transportadorasRepositorio;
        public TransportadorasServico(ITransportadorasRepositorio transportadorasRepositorio)
        {
            this.transportadorasRepositorio = transportadorasRepositorio;
        }
        public Transportadora EditarTransportadora(int codigoTransportadora, string razaoSocial, string nomeFantasia, 
    string cnpj, string inscricaoEstadual, IList<Email> emails, IList<Telefone> telefones, string endereco,
    string cidade, string cep, string uf, string site)
        {
           var transportadora = ValidarTransportadora(codigoTransportadora);
            if(!string.IsNullOrEmpty(razaoSocial) && transportadora.RazaoSocial != razaoSocial) transportadora.SetRazaoSocial(razaoSocial);
            if(!string.IsNullOrEmpty(nomeFantasia) && transportadora.NomeFantasia != nomeFantasia) transportadora.SetNomeFantasia(nomeFantasia);
            if(!string.IsNullOrEmpty(cnpj) && transportadora.Cnpj != cnpj) transportadora.SetCnpj(cnpj);
            if(!string.IsNullOrEmpty(inscricaoEstadual) && transportadora.InscricaoEstadual != inscricaoEstadual) transportadora.SetInscricaoEstadual(inscricaoEstadual);
            if(!string.IsNullOrEmpty(endereco) && transportadora.Endereco != endereco) transportadora.SetEndereco(endereco);
            if(!string.IsNullOrEmpty(cidade) && transportadora.Cidade != cidade) transportadora.SetCidade(cidade);
            if(!string.IsNullOrEmpty(cep) && transportadora.Cep != cep) transportadora.SetCep(cep);
            if(!string.IsNullOrEmpty(uf) && transportadora.Uf != uf) transportadora.SetUf(uf);
            if(!string.IsNullOrEmpty(site) && transportadora.Site != site) transportadora.SetSite(site);
            transportadora.SetTelefones(telefones);
            transportadora.SetEmail(emails);
            transportadora = transportadorasRepositorio.Editar(transportadora);
            return transportadora;
        }

        public void ExcluirTransportadora(int codigoTransportadora)
        {
            Transportadora transportadora = ValidarTransportadora(codigoTransportadora);
            transportadorasRepositorio.Excluir(transportadora);
        }

        public Transportadora InserirTransportadora(Transportadora transportadora)
        {
            var transportadoraResponse = transportadorasRepositorio.Inserir(transportadora);
            return transportadoraResponse;
        }

        public Transportadora InstanciarTransportadora(string razaoSocial, string nomeFantasia, 
    string cnpj, string inscricaoEstadual, IList<Email> emails, IList<Telefone> telefones, string endereco,
    string cidade, string cep, string uf, string site)
        {
            var transportadoraResponse = new Transportadora(razaoSocial,nomeFantasia, 
    cnpj, inscricaoEstadual, emails, telefones, emails, endereco,
 cidade, cep, uf, site);
            return transportadoraResponse;
        }

        public Transportadora ValidarTransportadora(int codigoTransportadora)
        {
            var transportadoraResponse = this.transportadorasRepositorio.Recuperar(codigoTransportadora);
            if(transportadoraResponse is null)
            {
                 throw new Exception("Transportadora não encontrada");
            }
            return transportadoraResponse;
        }
    }
}