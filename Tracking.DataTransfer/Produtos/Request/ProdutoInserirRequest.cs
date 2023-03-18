using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracking.DataTransfer.Produtos.Request
{
    public class ProdutoInserirRequest
    {
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }
    public decimal Largura { get; set; }
    public decimal Comprimento { get; set; }
    }
}