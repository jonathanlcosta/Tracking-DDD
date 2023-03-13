using NHibernate;
using Tracking.Dominio.Telefones.Entidades;
using Tracking.Dominio.Telefones.Repositorios;
using Tracking.Infra.Genericos;

namespace Tracking.Infra.Telefones
{
    public class TelefonesRepositorio : GenericoRepositorio<Telefone>, ITelefonesRepositorio
    {
        public TelefonesRepositorio(ISession session) : base(session)
        {
            
        }
    }
}