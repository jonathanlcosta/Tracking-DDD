using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.Produtos.Entidades;
using Tracking.Dominio.Produtos.Enumeradores;

namespace Tracking.Infra.Produtos.Mapeamentos
{
    public class ProdutosMap : ClassMap<Produto>
    {
        public ProdutosMap()
        {
        Schema("ProjetoComercio");
        Table("produtos");
        Id(x => x.CodigoProduto, "cod_produto").GeneratedBy.Native();
        Map(x => x.Descricao, "descricao");
        Map(x=>x.Situacao).CustomType<SituacaoProdutoEnum>().Column("situacao");
        Map(x => x.Preco, "preco");
        Map(x => x.Peso, "peso");
        Map(x => x.Altura, "altura");
        Map(x => x.Largura, "largura");
        Map(x => x.Comprimento, "comprimento");
        }
    }
}