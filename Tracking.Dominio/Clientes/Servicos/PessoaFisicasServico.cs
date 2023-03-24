using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Enumeradores;
using Tracking.Dominio.Clientes.Repositorios;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.ColetaMercadorias.Entidades;

namespace Tracking.Dominio.Clientes.Servicos
{
    public class PessoaFisicasServico : IPessoaFisicasServico
    {private readonly IClientesRepositorio clientesRepositorio;

        public PessoaFisicasServico(IClientesRepositorio clientesRepositorio)
        {
            this.clientesRepositorio = clientesRepositorio;
        }

        public Cliente Atualizar(int codigo, string? nome, string? email, string endereco, string cidade, string telefone, string cep, UfEnum uf, string cpf, RegiaoEnum regiao)
        {
            Cliente cliente = Validar(codigo);

            if (!String.IsNullOrEmpty(email)) cliente.SetEmail(email);
            if (!String.IsNullOrEmpty(nome)) cliente.SetNome(nome);
            if (!String.IsNullOrEmpty(cpf)) cliente.SetCPF(cpf);
            if (!String.IsNullOrEmpty(endereco)) cliente.SetEndereco(endereco);
            if (!String.IsNullOrEmpty(telefone)) cliente.SetTelefone(telefone);
            if (!String.IsNullOrEmpty(cidade)) cliente.SetCidade(cidade);
            if (!String.IsNullOrEmpty(cep)) cliente.SetCep(cep);
            cliente.SetUf(uf);
            cliente.SetRegiao(regiao);

            return cliente;
        }

        public Cliente Inserir(Cliente cliente)
        {
            return clientesRepositorio.Inserir(cliente);
        }

        public Cliente Instanciar(string? nome, string? email, string endereco, string cidade, string telefone, string cep, UfEnum uf, string cpf, RegiaoEnum regiao)
        {
            return new Cliente(nome, email, endereco, cidade, telefone, cep, uf, cpf, regiao);
        }

        public Cliente Validar(int codigo)
        {
             if (codigo == 0)
                throw new ArgumentException("Insira um codigo de cliente válido");

            var cliente = clientesRepositorio.Recuperar(codigo);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            if (cliente.TipoPessoa != TipoPessoa.Fisica)
                throw new Exception("Esse cliente não é uma pessoa física.");

            return cliente;
        }
    }
}