using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Generico.Repositorios;

namespace Tracking.Dominio.Emails.Repositorios
{
    public interface IEmailsRepositorio : IGenericoRepositorio<Email>
    {
        
    }
}