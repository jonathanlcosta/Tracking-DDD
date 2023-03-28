using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;

namespace Tracking.Infra.ColetaMercadorias.Mapeamentos
{
    public class ColetaMercadoriasMap : ClassMap<ColetaMercadoria>
    {
        public ColetaMercadoriasMap()
        {
            Schema("ProjetoComercio");
            Table("coletas");
            Id(x => x.Id, "id").GeneratedBy.Native();
            Map(x => x.NotaFiscal, "nota_fiscal");
            Map(x=>x.SituacaoColeta).CustomType<SituacaoColetaEnum>().Column("situacao");
            Map(x => x.PedidoCompra, "pedido_compra");
            Map(x => x.NomeFantasia, "nome_fantasia");
            References(x => x.Cliente, "idCliente");
            References(x => x.Transportadora, "codigo_transportadora");
            HasMany(x => x.ItensColetados).Cascade.All().Inverse().Not.LazyLoad();
        }
    }
}