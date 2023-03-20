using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.ColetaMercadorias.Entidades;

namespace Tracking.Dominio.Ocorrencias.Entidades
{
    public class OcorrenciaColetaMercadoria
    {
    public virtual int Id { get; set; }
    public virtual ColetaMercadoria? ColetaMercadoria { get; set; }
    public virtual Ocorrencia? Ocorrencia { get; set; }
    protected OcorrenciaColetaMercadoria()
    {
        
    }

    public OcorrenciaColetaMercadoria(ColetaMercadoria coletaMercadoria, Ocorrencia ocorrencia)
    {
        SetColetaMercadoria(coletaMercadoria);
        SetOcorrencia(ocorrencia);
    }

    public virtual void SetColetaMercadoria(ColetaMercadoria? coletaMercadoria)
        {
            if (coletaMercadoria == null)
                throw new ArgumentNullException("O campo de coleta mercadoria não pode ser nulo.");
            ColetaMercadoria = coletaMercadoria;
        }

    public virtual void SetOcorrencia(Ocorrencia? ocorrencia)
        {
            if (ocorrencia == null)
                throw new ArgumentNullException("O campo de ocorrência não pode ser nulo.");
            Ocorrencia = ocorrencia;
        }
    }
}