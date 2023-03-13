using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Telefones.Entidades;
using Tracking.Dominio.Telefones.Repositorios;
using Tracking.Dominio.Telefones.Servicos.Interfaces;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Telefones.Servicos
{
    public class TelefonesServico : ITelefonesServico
    {
        private readonly ITelefonesRepositorio telefonesRepositorio;

        public TelefonesServico(ITelefonesRepositorio telefonesrepositorio)
        {
            this.telefonesRepositorio = telefonesrepositorio;
        }

        public Telefone Editar(int codigo, string tipoTelefone, string numeroTelefone, Transportadora transportadora)
        {
            Telefone telefone = Validar(codigo);

            if (!String.IsNullOrEmpty(tipoTelefone)) telefone.SetTipoTelefone(tipoTelefone);
            if (!String.IsNullOrEmpty(numeroTelefone)) telefone.SetNumeroTelefone(tipoTelefone);
            if (!String.IsNullOrEmpty(tipoTelefone)) telefone.SetTipoTelefone(tipoTelefone);
            telefone.SetTransportadora(transportadora);

            telefone = telefonesRepositorio.Editar(telefone);
            return telefone;
        }

        public Telefone Inserir(Telefone telefone)
        {
            return telefonesRepositorio.Inserir(telefone);
        }

        public Telefone Instanciar(string tipoTelefone, string numeroTelefone, Transportadora transportadora)
        {
            if (String.IsNullOrEmpty(tipoTelefone))
                throw new ArgumentNullException("O tipo de telefone não pode ser vazio");
            if (String.IsNullOrEmpty(numeroTelefone))
                throw new ArgumentNullException("O numero de telefone não pode ser vazio");
            if (transportadora == null)
                throw new ArgumentNullException("A transportadora não pode ser nulo");

            return new Telefone(numeroTelefone, transportadora);
        }

        public Telefone Validar(int codigo)
        {
            if (codigo == 0)
                throw new ArgumentException("Insira um codigo de telefone válido");

            return telefonesRepositorio.Recuperar(codigo);
        }
    }
}