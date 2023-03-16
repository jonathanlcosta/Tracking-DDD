using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate;
using Tracking.Aplicacao.Clientes.Servicos.Interfaces;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Request;
using Tracking.DataTransfer.Clientes.PessoaJuridicas.Response;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Repositorios;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.Clientes.Servicos
{
    public class PessoaJuridicasAppServico : IPessoaJuridicasAppServico
    {
        private readonly IPessoaJuridicasServico pessoaJuridicasServico;
        private readonly IClientesServico clientesServico;
        private readonly IMapper mapper;
        private readonly ISession session;
        private readonly IClientesRepositorio clientesRepositorio;
        public PessoaJuridicasAppServico(IClientesServico clientesServico, IMapper mapper, ISession session, IClientesRepositorio clientesRepositorio, IPessoaJuridicasServico pessoaJuridicasServico)
        {
            this.clientesServico = clientesServico;
            this.mapper = mapper;
            this.session = session;
            this.clientesRepositorio = clientesRepositorio;
            this.pessoaJuridicasServico = pessoaJuridicasServico;
        }
        public PessoaJuridicasResponse Editar(int codigo, PessoaJuridicasEditarRequest pessoaJuridicasEditarRequest)
        {
           Cliente pessoa = pessoaJuridicasServico.Validar(codigo);
            pessoa = pessoaJuridicasServico.Atualizar(codigo, pessoaJuridicasEditarRequest.Nome,pessoaJuridicasEditarRequest.Email, pessoaJuridicasEditarRequest.Endereco,
            pessoaJuridicasEditarRequest.Cidade, pessoaJuridicasEditarRequest.Telefone, pessoaJuridicasEditarRequest.Cep, pessoaJuridicasEditarRequest.Uf, pessoaJuridicasEditarRequest.Cnpj,
            pessoaJuridicasEditarRequest.IE, pessoaJuridicasEditarRequest.RazaoSocial);
            var transacao = session.BeginTransaction();
            try
            {
                pessoa = clientesRepositorio.Editar(pessoa);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<PessoaJuridicasResponse>(pessoa);;
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
                var pessoa = pessoaJuridicasServico.Validar(codigo);
                clientesRepositorio.Excluir(pessoa);
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

        public PessoaJuridicasResponse Inserir(PessoaJuridicasCadastroRequest pessoaJuridicasCadastroRequest)
        {
            pessoaJuridicasCadastroRequest = pessoaJuridicasCadastroRequest ?? new PessoaJuridicasCadastroRequest();
            var pessoa = pessoaJuridicasServico.Instanciar(
                pessoaJuridicasCadastroRequest.Nome, 
                                    pessoaJuridicasCadastroRequest.Email, 
                                    pessoaJuridicasCadastroRequest.Endereco, 
                                    pessoaJuridicasCadastroRequest.Cidade,
                                    pessoaJuridicasCadastroRequest.Telefone,
                                    pessoaJuridicasCadastroRequest.Cep, pessoaJuridicasCadastroRequest.Uf,
                                    pessoaJuridicasCadastroRequest.Cnpj,
                                    pessoaJuridicasCadastroRequest.IE,
                                    pessoaJuridicasCadastroRequest.RazaoSocial);
            var transacao = session.BeginTransaction();
            try
            {
                pessoa = clientesRepositorio.Inserir(pessoa);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<PessoaJuridicasResponse>(pessoa);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<PessoaJuridicasResponse> Listar(int? pagina, int quantidade, PessoaJuridicasListarRequest pessoaJuridicasListarRequest)
        {
           var query = clientesRepositorio.Query();
            if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            if (pessoaJuridicasListarRequest is null)
                throw new Exception();

            if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Nome))
                query = query.Where(p => p.Nome.Contains(pessoaJuridicasListarRequest.Nome));
            
             if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Email))
                query = query.Where(p => p.Email.Contains(pessoaJuridicasListarRequest.Email));

                if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Cnpj))
                query = query.Where(p => p.CpfCnpj.Contains(pessoaJuridicasListarRequest.Cnpj));

            if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Cidade))
                query = query.Where(p => p.Cidade.Contains(pessoaJuridicasListarRequest.Cidade));

             if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Endereco))
                query = query.Where(p => p.Endereco.Contains(pessoaJuridicasListarRequest.Endereco));
            if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Uf))
                query = query.Where(p => p.Uf.Contains(pessoaJuridicasListarRequest.Uf));
            if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Cep))
                query = query.Where(p => p.Cep.Contains(pessoaJuridicasListarRequest.Cep));
            if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.Telefone))
                query = query.Where(p => p.Telefone.Contains(pessoaJuridicasListarRequest.Telefone));
             if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.IE))
                query = query.Where(p => p.IE.Contains(pessoaJuridicasListarRequest.IE));
            if (!String.IsNullOrEmpty(pessoaJuridicasListarRequest.RazaoSocial))
                query = query.Where(p => p.IE.Contains(pessoaJuridicasListarRequest.RazaoSocial));
            PaginacaoConsulta<Cliente> pessoaJuridicas = clientesRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<PessoaJuridicasResponse> response;
            response = mapper.Map<PaginacaoConsulta<PessoaJuridicasResponse>>(pessoaJuridicas);
            return response;
        }

        public PessoaJuridicasResponse Recuperar(int codigo)
        {
            var pessoaJuridicas = pessoaJuridicasServico.Validar(codigo);
            PessoaJuridicasResponse response = mapper.Map<PessoaJuridicasResponse>(pessoaJuridicas);
            return response;
        }
    }
}