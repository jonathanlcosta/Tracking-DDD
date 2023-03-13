using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Emails.Entidades;
using Tracking.Dominio.Emails.Repositorios;
using Tracking.Dominio.Emails.Servicos.Interfaces;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Emails.Servicos
{
    public class EmailsServico : IEmailsServico
    {
         private readonly IEmailsRepositorio emailsRepositorio;

        public EmailsServico(IEmailsRepositorio emailsRepositorio)
        {
            this.emailsRepositorio = emailsRepositorio;
        }
        public Email Editar(int codigo, string enderecoEmail, Transportadora transportadora)
        {
            Email email = Validar(codigo);

            if (!String.IsNullOrEmpty(enderecoEmail)) email.SetEnderecoEmail(enderecoEmail);
            email.SetTransportadora(transportadora);

            email = emailsRepositorio.Editar(email);
            return email;
        }

        public Email Inserir(Email email)
        {
            return emailsRepositorio.Inserir(email);
        }

        public Email Instanciar(string enderecoEmail, Transportadora transportadora)
        {
            if (String.IsNullOrEmpty(enderecoEmail))
                throw new ArgumentNullException("O endereco de email não pode ser vazio");
            if (transportadora == null)
                throw new ArgumentNullException("A transportadora não pode ser nulo");
                return new Email(enderecoEmail, transportadora);
        }

        public Email Validar(int codigo)
        {
            if (codigo == 0)
                throw new ArgumentException("Insira um codigo de email válido");

            return emailsRepositorio.Recuperar(codigo);
        }
    }
}