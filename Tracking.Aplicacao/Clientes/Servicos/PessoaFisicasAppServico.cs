using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NHibernate;
using Tracking.Aplicacao.Clientes.Servicos.Interfaces;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Request;
using Tracking.DataTransfer.Clientes.PessoaFisicas.Response;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Repositorios;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.Paginacao;

namespace Tracking.Aplicacao.Clientes.Servicos
{
    public class PessoaFisicasAppServico : IPessoaFisicasAppServico
    {
        private readonly IPessoaFisicasServico pessoaFisicasServico;
        private readonly IClientesServico clientesServico;
        private readonly IMapper mapper;
        private readonly ISession session;
        private readonly IClientesRepositorio clientesRepositorio;
        public PessoaFisicasAppServico(IClientesServico clientesServico, IMapper mapper, ISession session, IClientesRepositorio clientesRepositorio, IPessoaFisicasServico pessoaFisicasServico)
        {
            this.clientesServico = clientesServico;
            this.mapper = mapper;
            this.session = session;
            this.clientesRepositorio = clientesRepositorio;
            this.pessoaFisicasServico = pessoaFisicasServico;
        }
        public PessoaFisicasResponse Editar(int codigo, PessoaFisicasEditarRequest pessoaFisicasEditarRequest)
        {
            Cliente pessoa = pessoaFisicasServico.Validar(codigo);
            pessoa = pessoaFisicasServico.Atualizar(codigo, pessoaFisicasEditarRequest.Nome,pessoaFisicasEditarRequest.Email, pessoaFisicasEditarRequest.Endereco,
            pessoaFisicasEditarRequest.Cidade, pessoaFisicasEditarRequest.Telefone, pessoaFisicasEditarRequest.Cep, pessoaFisicasEditarRequest.Uf, pessoaFisicasEditarRequest.Cpf);
            var transacao = session.BeginTransaction();
            try
            {
                pessoa = clientesRepositorio.Editar(pessoa);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<PessoaFisicasResponse>(pessoa);;
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
                var pessoa = pessoaFisicasServico.Validar(codigo);
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

        public PessoaFisicasResponse Inserir(PessoaFisicasCadastroRequest pessoaFisicasCadastroRequest)
        {
           pessoaFisicasCadastroRequest = pessoaFisicasCadastroRequest ?? new PessoaFisicasCadastroRequest();
            var pessoa = pessoaFisicasServico.Instanciar(
                pessoaFisicasCadastroRequest.Nome, 
                                    pessoaFisicasCadastroRequest.Email, 
                                    pessoaFisicasCadastroRequest.Endereco, 
                                    pessoaFisicasCadastroRequest.Cidade,
                                    pessoaFisicasCadastroRequest.Telefone,
                                    pessoaFisicasCadastroRequest.Cep, pessoaFisicasCadastroRequest.Uf,
                                    pessoaFisicasCadastroRequest.Cpf);
            var transacao = session.BeginTransaction();
            try
            {
                pessoa = clientesRepositorio.Inserir(pessoa);
                if(transacao.IsActive)
                    transacao.Commit();
                return mapper.Map<PessoaFisicasResponse>(pessoa);
            }
            catch
            {
                if(transacao.IsActive)
                    transacao.Rollback();
                throw;
            }
        }

        public PaginacaoConsulta<PessoaFisicasResponse> Listar(int? pagina, int quantidade, PessoaFisicasListarRequest pessoaFisicasListarRequest)
        {
            var query = clientesRepositorio.Query();
            if (pagina.Value <= 0) throw new Exception("Pagina não especificada");

            if (pessoaFisicasListarRequest is null)
                throw new Exception();

            if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Nome))
                query = query.Where(p => p.Nome.Contains(pessoaFisicasListarRequest.Nome));
            
             if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Email))
                query = query.Where(p => p.Email.Contains(pessoaFisicasListarRequest.Email));

                if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Cpf))
                query = query.Where(p => p.CpfCnpj.Contains(pessoaFisicasListarRequest.Cpf));

            if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Cidade))
                query = query.Where(p => p.Cidade.Contains(pessoaFisicasListarRequest.Cidade));

             if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Endereco))
                query = query.Where(p => p.Endereco.Contains(pessoaFisicasListarRequest.Endereco));
            if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Uf))
                query = query.Where(p => p.Uf.Contains(pessoaFisicasListarRequest.Uf));
            if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Cep))
                query = query.Where(p => p.Cep.Contains(pessoaFisicasListarRequest.Cep));
            if (!String.IsNullOrEmpty(pessoaFisicasListarRequest.Telefone))
                query = query.Where(p => p.Telefone.Contains(pessoaFisicasListarRequest.Telefone));
            PaginacaoConsulta<Cliente> pessoafisicas = clientesRepositorio.Listar(query, pagina, quantidade);
            PaginacaoConsulta<PessoaFisicasResponse> response;
            response = mapper.Map<PaginacaoConsulta<PessoaFisicasResponse>>(pessoafisicas);
            return response;
        }

        public PessoaFisicasResponse Recuperar(int codigo)
        {
            var pessoaFisica = pessoaFisicasServico.Validar(codigo);
            PessoaFisicasResponse response = mapper.Map<PessoaFisicasResponse>(pessoaFisica);
            return response;
        }
    }
}