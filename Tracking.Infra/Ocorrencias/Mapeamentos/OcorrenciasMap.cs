using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.Ocorrencias.Entidades;
using Tracking.Dominio.Ocorrencias.Enumeradores;

namespace Tracking.Infra.Ocorrencias.Mapeamentos
{
    public class OcorrenciasMap : ClassMap<Ocorrencia>
    {
        public OcorrenciasMap()
        {
            Schema("ProjetoComercio");
            Table("ocorrencias");
            Id(x => x.Id, "id").GeneratedBy.Native();
            Map(x => x.NotaFiscal, "nota_fiscal");
            Map(x=>x.Tipo).CustomType<TipoOcorrenciaEnum>().Column("tipo_ocorrencia");
            Map(x => x.Observacao, "observacao");
            Map(x => x.Data, "data_ocorrencia");
            References(x => x.Transportadora, "codigo_transportadora");
            References(x => x.Cliente, "codigo_cliente");
             HasMany(x => x.Ocorrencias).Cascade.All().Inverse().Not.LazyLoad();
        }
    }
}