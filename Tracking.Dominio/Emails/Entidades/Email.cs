using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            string validacao = @"^[a-zA-Z0-9]+@\S+\.com(\.\w+)?$";
            Regex regex = new Regex(validacao);
            if (!regex.IsMatch(enderecoEmail))
            {
                throw new Exception("Email com formato invalido");
            }
        EnderecoEmail = enderecoEmail;
        }

        public virtual void SetTransportadora(Transportadora transportadora)
        {
             Transportadora = transportadora;
        }


    }
}