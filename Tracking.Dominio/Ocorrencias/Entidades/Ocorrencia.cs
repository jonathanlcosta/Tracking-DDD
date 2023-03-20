using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Ocorrencias.Enumeradores;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Ocorrencias.Entidades
{
    public class Ocorrencia
    {
         public virtual int Id { get; set; }
        public virtual string? NotaFiscal { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual TipoOcorrenciaEnum? Tipo { get; set; }
        public virtual Transportadora? Transportadora { get; set; }
        public virtual string? Observacao { get; set; }
        public virtual IList<OcorrenciaColetaMercadoria>? Ocorrencias { get; set; }
        public virtual DateTime Data { get; set; }
        protected Ocorrencia()
        {
                
        }

        public Ocorrencia(string notaFiscal, Cliente cliente, TipoOcorrenciaEnum tipoOcorrencia, Transportadora transportadora,
        IList<OcorrenciaColetaMercadoria> ocorrencias, DateTime data)
        {
            SetNotaFiscal(notaFiscal);
            SetCliente(cliente);
            SetTipoOcorrencia(TipoOcorrenciaEnum.EntregaColetada);
            SetTransportadora(transportadora);
            SetOcorrencias(ocorrencias);
            SetData(data);

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
         public virtual void SetTipoOcorrencia(TipoOcorrenciaEnum? tipoOcorrencia)
        {
            if (!tipoOcorrencia.HasValue)
            {
                throw new ArgumentNullException("O tipo da ocorrência não pode ser nulo.");
            }
            tipoOcorrencia = tipoOcorrencia.Value;
        }

        public virtual void SetOcorrencias(IEnumerable<OcorrenciaColetaMercadoria>? ocorrencias)
        {
            if (ocorrencias == null)
                ocorrencias = new List<OcorrenciaColetaMercadoria>();
            Ocorrencias = ocorrencias.ToList();
        }

         public virtual void SetData(DateTime data)
        {
            if (data == DateTime.MinValue)
            {
                throw new ArgumentNullException("A data não foi informada.");
            }
            Data = data;
        }

    }
}