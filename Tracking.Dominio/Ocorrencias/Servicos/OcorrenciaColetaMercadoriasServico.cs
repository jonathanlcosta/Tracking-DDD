using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Servicos.Interfaces;

namespace Tracking.Dominio.Ocorrencias.Servicos
{
    public class OcorrenciaColetaMercadoriasServico : IOcorrenciaColetaMercadoriasServico
    {
        public OcorrenciaColetaMercadoria Instanciar(ColetaMercadoria coletaMercadoria, Ocorrencia ocorrencia)
        {
            return new OcorrenciaColetaMercadoria(coletaMercadoria, ocorrencia);
        }
    }
}