using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Emails.Repositorios;
using Tracking.Infra.Genericos;

namespace Tracking.Infra.Emails
{
    public class EmailsRepositorio : GenericoRepositorio<Email>, IEmailsRepositorio
    {
        public EmailsRepositorio(ISession session) : base(session)
        {
            
        }
    }
}