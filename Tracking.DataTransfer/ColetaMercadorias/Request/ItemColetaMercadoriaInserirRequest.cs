using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.ColetaMercadorias.Request
{
    public class ItemColetaMercadoriaInserirRequest
    {
    public int IdProduto { get; set; }
    public string? Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorProduto { get; set; }
    public decimal Dimensoes { get; set; }
    }
}