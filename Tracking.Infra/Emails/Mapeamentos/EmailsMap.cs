using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.Emails.Entidades;

namespace Tracking.Infra.Emails.Mapeamentos
{
    public class EmailsMap : ClassMap<Email>
    {
        public EmailsMap()
        {
            Schema("ProjetoComercio");
            Table("emails");
            Id(x => x.Codigo, "id");
            References(x => x.Transportadora, "codTransportadora");
            Map(x => x.EnderecoEmail, "enderecoEmail");
        }
    }
}