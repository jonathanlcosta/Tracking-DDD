using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.Ocorrencias.Entidades;

namespace Tracking.Infra.Ocorrencias.Mapeamentos
{
    public class OcorrenciaColetaMercadoriasMap : ClassMap<OcorrenciaColetaMercadoria>
    {
        public OcorrenciaColetaMercadoriasMap()
        {
            Schema("ProjetoComercio");
            Table("coletas_ocorrencias");
            Id(x => x.Id, "id").GeneratedBy.Native();
            References(x => x.ColetaMercadoria, "codigo_coleta");
            References(x => x.Ocorrencia, "codigo_ocorrencia");
        }
    }
}