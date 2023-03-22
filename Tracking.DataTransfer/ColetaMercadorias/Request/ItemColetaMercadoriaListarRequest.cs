using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.ColetaMercadorias.Request
{
    public class ItemColetaMercadoriaListarRequest
    {
    public int Id { get; set; }
    public int IdColetaMercadoria { get; set; }
    public int IdProduto { get; set; }
    public string? Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorProduto { get; set; }
    public decimal Dimensoes { get; set; }
    public decimal ValorFrete { get; set; }
    }
}