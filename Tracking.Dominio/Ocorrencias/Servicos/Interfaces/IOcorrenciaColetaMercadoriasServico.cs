using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.Ocorrencias.Entidades;

namespace Tracking.Dominio.Ocorrencias.Servicos.Interfaces
{
    public interface IOcorrenciaColetaMercadoriasServico
    {
         OcorrenciaColetaMercadoria Instanciar(ColetaMercadoria coletaMercadoria, Ocorrencia ocorrencia);
    }
}