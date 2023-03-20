using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.ColetaMercadorias.Entidades
{
    public class ColetaMercadoria
    {
    public virtual int Id { get; protected set; }
    public virtual string? NotaFiscal { get; protected set; }
    public virtual string? PedidoCompra { get; protected set; }
    public virtual Cliente? Cliente { get; protected set; }
    public virtual Transportadora? Transportadora { get; protected set; }
    public virtual string? NomeFantasia { get; protected set; }
    public virtual SituacaoColetaEnum SituacaoColeta { get; protected set; }
    public virtual IList<ItemColetaMercadoria>? ItensColetados { get; protected set; }
    public virtual IList<OcorrenciaColetaMercadoria>? Ocorrencias { get; protected set; }

    protected ColetaMercadoria()
    {
        
    }

    public ColetaMercadoria(string notaFiscal, string pedidoCompra, Cliente cliente, Transportadora transportadora,
     string nomeFantasia, IList<ItemColetaMercadoria> itensColetados, IList<OcorrenciaColetaMercadoria> ocorrencias  )
    {
        SetSituacaoColeta(SituacaoColetaEnum.Ativo);
        SetCliente(cliente);
        SetItensColetados(itensColetados);
        SetNotaFiscal(notaFiscal);
        SetPedidoCompra(pedidoCompra);
        SetNomeFantasia(nomeFantasia);
        SetOcorrencias(ocorrencias);
    }

    public virtual void SetSituacaoColeta(SituacaoColetaEnum? situacaoColeta)
        {
            if (!situacaoColeta.HasValue)
            {
                throw new ArgumentNullException("O Status da compra não pode ser nulo.");
            }
            SituacaoColeta = situacaoColeta.Value;
        }
    
     public virtual void SetCliente(Cliente? cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException("O campo de cliente não pode ser nulo.");
            Cliente = cliente;
        }

    public virtual void SetTransportadora(Transportadora? transportadora)
        {
            if (transportadora == null)
                throw new ArgumentNullException("O campo de transportadora não pode ser nulo.");
            Transportadora = transportadora;
        }
     public virtual void SetItensColetados(IEnumerable<ItemColetaMercadoria>? itensColeta)
        {
            if (itensColeta == null)
                itensColeta = new List<ItemColetaMercadoria>();
            ItensColetados = itensColeta.ToList();
        }

     public virtual void SetNotaFiscal(string notaFiscal)
        {
            string pattern = @"^NF-\d{4}-\d{4}-\d{4}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(notaFiscal))
            {
                throw new ArgumentException("A nota fiscal está com formato invalido");
            }
            NotaFiscal = notaFiscal;
        }

        public virtual void SetPedidoCompra(string? pedidoCompra)
        {
            if (String.IsNullOrEmpty(pedidoCompra))
                throw new ArgumentNullException("O pedido da compra não pode ser vazio");
            if (pedidoCompra.Length > 100)
                throw new ArgumentOutOfRangeException("O pedido da compra nao pode ter mais que 100 caracteres");
            PedidoCompra = pedidoCompra;
        }

        public virtual void SetNomeFantasia(string? nomeFantasia)
        {
            if (String.IsNullOrEmpty(nomeFantasia))
                throw new ArgumentNullException("O nome fantasia não pode ser vazio");
            if (nomeFantasia.Length > 100)
                throw new ArgumentOutOfRangeException("O nome fantasia nao pode ter mais que 100 caracteres");
            NomeFantasia = nomeFantasia;
        }
    
     public virtual void SetOcorrencias(IEnumerable<OcorrenciaColetaMercadoria>? ocorrencias)
        {
            if (ocorrencias == null)
                ocorrencias = new List<OcorrenciaColetaMercadoria>();
            Ocorrencias = ocorrencias.ToList();
        }
    
    }
}