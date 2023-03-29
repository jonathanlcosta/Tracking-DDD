using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.Telefones.Entidades
{
    public class Telefone
    {
        public virtual int Codigo { get; protected set; }
        public virtual Transportadora? Transportadora { get; protected set; }
        public virtual string? NumeroTelefone { get; protected set; }
        public virtual string? TipoTelefone { get; protected set; }

        protected Telefone() { }

        public Telefone(string numeroTelefone, Transportadora transportadora)
        {
            SetTipoTelefone(numeroTelefone);
            SetNumeroTelefone(numeroTelefone);
            SetTransportadora(transportadora);
        }

        public virtual void SetTipoTelefone(string numeroTelefone)
        {
            string padraocel = "\\([1-9]{2}\\)[0-9]{5}-?[0-9]{4}";
            string padraocasa = "\\([1-9]{2}\\)[0-9]{4}-?[0-9]{4}";
            Match matchCasa = Regex.Match(numeroTelefone, padraocasa);
            Match matchCel = Regex.Match(numeroTelefone, padraocel);

            if (matchCasa.Success == true)
            {
                TipoTelefone = "Residencial";
            }
            else if (matchCel.Success == true)
            {
                TipoTelefone = "Celular";
            }
            else
            {
                TipoTelefone = "Não reconhecido";
            }
        }
        public virtual void SetNumeroTelefone(string numeroTelefone)
        {
            if (String.IsNullOrEmpty(numeroTelefone))
                throw new ArgumentNullException("O numero de telefone não pode ser vazio.");
            if (numeroTelefone.Length > 14)
                throw new ArgumentOutOfRangeException("O numero de Telefone deve possuir no máximo 14 caracteres");

            NumeroTelefone = numeroTelefone.Replace("-", "").Replace(".", "");
        }

        public virtual void SetTransportadora(Transportadora transportadora)
        {
            Transportadora = transportadora;
        }

    }
}