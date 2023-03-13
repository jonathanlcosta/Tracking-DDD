using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using Tracking.Dominio.Transportadoras.Entidades;
using Tracking.Dominio.Transportadoras.Repositorios;
using Tracking.Infra.Genericos;

namespace Tracking.Infra.Transportadoras
{
    public class TransportadorasRepositorio : GenericoRepositorio<Transportadora>, ITransportadorasRepositorio
    {
        public TransportadorasRepositorio(ISession session) : base(session)
        {
            
        }
    }
}