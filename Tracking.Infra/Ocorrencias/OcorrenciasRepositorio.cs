using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Repositorios;
using Tracking.Infra.Genericos;

namespace Tracking.Infra.Ocorrencias
{
    public class OcorrenciasRepositorio : GenericoRepositorio<Ocorrencia>, IOcorrenciasRepositorio
    {
        public OcorrenciasRepositorio(ISession session) : base(session)
        {
            
        }
    }
}