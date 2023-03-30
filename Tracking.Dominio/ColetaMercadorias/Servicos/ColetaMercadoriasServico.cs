using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Servicos.Interfaces;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Repositorios;
using Tracking.Dominio.ColetaMercadorias.Servicos.Interfaces;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;
using Tracking.Dominio.Transportadoras.Servicos.Interfaces;

namespace Tracking.Dominio.ColetaMercadorias.Servicos
{
    public class ColetaMercadoriasServico : IColetaMercadoriasServico
    {
        private readonly IClientesServico clientesServico;
        private readonly IColetaMercadoriasRepositorio coletaMercadoriasRepositorio;
        private readonly ITransportadorasServico transportadorasServico;

        public ColetaMercadoriasServico(IClientesServico clientesServico, IColetaMercadoriasRepositorio coletaMercadoriasRepositorio, ITransportadorasServico transportadorasServico)
        {
            this.clientesServico = clientesServico;
            this.coletaMercadoriasRepositorio = coletaMercadoriasRepositorio;
            this.transportadorasServico = transportadorasServico;
        }
        public void AdicionarItem(ColetaMercadoria coletaMercadoria, ItemColetaMercadoria? itemColetaMercadoria)
        {
             if (itemColetaMercadoria == null)
                throw new ArgumentNullException("Item coleta não pode ser nulo.");
            coletaMercadoria.ItensColetados!.Add(itemColetaMercadoria);
        }

        public void AdicionarItem(ColetaMercadoria coletaMercadoria, IList<ItemColetaMercadoria> itensColetaMercadoria)
        {
            if(itensColetaMercadoria != null)
            {
                foreach(var item in itensColetaMercadoria)
                {
                    coletaMercadoria.ItensColetados!.Add(item!);
                }
            }
        }

        public ColetaMercadoria Atualizar(int codigo, string notaFiscal, string pedidoCompra, int? codCliente, int? codTransportadora, string nomeFantasia)
        {
            Cliente? cliente = null;
            if (codCliente.HasValue && codCliente != 0)
            {
                cliente = clientesServico.Validar(codCliente.Value);
            }

            Transportadora? transportadora = null;
            if (codTransportadora.HasValue && codTransportadora != 0)
            {
                transportadora = transportadorasServico.Validar(codTransportadora.Value);
            }

            ColetaMercadoria coletaMercadoria = Validar(codigo);

            if (cliente != null && cliente.Id != coletaMercadoria.Cliente!.Id) coletaMercadoria.SetCliente(cliente);
            if (transportadora != null && transportadora.CodigoTransportadora != coletaMercadoria.Transportadora!.CodigoTransportadora) coletaMercadoria.SetTransportadora(transportadora);
            if (!String.IsNullOrEmpty(notaFiscal)) coletaMercadoria.SetNotaFiscal(notaFiscal);
            if (!String.IsNullOrEmpty(pedidoCompra)) coletaMercadoria.SetPedidoCompra(pedidoCompra);
            if (!String.IsNullOrEmpty(nomeFantasia)) coletaMercadoria.SetNomeFantasia(nomeFantasia);
            return coletaMercadoria;
        }

        public void Excluir(ColetaMercadoria coletaMercadoria)
        {
            coletaMercadoriasRepositorio.Excluir(coletaMercadoria);
        }

        public ColetaMercadoria Inserir(ColetaMercadoria coletaMercadoria)
        {
            return coletaMercadoriasRepositorio.Inserir(coletaMercadoria);
        }

        public ColetaMercadoria Instanciar(string notaFiscal, string pedidoCompra, int codCliente, int codTransportadora, string nomeFantasia)
        {
            Cliente cliente = clientesServico.Validar(codCliente);
            Transportadora transportadora = transportadorasServico.Validar(codTransportadora);


            return new ColetaMercadoria(notaFiscal, pedidoCompra, cliente, transportadora, 
            nomeFantasia, new List<ItemColetaMercadoria>(), new List<OcorrenciaColetaMercadoria>());
        }

        public ColetaMercadoria Validar(int codigo)
        {
           if (codigo == 0)
                throw new ArgumentException("Insira um código de coleta valido.");

            ColetaMercadoria coletaMercadoria = coletaMercadoriasRepositorio.Recuperar(codigo);

            if (coletaMercadoria == null)
                throw new ArgumentNullException("Coleta não encontrada.");
            return coletaMercadoria;
        }
    }
}