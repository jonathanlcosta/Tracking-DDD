using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Emails.Entidades
{
    public class Email
    {
        public virtual int Codigo { get; set; }
        public virtual string? EnderecoEmail { get; set; }
        public virtual Transportadora? Transportadora { get; set; }
        public Email(string enderecoEmail, Transportadora transportadora)
        {
            SetEnderecoEmail(enderecoEmail);
            
        }

        public Email()
        {
            
        }

        public virtual void SetEnderecoEmail(string enderecoEmail)
        {
             try
        {
            var addr = new System.Net.Mail.MailAddress(enderecoEmail);
        }
        catch
        {
            throw new Exception("Endereço de e-mail inválido.");
        }
        EnderecoEmail = enderecoEmail;
        }

        public virtual void SetTransportadora(Transportadora transportadora)
        {
             Transportadora = transportadora;
        }


    }
}