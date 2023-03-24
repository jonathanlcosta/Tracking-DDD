using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.Clientes.Enumeradores;

namespace Tracking.Infra.Clientes.Mapeamentos
{
    public class ClientesMap : ClassMap<Cliente>
    {
        public ClientesMap()
        {
        Schema("ProjetoComercio");
        Table("clientes");
        Id(x => x.Id, "id").GeneratedBy.Native();
        Map(x => x.Nome, "nome");
        Map(x=>x.TipoPessoa).CustomType<TipoPessoa>().Column("tipo_pessoa");
        Map(x=>x.Uf).CustomType<UfEnum>().Column("uf");
        Map(x=>x.Regiao).CustomType<RegiaoEnum>().Column("regiao");
        Map(x => x.Telefone, "telefone");
        Map(x => x.Email, "email");
        Map(x => x.Endereco, "endereco");
        Map(x => x.Cidade, "cidade");
        Map(x => x.CpfCnpj, "cpf_cnpj");
        Map(x => x.Cep, "cep");
        Map(x => x.CustoPorPeso, "custo_peso");
        Map(x => x.Seguro, "seguro");
        Map(x => x.IE, "ie");
        Map(x => x.RazaoSocial, "razaoSocial");
        HasMany(x => x.ColetasMercadoria).Cascade.All().Inverse().Not.LazyLoad();

        }
    }
}