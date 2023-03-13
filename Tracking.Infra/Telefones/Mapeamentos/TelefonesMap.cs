using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.Telefones.Entidades;

namespace Tracking.Infra.Telefones.Mapeamentos
{
    public class TelefonesMap : ClassMap<Telefone>
    {
        public TelefonesMap()
        {
            Schema("ProjetoComercio");
            Table("telefones");
            Id(x => x.Codigo, "id");
            References(x => x.Transportadora, "codTransportadora");
            Map(x => x.NumeroTelefone, "telefone");
            Map(x => x.TipoTelefone, "tipotelefone");
        }
    }
}