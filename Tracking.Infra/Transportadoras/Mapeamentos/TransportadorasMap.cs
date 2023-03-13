using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Infra.Transportadoras.Mapeamentos
{
    public class TransportadorasMap : ClassMap<Transportadora>
    {
        public TransportadorasMap()
        {
            Schema("ProjetoComercio");
            Table("transportadoras");
            Id(x => x.CodigoTransportadora, "cod_transportadora").GeneratedBy.Native();
            Map(x => x.RazaoSocial, "razao_social");
            Map(x => x.NomeFantasia, "nome_fantasia");
            Map(x => x.Cnpj, "cnpj").Unique();
            Map(x => x.InscricaoEstadual, "inscricao_estadual").Unique();
            Map(x => x.Endereco, "endereco");
            Map( x => x.Cidade, "cidade");
            Map( x => x.Cep, "cep");
            Map( x => x.Uf, "uf");
            Map(x => x.Site, "site");
            HasMany(x => x.Telefones).Cascade.All().Inverse().Not.LazyLoad();
            HasMany(x => x.Emails).Cascade.All().Inverse().Not.LazyLoad();
        }
    }
}