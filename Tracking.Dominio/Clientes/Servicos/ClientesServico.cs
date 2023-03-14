using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Enumeradores;
using Tracking.Dominio.Clientes.Repositorios;
using Tracking.Dominio.Clientes.Servicos.Interfaces;

namespace Tracking.Dominio.Clientes.Servicos
{
    public class ClientesServico : IClientesServico
    {
        private readonly IClientesRepositorio clientesRepositorio;

        public ClientesServico(IClientesRepositorio clientesRepositorio)
        {
            this.clientesRepositorio = clientesRepositorio;
        }

        public Cliente Validar(int codigo)
        {
            if (codigo == 0)
                throw new ArgumentException("Insira um codigo de cliente válido");

            var cliente = clientesRepositorio.Recuperar(codigo);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            return cliente;
        }

        public TipoPessoa GetTipoCliente(Cliente cliente)
        {
            return cliente.TipoPessoa;
        }
    }
}