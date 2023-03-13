using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Emails.Servicos.Interfaces
{
    public interface IEmailsServico
    {
        Email Validar(int codigo);
        Email Instanciar(string enderecoEmail, Transportadora transportadora);
        Email Editar(int codigo, string enderecoEmail, Transportadora transportadora);
        Email Inserir(Email email);
    }
}