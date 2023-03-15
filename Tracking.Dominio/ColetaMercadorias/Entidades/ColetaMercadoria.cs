using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Dominio.Clientes.Entidades;
using Tracking.Dominio.ColetaMercadorias.Enumeradores;
using Tracking.Dominio.Transportadoras.Entidades;

namespace Tracking.Dominio.ColetaMercadorias.Entidades
{
    public class ColetaMercadoria
    {
    public virtual int Id { get; set; }
    public virtual string? NotaFiscal { get; set; }
    public virtual string? PedidoCompra { get; set; }
    public virtual Cliente? Cliente { get; set; }
    public virtual Transportadora? Transportadora { get; set; }
    public virtual SituacaoColetaEnum Situacao { get; set; }
    public virtual DateTime DataCadastro { get; set; }
    public virtual DateTime? DataConclusao { get; set; }
    public virtual decimal ValorFrete { get; set; }
    // public virtual IList<ItemColetaMercadoria>? Produtos { get; set; }
    // public virtual IList<OcorrenciaColetaMercadoria>? Ocorrencias { get; set; }
    }
}