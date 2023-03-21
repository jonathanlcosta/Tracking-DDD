using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.ColetaMercadorias.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;

namespace Tracking.Infra.ColetaMercadorias.Mapeamentos
{
    public class ItemColetaMercadoriasMap : ClassMap<ItemColetaMercadoria>
    {
        public ItemColetaMercadoriasMap()
        {
            Schema("ProjetoComercio");
            Table("coletas_produtos");
            Id(x => x.Id, "id").GeneratedBy.Native();
            Map(x => x.ValorProduto, "valor_produto");
            Map(x=>x.TipoFrete).CustomType<FreteOpcoesEnum>().Column("situacao");
            Map(x => x.Descricao, "descricao_produto");
            Map(x => x.Dimensoes, "dimensoes_produto");
            Map(x => x.Quantidade, "quantidade");
            Map(x => x.ValorFrete, "valor_frete");
            References(x => x.ColetaMercadoria, "codigo_coleta");
            References(x => x.Produto, "codigo_produto");
        }
    }
}