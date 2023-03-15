
using NHibernate;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Repositorios;
using Tracking.Infra.Genericos;

namespace Tracking.Infra.Clientes
{
    public class ClientesRepositorio : GenericoRepositorio<Cliente>, IClientesRepositorio
    {
        public ClientesRepositorio(ISession session) : base(session)
        {
            
        }
    }
}